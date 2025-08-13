using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beautyy.Models
{
    public class ImageFileMetadata
    {
    }
    [MetadataType(typeof(ImageFileMetadata))]
    public partial class ImageFile
    {
        [NotMapped]
        public IFormFile? FormFile { get; set; }

        public ImageFile Create(CustomContext db, IFormFile formFile)
        {
            if (formFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(formFile.FileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + Path.GetExtension(formFile.FileName);
                string filePath = Path.Combine("wwwroot", "uploads", fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                FileName = fileName;
                FilePath = filePath;
                IsDelete = false;
                db.ImageFiles.Add(this);
            }
            return this;
        }
    }
}
