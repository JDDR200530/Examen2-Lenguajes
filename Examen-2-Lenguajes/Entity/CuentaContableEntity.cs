using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2_Lenguajes.Entity
{
 
    [Table("Catalago_Cuentas", Schema = "dbo")]

    public class CuentaContableEntity
    {
        [Key]
        [Column("codigos_cuentas")]
        public int CodigoCuenta { get; set; }

        [Column("nombre_cuenta")]
        public string NombreCuenta { get; set; }

        [Column("cantidad")]
    
        public int Monto { get; set; }


        [Column("tipo_movimiento")]
        public string Movimiento { get; set; }
    }


}


