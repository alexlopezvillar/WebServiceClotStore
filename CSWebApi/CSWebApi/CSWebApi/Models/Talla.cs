using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Talla
{
    public int IdTalla { get; set; }

    public string? Nom { get; set; }

    public virtual ICollection<TallasProducte> TallasProducte { get; set; } = new List<TallasProducte>();

    public Talla()
    {
    }

    public Talla(string? nom)
    {
        Nom = nom;
    }
}
