using MIUBlog.DataAccess.Abstract;
using MIUBlog.Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Text;

namespace MIUBlog.DataAccess.Concrete.EntityFramework
{
    public class EfCommentDal : EfEntityRepositoryBase<Comment>, ICommentDal
    {
        public EfCommentDal(MIUBlogDbContext context) : base(context)
        {
        }
    }
}
