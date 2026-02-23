using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DTO
{
    public class CategoryDTO
    {
        public int IdCategory { get; set; }

        public string NameCategory { get; set; } = null!;

        public bool? IsEnable { get; set; }
    }
}
