using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Color
{
    public int IdColor { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<ColorsProducte> ColorsProductes { get; set; } = new List<ColorsProducte>();

    public virtual ICollection<Preference> Preferences { get; set; } = new List<Preference>();

    public Color()
    {
    }

    public Color(string? nom)
    {
        Nom = nom;
    }
}
