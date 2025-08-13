using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace Beautyy.Models
{
    public class FormComponentTemplateMetadata
    {
    }
    [MetadataType(typeof(FormComponentTemplateMetadata))]
    public partial class FormComponentTemplate
    {
        [NotMapped]
        public string DataForm { get; set; } = string.Empty;

        public FormComponentTemplate Create (CustomContext db)
        {
            Type type = Type.GetType(this.TypeForm);

            dynamic json = JsonConvert.DeserializeObject(this.DataForm, type);

            json.TypeForm = type.FullName;
            json.FormId = this.FormId;
            json.IsDelete = false;

            foreach (CombineFormElementTemplate combine in json.CombineFormElementTemplates)
            {
                combine.Create(db);
            }

            db.Add(json);
            db.SaveChanges();

            return json;
        }

        public FormComponentTemplate Copy(CustomContext db)
        {
            this.Id = 0;
            this.Form = null;

            List<CombineFormElementTemplate> combineElements = this.CombineFormElementTemplates.ToList();
            this.CombineFormElementTemplates.Clear();

            db.Add(this);
            db.SaveChanges();

            foreach (CombineFormElementTemplate combine in combineElements)
            {
                CombineFormElementTemplate combineFormElement = new CombineFormElementTemplate();
                combineFormElement.FormComponentId = (this as FormComponentTemplate).Id;
                combineFormElement.FormElement = combineElements.FirstOrDefault(c => c.Id == combine.Id)?.FormElement;
                combineFormElement.Copy(db);
            }

            return this;
        }

        public FormComponentTemplate Delete(CustomContext db)
        {
            IsDelete = true;

            foreach (CombineFormElementTemplate combine in this.CombineFormElementTemplates)
            {
                CombineFormElementTemplate combinedele = db.CombineFormElementTemplates
                      .Include(f => f.FormElement)
                      .FirstOrDefault(t => t.Id == combine.Id);

                combinedele.Delete(db);
            }

            db.FormComponentTemplates.Update(this);
  

            return this;
        }

        public FormComponentTemplate Update(CustomContext db)
        {

            Type type = Type.GetType(this.TypeForm);

            dynamic json = JsonConvert.DeserializeObject(this.DataForm, type);


            (json as FormComponentTemplate).Id = this.Id;
            json.FormId = this.FormId;
            json.TypeForm = type.FullName;
            json.IsDelete = false;
            json.CombineFormElementTemplates = this.CombineFormElementTemplates;

            FormComponentTemplate oldformComponent = db.FormComponentTemplates
                .Include(c => c.CombineFormElementTemplates)
                .ThenInclude(e => e.FormElement)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == this.Id);


            foreach (CombineFormElementTemplate combine in json.CombineFormElementTemplates)
            {
                if (combine.Id == 0)
                {
                    combine.Create(db);
                }
                else
                {
                    combine.Update(db);
                }
            }
            foreach (CombineFormElementTemplate delete in oldformComponent.CombineFormElementTemplates)
            {
                bool existsInNew = this.CombineFormElementTemplates.Any(c => c.Id == delete.Id);
                if (!existsInNew)
                {
                    CombineFormElementTemplate deleteformele = db.CombineFormElementTemplates.Include(co => co.FormElement).Where(c => c.Id == delete.Id).AsNoTracking().FirstOrDefault();

                    deleteformele.Delete(db);
                }
            }
     
         

            db.Update(json);
            db.SaveChanges();


            return json;
        }
    }
}
