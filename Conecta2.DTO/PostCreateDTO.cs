using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DTO
{
    public class PostCreateDTO
    {
        public Guid IdUser { get; set; }
        // La categoría que eligió
        public int IdCategory { get; set; }
        public string TitlePost { get; set; } = null!;
        public string ContentPost { get; set; } = null!;
    }
}
