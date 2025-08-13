using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Beautyy.Models
{
    public class CombineFormElementTemplateMetadata
    {
    }
    [MetadataType(typeof(CombineFormElementTemplateMetadata))]
    public partial class CombineFormElementTemplate
    {
        [NotMapped]
        public FormElementTemplate? ele { get; set; }

        public CombineFormElementTemplate Create(CustomContext db)
        {
            IsDelete = false;

            if (ele != null)
            {
                FormElementTemplate formElementTemplate = ele.Create(db);
                this.FormElementId = formElementTemplate.Id;
               
            }
            db.CombineFormElementTemplates.Add(this);

            return this;
        }

        public CombineFormElementTemplate Copy(CustomContext db)
        {
            IsDelete = false;
            FormElement.Copy(db);
            db.CombineFormElementTemplates.Add(this);
            db.SaveChanges();

            return this;
        }

        public CombineFormElementTemplate Delete(CustomContext db)
        {
            IsDelete = true;

            FormElementTemplate formElement = db.FormElementTemplates
                .FirstOrDefault(f => f.Id == this.FormElementId);

            formElement.Delete(db);

            db.CombineFormElementTemplates.Update(this);

            return this;
        }

        public CombineFormElementTemplate Update(CustomContext db)
        {
            IsDelete = false;

            FormElement.Update(db);

            FormElement = null;

            db.CombineFormElementTemplates.Update(this);

            return this;
        }
    }
}