using System;
using System.Collections.Generic;
using System.Text;

using Conecta2.DTO;

namespace Conecta2.BLL.Services.Contract
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetAllAsync();
        Task<PostCreateDTO> Create(PostCreateDTO model);
        Task<bool> Update(PostDTO model);
        Task<bool> Delete(Guid id);

    }
}
