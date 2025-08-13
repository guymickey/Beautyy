using System.ComponentModel.DataAnnotations;

namespace Beautyy.Models
{
    public class FormComponentMetadata
    {
    }
    [MetadataType(typeof(FormComponentMetadata))]
    public partial class FormComponent
    {
        public FormComponent Create (int FormComponentTemplateID, CustomContext context)
        {
            List<FormComponentTemplate> FormComponentTemplate = context.FormComponentTemplates.Where(f => f.FormId == FormComponentTemplateID).ToList();
            
            foreach (FormComponentTemplate fff in FormComponentTemplate)
            {
                FormComponent formComponent = new FormComponent();
                formComponent.FormId = this.FormId;
                formComponent.TypeForm = fff.TypeForm;
                formComponent.FormComponentTemplateId = fff.Id;
                context.FormComponents.Add(formComponent);
                context.SaveChanges();

                List<CombineFormElementTemplate> combineFormComponentTemplate = context.CombineFormElementTemplates.Where(f => f.FormComponentId == fff.Id).ToList();

                foreach (CombineFormElementTemplate sss in combineFormComponentTemplate)
                {
                    FormCombineElement formCombine = new FormCombineElement();
                    formCombine.FormCompoentId = formComponent.Id;
                    formCombine.Create(sss.FormElementId, context);
                }

                
            }
            return this;
        }
    }
}
