using Conecta2.API.Utility;
using Conecta2.BLL.Services;
using Conecta2.BLL.Services.Contract;
using Conecta2.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Conecta2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        //Endpoint para listar post
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var rsp = new Response<List<PostDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _postService.GetAllAsync();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }


        //Crear
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] PostCreateDTO post)
        {
            var rsp = new Response<PostCreateDTO>();
            try
            {
                if(post == null)
                {
                    rsp.status = false;
                    rsp.msg = "los datos son requeridos";
                    return BadRequest(rsp);
                }
                else
                {
                   var session = await _postService.Create(post);
                    rsp.status = true;
                    rsp.value = session;
                    return Ok(rsp); 

                }
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
                return Ok(rsp);  
            }

        }


        //Eliminar
        [HttpDelete]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rsp = new Response<bool>();
            try
            {
                bool result = await _postService.Delete(id);

                if (result)
                {
                    rsp.status = true;
                    rsp.value = result;
                    rsp.msg = "Publicacion eliminada con exito";

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

    }
}
