using System.ComponentModel.DataAnnotations;

namespace Čtenářský_deník.Data
{
    public class Kniha
    {
        [Key]
        public int Id { get; set; }
        
        public string UserId { get; set; }

        [MaxLength(60), Required, Display(Name ="Název")]
        public string Nazev { get; set; }

        public string Druh { get; set; }

        [Display (Name ="Žánr")]
        public string Zanr { get; set; }
        [Display(Name ="Rok Vydání")]
        public int? RokVydani { get; set; }

        public string Popis { get; set; } 

        public int AutorId { get; set; }

        public Autor Autor { get; set; }

        public int? ObdobiMaturitaId { get; set; }   // ← nepovinné
        public ObdobiMaturita Obdobi { get; set; }  // ← nepovinné

        public List<KnihaImage> Images { get; set; } = new();

    }
}
