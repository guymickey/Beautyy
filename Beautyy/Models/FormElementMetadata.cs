using System.ComponentModel.DataAnnotations;

namespace Beautyy.Models
{
    public class FormElementMetadata
    {
    }
    [MetadataType(typeof(FormElementMetadata))]
    public partial class FormElement
    {
        public FormElement Create (FormElementTemplate form, CustomContext context)
        {
            ElementForm = form.ElementForm;
            FormElementTeplateId = form.Id;
            context.Add(this);
            context.SaveChanges();
            return this;
        }
    }
}
