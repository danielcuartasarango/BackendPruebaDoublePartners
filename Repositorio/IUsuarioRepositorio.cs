using PruebaDev.Modelos;
using PruebaDev.Modelos.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaDev.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<int> Register(Usuario user, string password);
        Task<string> Login(string username, string password);
        Task<bool> UserExiste(string username);
    }
}
