using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DTO
{
    public class PostDTO
    {
        public Guid IdPost { get; set; }

        public Guid IdUser { get; set; }

        //Nuevodato dato usuario creador post
        public string CompleteNameUser { get; set; }

        public int? IdCategory { get; set; }

        //Nuevodato dato usuario creador post
        public string NameCategory { get; set; }

        public string TitlePost { get; set; } = null!;

        public string ContentPost { get; set; } = null!;

        public bool? IsDeleted { get; set; }

        public DateTime? PublishedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeleteAt { get; set; }
    }
}
