using System;
using System.Collections.Generic;
using System.Text;

using Conecta2.Model;

namespace Conecta2.DAL.Repositories.Contract
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<Post> CreatePost(Post model);
    }
}
