namespace Nyomozas
{
    internal class CaseManager
    {
        private List<Case> ugyek;

        public CaseManager()
        {
            ugyek = [];
        }

        internal List<Case> Ugyek { get => ugyek; set => ugyek = value; }

        public void Letrehozas(Case ugy)
        {
            ugyek.Add(ugy);
            Console.WriteLine("Ugy sikeresen letrehozva!");
        }

        public void Listazas()
        {
            if (ugyek.Count > 0)
            {
                foreach (Case ugy in ugyek)
                {
                    Console.WriteLine(ugy);
                }
            }
            else
            {
                Console.WriteLine("Nincs megjelenitheto ugy.");
            }
        }

        public Case Ugyvalasztas()
        {
            int ugy;
            for (int i = 0; i < ugyek.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {ugyek[i]}");
            }
            Console.WriteLine("\nÜgy sorszáma:");
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

        public void CaseStatus(Case ugy)
        {
            int cmd;
            Console.WriteLine($"Az ügy aktuális státusza: {ugy.Allapot}");
            Console.WriteLine("(1) Ügy állapotának változtatása\n(2) Kilépés");
            do
            {
                cmd = int.Parse(Console.ReadLine()!);
            } while (cmd < 1 || cmd > 2);

            switch (cmd)
            {
                case 1:
                    Console.WriteLine("(1) Nyitott\n(2) Folyamatban\n(3) Lezárt\n(4) Kilépés");

                    do
                    {
                        cmd = int.Parse(Console.ReadLine()!);
                    } while (cmd < 1 || cmd > 4);

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
                    }

                    break;
                case 2:
                    break;
            }
        }

        // Személy hozzárendelése ügyhöz
        public void SzemelyHozzaadas(Case ugy, Person szemely)
        {
           ugy.Szemelyek.Add(szemely);
           Console.WriteLine("Szemely hozzarendelve az ugyhoz!");
        }

        public void BizonyitekHozzaadas(Case ugy, Evidence bizonyitek)
        {
            ugy.Bizonyitekok.Add(bizonyitek);
            Console.WriteLine("Bizonyitek hozzarendelve az ugyhoz!");
        }
    }
}
