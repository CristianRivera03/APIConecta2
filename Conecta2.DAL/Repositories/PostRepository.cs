using Conecta2.DAL.DBContext;
using Conecta2.DAL.Repositories.Contract;
using Conecta2.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DAL.Repositories
{
    public class PostRepository: GenericRepository<Post>, IPostRepository
    {
        private readonly Conecta2DbContext _dbcontext;

        public PostRepository(Conecta2DbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<Post> CreatePost(Post model)
        {
            throw new NotImplementedException();
        }
    }
}
