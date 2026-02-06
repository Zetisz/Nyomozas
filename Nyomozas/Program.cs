using System;

namespace Nyomozas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool fut = true;
            bool fut2 = true;
            int cmd = 0;
            CaseManager ugykezelo = new();
            DataStore adattar = new();

            do
            {
                Console.WriteLine("\n1. Ügyek kezelése\n2. Személyek kezelése\n3. Bizonyítékok kezelése\n4. Idővonal megtekintése\n5. Elemzés / döntések\n6. Kilépés");

                do
                {
                    cmd = int.Parse(Console.ReadLine()!);
                }
                while (cmd < 1 && cmd > 6);

                switch (cmd)
                {
                    case 1:
                        do
                        {
                            Console.WriteLine("\n1. Ugy letrehozasa\n2. Személy hozzadasa ugyhoz\n3. Bizonyíték hozzaadasa ugyhoz\n4. Ügy állapotának változtatása\n5. Ugyek listazasa\n6. Kilépés");
                            do
                            {
                                cmd = int.Parse(Console.ReadLine()!);
                            }
                            while (cmd < 1 && cmd > 6);

                        
                            switch (cmd)
                            {
                            case 1:
                                Console.WriteLine("Ugyazonosito:");
                                int ugyazonosito = int.Parse(Console.ReadLine()!);

                                Console.WriteLine("Cim:");
                                string cim = Console.ReadLine()!;

                                Console.WriteLine("Leiras:");
                                string leiras = Console.ReadLine()!;

                                Console.WriteLine("Allapot:");
                                string allapot = Console.ReadLine()!;

                                Case ujUgy = new(ugyazonosito, cim, leiras, allapot);
                                ugykezelo.Letrehozas(ujUgy);
                                adattar.Ugyek.Add(ujUgy);
                                break;

                            case 2: // Személy hozzáadása
                                Console.WriteLine("Ügyazonosító:");
                                int ugyIdSzemely = int.Parse(Console.ReadLine()!);
                                Console.WriteLine("Név:");
                                string nev = Console.ReadLine()!;
                                Console.WriteLine("Életkor:");
                                int eletkor = int.Parse(Console.ReadLine()!);
                                Console.WriteLine("Megjegyzés:");
                                string megjegyzes = Console.ReadLine()!;

                                Person szemely = new(nev, eletkor, megjegyzes);
                                ugykezelo.SzemelyHozzaadas(ugyIdSzemely, szemely);
                                adattar.Szemelyek.Add(szemely);
                                break;

                            case 3: // Bizonyíték hozzáadása
                                Console.WriteLine("Ügyazonosító:");
                                cmd = int.Parse(Console.ReadLine()!);
                                Console.WriteLine("Bizonyíték Azonosító:");
                                string azonosito = Console.ReadLine()!;
                                Console.WriteLine("Típus:");
                                string tipus = Console.ReadLine()!;
                                Console.WriteLine("Leírás:");
                                string bizonyitekLeiras = Console.ReadLine()!;
                                Console.WriteLine("Megbízhatóság:");
                                string megbizhatosag = Console.ReadLine()!;

                                Evidence bizonyitek = new(azonosito, tipus, bizonyitekLeiras, megbizhatosag);
                                ugykezelo.BizonyitekHozzaadas(cmd, bizonyitek);
                                adattar.Bizonyitekok.Add(bizonyitek);
                                break;

                            case 4: // Ügy állapotának változtatása
                                Console.WriteLine("Ügyazonosító:");
                                cmd = int.Parse(Console.ReadLine()!);
                                ugykezelo.CaseStatus(cmd);
                                break;

                            case 5: // Ügyek listázása
                                ugykezelo.Listazas();
                                break;

                            case 6:
                                fut2 = false;
                                break;
                            }
                        } while (fut2);
                        
                        
                        break;
                    case 2:
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.WriteLine();
                        break;
                    case 6:
                        fut = false;
                        break;
                }
            } while (fut);

            Console.WriteLine("Kilepes...");
        }
    }
}