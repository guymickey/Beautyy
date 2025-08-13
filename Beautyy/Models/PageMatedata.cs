using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Beautyy.Models
{
    public class PageMatedata
    {
    }
    [MetadataType(typeof(PageMatedata))]

    public partial class Page
    {
        public Component CreatePage(CustomContext db)
        {
            List<Containing> containings = this.Containings.ToList();
            this.Containings.Clear();

            Component page = this.Create(db);

            if (page is Page page1)
            {
                page1.EventId = this.EventId;
                db.SaveChanges();
            }

            foreach (Containing containing in containings)
            {
                Component child = containing.Component.Create(db);

                Containing co = new Containing();
                co.Order = containing.Order;
                co.IsShow = containing.IsShow;
                co.ContainerId = page.Id;
                co.ComponentId = child.Id;
                co.Create(db);

                if (child.Name == "Beautyy.Models.Section")
                {
                    Section section = (Section)child;
                    section.Containings = containing.Component.Containings.ToList() ?? new List<Containing>();
                    section.Create(db, child);
                }


            }

            return this;
        }

        public Component DeletePage(CustomContext db)
        {

            this.Delete(db);

            foreach (Containing containing in this.Containings)
            {
                Component child = containing.Component.Delete(db);

                containing.Delete(db);

                if (child.Name == "Beautyy.Models.Section")
                {
                    Section section = (Section)child;
                    section.DeleteSection(db);
                }


            }

            return this;
        }

        public Component UpdatePage(CustomContext db, Page page)
        {


            page.Update(db);



            List<Containing> old = db.Containings.Where(c => c.ContainerId == page.Id && c.IsDelete == false).AsNoTracking().ToList();


            foreach (Containing containing in page.Containings)
            {
                if (containing.Id == 0)
                {
                    Component child = containing.Component.Create(db);

                    Containing co = new Containing();
                    co.Order = containing.Order;
                    co.IsShow = containing.IsShow;
                    co.ContainerId = this.Id;
                    co.ComponentId = child.Id;
                    co.Create(db);

                    if (child.Name == "Beautyy.Models.Section")
                    {
                        Section section = (Section)child;
                        section.Containings = containing.Component.Containings.ToList() ?? new List<Containing>();
                        section.Create(db, child);
                    }
                }
                if (containing.Id != 0)
                {

                    Component childupdate = containing.Component.Update(db);

                    if (childupdate.Name == "Beautyy.Models.Section")
                    {
                        Section section = (Section)childupdate;

                        section.UpdateSection(db, childupdate);

                        section.Containings.Clear();
                    }

                    containing.Update(db);

                }

            }


            foreach (Containing deleteoldContaining in old)
            {
                bool existsInNew = page.Containings.Any(c => c.Id == deleteoldContaining.Id);
                if (!existsInNew)
                {
                    Component deletecomponent = db.Components.Where(c => c.Id == deleteoldContaining.ComponentId).Include(c => c.CombineElements).ThenInclude(f => f.ComponentElement).Include(f => f.FormTemplate.FormComponentTemplates).AsNoTracking().FirstOrDefault();

                    deletecomponent.Delete(db);

                    // Soft delete
                    Containing toDelete = db.Containings.AsNoTracking().FirstOrDefault(c => c.Id == deleteoldContaining.Id);

                    if (toDelete != null)
                    {
                        toDelete.Delete(db);
                    }

                    if (deletecomponent.Name == "Beautyy.Models.Section")
                    {
                        List<Containing> delesection = db.Containings.Include(c => c.Component).ThenInclude(c => c.CombineElements).ThenInclude(f => f.ComponentElement).AsNoTracking().Where(c => c.ContainerId == deletecomponent.Id).ToList();

                        Section section = (Section)deletecomponent;
                        section.Containings = delesection.ToList() ?? new List<Containing>();
                        section.DeleteSection(db);
                    }
                }
            }

            return this;
        }

        public Component CopyPage(CustomContext db)
        {

            Component component = this.CopyComponent(db);

            List<Containing> containings = db.Containings.Include(c => c.Component).Where(c => c.ContainerId == this.Id && c.IsDelete == false).AsNoTracking().ToList();

            foreach (Containing containing in containings)
            {

                Component child = containing.Component.CopyComponent(db);

                Containing co = new Containing();
                co.Order = containing.Order;
                co.IsShow = containing.IsShow;
                co.ContainerId = (component as Component).Id;
                co.ComponentId = child.Id;
                co.Create(db);


                if (child.Name == "Beautyy.Models.Section")
                {

                    List<Containing> containingsSection = db.Containings.Include(c => c.Component)
                        .ThenInclude(c => c.CombineElements)
                        .ThenInclude(c => c.ComponentElement)
                        .Include(c => c.Component)
                        .ThenInclude(f => f.FormTemplate.FormComponentTemplates)
                        .ThenInclude(f => f.CombineFormElementTemplates)
                        .ThenInclude(f => f.FormElement)
                        .Where(c => c.ContainerId == containing.ComponentId && c.IsDelete == false).AsNoTracking().ToList();

                    Section section = (Section)child;
                    section.Containings = containingsSection.ToList() ?? new List<Containing>();
                    section.CopySection(db, child);
                }


            }

            return this;
        }
    }
}
