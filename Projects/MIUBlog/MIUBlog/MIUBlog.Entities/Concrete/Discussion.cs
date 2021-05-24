using MIUBlog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MIUBlog.Entities.Concrete
{
    public class Discussion:IEntity

    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [Required(ErrorMessage = "Başlık alanı boş geçilemez!..")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Açıklama alanı zorunludur boş geçilemez")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Gövde alanı zorunludur boş geçilemez")]
        public string Body { get; set; }

        public string Image { get; set; }
    

        public DateTime Date { get; set; }

        public bool isApproved { get; set; }


        [Required(ErrorMessage = "Kategoriy Zorunlu Alandır Boş GEçilemez!..")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
