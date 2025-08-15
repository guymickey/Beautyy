using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;

namespace Beautyy.Models
{
    public class EventMetadata
    {
    }
    [MetadataType(typeof(EventMetadata))]
    public partial class Event
    {
        [NotMapped]
        public List<Component?> Component { get; set; }
        public Event Create(CustomContext db)
        {
            List<EventCategory> toBeCreated = this.EventCategories.ToList();
            this.EventCategories.Clear();

            IsDelete = false;
            db.Events.Add(this);
            db.SaveChanges();

            foreach (EventCategory eventCategory in toBeCreated)
            {
                eventCategory.EventId = this.Id;
                eventCategory.Create(db);
            }

            if (Component != null)
            {
                foreach (Component com in Component)
                {
                    Page page = new Page
                    {
                        Name = com.Name,
                        Containings = com.Containings,
                        Data = com.Data,
                        EventId = this.Id
                    };
                    page.CreatePage(db);
                }
            }

            return this;
        }
        
        public Event Cory(int eventid, CustomContext db)
        {
            Event ev = db.Events.Include(e => e.EventCategories).Include(c => c.Pages).FirstOrDefault(c => c.Id == eventid && c.IsDelete != true);

            Name = ev.Name;
            IsFav = ev.IsFav;
            ImageId = ev.ImageId;
            EndDate = ev.EndDate;

            db.Events.Add(this);
            db.SaveChanges();

            foreach (EventCategory evc in ev.EventCategories)
            {
                EventCategory copyec = new EventCategory
                {
                    CategoryId = evc.CategoryId,
                    EventId = this.Id,

                };
                copyec.Create(db);
            }

            foreach (Page com in ev.Pages)
            {
                Page page = new Page
                {
                    Id = (com as Component).Id,
                    Name = com.Name,
                    EventId = this.Id
                };
                page.CopyPage(db);
            }


            return this;
        }

        public Event Update(CustomContext db, Event @event)
        {
            List<EventCategory> toBeCreated = @event.EventCategories.ToList();
            @event.EventCategories.Clear();

            List<Page> pages = @event.Pages.ToList();
            @event.Pages.Clear();

            IsDelete = false;
            db.Events.Update(@event);
            db.SaveChanges();

            Event old = db.Events.Include(p => p.Pages).Include(ec => ec.EventCategories).Where(c => c.Id == @event.Id && c.IsDelete != true).AsNoTracking().FirstOrDefault();

            foreach (EventCategory eventCategory in toBeCreated)
            {
                if (eventCategory.Id == 0)
                {
                    eventCategory.EventId = @event.Id;
                    eventCategory.Create(db);
                }
                else
                {
                    eventCategory.Update(db, eventCategory);
                }
            }

            foreach (EventCategory deleteoldEventCategory in old.EventCategories)
            {
                bool existsInNew = this.EventCategories.Any(c => c.Id == deleteoldEventCategory.Id);
                if (!existsInNew)
                {
                    EventCategory delete = db.EventCategories.Where(c => c.Id == deleteoldEventCategory.Id).AsNoTracking().FirstOrDefault();
                    delete.Delete(db);
                }
            }
            if (pages != null)
            {
                foreach (Page page in pages)
                {

                    if (page.Id == 0)
                    {
                        page.EventId = @event.Id;
                        page.Create(db);
                    }
                    else
                    {
                        page.UpdatePage(db, page);
                    }
                }
            }
            else
            {
                foreach (Page deleteoldPage in old.Pages)
                {
                    bool existsInNew = this.Pages.Any(c => c.Id == (deleteoldPage as Component).Id);
                    if (!existsInNew)
                    {
                        Component deletePage = db.Components
                            .Include(c => c.CombineElements)
                                .ThenInclude(f => f.ComponentElement)
                            .Include(f => f.FormTemplate.FormComponentTemplates)
                            .AsNoTracking()
                            .FirstOrDefault(c => c.Id == (deleteoldPage as Component).Id);

                        deletePage.DeleteComponentRecursive(deletePage, db);
                    }
                }
            }
            return @event;
        }



        public Event Delete(CustomContext db)
        {
            List<EventCategory> toBeCreated = this.EventCategories.ToList();
            this.EventCategories.Clear();

            IsDelete = true;
            db.Events.Update(this);


            foreach (EventCategory eventCategory in toBeCreated)
            {
                eventCategory.Delete(db);
                eventCategory.Event = this;
            }

            foreach (Page page in this.Pages)
            {
                page.DeletePage(db);
            }
            return this;
        }

        public static List<Event> GetAll(CustomContext db, string? search)
        {
            IQueryable<Event> events = db.Events.Include(e => e.EventCategories).ThenInclude(ec => ec.Category).Include(i => i.Image).Where(i => i.IsDelete != true);

            if (!string.IsNullOrEmpty(search))
            {
                events = events.Where(q => q.Name.Contains(search));
            }

            List<Event> result = events.ToList();

            foreach (Event ima in result)
            {
                if (ima.Image != null)
                {
                    string fileName = Path.GetFileName(ima.Image.FileName);
                    ima.Image.FilePath = $"https://localhost:7077/uploads/{fileName}";
                }
            }

            return result;
        }

        public static Event GetById(int id, CustomContext db)
        {
            Event Ev = db.Events.Include(f => f.Image).Include(p => p.Pages.Where(i => i.IsDelete != true)).Include(e => e.EventCategories.Where(i => i.IsDelete != true)).Where(i => i.IsDelete != true).FirstOrDefault(e => e.Id == id);

            if (Ev.Image != null)
            {
                string fileName = Path.GetFileName(Ev.Image.FileName);
                Ev.Image.FilePath = $"https://localhost:7077/uploads/{fileName}";
            }

            foreach (EventCategory eventCategory in Ev.EventCategories)
            {
                Category category = db.Categories.Where(c => c.Id == eventCategory.CategoryId && c.IsDelete != true).FirstOrDefault();
            }

            foreach (Page page in Ev.Pages)
            {
                page.Id = (page as Component).Id;

                page.Containings = db.Containings.Where(p => p.ContainerId == page.Id && p.IsDelete != true).ToList();

                foreach (Containing containing in page.Containings)
                {
                    Component children = containing.Component = db.Components.Include(c => c.CombineElements.Where(i => i.IsDelete != true)).Where(i => i.IsDelete != true).FirstOrDefault(c => c.Id == containing.ComponentId);

                    Type type = Type.GetType(children.Name);


                    switch (type.Name)
                    {
                        case "Section":
                            LoadNestedSections(children, db);
                            break;
                        case "FormTemplate":
                            LoadForm(children, db);
                            break;
                        case "Banner":
                            Banner banner = (Banner)children;
                            banner.Id = (banner as Component).Id;
                            break;
                        case "TextBox":
                            TextBox TextBox = (TextBox)children;
                            TextBox.Id = (TextBox as Component).Id;
                            break;
                        case "ImageWithCaption":
                            ImageWithCaption ImageWithCaption = (ImageWithCaption)children;
                            ImageWithCaption.Id = (ImageWithCaption as Component).Id;
                            break;
                        case "GridTwoColumn":
                            GridTwoColumn GridTwoColumn = (GridTwoColumn)children;
                            GridTwoColumn.Id = (GridTwoColumn as Component).Id;
                            break;
                        case "ImageDesc":
                            ImageDesc ImageDesc = (ImageDesc)children;
                            ImageDesc.Id = (ImageDesc as Component).Id;
                            break;
                        case "GridForImage":
                            GridForImage GridForImage = (GridForImage)children;
                            GridForImage.Id = (GridForImage as Component).Id;
                            break;
                        case "TableWithTopicAndDesc":
                            TableWithTopicAndDesc TableWithTopicAndDesc = (TableWithTopicAndDesc)children;
                            TableWithTopicAndDesc.Id = (TableWithTopicAndDesc as Component).Id;
                            break;
                        case "OneTopicimagecaptionbutton":
                            OneTopicimagecaptionbutton OneTopicimagecaptionbutton = (OneTopicimagecaptionbutton)children;
                            OneTopicimagecaptionbutton.Id = (OneTopicimagecaptionbutton as Component).Id;
                            break;
                        case "TwoTopicimagecaptionbutton":
                            TwoTopicimagecaptionbutton TwoTopicimagecaptionbutton = (TwoTopicimagecaptionbutton)children;
                            TwoTopicimagecaptionbutton.Id = (TwoTopicimagecaptionbutton as Component).Id;
                            break;
                        case "Sale":
                            Sale Sale = (Sale)children;
                            Sale.Id = (Sale as Component).Id;
                            break;
                        case "Button":
                            Button Button = (Button)children;
                            Button.Id = (Button as Component).Id;
                            break;
                        case "About":
                            About About = (About)children;
                            About.Id = (About as Component).Id;
                            break;
                    }

                    foreach (CombineElement combine in children.CombineElements)
                    {
                        ComponentElement element = combine.ComponentElement = db.ComponentElements.Include(i => i.Picture.Image).Where(i => i.IsDelete != true).FirstOrDefault(c => c.Id == combine.ComponentElementId);

                        Type typeform = Type.GetType(element.Element);

                        switch (typeform.Name)
                        {
                            case "Text":
                                Text text = (Text)element;
                                text.Id = (text as ComponentElement).Id;
                                break;
                            case "Picture":
                                Picture Picture = (Picture)element;
                                Picture.Id = (Picture as ComponentElement).Id;

                                string fileName = Path.GetFileName(Picture.Image.FilePath);
                                Picture.Image.FilePath = $"https://localhost:7077/uploads/{fileName}";

                                break;
                            case "ButtonElement":
                                ButtonElement ButtonElement = (ButtonElement)element;
                                ButtonElement.Id = (ButtonElement as ComponentElement).Id;
                                break;
                            case "DateT":
                                DateT DateT = (DateT)element;
                                DateT.Id = (DateT as ComponentElement).Id;
                                break;
                            case "Number":
                                Number Number = (Number)element;
                                Number.Id = (Number as ComponentElement).Id;
                                break;
                        }



                    }

                }
            }



            return Ev;
        }

        public static void LoadNestedSections(Component component, CustomContext db)
        {
            Section section = db.Sections.Where(s => s.Id == component.Id && s.IsDelete != true).FirstOrDefault();
            section.Id = (section as Component).Id;
            section.Containings = db.Containings.Where(c => c.ContainerId == section.Id).ToList();

            foreach (Containing containing in section.Containings)
            {

                Component child = containing.Component = db.Components.Include(c => c.CombineElements.Where(i => i.IsDelete != true)).Where(i => i.IsDelete != true).FirstOrDefault(c => c.Id == containing.ComponentId);

                Type type = Type.GetType(child.Name);

                switch (type.Name)
                {
                    case "Section":
                        LoadNestedSections(child, db);
                        break;
                    case "FormTemplate":
                        LoadForm(child, db);
                        break;
                    case "Banner":
                        Banner banner = (Banner)child;
                        banner.Id = (banner as Component).Id;
                        break;
                    case "TextBox":
                        TextBox TextBox = (TextBox)child;
                        TextBox.Id = (TextBox as Component).Id;
                        break;
                    case "ImageWithCaption":
                        ImageWithCaption ImageWithCaption = (ImageWithCaption)child;
                        ImageWithCaption.Id = (ImageWithCaption as Component).Id;
                        break;
                    case "GridTwoColumn":
                        GridTwoColumn GridTwoColumn = (GridTwoColumn)child;
                        GridTwoColumn.Id = (GridTwoColumn as Component).Id;
                        break;
                    case "ImageDesc":
                        ImageDesc ImageDesc = (ImageDesc)child;
                        ImageDesc.Id = (ImageDesc as Component).Id;
                        break;
                    case "GridForImage":
                        GridForImage GridForImage = (GridForImage)child;
                        GridForImage.Id = (GridForImage as Component).Id;
                        break;
                    case "TableWithTopicAndDesc":
                        TableWithTopicAndDesc TableWithTopicAndDesc = (TableWithTopicAndDesc)child;
                        TableWithTopicAndDesc.Id = (TableWithTopicAndDesc as Component).Id;
                        break;
                    case "OneTopicimagecaptionbutton":
                        OneTopicimagecaptionbutton OneTopicimagecaptionbutton = (OneTopicimagecaptionbutton)child;
                        OneTopicimagecaptionbutton.Id = (OneTopicimagecaptionbutton as Component).Id;
                        break;
                    case "TwoTopicimagecaptionbutton":
                        TwoTopicimagecaptionbutton TwoTopicimagecaptionbutton = (TwoTopicimagecaptionbutton)child;
                        TwoTopicimagecaptionbutton.Id = (TwoTopicimagecaptionbutton as Component).Id;
                        break;
                    case "Sale":
                        Sale Sale = (Sale)child;
                        Sale.Id = (Sale as Component).Id;
                        break;
                    case "Button":
                        Button Button = (Button)child;
                        Button.Id = (Button as Component).Id;
                        break;
                    case "About":
                        About About = (About)child;
                        About.Id = (About as Component).Id;
                        break;
                }

                foreach (CombineElement combine in child.CombineElements)
                {
                    ComponentElement element = combine.ComponentElement = db.ComponentElements.Include(i => i.Picture.Image).Where(i => i.IsDelete != true).FirstOrDefault(c => c.Id == combine.ComponentElementId);

                    Type typeform = Type.GetType(element.Element);

                    switch (typeform.Name)
                    {
                        case "Text":
                            Text text = (Text)element;
                            text.Id = (text as ComponentElement).Id;
                            break;
                        case "Picture":
                            Picture Picture = (Picture)element;
                            Picture.Id = (Picture as ComponentElement).Id;

                            string fileName = Path.GetFileName(Picture.Image.FilePath);
                            Picture.Image.FilePath = $"https://localhost:7077/uploads/{fileName}";

                            break;
                        case "ButtonElement":
                            ButtonElement ButtonElement = (ButtonElement)element;
                            ButtonElement.Id = (ButtonElement as ComponentElement).Id;
                            break;
                        case "DateT":
                            DateT DateT = (DateT)element;
                            DateT.Id = (DateT as ComponentElement).Id;
                            break;
                        case "Number":
                            Number Number = (Number)element;
                            Number.Id = (Number as ComponentElement).Id;
                            break;
                    }


                }
            }
        }

        public static void LoadForm(Component component, CustomContext db)
        {
            FormTemplate form = db.FormTemplates.Where(s => s.Id == component.Id && s.IsDelete != true).Include(f => f.FormComponentTemplates.Where(i => i.IsDelete != true)).Where(i => i.IsDelete != true).FirstOrDefault();

            form.Id = (form as Component).Id;

            foreach (FormComponentTemplate s in form.FormComponentTemplates)
            {
                s.CombineFormElementTemplates = db.CombineFormElementTemplates.Where(f => f.FormComponentId == s.Id && f.IsDelete != true).ToList();

                Type type = Type.GetType(s.TypeForm);

                switch (type.Name)
                {
                    case "Singleselection":
                        Singleselection Singleselection = (Singleselection)s;
                        Singleselection.Id = (Singleselection as FormComponentTemplate).Id;
                        break;
                    case "Textfield":
                        Textfield Textfield = (Textfield)s;
                        Textfield.Id = (Textfield as FormComponentTemplate).Id;
                        break;
                    case "Date":
                        Date Date = (Date)s;
                        Date.Id = (Date as FormComponentTemplate).Id;
                        break;
                    case "Birthdate":
                        Birthdate Birthdate = (Birthdate)s;
                        Birthdate.Id = (Birthdate as FormComponentTemplate).Id;
                        break;
                    case "Imageupload":
                        Imageupload Imageupload = (Imageupload)s;
                        Imageupload.Id = (Imageupload as FormComponentTemplate).Id;
                        break;
                    case "ImageUploadwithImagecontent":
                        ImageUploadwithImagecontent ImageUploadwithImagecontent = (ImageUploadwithImagecontent)s;
                        ImageUploadwithImagecontent.Id = (ImageUploadwithImagecontent as FormComponentTemplate).Id;
                        break;
                    case "ButtonForm":
                        ButtonForm ButtonForm = (ButtonForm)s;
                        ButtonForm.Id = (ButtonForm as FormComponentTemplate).Id;
                        break;
                    case "Popup":
                        Popup Popup = (Popup)s;
                        Popup.Id = (Popup as FormComponentTemplate).Id;
                        break;

                }

                foreach (CombineFormElementTemplate item in s.CombineFormElementTemplates)
                {
                    FormElementTemplate formElementTemplate = item.FormElement = db.FormElementTemplates.Include(p => p.PictureForm.Image).Where(i => i.IsDelete != true).FirstOrDefault(c => c.Id == item.FormElementId);

                    Type typeform = Type.GetType(formElementTemplate.ElementForm);

                    switch (typeform.Name)
                    {
                        case "FormOptionTemplate":
                            FormOptionTemplate FormOptionTemplate = (FormOptionTemplate)formElementTemplate;
                            FormOptionTemplate.Id = (FormOptionTemplate as FormElementTemplate).Id;
                            break;
                        case "FormLabelTemplate":
                            FormLabelTemplate FormLabelTemplate = (FormLabelTemplate)formElementTemplate;
                            FormLabelTemplate.Id = (FormLabelTemplate as FormElementTemplate).Id;
                            break;
                        case "FormInpuTextTemplate":
                            FormInpuTextTemplate FormInpuTextTemplate = (FormInpuTextTemplate)formElementTemplate;
                            FormInpuTextTemplate.Id = (FormInpuTextTemplate as FormElementTemplate).Id;
                            break;
                        case "FormInputDateTemplate":
                            FormInputDateTemplate FormInputDateTemplate = (FormInputDateTemplate)formElementTemplate;
                            FormInputDateTemplate.Id = (FormInputDateTemplate as FormElementTemplate).Id;
                            break;
                        case "FormInputFileTemplate":
                            FormInputFileTemplate FormInputFileTemplate = (FormInputFileTemplate)formElementTemplate;
                            FormInputFileTemplate.Id = (FormInputFileTemplate as FormElementTemplate).Id;
                            break;
                        case "PictureForm":
                            PictureForm PictureForm = (PictureForm)formElementTemplate;
                            PictureForm.Id = (PictureForm as FormElementTemplate).Id;

                            string fileName = Path.GetFileName(PictureForm.Image.FilePath);
                            PictureForm.Image.FilePath = $"https://localhost:7077/uploads/{fileName}";

                            break;
                        case "ButtonElementForm":
                            ButtonElementForm ButtonElementForm = (ButtonElementForm)formElementTemplate;
                            ButtonElementForm.Id = (ButtonElementForm as FormElementTemplate).Id;
                            break;
                    }




                }
            }
        }





    }
}
