using System;

namespace Nyomozas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool fut = true;
            bool fut2;
            int cmd = 0;
            CaseManager ugykezelo = new();
            EvidenceManager bizkezelo = new();
            DataStore adattar = new();

            do
            {
                fut2 = true;
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
                            Console.WriteLine("\n1. Ugy letrehozasa\n2. Ügy állapotának változtatása\n3. Ugyek listazasa\n4. Kilépés");
                            do
                            {
                                cmd = int.Parse(Console.ReadLine()!);
                            }
                            while (cmd < 1 && cmd > 4);

                        
                            switch (cmd)
                            {
                                case 1:
                                    string allapot = "Nincs megadva";
                                    Console.WriteLine("\nUgyazonosito:");
                                    int ugyazonosito = int.Parse(Console.ReadLine()!);

                                    Console.WriteLine("\nCim:");
                                    string cim = Console.ReadLine()!;

                                    Console.WriteLine("\nLeiras:");
                                    string leiras = Console.ReadLine()!;

                                    Console.WriteLine("\nAllapot:");
                                    System.Console.WriteLine("(1) Nyitott\n(2) Folyamatban\n(3) Lezárt (4) Kilépés");

                                    do
                                    {
                                        cmd = int.Parse(Console.ReadLine()!);
                                    } while (cmd < 1 && cmd > 4);

                                    switch (cmd)
                                    {
                                        case 1:
                                        allapot = "Nyitott";
                                            break;
                                        case 2:
                                        allapot = "Folyamatban";
                                            break;
                                        case 3:
                                        allapot = "Lezárt";
                                            break;
                                        case 4:
                                            break;
                                    }

                                    Case ujUgy = new(ugyazonosito, cim, leiras, allapot);
                                    ugykezelo.Letrehozas(ujUgy);
                                    adattar.Ugyek.Add(ujUgy);
                                    break;

                                case 2: // Ügy állapotának változtatása

                                    if (ugykezelo.Ugyek.Count == 0)
                                        {
                                            System.Console.WriteLine("Nincs ügy.");
                                            break;
                                        }

                                    Console.WriteLine("\nÜgyazonosító:");
                                    cmd = int.Parse(Console.ReadLine()!);
                                    ugykezelo.CaseStatus(cmd);
                                    break;

                                case 3: // Ügyek listázása
                                    ugykezelo.Listazas();
                                    break;

                                case 4:
                                    fut2 = false;
                                    break;
                                }
                            } while (fut2);
                            break;

                    case 2:

                        do
                        {
                            Console.WriteLine("\n(1) Személy hozzadasa ugyhoz\n(2) Személyek listázása\n(3) Kilépés");
                            do
                            {
                                cmd = int.Parse(Console.ReadLine()!);
                            }
                            while (cmd < 1 && cmd > 3);

                            switch (cmd)
                            {
                                case 1:
                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        System.Console.WriteLine("Nincs ügy.");
                                        break;
                                    }

                                    ugykezelo.Listazas();

                                    Console.WriteLine("\nÜgyazonosító:");
                                    int ugyIdSzemely = int.Parse(Console.ReadLine()!);
                                    Console.WriteLine("\nNév:");
                                    string nev = Console.ReadLine()!;
                                    Console.WriteLine("\nÉletkor:");
                                    int eletkor = int.Parse(Console.ReadLine()!);
                                    Console.WriteLine("\nMegjegyzés:");
                                    string megjegyzes = Console.ReadLine()!;

                                    Person szemely = new(nev, eletkor, megjegyzes);
                                    ugykezelo.SzemelyHozzaadas(ugyIdSzemely, szemely);
                                    adattar.Szemelyek.Add(szemely);
                                    break;

                                case 2:
                                    foreach (Person p in adattar.Szemelyek)
                                    {
                                        System.Console.WriteLine(p);
                                    }
                                    break;

                                case 3:
                                    fut2 = false;
                                    break;
                            }
                        } while (fut2);
                        break;

                    case 3:
                        do
                        {
                            Console.WriteLine("\n(1) Bizonyíték hozzadasa ugyhoz\n(2) Bizonyíték törlése\n(3) Bizonyítékok listázása\n(3) Kilépés");
                            do
                            {
                                cmd = int.Parse(Console.ReadLine()!);
                            }
                            while (cmd < 1 && cmd > 3);

                            switch (cmd)
                            {
                                case 1:
                                    string megbizhatosag = "Nincs megadva";

                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        System.Console.WriteLine("Nincs ügy.");
                                        break;
                                    }

                                    ugykezelo.Listazas();

                                    Console.WriteLine("\nÜgyazonosító:");
                                    cmd = int.Parse(Console.ReadLine()!);
                                    Console.WriteLine("\nBizonyíték Azonosító:");
                                    string azonosito = Console.ReadLine()!;
                                    Console.WriteLine("\nTípus:");
                                    string tipus = Console.ReadLine()!;
                                    Console.WriteLine("\nLeírás:");
                                    string bizonyitekLeiras = Console.ReadLine()!;
                                    Console.WriteLine("\nMegbízhatóság:");
                                    do
                                    {
                                        cmd = int.Parse(Console.ReadLine()!);
                                    } while (cmd < 1 && cmd > 4);

                                    switch (cmd)
                                    {
                                        case 1:
                                        megbizhatosag = "Nyitott";
                                            break;
                                        case 2:
                                        megbizhatosag = "Folyamatban";
                                            break;
                                        case 3:
                                        megbizhatosag = "Lezárt";
                                            break;
                                        case 4:
                                            break;
                                    }

                                    Evidence bizonyitek = new(azonosito, tipus, bizonyitekLeiras, megbizhatosag);
                                    ugykezelo.BizonyitekHozzaadas(cmd, bizonyitek);
                                    adattar.Bizonyitekok.Add(bizonyitek);

                                    break;
                                
                                case 2:
                                    if (adattar.Bizonyitekok.Count == 0)
                                    {
                                        System.Console.WriteLine("Nincsen törölhető bizonyíték.");
                                        break;
                                    }

                                    foreach (Evidence e in adattar.Bizonyitekok)
                                    {
                                        System.Console.WriteLine(e);
                                    }

                                    Console.WriteLine("\nBizonyíték azonosítója:");
                                    string bizID = Console.ReadLine()!;
                                    bizkezelo.Torles(bizID);

                                    break;

                                case 3:
                                    foreach (Evidence e in adattar.Bizonyitekok)
                                    {
                                        System.Console.WriteLine(e);
                                    }
                                    break;

                                case 4:
                                    fut2 = false;
                                    break;
                            }
                        } while (fut2);
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

            Console.WriteLine("\nKilepes...");
        }
    }
}