using Beautyy.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Beautyy.Models
{
    public class ContainingMetadata
    {
    }
    [MetadataType(typeof(ContainingMetadata))]
    public partial class Containing
    {
        public Containing Create(CustomContext db)
        {
            IsDelete = false;
            db.Containings.Add(this);
            db.SaveChanges();

            return this;
        }

        public Containing Delete(CustomContext db)
        {
            IsDelete = true;
            db.Containings.Update(this);
            db.SaveChanges();

            return this;
        }

        public Containing Update(CustomContext db )
        {
            Component = null;

            db.Update(this);
            db.SaveChanges();

            return this;
        }


    }
}
