using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaDev.Data;
using PruebaDev.Modelos;
using PruebaDev.Modelos.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaDev.Repositorio
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public PersonaRepositorio(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PersonaDto> CreateUpdate(PersonaDto personaDto)
        {
            Persona persona = _mapper.Map<PersonaDto,Persona>(personaDto);
            if (persona.Id >0)
            {
                _db.Personas.Update(persona);

            }
            else
            {
                await _db.Personas.AddAsync(persona);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Persona, PersonaDto>(persona);
        }

        public async Task<bool> DeletePersona(int id)
        {
            try
            {
                Persona persona = await _db.Personas.FindAsync(id);
                if (persona == null) {
                    return false;
                 }
                _db.Personas.Remove(persona);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public async Task<PersonaDto> GetPersonaById(int id)
        {
            Persona persona = await _db.Personas.FindAsync(id);
            return _mapper.Map<PersonaDto>(persona);
        }

        public async  Task<List<PersonaDto>> GetPersonas()
        {
            List<Persona> lista = await _db.Personas.ToListAsync();

            return _mapper.Map<List<PersonaDto>>(lista);
        }
    }
}
