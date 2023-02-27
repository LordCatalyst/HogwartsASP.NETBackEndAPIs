using System;
using System.Collections.Generic;

namespace HogwartsBackEndAPIs.Models;

public partial class House
{
    public int HouseId { get; set; }

    public string? HouseName { get; set; }

    public virtual ICollection<WizardRequest> WizardRequests { get; } = new List<WizardRequest>();
}
