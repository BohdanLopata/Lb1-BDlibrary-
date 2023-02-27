using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class CarService
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Information { get; set; } = null!;

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Service> Services { get; } = new List<Service>();

    public virtual ICollection<Worker> Workers { get; } = new List<Worker>();
}
