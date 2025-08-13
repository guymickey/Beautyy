using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Beautyy.Models
{
    public class ComponentElementMetadata
    {
    }
    [MetadataType(typeof(ComponentElementMetadata))]
    public partial class ComponentElement
    {
        [NotMapped]
        public string EleData { get; set; } = string.Empty;
        public ComponentElement Create(CustomContext db)
        {
            Type type = Type.GetType(this.Element);

            dynamic json = JsonConvert.DeserializeObject(this.EleData, type);

            json.Element = this.Element;
            json.IsDelete = false;

            db.Add(json);
            db.SaveChanges();

            return json;
        }

        public ComponentElement Copy(CustomContext db)
        {            
            this.Id = 0;
            this.CombineElements = null;
            db.Add(this);
            db.SaveChanges();
            return this;
        }

        public ComponentElement Delete(CustomContext db)
        {
            IsDelete = true;
            db.Update(this);
            return this;
        }

        public ComponentElement Update(CustomContext db)
        {

            Type type = Type.GetType(this.Element);

            dynamic json = JsonConvert.DeserializeObject(this.EleData, type);


            (json as ComponentElement).Id = this.Id;
            json.Element = this.Element;
            json.IsDelete = false;

            db.Update(json);

            db.SaveChanges();

            return json;
        }
    }
}
