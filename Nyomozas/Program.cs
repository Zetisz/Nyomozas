namespace Nyomozas
{
    internal class Program
    {
        private static void Main()
        {
            bool fut = true;
            bool fut2;
            int cmd;
            DataStore adattar = new();
            EvidenceManager bizkezelo = new(adattar);
            DecisionEngine donteshozo = new(adattar);
            CaseManager ugykezelo = new(adattar, bizkezelo, donteshozo);

            do
            {
                fut2 = true;
                Log("\n1. Ügyek kezelése\n2. Személyek kezelése\n3. Bizonyítékok kezelése\n4. Idővonal megtekintése\n5. Elemzés / döntések\n6. Kilépés");

                do
                {
                    cmd = int.Parse(Console.ReadLine()!);
                }
                while (cmd < 1 || cmd > 6);

                switch (cmd)
                {
                    case 1: // Ügy
                        do
                        {
                            Log("\n1. Ügy létrehozasa\n2. Ügy állapotának változtatása\n3. Ügyek listázása\n4. Kilépés", ConsoleColor.DarkCyan);
                            cmd = int.Parse(Console.ReadLine()!);
                            
                            switch (cmd)
                            {
                                case 1: // Létrehozás
                                    ugykezelo.Letrehozas();
                                    break;

                                case 2: // Ügy állapotának változtatása
                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        Log("Nincs ügy.",  ConsoleColor.Red);
                                        break;
                                    }
                                    ugykezelo.CaseStatus();
                                    break;

                                case 3: // Ügyek listázása
                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        Log("Nincs ügy.",  ConsoleColor.Red);
                                        break;
                                    }
                                    ugykezelo.Listazas();
                                    break;

                                case 4:
                                    fut2 = false;
                                    break;
                                default:
                                    Log("Hibás kommand.",  ConsoleColor.Red);
                                    break;
                                }
                            } while (fut2);
                            break;

                    case 2: // Személy
                        do
                        {
                            Log("\n(1) Személy hozzáadasa ugyhoz\n(2) Személyek listázása\n(3) Kilépés",  ConsoleColor.DarkCyan);
                            cmd = int.Parse(Console.ReadLine()!);

                            switch (cmd)
                            {
                                case 1: // Személy létrehozása
                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        Log("Nincs ügy.",  ConsoleColor.Red);
                                        break;
                                    }
                                    ugykezelo.SzemelyHozzaadas();
                                    break;

                                case 2: // Listázás
                                    if (adattar.Szemelyek.Count == 0)
                                    {
                                        Log("Nincs listázható személy.",  ConsoleColor.Red);
                                        break;
                                    } 
                                    ugykezelo.SzemelyekListazasa();
                                    break;

                                case 3:
                                    fut2 = false;
                                    break;
                                default:
                                    Log("Hibás kommand", ConsoleColor.Red);
                                    break;
                            }
                        } while (fut2);
                        break;

                    case 3: // Bizonyíték
                        do
                        {
                            Log("\n(1) Bizonyíték hozzáadasa ugyhoz\n(2) Bizonyíték törlése\n(3) Bizonyítékok listázása\n(4) Kilépés",  ConsoleColor.DarkCyan);
                            cmd = int.Parse(Console.ReadLine()!);

                            switch (cmd)
                            {
                                case 1: // Biz létrehozás
                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        Log("Nincs ügy.",  ConsoleColor.Red);
                                        break;
                                    }
                                    ugykezelo.BizonyitekHozzaadas();
                                    break;
                                
                                case 2:
                                    Case ugybizc = ugykezelo.Ugyvalasztas();
                                    if (ugybizc.Bizonyitekok.Count == 0)
                                    {
                                        Log("Nincsen törölhető bizonyíték.",  ConsoleColor.Red);
                                        break;
                                    }
                                    bizkezelo.Torles(ugybizc);
                                    break;

                                case 3:
                                    bizkezelo.Listazas();
                                    break;

                                case 4:
                                    fut2 = false;
                                    break;
                                default:
                                    Log("Hibás kommand.", ConsoleColor.Red);
                                    break;
                            }
                        } while (fut2);
                        break;
                    
                    case 4:
                        if (adattar.Idovonal.Count == 0)
                        {
                            Log("Az idővonal üres.",   ConsoleColor.Red);
                            break;
                        }
                        Console.WriteLine();
                        foreach (var e in adattar.Idovonal)
                        {
                            Console.WriteLine(e);
                        }
                        break;

                    case 5:
                        if (ugykezelo.Ugyek.Count == 0)
                        {
                            Log("Nincs ügy.", ConsoleColor.Red);
                            break;
                        }
                        Case ugyd = ugykezelo.Ugyvalasztas();
                        var vangyan = donteshozo.GyanusitottakLista(ugyd);

                        if (ugyd.Szemelyek.Count == 0)
                        {
                            Log("Nincs személy az ügyhöz kötve.", ConsoleColor.Red);
                            break;
                        }
                        if (!vangyan)
                        {
                            Log("Nincs gyanusitott az ugyhoz kotve.", ConsoleColor.Red);
                            break;
                        }
                        if (ugyd.Bizonyitekok.Count == 0)
                        {
                            Log("Nincs bizonyíték az ügyhöz kötve.", ConsoleColor.Red);
                            break;
                        }
                        ugykezelo.Donteshozas(ugyd);
                        break;
                    
                    case 6:
                        fut = false;
                        break;
                }
            } while (fut);

            Log("\nKilepes...");
        }
        static void Log(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}