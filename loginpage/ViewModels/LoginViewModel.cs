using System.ComponentModel.DataAnnotations;

namespace loginpage.ViewModels
{
    //attribute declaration for login feature (acts as viewmodel)
    //conditions included for error handling
    public class LoginViewModel
    {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

        }
}
