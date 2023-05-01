using Pricat.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pricat.Domain.Entities
{
    [Table("Categories")]
    public class Category : EntityBase
    {
        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(50,ErrorMessage = "Description's Max Length is 50 Characters")]
        public string Description { get; set; } = null!;
        
    }
}
