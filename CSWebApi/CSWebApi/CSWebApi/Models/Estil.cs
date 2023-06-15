using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Estil
{
    public int IdEstil { get; set; }

    public string? Nom { get; set; }

    public string? Imatge { get; set; }

    public virtual ICollection<Preference> Preferences { get; set; } = new List<Preference>();

    public virtual ICollection<Producte> Productes { get; set; } = new List<Producte>();

    public Estil()
    {
    }

    public Estil(string? nom, string? imatge)
    {
        Nom = nom;
        Imatge = imatge;
    }
}
