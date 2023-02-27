using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CarService> CarServices { get; } = new List<CarService>();
}
