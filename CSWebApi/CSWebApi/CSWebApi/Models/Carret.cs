using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Carret
{
    public int IdCarret { get; set; }

    public double? PreuTotal { get; set; }

    public int? IdUsuari { get; set; }

    public virtual Usuari? IdUsuariNavigation { get; set; }
    public virtual ICollection<ProducteCarret> ProducteCarrets { get; set; } = new List<ProducteCarret>();

    public Carret()
    {
    }

    public Carret(double? preuTotal, int? idUsuari)
    {
        PreuTotal = preuTotal;
        IdUsuari = idUsuari;
    }
}
