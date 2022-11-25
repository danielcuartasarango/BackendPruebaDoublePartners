using Microsoft.EntityFrameworkCore;
using PruebaDev.Data;
using PruebaDev.Modelos;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace PruebaDev.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }
        public async  Task<string> Login(string username, string password)
        {
            var user = await _db.Usuarios.FirstOrDefaultAsync(
                x => x.User.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                return "nouser";
            }
            else if (!VerificarPassHash(password,user.PasswordHash, user.PasswordSalt))
            {
                return ("wrongpass");
            }
            else
            {
                return "Ok";
            }
        }

        public async Task<int> Register(Usuario user, string password)
        {
            try
            {
                if (await UserExiste(user.User))
                {
                    return -1;
                }
                CrearPassHash(password, out byte[] passHash, out byte[] passSalt);

                user.PasswordHash = passHash;
                user.PasswordSalt = passSalt;

                await _db.Usuarios.AddAsync(user);
                await _db.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception e)
            {

                return -500;
            }
        }

        public async Task<bool> UserExiste(string username)
        {
            if (await _db.Usuarios.AnyAsync(x => x.User.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }



        private void CrearPassHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerificarPassHash(string password, byte[] passHash, byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }

        }
    }
}
