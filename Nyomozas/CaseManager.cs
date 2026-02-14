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

        public void CaseStatus(int ugyAzonosito)
        {
            Case ugy = ugyek.FirstOrDefault(u => u.Ugyazonosito == ugyAzonosito)!;

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
                    Console.WriteLine("(1) Nyitott\n(2) Folyamatban\n(3) Lezárt (4) Kilépés");

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
        public void SzemelyHozzaadas(int ugyAzonosito, Person szemely)
        {
            Case ugy = ugyek.FirstOrDefault(u => u.Ugyazonosito == ugyAzonosito)!;

            ugy.Szemelyek.Add(szemely);
            Console.WriteLine("Szemely hozzarendelve az ugyhoz!");
        }

        public void BizonyitekHozzaadas(int ugyAzonosito, Evidence bizonyitek)
        {
            Case ugy = ugyek.FirstOrDefault(u => u.Ugyazonosito == ugyAzonosito)!;

            ugy.Bizonyitekok.Add(bizonyitek);
            Console.WriteLine("Bizonyitek hozzarendelve az ugyhoz!");
        }
    }
}
