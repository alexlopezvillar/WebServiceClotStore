using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Nom { get; set; }

    public string? Imatge { get; set; }

    public virtual ICollection<Preference> Preferences { get; set; } = new List<Preference>();

    public virtual ICollection<Producte> Productes { get; set; } = new List<Producte>();

    public Categoria()
    {
    }

    public Categoria(string? nom, string? imatge)
    {
        Nom = nom;
        Imatge = imatge;
    }
}
