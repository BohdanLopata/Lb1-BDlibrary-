using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string? Model { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; } = new List<Car>();
}
