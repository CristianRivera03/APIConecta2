using System;
using System.Collections.Generic;
using System.Text;
using Conecta2.DTO;


namespace Conecta2.BLL.Services.Contract
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAll();

        Task<List<int>> GetAssignedModuleIds(int roleId);

        Task<bool> UpdateRolePermissions(UpdatePermissionsDTO request);

        Task<RoleDTO> CreateRole(CreateRoleDTO request);
    }
}
