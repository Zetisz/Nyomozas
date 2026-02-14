namespace Nyomozas
{
    internal class Program
    {
        private static void Main()
        {
            bool fut = true;
            bool fut2;
            int cmd;
            CaseManager ugykezelo = new();
            EvidenceManager bizkezelo = new();
            DataStore adattar = new();
            DecisionEngine donteshozo = new();

            do
            {
                fut2 = true;
                Console.WriteLine("\n1. Ügyek kezelése\n2. Személyek kezelése\n3. Bizonyítékok kezelése\n4. Idővonal megtekintése\n5. Elemzés / döntések\n6. Kilépés");

                do
                {
                    cmd = int.Parse(Console.ReadLine()!);
                }
                while (cmd < 1 || cmd > 6);

                switch (cmd)
                {
                    case 1:
                        do
                        {
                            Console.WriteLine("\n1. Ügy letrehozasa\n2. Ügy állapotának változtatása\n3. Ügyek listazasa\n4. Kilépés");
                            do
                            {
                                cmd = int.Parse(Console.ReadLine()!);
                            }
                            while (cmd < 1 || cmd > 4);

                        
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
                                    Console.WriteLine("(1) Nyitott\n(2) Folyamatban\n(3) Lezárt");

                                    do
                                    {
                                        cmd = int.Parse(Console.ReadLine()!);
                                    } while (cmd < 1 || cmd > 3);

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
                                    }

                                    Case ujUgy = new(ugyazonosito, cim, leiras, allapot);
                                    ugykezelo.Letrehozas(ujUgy);
                                    adattar.Ugyek.Add(ujUgy);
                                    break;

                                case 2: // Ügy állapotának változtatása

                                    if (ugykezelo.Ugyek.Count == 0)
                                        {
                                            Console.WriteLine("Nincs ügy.");
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
                            while (cmd < 1 || cmd > 3);

                            switch (cmd)
                            {
                                case 1:
                                    string fajta = "Nincs megadva";
                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        Console.WriteLine("Nincs ügy.");
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
                                    
                                    Console.WriteLine("(1) Gyanusított (2) Tanú (3) Kihagyás");
                                    
                                    do
                                    {
                                        cmd = int.Parse(Console.ReadLine()!);
                                    } while (cmd < 1 || cmd > 3);

                                    switch (cmd)
                                    {
                                        case 1:
                                            fajta = "gyanusitott";
                                            break;
                                        case 2:
                                            fajta = "tanu";
                                            break;
                                        case 3:
                                            break;
                                    }
                                    
                                    Person szemely = new(nev, eletkor, megjegyzes);
                                    ugykezelo.SzemelyHozzaadas(ugyIdSzemely, szemely);
                                    adattar.Szemelyek.Add(szemely);

                                    if (fajta == "gyanusitott")
                                    {
                                        int szint;
                                        string statusz = "Nincs megadva";
                                        Console.WriteLine("Gyanusítottsági szint (0-100)");
                                        do
                                        {
                                            szint = int.Parse(Console.ReadLine()!);
                                        } while (szint < 0 || szint > 100);
                                        
                                        Console.WriteLine("(1) Szabad (2) Megfigyelt (3) Őrizetben (4) Kihagyás");
                                    
                                        do
                                        {
                                            cmd = int.Parse(Console.ReadLine()!);
                                        } while (cmd < 1 || cmd > 4);

                                        switch (cmd)
                                        {
                                            case 1:
                                                statusz = "Szabad";
                                                break;
                                            case 2:
                                                statusz = "Megfigyelt";
                                                break;
                                            case 3:
                                                statusz = "Őrizetben";
                                                break;
                                            case 4:
                                                break;
                                        }
                                        
                                        Suspect s = new(szemely, szint, statusz);
                                        adattar.Gyanusitottak.Add(s);
                                    }
                                    else if (fajta == "tanu")
                                    {
                                        Console.WriteLine("Vallomás:");
                                        string vallomas = Console.ReadLine()!;
                                        Console.WriteLine("Dátum:");
                                        string datum = Console.ReadLine()!;
                                        
                                        Witness w = new(szemely, vallomas, datum);
                                        adattar.Tanuk.Add(w);
                                    }
                                    
                                    break;

                                case 2:
                                    foreach (Person p in adattar.Szemelyek)
                                    {
                                        Console.WriteLine(p);
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
                            Console.WriteLine("\n(1) Bizonyíték hozzadasa ugyhoz\n(2) Bizonyíték törlése\n(3) Bizonyítékok listázása\n(4) Kilépés");
                            do
                            {
                                cmd = int.Parse(Console.ReadLine()!);
                            }
                            while (cmd < 1 || cmd > 4);

                            switch (cmd)
                            {
                                case 1:
                                    string megbizhatosag = "Nincs megadva";

                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        Console.WriteLine("Nincs ügy.");
                                        break;
                                    }

                                    ugykezelo.Listazas();

                                    Console.WriteLine("\nÜgyazonosító:");
                                    int ugyIdBiz = int.Parse(Console.ReadLine()!);
                                    Console.WriteLine("\nBizonyíték Azonosító:");
                                    string azonosito = Console.ReadLine()!;
                                    Console.WriteLine("\nTípus:");
                                    string tipus = Console.ReadLine()!;
                                    Console.WriteLine("\nLeírás:");
                                    string bizonyitekLeiras = Console.ReadLine()!;
                                    Console.WriteLine("\nMegbízhatóság:");
                                    Console.WriteLine("(1) Alacsony\n(2) Közepes\n(3) Magas");
                                    do
                                    {
                                        cmd = int.Parse(Console.ReadLine()!);
                                    } while (cmd < 1 || cmd > 3);

                                    switch (cmd)
                                    {
                                        case 1:
                                        megbizhatosag = "Alacsony";
                                            break;
                                        case 2:
                                        megbizhatosag = "Közepes";
                                            break;
                                        case 3:
                                        megbizhatosag = "Magas";
                                            break;
                                    }

                                    Evidence bizonyitek = new(azonosito, tipus, bizonyitekLeiras, megbizhatosag);
                                    ugykezelo.BizonyitekHozzaadas(ugyIdBiz, bizonyitek);
                                    bizkezelo.Hozzadas(bizonyitek);
                                    adattar.Bizonyitekok.Add(bizonyitek);

                                    break;
                                
                                case 2:
                                    if (adattar.Bizonyitekok.Count == 0)
                                    {
                                        Console.WriteLine("Nincsen törölhető bizonyíték.");
                                        break;
                                    }

                                    bizkezelo.Listazas();  

                                    Console.WriteLine("\nBizonyíték azonosítója:");
                                    string bizId = Console.ReadLine()!;
                                    bizkezelo.Torles(bizId);

                                    break;

                                case 3:
                                    bizkezelo.Listazas();
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
                        ugykezelo.Listazas();
                        Console.WriteLine("\nÜgyazonosító:");
                        int ugyId = int.Parse(Console.ReadLine()!);
                        Person valszemely = ugykezelo.SzemelyekValasztas(ugyId);
                        List<Evidence> ugybiz = ugykezelo.UgyBizonyitekai(ugyId);
                        
                        donteshozo.Ertekeles(valszemely, ugybiz);
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