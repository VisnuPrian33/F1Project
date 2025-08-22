using System;
using System.Collections.Generic;

namespace F1Project.Models;

public partial class F1driver
{
    public int DriverNumber { get; set; }

    public string? DriverName { get; set; }

    public string? Nationality { get; set; }

    public int? TeamId { get; set; }

    public string? TeamName { get; set; }

    public virtual F1team? Team { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<WorldChampion> WorldChampions { get; set; } = new List<WorldChampion>();
}
