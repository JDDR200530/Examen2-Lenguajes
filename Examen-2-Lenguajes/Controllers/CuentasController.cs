using Azure;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Dto.CuentaContable;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace Examen_2_Lenguajes.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaContableService cuentaContableService;

        public CuentasController(ICuentaContableService cuentaContableService)

        {
            this.cuentaContableService = cuentaContableService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<CuentaContableDto>>> GetAll()
        {
            var response = await cuentaContableService.GetCuentasListAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<CuentaContableDto>>> Create(CreacionCuentaCreateDto dto)
        {
            var response = await cuentaContableService.CreatedCuentaAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{CodigoCuenta}")]
        public async Task<ActionResult<ResponseDto<CuentaContableDto>>> Edit(CuentaContableEditDto dto, int CodigoCuenta)
        {
            var response = await cuentaContableService.EditAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
