using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Dto.Partida;

namespace Examen_2_Lenguajes.Services.Intefaces
{
    public interface IPartidaService
    {

        Task<ResponseDto<PartidaDto>> GetByIdAsync(int numPartida);
        Task<ResponseDto<PartidaDto>> CreatePartidaAsync(PartidaCreatedDto dto);
        Task<ResponseDto<PartidaDto>> EditAsync(PartidaEditDto dto, int numPartida);
    }
}
