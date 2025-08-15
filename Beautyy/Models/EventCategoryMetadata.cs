using System.ComponentModel.DataAnnotations;

namespace Beautyy.Models
{
    public class EventCategoryMetadata
    {
    }
    [MetadataType(typeof(EventCategoryMetadata))]
    public partial class EventCategory
    {
        public EventCategory Create (CustomContext db)
        {
            EventCategory evc = new EventCategory ();

            evc.CategoryId = this.CategoryId;
            evc.EventId = this.EventId;
            evc.IsDelete = false;
            db.EventCategories.Add (evc);
            db.SaveChanges();

            return evc;
        }

        public EventCategory Update(CustomContext db, EventCategory eventCategory)
        {
            eventCategory.IsDelete = false;
            db.EventCategories.Update(eventCategory);
            db.SaveChanges();
            return this;
        }

        public EventCategory Delete(CustomContext db)
        {

 
            IsDelete = true;
            db.EventCategories.Update(this);
            db.SaveChanges();
            return this;

        }

       
    }
}
