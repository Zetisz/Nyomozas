namespace Nyomozas
{
    internal class DecisionEngine
    {
        private int kuszobertek;

        public DecisionEngine(int kuszobertek = 8)
        {
            this.kuszobertek = kuszobertek;
        }

        public void Ertekeles(Person szemely, List<Evidence> bizonyitekok)
        {
            int osszPont = 0;

            foreach (Evidence b in bizonyitekok)
            {
                osszPont += MegbizhatosagPont(b.Megbizhatosag);
            }
            osszPont /= bizonyitekok.Count;

            Console.WriteLine($"{szemely.Nev} megbíhatósági szintje: /100");
            
            Console.WriteLine(
                $"Döntés megbíhatósága: {osszPont}/10"
            );

            if (osszPont >= kuszobertek)
            {
                Console.WriteLine(
                    $"Figyelem: {szemely.Nev} elerte a kuszoberteket!"
                );
            }
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
