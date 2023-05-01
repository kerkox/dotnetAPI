using Pricat.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pricat.Domain.Entities;

[Table("Products")]
public class Product : EntityBase
{
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "EanCode is Required")]
    [MaxLength(13, ErrorMessage = "EanCode's Max Length is 13 digits")]
    public string EanCode { get; set; } = null!;
    [Required(ErrorMessage = "Description is Required")]
    [MaxLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
    public string Description { get; set; } = null!;
    [Required(ErrorMessage = "Unit is Required")]
    [MaxLength(20, ErrorMessage = "Unit's Max Length is 20 Characters" )]
    public string Unit { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    
}