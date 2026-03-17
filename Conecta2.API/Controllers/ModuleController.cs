using Conecta2.BLL.Services.Contract;
using Conecta2.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

using Conecta2.API.Utility;

namespace Conecta2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {

        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }


        [HttpGet]
        [Route("Get")]

        public async Task<IActionResult> Get()
        {
            var rsp = new Response<List<ModuleDTO>>();

            try
            {
                var listaModulos = await _moduleService.GetAllAsync();

                if (listaModulos == null || listaModulos.Count == 0)
                {
                    rsp.status = false;
                    rsp.msg = "No se encontraron registros";
                    rsp.value = new List<ModuleDTO>();
                    return NotFound(rsp);

                }

                rsp.status = true;
                rsp.value = listaModulos;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = "Error en el sistema" + ex.Message;
                return StatusCode(500, rsp);
            }

        }
    }
}
