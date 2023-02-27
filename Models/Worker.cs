using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Worker
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public int WorkExperience { get; set; }

    public string Information { get; set; } = null!;

    public int CarServiceId { get; set; }

    public virtual CarService CarService { get; set; } = null!;

    public virtual ICollection<Service> Services { get; } = new List<Service>();
}
