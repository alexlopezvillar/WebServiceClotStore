
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSWebApi.Models;

public partial class ColorsProducte
{
    public int IdColorProducte { get; set; }

    public int? IdProducte { get; set; }

    public int? IdColor { get; set; }
    [JsonIgnore]
    public virtual Color IdColorNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Producte IdProducteNavigation { get; set; } = null!;

    public ColorsProducte()
    {
    }

    public ColorsProducte(int idColor, int idProducte)
    {
        IdColor = idColor;
        IdProducte = idProducte;
    }
}
