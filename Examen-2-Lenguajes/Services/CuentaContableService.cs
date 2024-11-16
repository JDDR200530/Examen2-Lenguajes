using AutoMapper;
using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Dto.CuentaContable;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Examen_2_Lenguajes.Services
{
    public class CuentaContableService : ICuentaContableService
    {
        private readonly PartidasDbContext _context;
        private readonly ILogger<CuentaContableService> _logger;
        private readonly IMapper _mapper;

        public CuentaContableService(PartidasDbContext context, ILogger<CuentaContableService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CuentaContableDto>>> GetCuentasListAsync()
        {
            try
            {
                var cuentasEntity = await _context.CuentaContables.ToListAsync();
                var cuentaDtos = _mapper.Map<List<CuentaContableDto>>(cuentasEntity);

                return new ResponseDto<List<CuentaContableDto>>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Lista de Cuentas contables",
                    Data = cuentaDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de cuentas contables.");
                return new ResponseDto<List<CuentaContableDto>>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Ocurrió un error al obtener la lista de cuentas contables."
                };
            }
        }

        public async Task<ResponseDto<CuentaContableDto>> EditAsync(CuentaContableEditDto dto)
        {
            var response = new ResponseDto<CuentaContableDto>();

            try
            {
                var cuenta = await _context.CuentaContables.FirstOrDefaultAsync(c => c.CodigoCuenta == dto.CodigoCuenta);

                if (cuenta == null)
                {
                    response.Status = false;
                    response.Message = "La cuenta contable no fue encontrada.";
                    return response;
                }

                cuenta.Monto = dto.Monto;

                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "El monto de la cuenta contable fue actualizado exitosamente.";
                response.Data = _mapper.Map<CuentaContableDto>(cuenta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la cuenta contable con CódigoCuenta {CodigoCuenta}.", dto.CodigoCuenta);
                response.Status = false;
                response.Message = "Ocurrió un error al actualizar la cuenta contable.";
            }

            return response;
        }

        public async Task<ResponseDto<CuentaContableDto>> CreatedCuentaAsync(CreacionCuentaCreateDto dto)
        {
            try
            {
                var cuentaEntity = _mapper.Map<CuentaContableEntity>(dto);
                _context.CuentaContables.Add(cuentaEntity);
                await _context.SaveChangesAsync();

                var cuentaDto = _mapper.Map<CuentaContableDto>(cuentaEntity);

                return new ResponseDto<CuentaContableDto>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = "La cuenta se ha creado correctamente",
                    Data = cuentaDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la cuenta contable.");
                return new ResponseDto<CuentaContableDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Ocurrió un error al crear la cuenta contable."
                };
            }
        }
    }



}
