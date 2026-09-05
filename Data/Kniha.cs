using System.ComponentModel.DataAnnotations;

namespace Čtenářský_deník.Data
{
    public class Kniha
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [MaxLength(60), Required]
        public string Nazev { get; set; }

        public int? RokVydani { get; set; }

        public string Popis { get; set; } 

        public int AutorId { get; set; }

        public Autor Autor { get; set; }


    }
}
