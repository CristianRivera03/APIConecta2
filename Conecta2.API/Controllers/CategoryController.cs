using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Conecta2.BLL.Services.Contract;
using Conecta2.DTO;
using Conecta2.API.Utility;

namespace Conecta2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        //metodo para obtener
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var rsp = new Response<List<CategoryDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _categoryService.GetAllAsync();

            }catch(Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
