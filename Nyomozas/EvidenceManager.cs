namespace Nyomozas
{
    internal class EvidenceManager
    {
        private List<Evidence> bizonyitekok;
        private DataStore adattar;

        public EvidenceManager(DataStore adattar)
        {
            this.bizonyitekok = [];
            this.adattar = adattar;
        }

        public void Hozzadas(Evidence bizonyitek)
        {
            bizonyitekok.Add(bizonyitek);
            Log("Bizonyíték hozzáadva!", ConsoleColor.Green);
        }

        public void Torles(Case ugy)
        {
            int cmd;
            Console.WriteLine();
            for (int i = 0; i < ugy.Bizonyitekok.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {ugy.Bizonyitekok[i]}");
            }
            Log("\nBizonyíték sorszáma:");
            do
            {
                cmd = int.Parse(Console.ReadLine()!);
            } while (cmd < 1 || ugy.Bizonyitekok.Count < cmd);
            
            Evidence biz = ugy.Bizonyitekok[cmd - 1];

            if (bizonyitekok.Remove(biz))
            {
                Log("Bizonyíték törölve!",  ConsoleColor.Green);
            }
            else
            {
                Log("A bizonyíték nem található!", ConsoleColor.Red);
            }
            ugy.Bizonyitekok.Remove(biz);
            adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Bizonyíték törölve ({ugy.Ugyazonosito})"));
        }

        public void Listazas()
        {
            Console.WriteLine();
            if (bizonyitekok.Count > 0) 
            {
                foreach (Evidence b in bizonyitekok)
                {
                    Console.WriteLine(b);
                }
            }
            else
            {
                Log("\nNincs kilistázható elem.",  ConsoleColor.Red);
            }
        }
        static void Log(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
