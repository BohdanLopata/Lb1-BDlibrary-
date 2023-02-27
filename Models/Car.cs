using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Car
{
    public int Id { get; set; }

    public string Breakage { get; set; } = null!;

    public string? Information { get; set; }

    public int OwnerId { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Owner Owner { get; set; } = null!;

    public virtual ICollection<Service> Services { get; } = new List<Service>();
}
