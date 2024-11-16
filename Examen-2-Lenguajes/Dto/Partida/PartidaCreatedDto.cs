namespace Examen_2_Lenguajes.Dto.Partida
{
    public class PartidaCreatedDto 
    {
        public int NumPartida { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Description { get; set; }
        public int CodigoCuenta { get; set; }
        public decimal Monto { get; set; }
        public string? TipoTransaccion { get; set; }
        public string? UserId { get; set; }

    }
}
