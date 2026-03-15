using System;
using System.Collections.Generic;
using System.Text;

using Conecta2.DTO;

namespace Conecta2.BLL.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<SessionDTO> CheckCredentials(string email , string password);
        Task<UserDTO> Create(UserCreateDTO model);
        Task<bool> Update(UserDTO model);
        Task<bool> Delete(Guid id);

    }
}
