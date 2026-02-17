using System;
using System.Collections.Generic;

namespace Conecta2.Model;

public partial class Post
{
    public Guid IdPost { get; set; }

    public Guid IdUser { get; set; }

    public int? IdCategory { get; set; }

    public string TitlePost { get; set; } = null!;

    public string ContentPost { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
