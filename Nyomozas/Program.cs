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
            DecisionEngine donteshozo = new(adattar);

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
                                    adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Ugy {ugyazonosito} letrehozva."));
                                    break;

                                case 2: // Ügy állapotának változtatása

                                    if (ugykezelo.Ugyek.Count == 0)
                                        {
                                            Console.WriteLine("Nincs ügy.");
                                            break;
                                        }

                                    Case ugyA = ugykezelo.Ugyvalasztas();
                                    ugykezelo.CaseStatus(ugyA);
                                    adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Ugy {ugyA.Ugyazonosito} allapota megvaltoztatva."));
                                    break;

                                case 3: // Ügyek listázása
                                    if (ugykezelo.Ugyek.Count == 0)
                                    {
                                        Console.WriteLine("Nincs ügy.");
                                        break;
                                    }
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

                                    Case ugySzemely = ugykezelo.Ugyvalasztas();
                                    Console.WriteLine("\nNév:");
                                    string nev = Console.ReadLine()!;
                                    Console.WriteLine("\nÉletkor:");
                                    int eletkor = int.Parse(Console.ReadLine()!);
                                    Console.WriteLine("\nMegjegyzés:");
                                    string megjegyzes = Console.ReadLine()!;
                                    
                                    Console.WriteLine("(1) Gyanusított (2) Tanú");
                                    
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
                                    }
                                    
                                    Person szemely = new(nev, eletkor, megjegyzes);
                                    ugykezelo.SzemelyHozzaadas(ugySzemely, szemely);
                                    adattar.Szemelyek.Add(szemely);

                                    if (fajta == "gyanusitott")
                                    {
                                        int szint;
                                        string statusz = "";
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
                                    
                                    adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"{nev} szemely letrehozasa. ({ugySzemely.Ugyazonosito} ugyhoz hozzarendelve)"));
                                    break;

                                case 2:
                                    if (adattar.Szemelyek.Count == 0)
                                    {
                                        Console.WriteLine("Nincs listázható személy.");
                                        break;
                                    } 
                                    if (adattar.Gyanusitottak.Count == 0 && adattar.Tanuk.Count != 0)
                                    {
                                        Console.WriteLine("Nincs megjeleníthető gyanúsított.\nTanúk:");
                                        foreach (var tanu in adattar.Tanuk)
                                        {
                                            Console.WriteLine(tanu);
                                        }
                                    }
                                    else if (adattar.Gyanusitottak.Count != 0 && adattar.Tanuk.Count == 0)
                                    {
                                        Console.WriteLine("Nincs megjeleníthető tanu.\nGyanusítottak:");
                                        foreach (var gyan in adattar.Gyanusitottak)
                                        {
                                            Console.WriteLine(gyan);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Gyanúsítottak:");
                                        foreach (var gyan in adattar.Gyanusitottak)
                                        {
                                            Console.WriteLine(gyan);
                                        }

                                        Console.WriteLine("Tanuk:");
                                        foreach (var tanu in adattar.Tanuk)
                                        {
                                            Console.WriteLine(tanu);
                                        }
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

                                    Case ugy = ugykezelo.Ugyvalasztas();
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
                                    ugykezelo.BizonyitekHozzaadas(ugy, bizonyitek);
                                    bizkezelo.Hozzadas(bizonyitek);
                                    adattar.Bizonyitekok.Add(bizonyitek);
                                    adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Bizonyitek {azonosito} letrehozva. ({ugy.Ugyazonosito} ugyhoz hozzarendelve)"));
                                    break;
                                
                                case 2:
                                    Case ugybizc = ugykezelo.Ugyvalasztas();
                                    
                                    if (ugybizc.Bizonyitekok.Count == 0)
                                    {
                                        Console.WriteLine("Nincsen törölhető bizonyíték.");
                                        break;
                                    }
                                    
                                    bizkezelo.Torles(ugybizc);
                                    adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Bizonyitek torolve (Ugyazonosito: {ugybizc.Ugyazonosito})"));
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
                        if (adattar.Idovonal.Count == 0)
                        {
                            Console.WriteLine("Az idovonal ures.");
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
                            Console.WriteLine("Nincs ügy.");
                            break;
                        }

                        Case ugyd = ugykezelo.Ugyvalasztas();

                        if (ugyd.Szemelyek.Count == 0)
                        {
                            Console.WriteLine("Nincs személy az ügyhöz kötve.");
                            break;
                        }
                        
                        var vangyan = donteshozo.GyanusitottakLista(ugyd);
                        donteshozo.TanuLista(ugyd);
                        
                        if (!vangyan)
                        {
                            Console.WriteLine("Nincs gyanusitott az ugyhoz kotve.");
                            break;
                        }
                        if (ugyd.Bizonyitekok.Count == 0)
                        {
                            Console.WriteLine("Nincs bizonyíték az ügyhöz kötve.");
                            break;
                        }
                        
                        List<Evidence> ugybiz = ugykezelo.UgyBizonyitekai(ugyd);
                        donteshozo.Ertekeles(ugybiz);
                        adattar.Idovonal.Add(new TimelineEvent(DateTime.Now.ToString(), $"Ugy {ugyd.Ugyazonosito} elemezve."));
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