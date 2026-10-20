using System.Runtime.ConstrainedExecution;
using static Azure.Core.HttpHeader;
using Microsoft.EntityFrameworkCore;

namespace Čtenářský_deník.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext Db)
        {
            // při prvním spuštění se přidá pár autorů a knih na ukázku
            if (!await Db.Autori.AnyAsync())
            {
                Db.Autori.AddRange(
                    new Autor { Jmeno = "Nikolaj Vasilijevič", Prijmeni = "Gogol", RokNarozeni = 1809, RokUmrti = 1852, Bio = "Základní informace\r\nruský spisovatel, dramatik a satirik\r\n\r\njeden z nejvýznamnějších autorů ruského realismu 19. století\r\n\r\nnarodil se na Ukrajině (tehdy součást Ruského impéria)\r\n\r\npsal rusky a je považován za zakladatele moderní ruské prózy\r\n\r\nTvorba a význam\r\nzaměřoval se na kritiku společnosti, úřednictva, pokrytectví a lidských slabostí\r\n\r\njeho díla jsou plná satiry, ironie, grotesky a karikatury\r\n\r\nčasto ukazuje, jak se lidé nechají ovlivnit strachem, hloupostí nebo touhou po moci\r\n\r\numěl vystihnout atmosféru maloměsta a typické lidské charaktery", ImagePath = "/uploads/autor/52e3937f-8bb7-4764-8498-e6a89504dafa.jpg" },
                    new Autor { Jmeno = "Karel", Prijmeni = "Čapek", RokNarozeni = 1890, RokUmrti = 1938, Bio = "byl český spisovatel, intelektuál, novinář, dramatik, překladatel a amatérský fotograf. Byl mladším bratrem malíře a spisovatele Josefa Čapka(1887–1945).", ImagePath = "/uploads/autor/8cc2d789-36ba-4d0a-8bc1-2bab4dbbbabc.jpg" },
                    new Autor { Jmeno = "Milan", Prijmeni = "Kundera", RokNarozeni = 1929, RokUmrti = 2023, Bio = "byl česko-francouzský spisovatel. Od roku 1975 žil ve Francii, v roce 1979 byl zbaven československého státního občanství, roku 1981 získal občanství francouzské a v roce 2019 mu bylo vráceno občanství české.", ImagePath = "/uploads/autor/22b65ab8-b9a1-4657-81bb-b7da0a27fd7d.jpg" },
                    new Autor { Jmeno = "Josef", Prijmeni = "Dobrovský", RokNarozeni = 1753, RokUmrti = 1829, Bio = "byl český kněz, filolog, historik a zakladatel vědecké bohemistiky a slavistiky. Svým mnohostranným dílem předznamenal i podnítil české národní obrození, třebaže sám k romantickým obrozencům nejen chronologicky, ale ani svým založením nepatřil.", ImagePath = "/uploads/autor/27f187e4-92aa-4859-a892-007c9a3d33bf.jpg" }
                );                      
                Db.Knihy.AddRange(
                    new Kniha { Nazev = "Revizor", RokVydani = 1833, AutorId = 1, Zanr = "Komedie",Druh = "Drama", Popis = "Literární směr: ruský realismus, satira\r\n\r\nTéma: kritika úplatkářství, korupce, hlouposti a pokrytectví úředníků\r\n\r\n1) Stručný obsah díla (děj)\r\nDo malého ruského města přijede mladý úředník Chlestakov. Místní představitelé ho omylem považují za tajného státního revizora, který má provést kontrolu. Místní představitelé nemají čisté svědomí, proto se mu snaží zalíbit, dávají mu úplatky a odhalují vlastní nepoctivost. Chlestakov situace využívá a hraje svou roli. Nakonec odjede a obyvatelé zjistí, že byli podvedeni. V závěru přichází zpráva, že skutečný revizor právě dorazil.\r\n\r\n2) Hlavní postavy\r\nIvan Alexandrovič Chlestakov\r\nmladý, lehkomyslný, nezodpovědný, rád se chlubí\r\nnení zlý, spíš hloupý a zbabělý\r\nvyužije situaci, když ho považují za revizora\r\ntypická postava „antihrdiny“\r\n\r\nMěstský hejtman (Anton Antonovič Skvoznik-Dmuchanovskij)\r\nhlavní představitel korupce\r\nbojí se odhalení\r\nsnaží se Chlestakova uplatit\r\npokrytec, který se tváří jako čestný člověk\r\n\r\nÚředníci města\r\nsoudce, poštmistr, školní inspektor, lékař\r\nkaždý má své chyby a strach z odhalení\r\nspolečně tvoří obraz zkorumpované společnosti\r\n\r\n3) Témata a motivy\r\nKorupce a úplatkářství – všichni úředníci mají máslo na hlavě\r\nPokrytectví – tváří se jako čestní, ale jsou zkorumpovaní\r\nHloupost a naivita – město naletí obyčejnému podvodníkovi\r\nSatira společnosti – Gogol kritizuje celou státní správu\r\nStrach z autority – všichni se bojí vyšší moci\r\n\r\n4) Kontext a význam díla\r\nGogol kritizuje ruskou státní správu 19. století\r\ndílo bylo kontroverzní — úředníci se cítili dotčeni\r\ndodnes patří mezi nejznámější satirické komedie\r\nukazuje, že lidská hloupost a korupce jsou nadčasové\r\n\r\n5) Možné otázky u maturity (a jak odpovědět)\r\nProč je Revizor komedie?\r\nProtože je založený na omylu, karikatuře postav a komických situacích.\r\nCo Gogol kritizuje?\r\nKorupci, pokrytectví, strach z autority a hloupost úředníků.\r\nJaký je Chlestakov?\r\nNaivní, zbabělý, rád se chlubí, není to skutečný podvodník — spíš využije situaci.\r\nJak končí dílo?\r\nSkutečný revizor přijede až po odjezdu Chlestakova → úředníci pochopí, že byli podvedeni.\r\nCo můžeš říct navíc\r\nGogol nepoukazuje jen na jednotlivé špatné úředníky, ale kritizuje celý systém státní správy."},
                    new Kniha { Nazev = "R.U.R.", RokVydani = 1920, AutorId = 2, Zanr = "Komedie",Druh = "Drama", Popis = "R.U.R. (zkratka z Rossumovi univerzální roboti) je vědeckofantastické drama o vstupní komedii a třech dějstvích Karla Čapka z roku 1920.\r\n\r\nAutor v příběhu varuje před případnými negativními vlivy techniky na lidstvo. Je ovlivněn svým zájmem o techniku, ale také obavami o budoucnost lidstva (destrukce za první světové války).\r\n\r\nDěj\r\n\r\nScéna z prvního dějství. Čapek ji popsal jako Helenin salón s tapetovými dveřmi nalevo do hudebního salónu a dveřmi vpravo do ložnice Heleny. V centru jsou okna k moři a přístavu. Toaletní zrcadlo, psací stůl, komoda, pohovka, křesla. Celý salón má moderní, ryze ženský ráz.\r\nÚvodní dějství se odehrává v blíže nespecifikované budoucnosti na Rossumově ostrově výrobny robotů, kde je celá výroba prováděna roboty. Připluje za nimi pěkná dcera prezidenta Helena Gloryová, s cílem roboty zrovnoprávnit, a do které se všichni zamilují. Postupně jí vyprávějí příběh vynálezu prvních robotů starého Rossuma a jeho synovce. Helena se poté vdá za ředitele Harryho Domina.\r\n\r\nPříběh dále pokračuje po deseti letech, kdy už jsou roboti široce rozšířeni. Heleně je smutno z toho, že roboti, ač velmi podobní lidem, nemohou mít city jako lidé, a tak přiměje Dr. Galla, aby začal experimentovat s jejich „duší“. Jejich původním cílem bylo vytvořit pro člověka ráj na zemi, aby již nikdy nemuseli lidé umírat hlady a měli všeho dostatek. Jenže upadly mravy, lidé zlenivěli a začali roboty původně určené k práci používat jako vojáky pro své války.\r\n\r\nNěkolik robotů se vzbouřilo, ustanovilo Ústřední výbor robotů a vyhlásilo válku lidstvu, jež drtivě porazilo a nenechalo nikoho přežít. Dorazili až na výrobní ostrov a pozabíjeli všechny až na Alquista, jelikož on ještě pracoval rukama (což ho uklidňovalo; věřil, že je práce smyslem života). Jenže roboti se začali opotřebovávat a umírali. Chtěli po něm, aby objevil původní Rossumův výrobní postup, ten však spálila Helena předtím, než ji roboti zabili.\r\n\r\nJedné noci Alquista vzbudili robot Primus a robotka Helena. Když chtěl jednoho z nich pitvat pro výzkum, druhý se nabízel místo něj – vůbec se nechovali jako roboti (snažili se navzájem chránit; záleželo jednomu na druhém). Alquist zjistil, že se mají rádi, a tak je poslal pryč, aby udrželi život a začali s láskou znovu jako Adam a Eva.\r\n\r\nZávěr je symbolickým odkazem k bibli, z níž Alquist cituje." },
                    new Kniha { Nazev = "Žert", RokVydani = 1965, AutorId = 3, Zanr = "Román",Druh = "Próza", Popis = "Hlavní postavou je Ludvík Jahn, v raných padesátých letech jako student horlivý komunista, který byl kvůli žertovné pohlednici (s obsahem „Ať žije Trockij“), poslané spolužačce Markétě na socialistické školení, vyhozen z vysoké školy i ze strany. Ludvík se chce po deseti letech pomstít bývalému spolužákovi Zemánkovi, jelikož právě kvůli Zemánkovi Ludvíka vyhodili ze školy i ze strany. Ludvík se Zemánkovi mstí tak, že svede jeho ženu Helenu. Zjistí však, že Zemánkovi spolu již stejně nežijí a Zemánek má nyní mladou a krásnou přítelkyni, takže Ludvíkova msta byla ubohá. Ludvík pak ve zlosti Heleně přizná pravdu, nemiluje ji, pouze ji využil. Helena se kvůli tomu chce zabít, jenže místo omamných pilulek omylem spolyká celou tubu projímacích prášků." },
                    new Kniha { Nazev = "Nesmrtelnost", RokVydani = 1990, AutorId = 3, Zanr = "Román",Druh = "Próza", Popis = "Charakteristika díla\r\nRomán je rozdělen do sedmi kapitol. Na rozdíl od většiny Kunderových starších próz je určen spíše pro francouzské čtenáře. Nevystupují v něm žádní Češi, neobjevuje se motiv emigrace a nejsou reflektovány autorovy zkušenosti z komunistického Československa. Obsahuje jen několik ne příliš důležitých českých reminiscencí; to jej spojuje s Kunderovým dalším dílem, románem L'Identité (česky Totožnost).\r\n\r\nBěhem vyprávění se v několika kapitolách mění vypravěč. V některých kapitolách je spíše heterodiegetický (tj. není součástí vyprávěného světa, vypráví příběh někoho jiného, není protagonistou), ale v některých kapitolách je naopak čistě autodiegetický (tj. používá ich-formu, je součástí děje). Tento způsob užívání vypravěče není v Kunderově díle novým jevem (objevuje se např. v Knize smíchu a zapomnění), zde však nabývá nebývalého významu. Román stojí na jakési hranici fikce a skutečnosti, kdy nám autor dokládá, že děj románu je pouhý jeho výmysl a postavy vznikly ze spisovatelových metafor, ale když do příběhu vplétá vlastní vzpomínky na setkání se svým přítelem Avenariem, můžeme mít pocit, že vypravěčem je sám autor. Vypravěč však nikdy není ztotožnitelný se samotným Kunderou, autorem románu.[1]\r\n\r\nRomán se jako řady jiných Kunderových děl vyznačuje typickým filozoficko-estetickým přesahem. Obsahuje úvahy o umění a jeho významu pro současného člověka. Pro Paula, jednu z románových postav, je četba biografií Ernesta Hemingwaye důležitější než jeho vlastní literární práce atp. Kundera otevírá téma tzv. „imagologie“, disciplíny, jež se zabývá vytvářením obrazů v reklamě, v literatuře a v denním životě, a která nahradila různé dřívější ideologie, jež již na lidi přestaly působit. Ke srovnání ideologie a imagologie chci dodat ještě toto: Ideologie byly jako obrovská kola v zákulisí, která se točila a uváděla do pohybu války, revoluce, reformy. Imagologická kola se otáčejí a na historii to nemá žádný vliv. Ideologie válčily jedna s druhou a každá z nich byla s to naplnit svým myšlením celou epochu. Imagologie organizuje sama mírumilovné střídání svých systémů ve svižném rytmu sezón. Řečeno Paulovými slovy: Ideologie patřily historii, kdežto vláda imagologie začíná tam, kde historie končí. (Milan Kundera: Nesmrtelnost, s. 119). Imagologové jsou lidé, kteří nám radí, jak se chovat, co je právě „in“, vhodné v tomto období.\r\n\r\nDalším předmětem Kunderových úvah je otázka přítomnosti zemřelého člověka v paměti živých, tedy jeho posmrtného života mezi živými (motiv Bettiny von Arnim, Johanna Wolfganga von Goethe a Laury). Tato duchovní linie dala knize její název. Nesmrtelností je tedy v tomto díle myšlen pozůstatek obrazu zemřelého v mysli živých." }
                );
                await Db.SaveChangesAsync();

                string kniha1 = "Revizor";
                string kniha2 = "R.U.R.";
                string kniha3 = "Nesmrtelnost";

                for (int i = 0; i < 3; i++)
                {
                    string kniha = "";
                    if (i == 0) { kniha = kniha1; }
                    if (i == 1) { kniha = kniha2; }
                    if (i == 2) { kniha = kniha3; }

                    var druhaKniha = await Db.Knihy
                            .Where(k => k.Nazev == kniha)
                            .Skip(1)
                            .FirstOrDefaultAsync();

                    if (druhaKniha != null)
                    {
                        int id = druhaKniha.Id;

                        if (kniha == "Revizor")
                        {
                            Db.KnihaImages.AddRange(
                            new KnihaImage { BookId = id, ImagePath = "/uploads/knihy/c6587799-f1dc-4133-8705-f340734d8181.jpg" },
                            new KnihaImage { BookId = id, ImagePath = "/uploads/knihy/1cdb2db1-b86c-4e07-a1ac-da1de9fa08c4.jpg" }
                            );
                        }
                        if (kniha == "R.U.R.")
                        {
                            Db.KnihaImages.AddRange(
                            new KnihaImage { BookId = id, ImagePath = "/uploads/knihy/1cce1634-3254-42ea-9395-9f83a952a9f3.jpg" },
                            new KnihaImage { BookId = id, ImagePath = "/uploads/knihy/2d2c9e54-504a-4ed5-b97a-db4be7878f6f.jpg" }
                            );
                        }
                        if (kniha == "Nesmrtelnost")
                        {
                            Db.KnihaImages.AddRange(
                            new KnihaImage { BookId = id, ImagePath = "/uploads/knihy/d9aaec61-9a2e-4ba1-b19b-f826dde1f63e.jpg" }
                            );

                        }
                    }
                }
                await Db.SaveChangesAsync();
            }     
        }                  
    }
}
