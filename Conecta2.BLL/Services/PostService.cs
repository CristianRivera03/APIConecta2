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

namespace Conecta2.BLL.Services
{
    public class PostService : IPostService    
    {
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IGenericRepository<Post> postRepository, IMapper mapper, ILogger<PostService> logger)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<PostDTO>> GetAllAsync()
        {
            try
            {
                var queryPost = await _postRepository.Query();
                var listPost = queryPost
                    .Include(p => p.IdCategoryNavigation)
                    .Include(p => p.IdUserNavigation).ToList();

                return _mapper.Map<List<PostDTO>>(listPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting the post");
                throw;
            }
        }

        public async Task<PostCreateDTO> Create(PostCreateDTO model)
        {
            try
            {
                // Mapeamos de DTO a Modelo primero
                var postToCreate = _mapper.Map<Post>(model);

                // Asignamos datos de auditoría ANTES de guardar
                postToCreate.PublishedAt = DateTime.UtcNow;
                postToCreate.IsDeleted = false;
                // Si el ID es Guid y no se genera en DB, asegúrate de que tenga uno
                if (postToCreate.IdPost == Guid.Empty) postToCreate.IdPost = Guid.NewGuid();

                // 3. Enviamos el modelo ya preparado al repositorio
                var postCreated = await _postRepository.Create(postToCreate);

                if (postCreated.IdPost == Guid.Empty)
                    throw new TaskCanceledException("El post no se pudo crear");

                return _mapper.Map<PostCreateDTO>(postCreated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating the post");
                throw;
            }
        }
        public async Task<bool> Update(PostDTO model)
        {
            try
            {
                var postModel = _mapper.Map<Post>(model);
                var postFound = await _postRepository.Get(p => p.IdPost == postModel.IdPost);

                if (postFound == null)
                    throw new TaskCanceledException("No existe el post");

                postFound.IdCategory = postModel.IdCategory;
                postFound.TitlePost = postModel.TitlePost;
                postFound.ContentPost = postModel.ContentPost;
                //date updated
                postFound.UpdatedAt = DateTime.UtcNow;

                bool response = await _postRepository.Update(postFound);

                if (!response)
                    throw new TaskCanceledException("No se ha podido actualizar el post");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating the post");
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var postFound = await _postRepository.Get(p => p.IdPost == id);

                if(postFound == null)
                    throw new TaskCanceledException("El post no existe");

                postFound.IsDeleted = true;
                postFound.DeleteAt = DateTime.UtcNow;

                bool response = await _postRepository.SoftDelete(postFound);

                if (!response)
                    throw new TaskCanceledException("El post no se pudo eliminar ");
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting post with ID: {UserId}", id);
                throw;
            }
        }
    }
}
