using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Usuari
{
    public int IdUsuari { get; set; }

    public string? Nom { get; set; }

    public string? Contra { get; set; }

    public string? Tipus { get; set; }

    public virtual ICollection<Carret> Carrets { get; set; } = new List<Carret>();

    public virtual ICollection<Preference> Preferences { get; set; } = new List<Preference>();

    public Usuari()
    {
    }

    public Usuari(string? nom, string? contra, string? tipus)
    {
        Nom = nom;
        Contra = contra;
        Tipus = tipus;
    }
}
