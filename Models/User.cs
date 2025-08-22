using System;
using System.Collections.Generic;

namespace F1Project.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual F1driver UserNavigation { get; set; } = null!;
}
