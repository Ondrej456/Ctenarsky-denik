using System.ComponentModel.DataAnnotations;

namespace Čtenářský_deník.Data
{
    public class ObdobiMaturita
    {
        [Key]
        public int Id { get; set; }
        public string Nazev { get; set; }

        public List<Kniha> Knihy { get; set; }
    }
}