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
    }
}
