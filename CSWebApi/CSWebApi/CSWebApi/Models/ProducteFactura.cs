namespace CSWebApi.Models
{
    public class ProducteFactura
    {
        public int? IdProducte { get; set; }
        public int? IdTallaProducte { get; set; }
        public string? Nom { get; set; }
        public string? Imatge { get; set; }
        public double? Preu { get; set; }
        public int? Quantitat { get; set; }

        public ProducteFactura(int idProducte, int? idTallaProducte, string? nom, string? imatge, double? preu, int? quantitat)
        {
            IdProducte = idProducte;
            IdTallaProducte = idTallaProducte;
            Nom = nom;
            Imatge = imatge;
            Preu = preu;
            Quantitat = quantitat;
        }

        public ProducteFactura()
        {
        }
    }
}
