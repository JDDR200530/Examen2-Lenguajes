using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen_2_Lenguajes.Entity
{
    [Table ("Partidas", Schema = "dbo")]
    public class PartidaEntity
    {
        [Key]
        [Column("numero_partida")]
        public int NumPartida { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("descripcion")]
        public string? Description { get; set; }

        [Column("id_user")]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }

        [Column("codigo_cuenta")]
        public int CodigoCuenta { get; set; }

        [ForeignKey("CodigoCuenta")]
        public virtual CuentaContableEntity CuentaContable { get; set; }

        [Column("nombre_cuenta")]
        public string? NombreCuenta { get; set; }

        [Column("cantidad")]
        public int? Monto { get; set; }

        [Column("tipo_transaccion")]
        public string? TipoTransaccion { get; set; }
    }





}
