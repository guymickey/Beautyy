using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beautyy.Models
{
    public class CombineElementMetadata
    {
    }
    [MetadataType(typeof(CombineElementMetadata))]
    public partial class CombineElement
    {
        [NotMapped]
        public ComponentElement? com { get; set; }
        public CombineElement Create (CustomContext db)
        {
            IsDelete = false;
            if (com != null)
            {
                ComponentElement componentElement = com.Create(db) ;
                this.ComponentElementId = componentElement.Id;
            }
            db.CombineElements.Add(this);
            
            return this;
        }

        public CombineElement Copy(CustomContext db)
        {
            
            IsDelete = false;
            ComponentElement.Copy(db);
            db.CombineElements.Add(this);
            db.SaveChanges();
            return this;
        }

        public CombineElement Delete(CustomContext db)
        {
            IsDelete = true;
            ComponentElement.Delete(db);
            db.CombineElements.Update(this);
            return this;
        }

        public CombineElement Update(CustomContext db)
        {
            IsDelete = false;

            ComponentElement.Update(db);

            ComponentElement = null;
            db.Update(this);

            return this;
        }
    }
}
