using System;
using System.Collections.Generic;

namespace LibraryWebApplication.Models;

public partial class Owner
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Car> Cars { get; } = new List<Car>();
}
