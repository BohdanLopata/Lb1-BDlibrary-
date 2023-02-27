using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime Deadline { get; set; }

    public string? Information { get; set; }

    public int WorkerId { get; set; }

    public int CarServiceId { get; set; }

    public int CarId { get; set; }

    public decimal Price { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual CarService CarService { get; set; } = null!;

    public virtual Worker Worker { get; set; } = null!;
}
