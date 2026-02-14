namespace Nyomozas
{
    internal class EvidenceManager
    {
        private List<Evidence> bizonyitekok;

        public EvidenceManager()
        {
            bizonyitekok = [];
        }

        internal List<Evidence> Bizonyitekok { get => bizonyitekok; set => bizonyitekok = value; }

        public void Hozzadas(Evidence bizonyitek)
        {
            bizonyitekok.Add(bizonyitek);
            Console.WriteLine("Bizonyitek hozzaadva!");
        }

        public void Torles(string bizAzonosito)
        {
            Evidence biz = bizonyitekok.FirstOrDefault(b => b.Azonosito == bizAzonosito)!;

            if (bizonyitekok.Remove(biz))
            {
                Console.WriteLine("Bizonyitek torolve!");
            }
            else
            {
                Console.WriteLine("A bizonyitek nem talalhato!");
            }
        }

        public void Listazas()
        {
            if (bizonyitekok.Count > 0) 
            {
                foreach (Evidence b in bizonyitekok)
                {
                    Console.WriteLine(b);
                }
            }
            else
            {
                Console.WriteLine("Nincs kilistazhato elem.");
            }
        }
    }
}
