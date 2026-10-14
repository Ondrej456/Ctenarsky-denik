using System.Runtime.ConstrainedExecution;

namespace Čtenářský_deník.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext Db)
        {
            if (!Db.Autori.Any(a => string.IsNullOrEmpty(a.UserId)))
            {
                Db.Autori.AddRange(
                    new Autor { Jmeno = "Nikolaj Vasilijevič", Prijmeni = "Gogol", RokNarozeni = 1809, RokUmrti = 1852, Bio = "Základní informace\r\nruský spisovatel, dramatik a satirik\r\n\r\njeden z nejvýznamnějších autorů ruského realismu 19. století\r\n\r\nnarodil se na Ukrajině (tehdy součást Ruského impéria)\r\n\r\npsal rusky a je považován za zakladatele moderní ruské prózy\r\n\r\nTvorba a význam\r\nzaměřoval se na kritiku společnosti, úřednictva, pokrytectví a lidských slabostí\r\n\r\njeho díla jsou plná satiry, ironie, grotesky a karikatury\r\n\r\nčasto ukazuje, jak se lidé nechají ovlivnit strachem, hloupostí nebo touhou po moci\r\n\r\numěl vystihnout atmosféru maloměsta a typické lidské charaktery", ImagePath = "/uploads/autor/52e3937f-8bb7-4764-8498-e6a89504dafa.jpg" },
                    new Autor { Jmeno = "Karel", Prijmeni = "Čapek", RokNarozeni = 1890, RokUmrti = 1938, Bio = "byl český spisovatel, intelektuál, novinář, dramatik, překladatel a amatérský fotograf. Byl mladším bratrem malíře a spisovatele Josefa Čapka(1887–1945).", ImagePath = "/uploads/autor/8cc2d789-36ba-4d0a-8bc1-2bab4dbbbabc.jpg" },
                    new Autor { Jmeno = "Milan", Prijmeni = "Kundera", RokNarozeni = 1929, RokUmrti = 2023, Bio = "byl česko-francouzský spisovatel. Od roku 1975 žil ve Francii, v roce 1979 byl zbaven československého státního občanství, roku 1981 získal občanství francouzské a v roce 2019 mu bylo vráceno občanství české.", ImagePath = "/uploads/autor/22b65ab8-b9a1-4657-81bb-b7da0a27fd7d.jpg" },
                    new Autor { Jmeno = "Josef", Prijmeni = "Dobrovský", RokNarozeni = 1753, RokUmrti = 1829, Bio = "byl český kněz, filolog, historik a zakladatel vědecké bohemistiky a slavistiky. Svým mnohostranným dílem předznamenal i podnítil české národní obrození, třebaže sám k romantickým obrozencům nejen chronologicky, ale ani svým založením nepatřil.", ImagePath = "/uploads/autor/27f187e4-92aa-4859-a892-007c9a3d33bf.jpg" }
                );
                await Db.SaveChangesAsync();
            }
        }
    }
}
