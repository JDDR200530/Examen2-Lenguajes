using AutoMapper;
using Examen_2_Lenguajes.Dto.CuentaContable;
using Examen_2_Lenguajes.Dto.Partida;
using Examen_2_Lenguajes.Dto.User;
using Examen_2_Lenguajes.Entity;

namespace Examen_2_Lenguajes.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForCuentasContables();
            MapsForUser();
            MapsForPartidas();

        }

        private void MapsForCuentasContables()
        {
            CreateMap<CuentaContableEntity, CuentaContableDto>();
            CreateMap<CuentaContableEditDto, CuentaContableEntity>();
            CreateMap<CreacionCuentaCreateDto, CuentaContableEntity>();
        }

        private void MapsForUser()
        {
            CreateMap<UserEntity, UserCreatedDto>();
            CreateMap<UserCreatedDto, UserEntity>();
            CreateMap<UserEditDto, UserEntity>();
        }

        private void MapsForPartidas() 
        {
            CreateMap<PartidaEntity, PartidaDto>();
            CreateMap<PartidaCreatedDto, PartidaEntity>();
            CreateMap<PartidaEditDto, PartidaEntity>();
        }
    }
}
