using System.ComponentModel.DataAnnotations;

namespace Fora.Shared.PostModels;

public class CreateInterestsModel
{
    [Required]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Interest name has to be 3-15 characters")]
    public string Name { get; set; }
    public int? UserID { get; set; }
}