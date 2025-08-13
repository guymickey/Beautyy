using System.ComponentModel.DataAnnotations;

namespace Beautyy.Models
{
    public class FormCombineElementMetadata
    {
    }
    [MetadataType(typeof(FormCombineElementMetadata))]
    public partial class FormCombineElement
    {
        public FormCombineElement Create (int? s, CustomContext context)
        {
            FormElementTemplate ss = context.FormElementTemplates.FirstOrDefault(c => c.Id == s);

            FormElement d = new FormElement ();
            d.Create(ss, context);

            FormElementId = d.Id;

            context.FormCombineElements.Add(this);
            context.SaveChanges();

            return this;
        }
    }
}
