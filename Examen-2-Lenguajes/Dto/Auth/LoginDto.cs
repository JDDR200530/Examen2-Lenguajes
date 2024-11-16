using System.ComponentModel.DataAnnotations;

namespace Examen_2_Lenguajes.Dto.Auth
{
    public class LoginDto
    {
        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = " El {0} es obligatoria")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = " La {0} es obligatoria")]
        public string Password { get; set; }
    }
}
