using BCrypt.Net;
using LearnSecureAPI.Data;
using LearnSecureAPI.DTO;
using LearnSecureAPI.Model;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnSecureAPI.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public AppDataContext _context { get; set; }

        public UserController(AppDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SingUpUserDTO>> SingUp(SingUpUserDTO dto)
        {
            var existEmail = await _context.User.AnyAsync(p => p.Email == dto.Email);

            if (existEmail)
                return Conflict("Email already registered");

            var existUsername = await _context.User.AnyAsync(u => u.Username == dto.Username);

            if (existUsername)
                return Conflict("Username already registered");

            var user = dto.Adapt<User>();

            string hashString = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Password = hashString;

            await _context.User.AddAsync(user);

            await _context.SaveChangesAsync();

            var response = user.Adapt<UserGetDTO>();

            return Ok(response);
        }


    }
}
