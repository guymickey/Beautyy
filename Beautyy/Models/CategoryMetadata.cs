using System.ComponentModel.DataAnnotations;

namespace Beautyy.Models
{
    public class CategoryMetadata
    {
    }
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
        public Category Create (CustomContext db)
        {
            IsDelete = false;
            db.Categories.Add(this);
            return this;
        }

        public Category Update (CustomContext db ,Category category)
        {
            category.IsDelete = false;
            db.Categories.Update(category);

            return category;
        }

        public static List<Category> GetAll(CustomContext db)
        {
            return db.Categories.Where(c => !c.IsDelete).ToList();
        }

        public Category Delete (CustomContext db)
        {
            IsDelete = true;
            db.Update(this);
            return this;
        }
    }
}
