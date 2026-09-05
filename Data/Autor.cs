using System.ComponentModel.DataAnnotations;

namespace Čtenářský_deník.Data
{
    public class Autor
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [MaxLength(20), Display(Name = "Jméno")]
        public string Jmeno { get; set; }

        [MaxLength(30), Required, Display(Name = "Příjmení")]
        public string Prijmeni { get; set; }

        [Display(Name = "Rok narození")]
        public int? RokNarozeni {  get; set; }

        [Display(Name = "Rok úmrtí")]
        public int? RokUmrti {  get; set; }
        [DataType(DataType.MultilineText), Display(Name = "O autorovi")]
        public string? Bio {  get; set; }

        public ICollection<Kniha> Knihy {  get; set; }


    }
}
