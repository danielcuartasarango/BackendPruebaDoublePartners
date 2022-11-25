using PruebaDev.Modelos.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaDev.Repositorio
{
    public interface IPersonaRepositorio
    {
        Task<List<PersonaDto>> GetPersonas();
        Task<PersonaDto> GetPersonaById(int id);
        Task<PersonaDto> CreateUpdate(PersonaDto persona);
        Task<bool> DeletePersona(int id);
    }
}
