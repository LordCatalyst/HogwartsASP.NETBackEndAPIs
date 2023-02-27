using System;
using System.Collections.Generic;

namespace HogwartsBackEndAPIs.Models;

public partial class WizardRequest
{
    public int WizardId { get; set; }

    public string? WizardName { get; set; }

    public string? WizardLastName { get; set; }

    public int? WizardMuggleId { get; set; }

    public int? WizardAge { get; set; }

    public int? HouseId { get; set; }

    public virtual House? House { get; set; }
}
