using System;
using System.Collections.Generic;

namespace Conecta2.Model;

public partial class Role
{
    public int IdRole { get; set; }

    public string NameRol { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
