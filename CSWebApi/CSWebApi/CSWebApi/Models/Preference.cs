using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Preference
{
    public int IdPreferences { get; set; }

    public int? Temporada { get; set; }

    public int? Estil { get; set; }

    public int? Categoria { get; set; }

    public int? IdUsuari { get; set; }

    public int? IdColor { get; set; }

    public virtual Categoria? CategoriaNavigation { get; set; }

    public virtual Estil? EstilNavigation { get; set; }

    public virtual Usuari? IdUsuariNavigation { get; set; }

    public virtual Temporada? TemporadaNavigation { get; set; }

    public virtual Color? IdColorNavigation { get; set; }

    public Preference()
    {
    }

    public Preference(int? temporada, int? estil, int? categoria, int? idUsuari, int? idColor)
    {
        Temporada = temporada;
        Estil = estil;
        Categoria = categoria;
        IdUsuari = idUsuari;
        IdColor = idColor;
    }
}
