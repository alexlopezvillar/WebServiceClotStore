using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CSWebApi.Models;

public partial class Producte
{
    public int IdProducte { get; set; }

    public string? Nom { get; set; }

    public double? Preu { get; set; }

    public string? Imatge { get; set; }

    public int? Categoria { get; set; }

    public int? Prenda { get; set; }

    public int? Marca { get; set; }

    public int? Temporada { get; set; }

    public int? Estil { get; set; }

    public virtual Categoria? CategoriaNavigation { get; set; }

    public virtual ICollection<ColorsProducte> ColorsProductes { get; set; } = new List<ColorsProducte>();

    public virtual Estil? EstilNavigation { get; set; }

    public virtual Marca? MarcaNavigation { get; set; }

    public virtual Prenda? PrendaNavigation { get; set; }

    public virtual ICollection<TallasProducte> TallasProductes { get; set; } = new List<TallasProducte>();

    public virtual Temporada? TemporadaNavigation { get; set; }
    
    public Producte()
    {
    }

    public Producte(string? nom, double? preu, string? imatge, int? categoria, int? prenda, int? marca, int? temporada, int? estil)
    {
        Nom = nom;
        Preu = preu;
        Imatge = imatge;
        Categoria = categoria;
        Prenda = prenda;
        Marca = marca;
        Temporada = temporada;
        Estil = estil;
    }
}
