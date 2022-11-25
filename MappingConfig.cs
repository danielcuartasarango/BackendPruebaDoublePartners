using AutoMapper;
using PruebaDev.Modelos;
using PruebaDev.Modelos.Dto;

namespace PruebaDev
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var MappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PersonaDto, Persona>();
                config.CreateMap<Persona, PersonaDto>();
            });
            return MappingConfig;
        }
    }
}
