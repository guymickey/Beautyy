using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Beautyy.Models;


namespace Beautyy.Models
{
    public class TextboxMetadata
    {

    }
    [MetadataType(typeof(TextboxMetadata))]
    public partial class TextBox : Component
    {
       
    }
  
}
    