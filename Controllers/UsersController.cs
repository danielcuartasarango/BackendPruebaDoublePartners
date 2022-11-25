using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaDev.Modelos;
using PruebaDev.Modelos.Dto;
using PruebaDev.Repositorio;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PruebaDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsuarioRepositorio _userRepositorio;
        protected ResponseDto _response; 
        public UsersController(IUsuarioRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
            _response = new ResponseDto();
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UsuarioDto user)
        {
            var respuesta = await _userRepositorio.Login(user.User, user.Password);
            if (respuesta == "nouser") {
                _response.IsSucces = false;
                _response.DisplayMessage = "Usuario no existe";
                return BadRequest(_response);
            }
            if (respuesta =="wrongpass")
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Contraseña incorrecta";
                return BadRequest(_response);

            }return Ok("UsuarioConectado");
        }

        [HttpPost("Registrer")]
        public async Task<ActionResult> Register(UsuarioDto user)
        {
            var respuesta = await _userRepositorio.Register(
                    new Usuario
                    {
                        User = user.User
                    }, user.Password
                );
            if (respuesta == -1)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "El usuario ya existe";
                return BadRequest(_response);
            }
            if (respuesta == -500)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Error al crear usuario";
                return BadRequest(_response);
            }
            _response.DisplayMessage = "Usuario creado con exito";
            _response.Result = respuesta;

            return Ok(_response);
            {

            }
        }
    }
}
