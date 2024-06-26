using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MIS_REPORTES.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;


namespace MIS_REPORTES.Service
{
    public class UserService : IUserService
    {
        private readonly MetrologyContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(IHttpContextAccessor httpContextAccessor, MetrologyContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RbacUsuarios> Authenticate(string username, string password)
        {
            var user = await _context.RbacUsuarios.SingleOrDefaultAsync(u => u.Nomusu == username);

            if (user != null && VerifyPassword(user, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Nomusu)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = false });

                return user;
            }

            return null;
        }




        public async Task<bool> Register(string username, string password, string email)
        {
            if (await _context.RbacUsuarios.AnyAsync(u => u.Nomusu == username))
            {
                return false;
            }
            var newUser = new RbacUsuarios
            {
                Nomusu = username,
                Clave = HashPassword(password), // Hashear la contraseña antes de guardarla
                Email = email,
                Estado = "Activo" // Ejemplo de estado inicial
                                  // Otras propiedades según sea necesario
            };

            _context.RbacUsuarios.Add(newUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var user = await _context.RbacUsuarios.FindAsync(userId);

            if (user == null)
            {
                return false;
            }

            if (!VerifyPassword(user, currentPassword))
            {
                return false;
            }

            user.Clave = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<RbacUsuarios> GetUserById(int userId)
        {
            return await _context.RbacUsuarios.FindAsync(userId);
        }

        private bool VerifyPassword(RbacUsuarios user, string password)
        {
            var hashedPassword = HashPassword(password);
            
            return user.Clave == hashedPassword.ToLower();
        }

        private string HashPassword(string password)
        {
            // Implementación básica para hashear la contraseña (usando MD5 por ejemplo)
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

    }
}
