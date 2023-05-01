using System.ComponentModel.DataAnnotations;

namespace wuav_api.Identity.BindingModels;

public class AuthenticateRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}