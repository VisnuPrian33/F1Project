using System;
using System.Collections.Generic;

namespace F1Project.Models;

public partial class WorldChampion
{
    public int ChampionId { get; set; }

    public int? DriverNumber { get; set; }

    public int? WinningYear { get; set; }

    public string? DriverName { get; set; }

    public string? TeamName { get; set; }

    public int? Points { get; set; }

    public virtual F1driver? DriverNumberNavigation { get; set; }
}
