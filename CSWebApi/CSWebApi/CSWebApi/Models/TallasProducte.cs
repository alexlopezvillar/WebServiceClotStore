using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class TallasProducte
{
    public int IdTallaProducte { get; set; }

    public int? IdTalla { get; set; }

    public int? IdProducte { get; set; }

    public int? existencies { get; set; }

    public virtual Talla IdTallaNavigation { get; set; } = null!;

    public virtual Producte IdProducteNavigation { get; set; } = null!;

    public virtual ICollection<ProducteCarret> ProducteCarrets { get; set; } = new List<ProducteCarret>();

    public TallasProducte()
    {
    }

    public TallasProducte(int IdTalla, int idProducte, int? existencies)
    {
        this.IdTalla = IdTalla;
        IdProducte = idProducte;
        this.existencies = existencies;
    }
}
