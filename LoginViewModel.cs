using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tqmrin2.Models;


public class LoginViewModel
{
    [StringLength(100)]
    [Required(ErrorMessage ="این فیلد نباید خالی باشد")]
    [MinLength(4)]

    public string Username { get; set; }

    
    [Required(ErrorMessage = "این فیلد نباید خالی باشد")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [MinLength(8)]

    public string Password { get; set; }
}
