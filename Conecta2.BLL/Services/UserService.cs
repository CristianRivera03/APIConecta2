using System;
using System.Collections.Generic;
using System.Numerics;
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
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            try
            {
                var query = await _userRepository.Query();
                //join
                var listUsers = await query.Include(u => u.IdRoleNavigation).Where(u => u.IsActive ==true).ToListAsync();
                return _mapper.Map<List<UserDTO>>(listUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to get the users");
                throw;
            }
        }
        public async Task<SessionDTO> CheckCredentials(string email, string password)
        {

            try
            {
                //se envuelve el pack del query luego se le hacen los include y where correspondientes

                var queryUser =  await _userRepository
                    .Query(u => u.Email == email && u.IsActive == true);

                var userFound = await queryUser
                    .Include(u => u.IdRoleNavigation)
                    .ThenInclude(r => r.Rolemodules)
                    .ThenInclude(rm => rm.Module)
                    .FirstOrDefaultAsync();

                if (userFound == null || !SecurityHelper.VerifyPassword(password, userFound.PasswordHash))
                {
                    throw new UnauthorizedAccessException("El usuario no existe o la contraseña es incorrecta");
                }

      

                return _mapper.Map<SessionDTO>(userFound);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error to get the user");
                    throw;
                }

            //try
            //{
            //    var queryUser = await _userRepository.Query(
            //        u => u.Email == email &&
            //        u.PasswordHash == password);

            //    if (queryUser.FirstOrDefault() == null)
            //        throw new TaskCanceledException("El usuario no existe");

            //    return _mapper.Map<SessionDTO>(queryUser);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error to get the user");
            //    throw;
            //}
        }

        public async Task<UserDTO> Create(UserCreateDTO model)
        {
            try
            {
                var userModel = _mapper.Map<User>(model);
                //emcriptacion
                userModel.PasswordHash = SecurityHelper.HashPassword(model.Password);

                userModel.IdRole = 3;
                userModel.CreateAt = DateTime.UtcNow;
                userModel.IsActive = true;
                

                var userCreated = await _userRepository.Create(userModel);
                
                if (userCreated.IdUser == Guid.Empty)
                    
                    throw new TaskCanceledException("El usuario no se puedo crear ");

                return _mapper.Map<UserDTO>(userCreated);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new user");
                throw;
            }

            //try
            //{
            //    var userCreated = await _userRepository.Create(_mapper.Map<User>(model));
            //    if (userCreated.IdUser == Guid.Empty)
            //        throw new TaskCanceledException("El usuario no se puedo crear ");

            //    return _mapper.Map<UserDTO>(userCreated);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error creating a new user");
            //    throw;
            //}
        }

        public async Task<bool> Update(UserDTO model)
        {

            try
            {
            var userModel = _mapper.Map<User>(model);
            var userFound = await _userRepository.Get(u => u.IdUser == userModel.IdUser);

            if (userFound == null)
                throw new TaskCanceledException("El usuario no existe");

            userFound.NameUser = userModel.NameUser;
            userFound.LastnameUser = userModel.LastnameUser;
            userFound.Username = userModel.Username;

                bool response = await _userRepository.Update(userFound);

                if (!response)
                    throw new TaskCanceledException("El usuario no se pudo actualizar");
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating the user with ID: {UserId}", model.IdUser);
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var userFound = await _userRepository.Get(u => u.IdUser == id);

                if (userFound == null)
                    throw new TaskCanceledException("El usuario no existe");

                userFound.IsActive = false;
                userFound.DeleteAt = DateTime.UtcNow;

                bool response = await _userRepository.SoftDelete(userFound);

                if (!response)
                    throw new TaskCanceledException("El usuario no se pudo eliminar ");
                return response;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID: {UserId}", id);
                throw;
            }
        }
    }
}
