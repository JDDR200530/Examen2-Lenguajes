using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examen_2_Lenguajes.Entity
{
    public class UserEntity : IdentityUser
    {
        
        public Guid? UserId { get; set; }  // Esto será el GUID, no string

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
