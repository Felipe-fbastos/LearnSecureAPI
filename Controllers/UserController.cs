using BCrypt.Net;
using LearnSecureAPI.Data;
using LearnSecureAPI.DTO;
using LearnSecureAPI.Model;
using LearnSecureAPI.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Claims; 

namespace LearnSecureAPI.Controllers
{
    // Exige autenticação todas as rotas (A não ser que ela seja AllowAnonymous)
    [Authorize]
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public AppDataContext _context { get; set; }
        private readonly TokenService _tokenService;

        public UserController(AppDataContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // Authorize(Roles ="Nome_da_Role") - Bloqueia acesso para role que não estejam especificadas
        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UserGetDTO>>> GetAll()
        {
            var user = await _context.User.ToListAsync();

            if (!user.Any())
            {
                return NotFound();
            }

            var response = user.Adapt<List<UserGetDTO>>();

            return Ok(response);

        }

        // AllowAnonymous - Libera acesso para usuários não cadastrados.
        [AllowAnonymous]
        [HttpGet("Single")]
        public async Task<ActionResult<UserGetDTO>> GetSingle()
        {
            // Pega informação dentro do Token
            // FindFirst - Busca uma Claim dentro do Token
            // ClaimTypes.NameIdentifier - Seria o identitificador único do usuárip (geralmente o id)
            // ?.Value - Se a claim existir pega o valor dela. Senão ele retorna null sem gerar NullReferenceException
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            // Busca o usuário com o id informado pelo token
            var user = await _context.User.FindAsync(int.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            var response = user.Adapt<UserGetDTO>();

            return Ok(response);
        }

        // AllowAnonymous - Libera acesso para usuários não cadastrados.
        [AllowAnonymous]
        [HttpPost("SingUp")]
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

        // AllowAnonymous - Libera acesso para usuários não cadastrados.
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginUserDTO>> Login(LoginUserDTO dto)
        {
            // Busco pelo email do usuário
            /*
                SELECT * FROM User
                WHERE Email = (ao passado pela dto) email@email.com
             
             */

            var user = await _context.User
                .FirstOrDefaultAsync(e => e.Email == dto.Email);

            // Verifico se ele não veio null
            if (user == null)
                return Unauthorized("Invalid email or password");

            // Veirifico se a senha digitada corresponde ao hash salvo
            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            // Valido se a senha digitada no Json bate com a do banco
            if (!validPassword)
                return BadRequest("Invalid email or password");

            // Retorno uma resposta amigavel

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                token
            });
        }


    }
}



