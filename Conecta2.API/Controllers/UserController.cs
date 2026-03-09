using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Conecta2.BLL.Services.Contract;
using Conecta2.DTO;
using Conecta2.API.Utility;

namespace Conecta2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Endpoint para listar usuarios
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var rsp = new Response<List<UserDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _userService.GetAllAsync();
            }catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        //Endpoint para hacer login  
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var rsp = new Response<SessionDTO>();
            try
            {
                var session = await _userService.CheckCredentials(login.Email, login.Password);
                rsp.status = true;
                rsp.value = session;

                return Ok(rsp); //200
            }
            catch (TaskCanceledException ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

                return Unauthorized(rsp); //401 fallos de auth
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = "Ocurrio un error inesperado";

                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }

        }


        //Endpoint para crear usuario
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UserDTO user)
        {
            var rsp = new Response<UserDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _userService.Create(user);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}