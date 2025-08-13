using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beautyy.Models
{
    public class FormElementTemplateMetadata
    {
    }
    public partial class FormLabelTemplate : FormElementTemplate
    {

    }
    public partial class FormOptionTemplate : FormElementTemplate
    {

    }
    public partial class FormInputFileTemplate : FormElementTemplate
    {

    }
    public partial class FormInputDateTemplate : FormElementTemplate
    {

    }
    public partial class FormInpuTextTemplate : FormElementTemplate
    {

    }

    public partial class ButtonElementForm : FormElementTemplate
    {

    }

    public partial class PictureForm : FormElementTemplate
    {

    }

    [MetadataType(typeof(FormElementTemplateMetadata))]
    public partial class FormElementTemplate
    {
        [NotMapped]
        public string DataEle { get; set; } = string.Empty;
        public FormElementTemplate Create(CustomContext db)
        {
            Type type = Type.GetType(this.ElementForm);

            dynamic json = JsonConvert.DeserializeObject(this.DataEle, type);

            json.ElementForm = type.FullName;
            json.IsDelete = false;

            db.Add(json);
            db.SaveChanges();

            return json;
        }

        public FormElementTemplate Copy(CustomContext db)
        {
            this.Id = 0;
            this.CombineFormElementTemplates = null;
            db.Add(this);
            db.SaveChanges();
            return this;
        }

        public FormElementTemplate Delete(CustomContext db)
        {
            IsDelete = true;
            db.FormElementTemplates.Update(this);
            return this;
        }

        public FormElementTemplate Update(CustomContext db)
        {
            Type type = Type.GetType(this.ElementForm);

            dynamic json = JsonConvert.DeserializeObject(this.DataEle, type);

            json.ElementForm = type.FullName;
            (json as FormElementTemplate).Id = this.Id;
            json.IsDelete = false;

            db.Update(json);
            db.SaveChanges();

            return json;
        }
    }
}
