namespace Nyomozas
{
    internal class DecisionEngine
    {
        private int kuszobertek;
        private List<Suspect> gyanlista;
        private List<Witness> tanulista;
        private DataStore adattar;

        public DecisionEngine(DataStore adattar, int kuszobertek = 8)
        {
            this.kuszobertek = kuszobertek;
            this.gyanlista = new List<Suspect>();
            this.tanulista = new List<Witness>();
            this.adattar = adattar;
        }

        public void Ertekeles(List<Evidence> bizonyitekok)
        {
            int osszPont = 0;
            int tanpont = 0;
            Suspect gyanusitott = GyanValasztas();

            foreach (Evidence b in bizonyitekok)
            {
                osszPont += MegbizhatosagPont(b.Megbizhatosag);
            }
            
            // megvaltozik tanu szerint
            if (tanulista.Count > 0)
            {
                for (int i = 0; i <= tanulista.Count; i++)
                {
                    tanpont += i;
                }
                
                tanpont /= tanulista.Count;
            
                osszPont = (osszPont + tanpont) / (bizonyitekok.Count + tanulista.Count);
            }
            else
            {
                osszPont /= bizonyitekok.Count;
            }
            
            Console.WriteLine($"{gyanusitott.Szemely.Nev} megbízhatósági szintje: {gyanusitott.Szint}/100");
            Console.WriteLine(
                $"Döntés megbíhatósága: {osszPont}/10"
            );

            if (osszPont >= kuszobertek)
            {
                Console.WriteLine(
                    $"Figyelem: {gyanusitott.Szemely.Nev} elerte a kuszoberteket!"
                );
            }
            
            adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Gyanusitott {gyanusitott.Szemely.Nev} elemezve."));
        }

        public bool GyanusitottakLista(Case ugy)
        {
            gyanlista = [];
            
            foreach (var gy in adattar.Gyanusitottak)
            {
                foreach (var sz in ugy.Szemelyek)
                {
                    if (gy.Szemely.Nev == sz.Nev)
                    {
                        gyanlista.Add(gy);
                    }
                }
            }
            return gyanlista.Count != 0;
        }
        
        public void TanuLista(Case ugy)
        {
            tanulista = [];
            foreach (var w in adattar.Tanuk)
            {
                foreach (var sz in ugy.Szemelyek)
                {
                    if (w.Szemely == sz)
                    {
                        tanulista.Add(w);
                    }
                }
            }
        }
        private Suspect GyanValasztas()
        {
            int cmd;
            Console.WriteLine("Ügy gyanusitottjai");
            for (int i = 0; i < gyanlista.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {gyanlista[i]}");
            }

            do
            {
                cmd = int.Parse(Console.ReadLine()!);
            } while (cmd < 1 || gyanlista.Count < cmd);
            
            return gyanlista[cmd - 1];
        }

        private int MegbizhatosagPont(string megbizhatosag)
        {
            switch (megbizhatosag.ToLower())
            {
                case "alacsony":
                    return 2;
                case "kozepes":
                    return 5;
                case "magas":
                    return 10;
                default:
                    return 1;
            }
        }
    }
}
