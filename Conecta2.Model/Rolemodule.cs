using System;
using System.Collections.Generic;

namespace Conecta2.Model;

public partial class Rolemodule
{
    public int Roleid { get; set; }

    public int Moduleid { get; set; }

    public DateTime? Createdat { get; set; }


    //Referencias

    public virtual Module Module { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
