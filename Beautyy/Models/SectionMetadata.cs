using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace Beautyy.Models
{
    public class SectionMetadata
    {
    }
    [MetadataType(typeof(SectionMetadata))]
    public partial class Section
    {
        public void Create(CustomContext db, Component component)
        {
            if (this.Containings == null || !this.Containings.Any())
                return;

            foreach (Containing subcontaining in this.Containings)
            {
                Component child = subcontaining.Component.Create(db);

                Containing containings = new Containing
                {
                    Order = subcontaining.Order,
                    IsShow = subcontaining.IsShow,
                    ComponentId = child.Id,
                    ContainerId = component.Id,
                };

                containings.Create(db);

                if (child.Name == "Beautyy.Models.Section")
                {
                    Section nestedSection = (Section)child;
                    nestedSection.Containings = subcontaining.Component.Containings?.ToList() ?? new List<Containing>();
                    nestedSection.Create(db, child);
                }
            }
        }

        public void CopySection(CustomContext db, Component component)
        {
            if (this.Containings == null || !this.Containings.Any())
                return;

            foreach (Containing subcontaining in this.Containings)
            {
          
                Component child = subcontaining.Component.CopyComponent(db);

                Containing containings = new Containing();

                containings.Order = subcontaining.Order;
                containings.IsShow = subcontaining.IsShow;
                containings.ComponentId = child.Id;
                containings.ContainerId = component.Id;

                containings.Create(db);

                if (child.Name == "Beautyy.Models.Section")
                {

                    List<Containing> containingsSection = db.Containings.Include(c => c.Component).ThenInclude(c => c.CombineElements)
                        .ThenInclude(c => c.ComponentElement)
                        .Include(c => c.Component)
                        .ThenInclude(f => f.FormTemplate.FormComponentTemplates)
                        .ThenInclude(f => f.CombineFormElementTemplates)
                        .ThenInclude(f => f.FormElement)
                        .Where(c => c.ContainerId == subcontaining.ComponentId && c.IsDelete == false).AsNoTracking().ToList();

                    Section nestedSection = (Section)child;
                    nestedSection.Containings = containingsSection.ToList() ?? new List<Containing>();
                    nestedSection.Create(db, child);
                }
            }
        }


        public void DeleteSection (CustomContext db)
        {
            if (this.Containings == null || !this.Containings.Any())
                return;

            foreach (Containing subcontaining in this.Containings)
            {
                Component child = subcontaining.Component.Delete(db);

                subcontaining.Delete(db);

                if (child.Name == "Beautyy.Models.Section" && IsDelete != true)
                {
                    Section nestedSection = (Section)child;
                    nestedSection.DeleteSection(db);
                }
                if (child.Name == "Beautyy.Models.Section")
                {
                    List<Containing> delesection = db.Containings.Include(c => c.Component).ThenInclude(c => c.CombineElements).ThenInclude(f => f.ComponentElement).Where(c => c.ContainerId == child.Id).ToList();

                    Section nestedSection = (Section)child;
                    nestedSection.Containings = delesection.ToList() ?? new List<Containing>();
                    nestedSection.DeleteSection(db);
                }

            }
        }

        public void UpdateSection(CustomContext db, Component component)
        {
            
            List<Containing> old = db.Containings.Where(c => c.ContainerId == this.Id && c.IsDelete == false).AsNoTracking().ToList();



            foreach (Containing containing in this.Containings)
            {
                if (containing.Id != 0)
                {
                    Component child = containing.Component.Update(db);

                    if (child.Name == "Beautyy.Models.Section")
                    {
                        Section nestedSection = (Section)child;

                        nestedSection.UpdateSection(db, child);

                        nestedSection.Containings.Clear();
                    }

                    containing.Update(db);
                }
                

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
               


            }



            foreach (Containing deleteoldContaining in old)
            {
                bool existsInNew = this.Containings.Any(c => c.Id == deleteoldContaining.Id);

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

        }


    }
}
