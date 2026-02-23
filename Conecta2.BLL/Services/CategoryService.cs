using System;
using System.Collections.Generic;
using System.Text;


using AutoMapper;
using Conecta2.BLL.Services.Contract;
using Conecta2.DAL.Repositories.Contract;
using Conecta2.DTO;
using Conecta2.Model;
using Microsoft.Extensions.Logging;

namespace Conecta2.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IGenericRepository<Category> categoryRepository, 
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            try
            {
                var listCategories = await _categoryRepository.Query();
                return _mapper.Map<List<CategoryDTO>>(listCategories.ToList());

            }
            catch (Exception ex)
            {
                // _logger es una instancia de ILogger<CategoryService> que recibirías en el constructor
                _logger.LogError(ex, "Error to get the categories");
                throw;
            }
        }
    }
}
