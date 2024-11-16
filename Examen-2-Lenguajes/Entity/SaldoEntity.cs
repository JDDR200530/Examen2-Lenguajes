using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2_Lenguajes.Entity
{
    [Table ("saldo", Schema = "dbo")]
    public class SaldoEntity
    {
        [Key]
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("codigo_cuenta")]
        public int CodigoCuenta { get; set; }

        [ForeignKey(nameof(CodigoCuenta))]
        public virtual CuentaContableEntity CuentaContable { get; set; }

        [Column("nombre_cuenta")]
        public string NombreCuenta { get; set; }

        [Column("cantidad")]
       
        public int Monto { get; set; }


    }


}
