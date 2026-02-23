using System;
using System.Collections.Generic;
using System.Text;
using Conecta2.DTO;

namespace Conecta2.BLL.Services.Contract
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();
    }
}
