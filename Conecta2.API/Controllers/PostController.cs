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

    }
}
