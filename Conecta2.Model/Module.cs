using System;
using System.Collections.Generic;

namespace Conecta2.Model;

public partial class Module
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string? Icon { get; set; }

    public bool? Isactive { get; set; }

    public virtual ICollection<Rolemodule> Rolemodules { get; set; } = new List<Rolemodule>();
}
