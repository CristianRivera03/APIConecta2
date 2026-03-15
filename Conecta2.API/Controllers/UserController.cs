using Conecta2.API.Utility;
using Conecta2.BLL.Services;
using Conecta2.BLL.Services.Contract;
using Conecta2.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        //Endpoint para eliminar usuario
        [HttpDelete]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rsp = new Response<bool>();
            try
            {
                bool result = await _userService.Delete(id);

                if (result)
                {
                    rsp.status = true;
                    rsp.value = result;
                    rsp.msg = "usuario eliminado con exito";

                    return Ok(rsp);
                }
                else
                {
                    rsp.status = false;
                    rsp.msg = "No se pudo eliminar";
                    return BadRequest(rsp);
                }
            }
            catch (TaskCanceledException ex)
            {
                // Si no se encuentra el servicio
                rsp.status = false;
                rsp.msg = ex.Message;
                return NotFound(rsp); //  HTTP 404 si el post no existe
            }
            catch (Exception ex)
            {
                //  errores del servidor
                rsp.status = false;
                rsp.msg = "Ocurrió un error interno en el servidor.";
                return StatusCode(StatusCodes.Status500InternalServerError, rsp);
            }

        }



        //Endpoint para crear usuario
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO user)
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