using System;
using System.Collections.Generic;
using System.Text;

namespace Conecta2.DTO
{
    public class ModuleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Path { get; set; } = null!;

        public string? Icon { get; set; }
    }
}
