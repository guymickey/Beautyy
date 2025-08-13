using System.ComponentModel.DataAnnotations;

namespace Beautyy.Models.Dto
{
    public class DtoFile
    {
        [Required]
        public IFormFile FormFile { get; set; }
    }
}
