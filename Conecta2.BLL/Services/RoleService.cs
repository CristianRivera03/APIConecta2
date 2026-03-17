using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Conecta2.BLL.Services.Contract;
using Conecta2.DAL.Repositories.Contract;
using Conecta2.DTO;
using Conecta2.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//para rollback
using System.Transactions;




namespace Conecta2.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<Rolemodule> _roleModuleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public RoleService(IGenericRepository<Role> roleRepository, IGenericRepository<Rolemodule> roleModuleRepository, IMapper mapper, ILogger<CategoryService> logger)
        {
            _roleRepository = roleRepository;
            _roleModuleRepository = roleModuleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RoleDTO> CreateRole(CreateRoleDTO request)
        {
            try
            {
                var roleModel = _mapper.Map<Role>(request);

                var roleCreated = await _roleRepository.Create(roleModel);

                return _mapper.Map<RoleDTO>(roleCreated);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<RoleDTO>> GetAll()
        {
            try
            {
                var query = await _roleRepository.Query();
                return _mapper.Map<List<RoleDTO>>(query.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los roles" + ex);
                throw;
            }
        }

        public async Task<List<int>> GetAssignedModuleIds(int roleId)
        {
            try
            {
                var query = await _roleModuleRepository.Query();

                var activeModuleIds = await query
                    .Where(rm => rm.Roleid == roleId)
                    .Select(rm => rm.Moduleid)
                    .ToListAsync();

                return activeModuleIds;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error al obtener los modulos");
                throw;
            }
        }

        public async Task<bool> UpdateRolePermissions(UpdatePermissionsDTO request)
        {
            try
            {

                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var queryOldPermissions = await _roleModuleRepository.Query();

                    //se buscan los viejos permisos
                    var oldPermissions = await queryOldPermissions
                        .Where(rm => rm.Roleid == request.RoleId)
                        .ToListAsync();


                    // se borran los viejos permisos
                    if (oldPermissions.Any())
                    {
                        await _roleModuleRepository.RemoveRange(oldPermissions);
                    }

                    // Se preparan los nuevos permisos
                    if (request.ModuleIds != null && request.ModuleIds.Any())
                    {
                        var newPermissions = request.ModuleIds.Select(moduleId => new Rolemodule
                        {
                            Roleid = request.RoleId,
                            Moduleid = moduleId,
                            Createdat = DateTime.Now
                        }).ToList();

                        await _roleModuleRepository.AddRange(newPermissions);
                    }

                    transaction.Complete();
                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar los permisos para el Rol {RoleId}", request.RoleId);
                throw;
            }

        }
    }
}
