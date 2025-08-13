using System.ComponentModel.DataAnnotations;

namespace Beautyy.Models
{
    public class FormMetadata
    {
    }
    [MetadataType(typeof(FormMetadata))]
    public partial class Form
    {
        public Form Create(int formid, CustomContext context)
        {
            Component form = context.Components.FirstOrDefault(f => f.Id == formid);

            Type type = Type.GetType(form.Name);

            if (type.Name != "FormTemplate")
            {
                return this;
            }

            Topic = form.FormTemplate.Topic;
            ButtonName = form.FormTemplate.ButtonName;
            Url = form.FormTemplate.Url;
            FormTemplateId = form.Id;
            context.Forms.Add(this);
            context.SaveChanges();

            FormComponent formComponent = new FormComponent();
            formComponent.FormId = this.Id;
            formComponent.Create(formid, context);

            return this;
        }
    }
}
