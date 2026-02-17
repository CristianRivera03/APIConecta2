using System;
using System.Collections.Generic;

namespace Conecta2.Model;

public partial class User
{
    public Guid IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? NameUser { get; set; }

    public string? LastnameUser { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
