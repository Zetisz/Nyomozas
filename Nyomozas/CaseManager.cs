namespace Nyomozas
{
    internal class CaseManager
    {
        private List<Case> ugyek;
        private DataStore adattar;
        private EvidenceManager bizkezelo;
        private DecisionEngine donteshozo;

        public CaseManager(DataStore adattar, EvidenceManager bizkezelo, DecisionEngine donteshozo)
        {
            this.ugyek = [];
            this.adattar = adattar;
            this.bizkezelo = bizkezelo;
            this.donteshozo = donteshozo;
        }

        internal List<Case> Ugyek { get => ugyek; set => ugyek = value; }

        public void Letrehozas()
        {
            int cmd;
            string allapot = "Nincs megadva";
            Log("\nUgyazonosito:", ConsoleColor.Cyan);
            int ugyazonosito = int.Parse(Console.ReadLine()!);

            Log("\nCim:",  ConsoleColor.Cyan);
            string cim = Console.ReadLine()!;

            Log("\nLeiras:",  ConsoleColor.Cyan);
            string leiras = Console.ReadLine()!;

            Log("\nAllapot:",  ConsoleColor.Cyan);
            Log("(1) Nyitott\n(2) Folyamatban\n(3) Lezárt");

            do
            {
                cmd = int.Parse(Console.ReadLine()!);
            } while (cmd < 1 || cmd > 3);

            switch (cmd)
            {
                case 1:
                    allapot = "Nyitott";
                    break;
                case 2:
                    allapot = "Folyamatban";
                    break;
                case 3:
                    allapot = "Lezárt";
                    break;
            }

            Case ujUgy = new(ugyazonosito, cim, leiras, allapot);
            ugyek.Add(ujUgy);
            adattar.Ugyek.Add(ujUgy);
            adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Ugy letrehozva ({ugyazonosito})"));
            
            Log("Ugy sikeresen letrehozva!",   ConsoleColor.Green);
        }

        public void Listazas()
        {
            Console.WriteLine();
            if (ugyek.Count > 0)
            {
                foreach (Case ugy in ugyek)
                {
                    Console.WriteLine(ugy);
                }
            }
            else
            {
                Log("Nincs megjelenitheto ugy.",  ConsoleColor.Red);
            }
        }

        public void SzemelyekListazasa()
        {
            if (adattar.Gyanusitottak.Count == 0 && adattar.Tanuk.Count != 0)
            {
                Log("\nNincs megjeleníthető gyanúsított.\nTanúk:",  ConsoleColor.Cyan);
                foreach (var tanu in adattar.Tanuk)
                {
                    Console.WriteLine(tanu);
                }
            }
            else if (adattar.Gyanusitottak.Count != 0 && adattar.Tanuk.Count == 0)
            {
                Log("\nNincs megjeleníthető tanu.\nGyanusítottak:", ConsoleColor.Cyan);
                foreach (var gyan in adattar.Gyanusitottak)
                {
                    Console.WriteLine(gyan);
                }
            }
            else
            {
                Log("\nGyanúsítottak:",  ConsoleColor.Cyan);
                foreach (var gyan in adattar.Gyanusitottak)
                {
                    Console.WriteLine(gyan);
                }

                Log("\nTanuk:",   ConsoleColor.Cyan);
                foreach (var tanu in adattar.Tanuk)
                {
                    Console.WriteLine(tanu);
                }
            }
        }

        public Case Ugyvalasztas()
        {
            int ugy;
            Console.WriteLine();
            for (int i = 0; i < ugyek.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {ugyek[i]}");
            }
            Log("\nÜgy sorszáma:",   ConsoleColor.Cyan);
            do
            {
                ugy = int.Parse(Console.ReadLine()!);
            } while (ugy < 1 || ugyek.Count < ugy);
            
            return ugyek[ugy - 1];
        }
        
        public List<Evidence> UgyBizonyitekai(Case ugy)
        {
            return ugy.Bizonyitekok;
        }

        public void CaseStatus()
        {
            int cmd;
            Case ugy = Ugyvalasztas();
            Log($"\nAz ügy aktuális státusza: {ugy.Allapot}",  ConsoleColor.Cyan);
            Log("\n(1) Ügy állapotának változtatása\n(2) Kilépés");
            cmd = int.Parse(Console.ReadLine()!);

            switch (cmd)
            {
                case 1:
                    Log("\n(1) Nyitott\n(2) Folyamatban\n(3) Lezárt\n(4) Kilépés");
                    cmd = int.Parse(Console.ReadLine()!);

                    switch (cmd)
                    {
                        case 1:
                        ugy.Allapot = "Nyitott";
                            break;
                        case 2:
                        ugy.Allapot = "Folyamatban";
                            break;
                        case 3:
                        ugy.Allapot = "Lezárt";
                            break;
                        case 4:
                            break;
                        default:
                            Log("Hibás kommand", ConsoleColor.Red);
                            break;
                    }
                    Log("Sikeresen megváltoztatva.",  ConsoleColor.Green);
                    adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Ügy állapota megváltoztatva ({ugy.Ugyazonosito})"));
                    break;
                case 2:
                    break;
                default:
                    Log("Hibás kommand", ConsoleColor.Red);
                    break;
            }
        }
        
        public void SzemelyHozzaadas()
        {
            int cmd;
            string fajta = "Nincs megadva";
            Case ugySzemely = Ugyvalasztas();
            Log("\nNév:",  ConsoleColor.Cyan);
            string nev = Console.ReadLine()!;
            Log("\nÉletkor:",   ConsoleColor.Cyan);
            int eletkor = int.Parse(Console.ReadLine()!);
            Log("\nMegjegyzés:",   ConsoleColor.Cyan);
            string megjegyzes = Console.ReadLine()!;
                                    
            Log("(1) Gyanusított (2) Tanú");
                                    
            do
            {
                cmd = int.Parse(Console.ReadLine()!);
            } while (cmd < 1 || cmd > 3);

            switch (cmd)
            {
                case 1:
                    fajta = "gyanusitott";
                    break;
                case 2:
                    fajta = "tanu";
                    break;
            } 
            Person szemely = new(nev, eletkor, megjegyzes);
            ugySzemely.Szemelyek.Add(szemely);
            adattar.Szemelyek.Add(szemely);
            Log("Személy hozzárendelve az ügyhoz!",  ConsoleColor.Green);
            
            if (fajta == "gyanusitott")
            {
                int szint;
                string statusz = "";
                Log("Gyanusítottsági szint (0-100)",  ConsoleColor.Cyan);
                do
                {
                    szint = int.Parse(Console.ReadLine()!);
                } while (szint < 0 || szint > 100);
                
                Log("(1) Szabad (2) Megfigyelt (3) Őrizetben (4) Kihagyás");
            
                do
                {
                    cmd = int.Parse(Console.ReadLine()!);
                } while (cmd < 1 || cmd > 4);

                switch (cmd)
                {
                    case 1:
                        statusz = "Szabad";
                        break;
                    case 2:
                        statusz = "Megfigyelt";
                        break;
                    case 3:
                        statusz = "Őrizetben";
                        break;
                    case 4:
                        break;
                }
                
                Suspect s = new(szemely, szint, statusz);
                adattar.Gyanusitottak.Add(s);
            }
            else if (fajta == "tanu")
            {
                Log("Vallomás:",   ConsoleColor.Cyan);
                string vallomas = Console.ReadLine()!;
                Log("Dátum:",    ConsoleColor.Cyan);
                string datum = Console.ReadLine()!;
                
                Witness w = new(szemely, vallomas, datum);
                adattar.Tanuk.Add(w);
            }
            adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"{nev} személy letrehozva. ({ugySzemely.Ugyazonosito} ügyhöz hozzárendelve)"));
        }

        public void BizonyitekHozzaadas()
        {
            int cmd;
            string megbizhatosag = "Nincs megadva";
            Case ugy = Ugyvalasztas();
            Log("\nBizonyíték Azonosító:", ConsoleColor.Cyan);
            string azonosito = Console.ReadLine()!;
            Log("\nTípus:",  ConsoleColor.Cyan);
            string tipus = Console.ReadLine()!;
            Log("\nLeírás:", ConsoleColor.Cyan);
            string bizonyitekLeiras = Console.ReadLine()!;
            Log("\nMegbízhatóság:", ConsoleColor.Cyan);
            Log("(1) Alacsony\n(2) Közepes\n(3) Magas");
            do
            {
                cmd = int.Parse(Console.ReadLine()!);
            } while (cmd < 1 || cmd > 3);

            switch (cmd)
            {
                case 1:
                    megbizhatosag = "Alacsony";
                    break;
                case 2:
                    megbizhatosag = "Közepes";
                    break;
                case 3:
                    megbizhatosag = "Magas";
                    break;
            }

            Evidence bizonyitek = new(azonosito, tipus, bizonyitekLeiras, megbizhatosag);
            bizkezelo.Hozzadas(bizonyitek);
            adattar.Bizonyitekok.Add(bizonyitek);
            ugy.Bizonyitekok.Add(bizonyitek);
            Log("Bizonyíték hozzárendelve az ügyhöz!",   ConsoleColor.Green);
            adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Bizonyíték létrehozva ({azonosito}, {ugy.Ugyazonosito} ügyhöz hozzárendelve)"));
        }

        public void Donteshozas(Case ugyd)
        {
            donteshozo.TanuLista(ugyd);
            List<Evidence> ugybiz = UgyBizonyitekai(ugyd);
            donteshozo.Ertekeles(ugybiz);
            adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Ügy elemezve. ({ugyd.Ugyazonosito})"));
        }
        
        static void Log(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
