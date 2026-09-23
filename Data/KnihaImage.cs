using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Čtenářský_deník.Data
{
    public class KnihaImage
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Kniha Kniha { get; set; }

        public string ImagePath { get; set; }
    }
}
