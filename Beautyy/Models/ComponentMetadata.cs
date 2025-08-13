using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Beautyy.Models
{

    public class ComponentMetadata
    {
    }
    [MetadataType(typeof(ComponentMetadata))]
    public partial class Component
    {
        [NotMapped]
        public string? Data { get; set; } = string.Empty;


        public Component Create(CustomContext db)
        {

            Type type = Type.GetType(this.Name);

            dynamic json = JsonConvert.DeserializeObject(this.Data, type);

            json.Name = this.Name;
            json.IsDelete = false;

            foreach (CombineElement combine in json.CombineElements)
            {
                combine.Create(db);
            }

            List<FormComponentTemplate> toBeCreated = new();

            //เขียนไว้แบบนี้ เพราะ ตอนมันเกิดการ Add(json) มันจะมีการสร้างตัว FormComponentTemplate ขึ้นมาอีก1ตัว ซึ่ง พอเข้าไปทำในฟังชั่นด์ของมันจะทำให้มี 2 ตัว
            if (type.Name == "FormTemplate")
            {

                foreach (FormComponentTemplate componentTemplate in json.FormComponentTemplates)
                {
                    toBeCreated.Add(componentTemplate);
                }

                json.FormComponentTemplates.Clear();
            }


            db.Add(json);
            db.SaveChanges();


            if (type.Name == "FormTemplate")
            {
                foreach (FormComponentTemplate componentTemplate in toBeCreated)
                {
                    componentTemplate.FormId = (json as Component).Id;
                    componentTemplate.Create(db);
                }

            }



            return json;
        }

        public Component CopyComponent (CustomContext db)
        {
           this.Id = 0;
           (this as Component).Containings.Clear();

            List<CombineElement> combineElements = this.CombineElements.ToList();   
            this.CombineElements.Clear();

            Type type = Type.GetType(this.Name);

            List<FormComponentTemplate> toBeCreated = new();

            if (type.Name == "FormTemplate")
            {

                foreach (FormComponentTemplate componentTemplate in this.FormTemplate.FormComponentTemplates)
                {
                    toBeCreated.Add(componentTemplate);
                }
                this.FormTemplate.FormComponentTemplates.Clear();
            }

            db.Add(this);
            db.SaveChanges();


            foreach (CombineElement combine in combineElements)
            {
                CombineElement combineElement = new CombineElement();
                combineElement.ComponentId = (this as Component).Id;
                combineElement.ComponentElement = combineElements.FirstOrDefault(c => c.Id == combine.Id)?.ComponentElement;
                combineElement.Copy(db);
            }

            if (type.Name == "FormTemplate")
            {
                foreach (FormComponentTemplate componentTemplate in toBeCreated)
                {
                    componentTemplate.FormId = (this as Component).Id;
                    componentTemplate.Copy(db);
                }

            }

            return this;
        }

        public Component Delete(CustomContext db)
        {

            foreach (CombineElement combine in this.CombineElements)
            {
                combine.Delete(db);
            }

            if (this.Name == "Beautyy.Models.FormTemplate")
            {
                int formId = (this as Component).Id;

                
                List<FormComponentTemplate> templates = db.FormComponentTemplates.Include(fc => fc.CombineFormElementTemplates).ThenInclude(f =>f.FormElement)
                    .Where(t => t.FormId == formId)
                    .ToList();

                foreach (FormComponentTemplate componentTemplate in templates)
                {
                    FormComponentTemplate combinedele = db.FormComponentTemplates.Include(c => c.CombineFormElementTemplates)
                     .ThenInclude(f => f.FormElement)

                     .FirstOrDefault(t => t.Id == componentTemplate.Id);

                    combinedele.Delete(db);
                }
            }
      

            IsDelete = true;
            db.Components.Update(this);

            return this;
        }

        public void DeleteComponentRecursive(Component component, CustomContext db)
        {
            if (component == null) return;


            List<Containing> containings = db.Containings
                .Include(c => c.Component)
                    .ThenInclude(c => c.CombineElements)
                        .ThenInclude(f => f.ComponentElement)
                .Include(c => c.Component.FormTemplate.FormComponentTemplates)
                .AsNoTracking()
                .Where(c => c.ContainerId == component.Id)
                .ToList();


            foreach (Containing containing in containings)
            {

                DeleteComponentRecursive(containing.Component, db);


                Containing containingEntity = db.Containings.FirstOrDefault(c => c.Id == containing.Id);
                if (containingEntity != null)
                {
                    containingEntity.Delete(db);
                }
            }


            if (component.Name == "Beautyy.Models.Section")
            {
                Section section = (Section)component;
                section.Containings = containings ?? new List<Containing>();
                section.Delete(db);
            }
            else
            {

                component.Delete(db);
            }
        }


        public Component Update(CustomContext db)
        {

            Type type = Type.GetType(this.Name);

            dynamic json = JsonConvert.DeserializeObject(this.Data, type);

            Component old = db.Components.Include(co => co.CombineElements)
                .Include(c => c.Containings)
                .Where(c => c.Id == this.Id && c.IsDelete == false).AsNoTracking()
              .FirstOrDefault();

            if(type.Name == "Page")
            {
                Component jsonPage = (Component)this;
                (json as Component).Id = (jsonPage as Page).Id;
                json.Name = this.Name;
                json.EventId = (jsonPage as Page).EventId;

                db.Update(json);
                db.SaveChanges();
                return json;
            }


            (json as Component).Id = this.Id;
            json.Name = this.Name;
            json.Containings = this.Containings;
            json.IsDelete = false;
            json.CombineElements = this.CombineElements;

            foreach (CombineElement combine in json.CombineElements)
            {
                if (combine.Id == 0)
                {
                    combine.Create(db);
                }
                else
                {
                    combine.Update(db);
                }
            }

            if (old != null)
            {
                foreach (CombineElement delete in old.CombineElements)
                {
                    bool existsInNew = this.CombineElements.Any(c => c.Id == delete.Id);
                    if (!existsInNew)
                    {
                        CombineElement deletecombine = db.CombineElements.Include(co => co.ComponentElement).Where(c => c.Id == delete.Id).FirstOrDefault();

                        deletecombine.Delete(db);
                    }
                }
            }


            if (type.Name == "FormTemplate")
            {
                FormTemplate form = db.FormTemplates.Include(f => f.FormComponentTemplates)
                    .ThenInclude(c => c.CombineFormElementTemplates)
                    .ThenInclude(e => e.FormElement)
                    .AsNoTracking()
                    .FirstOrDefault(c => c.Id == this.Id && c.IsDelete == false);

                foreach (FormComponentTemplate componentTemplate in json.FormComponentTemplates)
                {
                    if (componentTemplate.Id == 0)
                    {
                        componentTemplate.FormId = this.Id;
                        componentTemplate.Create(db);
                    }
                    else
                    {
                        componentTemplate.Update(db);
                    }
                }


                foreach (FormComponentTemplate delete in form.FormComponentTemplates)
                {
                    var newTemplates = (List<FormComponentTemplate>)json.FormComponentTemplates;

                    bool existsInNew = newTemplates.Any(c => c.Id == delete.Id);
                    if (!existsInNew)
                    {
                        FormComponentTemplate deletecomponent = db.FormComponentTemplates
                            .Include(c => c.CombineFormElementTemplates)
                            .ThenInclude(e => e.FormElement)
                            .Where(c => c.Id == delete.Id).FirstOrDefault();

                        deletecomponent.Delete(db);
                    }
                }

                json.FormComponentTemplates = null;

            }


            db.Update(json);
            db.SaveChanges();
            return json;
        }


    }


}
