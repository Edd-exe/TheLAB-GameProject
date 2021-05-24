using MIUBlog.Entities.Abstract;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MIUBlog.Entities.Concrete
{
    public class Category : IEntity
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Categoriy adı boş gecilemez!..")]
        public string Name { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
