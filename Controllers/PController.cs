using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaDev.Data;
using PruebaDev.Modelos;
using PruebaDev.Modelos.Dto;
using PruebaDev.Repositorio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace PruebaDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PController : ControllerBase
    {
        private readonly IPersonaRepositorio _personaRepositorio;
        protected ResponseDto _response;
        public PController(IPersonaRepositorio personaRepositorio)
        {
            _personaRepositorio = personaRepositorio;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            try
            {
                var lista = await _personaRepositorio.GetPersonas();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Clientes";
            }
            catch (Exception error)
            {
                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { error.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _personaRepositorio.GetPersonaById(id);
            if (persona == null)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "La persona no existe";
                return NotFound(_response);
            }
            _response.Result = persona;
            _response.DisplayMessage = "Info del cliente";
            return Ok(_response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPersona(int id, PersonaDto personaDto)
        {
            try
            {
                PersonaDto model = await _personaRepositorio.CreateUpdate(personaDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception error)
            {

                _response.IsSucces = false;
                _response.DisplayMessage = "Error al actualizar";
                _response.ErrorMessages = new List<string> { error.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(PersonaDto personaDto)
        {
            try
            {
                PersonaDto model = await _personaRepositorio.CreateUpdate(personaDto);
                _response.Result = model;
                return CreatedAtAction("GetPersona", new { id = personaDto.Id }, _response);
            }
            catch (Exception error)
            {

                _response.IsSucces = false;
                _response.DisplayMessage = "Error al crear";
                _response.ErrorMessages = new List<string> { error.ToString() };
                return BadRequest(_response);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            try
            {
                bool estaEliminado = await _personaRepositorio.DeletePersona(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Eliminado con exito";
                    return Ok(_response);

                }
                else
                {
                    _response.IsSucces = false;
                    _response.DisplayMessage = "Error al eliminar";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex) 
            {
                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
               
            }
        }
    }
}
