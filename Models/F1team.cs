using System;
using System.Collections.Generic;

namespace F1Project.Models;

public partial class F1team
{
    public int TeamId { get; set; }

    public string? TeamName { get; set; }

    public string? BaseCountry { get; set; }

    public string? TeamPrincipal { get; set; }

    public int? FoundedYear { get; set; }

    public virtual ICollection<F1driver> F1drivers { get; set; } = new List<F1driver>();
}
