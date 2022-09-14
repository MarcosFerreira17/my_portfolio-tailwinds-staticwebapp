using System.ComponentModel.DataAnnotations;

namespace MWF.Blog.Domain.Dtos;

public record class MWF.BlogDto
{
    [Required(ErrorMessage = "The field {0} is required")]
    public long Id { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    public string ExampleString { get; set; }
}
