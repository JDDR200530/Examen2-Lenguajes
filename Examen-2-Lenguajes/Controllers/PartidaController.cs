using Azure;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Dto.Partida;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace Examen_2_Lenguajes.Controllers
{
    [ApiController]
    [Route("/api/partidas")]
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaService partidaService;

        public PartidaController(IPartidaService partidaService)
        {
            this.partidaService = partidaService;
        }

        [HttpGet("{numPartida}")]
        public async Task<ActionResult<Response<PartidaDto>>> GetByNumPartidaAsync(int numPartida)
        {
            var response = await partidaService.GetByIdAsync(numPartida);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<PartidaDto>>> Create(PartidaCreatedDto dto)
        {
            var response = await partidaService.CreatePartidaAsync(dto);
            return StatusCode(response.StatusCode, response);
        }


    }
}
