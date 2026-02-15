namespace Nyomozas
{
    internal class EvidenceManager
    {
        private List<Evidence> bizonyitekok;

        public EvidenceManager()
        {
            bizonyitekok = [];
        }

        public void Hozzadas(Evidence bizonyitek)
        {
            bizonyitekok.Add(bizonyitek);
            Console.WriteLine("Bizonyitek hozzaadva!");
        }

        public void Torles(Case ugy)
        {
            int cmd;
            for (int i = 0; i < ugy.Bizonyitekok.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {ugy.Bizonyitekok[i]}");
            }

            do
            {
                cmd = int.Parse(Console.ReadLine()!);
            } while (cmd < 1 || ugy.Bizonyitekok.Count < cmd);
            
            Evidence biz = ugy.Bizonyitekok[cmd - 1];

            if (bizonyitekok.Remove(biz))
            {
                Console.WriteLine("Bizonyíték törölve!");
            }
            else
            {
                Console.WriteLine("A bizonyitek nem talalhato!");
            }
            ugy.Bizonyitekok.Remove(biz);
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
