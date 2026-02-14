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

            Console.WriteLine(
                $"{szemely.Nev} osszesitett gyanusitottsagi szintje: {osszPont}"
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
                    return 1;
                case "kozepes":
                    return 3;
                case "magas":
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
