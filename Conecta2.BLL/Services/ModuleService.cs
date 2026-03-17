using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Conecta2.BLL.Services.Contract;
using Conecta2.DAL.Repositories.Contract;
using Conecta2.DTO;
using Conecta2.Model;
using Conecta2.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Conecta2.BLL.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IGenericRepository<Module> _moduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IModuleService> _logger;

        public ModuleService(IGenericRepository<Module> moduleRepository, IMapper mapper, ILogger<IModuleService> logger)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<List<ModuleDTO>> GetAllAsync()
        {
            try 
            {
                var listModules = await _moduleRepository.Query();

                
                    return _mapper.Map<List<ModuleDTO>>(listModules.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting modules");
                throw;
            }
        }


    }
}
