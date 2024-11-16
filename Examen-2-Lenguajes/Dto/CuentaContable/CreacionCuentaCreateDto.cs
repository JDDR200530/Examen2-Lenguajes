using Examen_2_Lenguajes.Entity;

namespace Examen_2_Lenguajes.Dto.CuentaContable
{
    public class CreacionCuentaCreateDto  
    {
        public int CodigoCuenta { get; set; }
        public string NombreCuenta { get; set; }
        public int Monto { get; set; }
        public string Movimiento { get; set; }
    }
}
