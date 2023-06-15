using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class ProducteCarret
{
    public int IdProducteCarret { get; set; }

    public int? IdCarret { get; set; }

    public int? IdTallaProducte { get; set; }

    public int? Quantitat { get; set; }

    public virtual Carret IdCarretNavigation { get; set; } = null!;

    public virtual TallasProducte IdTallasProducteNavigation { get; set; } = null!;

    public ProducteCarret()
    {
    }

    public ProducteCarret(int? idCarret, int? idTallaProducte, int? quantitat)
    {
        IdCarret = idCarret;
        IdTallaProducte = idTallaProducte;
        Quantitat = quantitat;
    }
}
