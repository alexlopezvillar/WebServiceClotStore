using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Prenda
{
    public int IdPrenda { get; set; }

    public string? Nom { get; set; }

    public string? Imatge { get; set; }

    public virtual ICollection<Producte> Productes { get; set; } = new List<Producte>();

    public Prenda()
    {
    }

    public Prenda(string? nom, string? imatge)
    {
        Nom = nom;
        Imatge = imatge;
    }
}
