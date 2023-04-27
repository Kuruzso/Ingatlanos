using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ingatlanok
{
    internal class Program
    {
        struct ingatlanStrunct
        {
            public int azonosito;
            public string cim;
            public int alapterulet;
            public int ar;
        }
        struct ugyfelStruct
        {
            public int azonosito;
            public string nev;
            public string telefonszam;
        }
        struct MAINStrunct
        {
            public int UgyfelAzonosito;
            public DateTime datum;
            public List<ingatlanStrunct> ingatlanok;
        }

        struct kiiratasilista
        {
            public List<ingatlanStrunct> ingatlanok;
            public ugyfelStruct ugyfel;
            public int cim_hosz;
            public int alapterulet_hosz;
            public int id_hosz;
        }

        static List<ugyfelStruct> ugyfelek = new List<ugyfelStruct>();
        static List<ingatlanStrunct> ingatlanok = new List<ingatlanStrunct>();
        static List<MAINStrunct> main = new List<MAINStrunct>();

        static List<int> cursorTops = new List<int>();
        static int maxview = 4;
        static void Main(string[] args)
        {
            filekezeles("");
            filekezeles("load");
            menu();
        }


        static void menu()
        {
            ConsoleKey consoleKey = default;
            while (consoleKey != ConsoleKey.Escape)
            {
                Console.Clear();
                cursorTops.Clear();
                Console.WriteLine("Fő Menü (Kilépés: Esc)  (Kiválasztás: Enter)");
                cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Felhasználói felület");
                cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Szolgáltatói felület");
                consoleKey = mozgas(consoleKey);
                if (consoleKey == ConsoleKey.Enter && cursorTops[0] == Console.CursorTop)//felhasználói felület
                {
                    //segédváltozók
                    string name = "";
                    string phoneNumber = "";
                    while (consoleKey != ConsoleKey.Escape)//felhasználó menü KÉSZ!
                    {
                        Console.Clear();
                        cursorTops.Clear();
                        Console.WriteLine("Felhasználói Menü (Kilépés: Esc)  (Kiválasztás: Enter)");
                        Console.WriteLine("Bejelentkezés:");
                        Console.WriteLine("Adja meg a felhasználó nevét vagy telefonszámát!");
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Név megadása: {0} |: ", name);
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Telefonszám megadása: {0} |: ", phoneNumber);
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Bevitel");
                        Console.WriteLine("Regisztráció:");
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Hozz létre új felhasználót");
                        Console.WriteLine("Fiók Törlése:");
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Törölje fiókját");
                        consoleKey = mozgas(consoleKey);
                        if (consoleKey == ConsoleKey.Enter && cursorTops[0] == Console.CursorTop)//név megadása KÉSZ!
                        {
                            Console.SetCursorPosition("( ) Név megadása: {0} |: ".Length + name.Length, cursorTops[0]);
                            name = Console.ReadLine();
                        }
                        else if (consoleKey == ConsoleKey.Enter && cursorTops[1] == Console.CursorTop)//telefonszám megadása KÉSZ!
                        {
                            Console.SetCursorPosition("( ) Telefonszám megadása: {0} |: ".Length + phoneNumber.Length, cursorTops[1]);
                            phoneNumber = Console.ReadLine();
                        }
                        else if (consoleKey == ConsoleKey.Enter && cursorTops[2] == Console.CursorTop)//bevitel 
                        {

                            while (consoleKey != ConsoleKey.Escape)//fő adattábla
                            {
                                kiiratasilista listamenu = MainlistaMenu(name, phoneNumber);
                                consoleKey = mozgas(consoleKey);
                                if (consoleKey == ConsoleKey.Enter && cursorTops[0] == Console.CursorTop)//al adattáblas
                                {
                                 
                                    while (consoleKey != ConsoleKey.Escape)
                                    {
                                        SecondList(listamenu);
                                        consoleKey = mozgas(consoleKey);
                                        if (consoleKey == ConsoleKey.Enter && cursorTops[1] == Console.CursorTop)//módosítás KÉSZ!
                                        {
                                            for (int i = 0; i < ugyfelek.Count; i++)
                                            {
                                                if (ugyfelek[i].azonosito == listamenu.ugyfel.azonosito)
                                                {
                                                    Console.Clear();
                                                    cursorTops.Clear();
                                                    Console.WriteLine("{0} felhasználó adatainak módosítása folyamatban", ugyfelek[i].azonosito);
                                                    ugyfelStruct ideiglenes_ugyfel = ugyfel_adatbekeres("name-number");
                                                    ideiglenes_ugyfel.azonosito = ugyfelek[i].azonosito;
                                                    ugyfelek[i] = ideiglenes_ugyfel;
                                                    filekezeles("save");
                                                }
                                            }
                                        }
                                    }
                                    consoleKey = default;
                                }
                                if (consoleKey == ConsoleKey.Enter && cursorTops[1] == Console.CursorTop)//módosítás KÉSZ!
                                {
                                    for (int i = 0; i < ugyfelek.Count; i++)
                                    {
                                        if (ugyfelek[i].azonosito == listamenu.ugyfel.azonosito)
                                        {
                                            Console.Clear();
                                            cursorTops.Clear();
                                            Console.WriteLine("{0} felhasználó adatainak módosítása folyamatban", ugyfelek[i].azonosito);
                                            ugyfelStruct ideiglenes_ugyfel = ugyfel_adatbekeres("name-number");
                                            ideiglenes_ugyfel.azonosito = ugyfelek[i].azonosito;
                                            ugyfelek[i] = ideiglenes_ugyfel;
                                            filekezeles("save");
                                        }
                                    }
                                }
                            }
                            consoleKey = default;
                        }
                        else if (consoleKey == ConsoleKey.Enter && cursorTops[3] == Console.CursorTop)//felhasználó létrehozása KÉSZ!
                        {
                            Console.Clear();
                            cursorTops.Clear();
                            ugyfelNew(ugyfel_adatbekeres("name-number"));
                            Console.Clear();
                            Console.WriteLine("Felhasználó létrehozva");
                            filekezeles("save");
                            Console.ReadLine();
                        }
                        else if (consoleKey == ConsoleKey.Enter && cursorTops[4] == Console.CursorTop)//felhasználó törlése KÉSZ!
                        {
                            Console.Clear();
                            cursorTops.Clear();
                            ugyfelDelete(ugyfel_adatbekeres("name"));
                            Console.Clear();
                            Console.WriteLine("Felhasználó Törölve");
                            filekezeles("save");
                            Console.ReadLine();
                        }
                    }
                    consoleKey = default;
                }
                else if (consoleKey == ConsoleKey.Enter && cursorTops[1] == Console.CursorTop)//szolgáltatói felület
                {
                    while (consoleKey != ConsoleKey.Escape)
                    {
                        Console.Clear();
                        cursorTops.Clear();
                        Console.WriteLine("Fő Menü (Kilépés: Esc)  (Kiválasztás: Enter)");
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Felhasználói ajánlatok bővítése");
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Ingatlan hozzáadás");
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Ingatlan törlése");
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Felhasználói adatok módosítása");
                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Ingatlan adatok módosítása");
                        consoleKey = mozgas(consoleKey);
                        if (consoleKey == ConsoleKey.Enter && cursorTops[0] == Console.CursorTop)//felhasználói ajánlatok bővítése
                        {
                            while (consoleKey != ConsoleKey.Escape)
                            {
                                string name = "";
                                string phoneNumber = "";
                                int id = 0;
                                Console.Clear();
                                cursorTops.Clear();
                                Console.WriteLine("Válassza ki az ügyfélt (1 gyorskeresés) (2 kiválasztás)");
                                cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) 1 Gyorskeresés");
                                cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) 2 Ügyfelek kilistázása");
                                consoleKey = mozgas(consoleKey);
                                if (consoleKey == ConsoleKey.Enter && cursorTops[0] == Console.CursorTop)//gyorskeresés
                                {
                                    while (consoleKey != ConsoleKey.Escape)
                                    {
                                        Console.Clear();
                                        cursorTops.Clear();
                                        Console.WriteLine("Adja meg a felhasználó nevet vagy telefonszámot vagy a felhasználó azonosítóját!");
                                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Név megadása: {0} |: ", name);
                                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Telefonszám megadása: {0} |: ", phoneNumber);
                                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) ID megadása: {0} |: ", id);
                                        cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Bevitel");
                                        consoleKey = mozgas(consoleKey);
                                        if (consoleKey == ConsoleKey.Enter && cursorTops[0] == Console.CursorTop)//nev megadása
                                        {
                                            Console.SetCursorPosition("( ) Név megadása: {0} |: ".Length + name.Length, cursorTops[0]);
                                            name = Console.ReadLine();
                                        }
                                        else if (consoleKey == ConsoleKey.Enter && cursorTops[1] == Console.CursorTop)//telefonszám megadása
                                        {
                                            Console.SetCursorPosition("( ) Telefonszám megadása: {0} |: ".Length + phoneNumber.Length, cursorTops[1]);
                                            phoneNumber = Console.ReadLine();
                                        }
                                        else if (consoleKey == ConsoleKey.Enter && cursorTops[2] == Console.CursorTop)//id megadása
                                        {
                                            Console.SetCursorPosition("( ) Azonosító megadása: {0} |: ".Length + id.ToString().Length, cursorTops[2]);
                                            id = int.Parse(Console.ReadLine());
                                        }
                                        else if (consoleKey == ConsoleKey.Enter && cursorTops[3] == Console.CursorTop)//bevitel
                                        {
                                            Console.Clear();
                                            cursorTops.Clear();
                                            Console.WriteLine("Válassza ki a felhasználót");
                                            List<int> elemek = new List<int>();
                                            int indexer = 0;
                                            for (int i = 0; i < ugyfelek.Count; i++)
                                            {
                                                if (ugyfelek[i].nev == name || ugyfelek[i].telefonszam == phoneNumber || (id != 0 && ugyfelek[i].azonosito == id))
                                                {
                                                    indexer++;
                                                    elemek.Add(i);
                                                    cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) {0} name: {1} tsz: {2}", ugyfelek[i].azonosito,  ugyfelek[i].nev, ugyfelek[i].telefonszam);
                                                }
                                            }
                                            if (indexer == 0)
                                            {
                                                cursorTops.Add(Console.CursorTop);  Console.WriteLine("( ) Nincsen ilyen felhasználó");
                                                consoleKey = mozgas(consoleKey);
                                            }
                                            else
                                            {
                                                consoleKey = mozgas(consoleKey);
                                                Console.WriteLine(ugyfelek[elemek[cursorTops.IndexOf(Console.CursorTop)]].nev);
                                                List<string> strings = new List<string>();
                                                strings.Add("1");
                                                strings.Add("2");
                                                strings.Add("3");
                                                TablaLista(strings,consoleKey);
                                                Console.ReadLine();
                                                
                                            }
                                            


                                        }
                                    }
                                    consoleKey = default;


                                }
                                else if (consoleKey == ConsoleKey.Enter && cursorTops[1] == Console.CursorTop)
                                {
                                    Console.Clear();
                                    cursorTops.Clear();
                                    consoleKey = mozgas(consoleKey);
                                }
                            }
                            consoleKey = default;
                        }
                        else if (consoleKey == ConsoleKey.Enter && cursorTops[1] == Console.CursorTop)
                        {
                            Console.Clear();
                            cursorTops.Clear();
                            ingatlanNew(ingatlan_adatbekeres("cim-alapterület-ar"));
                            Console.Clear();
                            Console.WriteLine("Ingatlan Hozzáadva");
                            filekezeles("save");
                            Console.ReadLine();
                        }
                        else if (consoleKey == ConsoleKey.Enter && cursorTops[2] == Console.CursorTop)
                        {
                            Console.Clear();
                            cursorTops.Clear();
                            ingatlanDelete(ingatlan_adatbekeres("id"));
                            Console.Clear();
                            Console.WriteLine("Ingatlan Törölve");
                            filekezeles("save");
                            Console.ReadLine();
                        }
                        else if (consoleKey == ConsoleKey.Enter && cursorTops[3] == Console.CursorTop)
                        {

                        }
                        else if (consoleKey == ConsoleKey.Enter && cursorTops[4] == Console.CursorTop)
                        {

                        }
                    }
                    consoleKey = default;
                }

            }
            consoleKey = default;
        }

        static void TablaLista(List<string> adatok,ConsoleKey consoleKey)
        {
            int pozitcio = 0;
            int pozitcio2 = 0;
            consoleKey = default;
            int kurzolpozicio = Console.CursorTop;
            while (consoleKey != ConsoleKey.Enter && consoleKey != ConsoleKey.Escape)
            {
                cursorTops.Clear();
                Console.SetCursorPosition(0,kurzolpozicio);
                Console.WriteLine("---");
                for (int i = pozitcio2; i < adatok.Count && i < pozitcio2+maxview; i++)
                {
                    Console.WriteLine(hossz(40,0," "));
                }
                Console.SetCursorPosition(0, kurzolpozicio);

                Console.WriteLine("---");
                for (int i = pozitcio2; i < adatok.Count && i < pozitcio2+maxview; i++)
                {
                    if (cursorTops.Count == pozitcio)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) "+adatok[i]);
                    if (cursorTops.Count - 1 == pozitcio)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                Console.WriteLine("---");

                Console.SetCursorPosition(1, cursorTops[pozitcio]);
                consoleKey = Console.ReadKey().Key;

                switch (consoleKey)
                {
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop != cursorTops[0])
                        {
                            pozitcio--;
                        }
                        else if (pozitcio2 > 0)
                        {
                            pozitcio2--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop != cursorTops[cursorTops.Count - 1])
                        {
                            pozitcio++;
                        }
                        else if(pozitcio2 <= cursorTops.Count+1 && pozitcio2 < cursorTops.Count)
                        {
                            pozitcio2++;
                        }
                        break;

                    case ConsoleKey.W:
                        if (Console.CursorTop != cursorTops[0])
                        {
                            pozitcio--;
                        }
                        else if (pozitcio2 > 0)
                        {
                            pozitcio2--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (Console.CursorTop != cursorTops[cursorTops.Count - 1])
                        {
                            pozitcio++;
                        }
                        else if (pozitcio2 <= cursorTops.Count + 1)
                        {
                            pozitcio2++;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        static void SecondList(kiiratasilista listamenu)
        {

            Console.Clear();
            cursorTops.Clear();

            Console.WriteLine("{0} felhasználó adatai (Kilépés: Esc)  (Kiválasztás: Enter)", listamenu.ugyfel.nev);


            Console.WriteLine("{0}", listamenu.ugyfel.telefonszam);


            Console.WriteLine("ID: {0}", listamenu.ugyfel.azonosito);


            cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Ajánlott ingatlanok");



            Console.WriteLine("--- " + "Azonosító" + hossz(listamenu.id_hosz, "Azonosító".Length, " ") + "Cím" + hossz(listamenu.cim_hosz, "Cím".Length, " ") + "Alapterület" + hossz(listamenu.alapterulet_hosz, "Alapterület".Length, " ") + "Ár");
            if (listamenu.ingatlanok.Count == 0)
            {
                Console.WriteLine("Nincs ajánlott ingatlan");
            }
            else
            {
                for (int x = listamenu.ingatlanok.Count; x < listamenu.ingatlanok.Count && x < maxview; x++)
                {
                    Console.WriteLine("( ) " + listamenu.ingatlanok[x].azonosito + hossz(listamenu.id_hosz, listamenu.ingatlanok[x].azonosito.ToString().Length, " ") + listamenu.ingatlanok[x].cim + hossz(listamenu.cim_hosz, listamenu.ingatlanok[x].cim.Length, " ") + listamenu.ingatlanok[x].alapterulet + hossz(listamenu.alapterulet_hosz, listamenu.ingatlanok[x].alapterulet.ToString().Length, " ") + listamenu.ingatlanok[x].ar);
                }
            }


            Console.WriteLine("---");

            cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Módosítás");



        }
        static kiiratasilista MainlistaMenu(string name, string phoneNumber)
        {
            kiiratasilista vege = new kiiratasilista();

            for (int i = 0; i < ugyfelek.Count; i++)
            {
                if (name == ugyfelek[i].nev || ugyfelek[i].telefonszam == phoneNumber)
                {
                    vege.ugyfel = ugyfelek[i];
                    vege.ingatlanok = new List<ingatlanStrunct>();
                    Console.Clear();
                    cursorTops.Clear();
                    Console.WriteLine("{0} felhasználó adatai (Kilépés: Esc)  (Kiválasztás: Enter)", ugyfelek[i].nev);
                    Console.WriteLine("{0}", ugyfelek[i].telefonszam);
                    Console.WriteLine("ID: {0}", ugyfelek[i].azonosito);
                    cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Ajánlott ingatlanok");
                    for (int j = 0; j < main.Count; j++)
                    {
                        if (main[i].UgyfelAzonosito == ugyfelek[i].azonosito)
                        {
                            vege.cim_hosz = 0;
                            vege.id_hosz = 0;
                            vege.alapterulet_hosz = 0;
                            Console.WriteLine("--- " + "Azonosító" + hossz(vege.id_hosz, "Azonosító".Length, " ") + "Cím" + hossz(vege.cim_hosz, "Cím".Length, " ") + "Alapterület" + hossz(vege.alapterulet_hosz, "Alapterület".Length, " ") + "Ár");
                            for (int x = 0; x < main[i].ingatlanok.Count; x++)
                            {

                                vege.ingatlanok.Add(main[i].ingatlanok[x]);
                                if (main[i].ingatlanok[x].azonosito.ToString().Length > vege.id_hosz)
                                {
                                    vege.id_hosz = main[i].ingatlanok[x].azonosito.ToString().Length;
                                }
                                if (main[i].ingatlanok[x].cim.Length > vege.cim_hosz)
                                {
                                    vege.cim_hosz = main[i].ingatlanok[x].cim.Length;
                                }
                                if (main[i].ingatlanok[x].alapterulet.ToString().Length > vege.alapterulet_hosz)
                                {
                                    vege.alapterulet_hosz = main[i].ingatlanok[x].alapterulet.ToString().Length;
                                }
                            }
                            //int x = main[i].ingatlanok.Count - pozitcio >= 4 ? pozitcio : main[i].ingatlanok.Count - 4; x < main[i].ingatlanok.Count || x < pozitcio + maxview; x++
                            for (int x = 0; x < maxview; x++)
                            {
                                Console.WriteLine("( ) " + main[i].ingatlanok[x].azonosito + hossz(vege.id_hosz, main[i].ingatlanok[x].azonosito.ToString().Length, " ") + main[i].ingatlanok[x].cim + hossz(vege.cim_hosz, main[i].ingatlanok[x].cim.Length, " ") + main[i].ingatlanok[x].alapterulet + hossz(vege.alapterulet_hosz, main[i].ingatlanok[x].alapterulet.ToString().Length, " ") + main[i].ingatlanok[x].ar);
                            }
                            Console.WriteLine("---");

                        }
                    }
                    cursorTops.Add(Console.CursorTop); Console.WriteLine("( ) Módosítás");
                }

            }
            return vege;

        }
        static ConsoleKey mozgas(ConsoleKey consoleKey)
        {
            consoleKey = default;
            Console.SetCursorPosition(1, cursorTops[0]);
            while (consoleKey != ConsoleKey.Enter && consoleKey != ConsoleKey.Escape)
            {
                consoleKey = Console.ReadKey().Key;
                switch (consoleKey)
                {
                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop != cursorTops[0])
                        {
                            Console.SetCursorPosition(1, cursorTops[cursorTops.IndexOf(Console.CursorTop) - 1]);
                        }
                        else
                        {
                            Console.SetCursorPosition(1, Console.CursorTop);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop != cursorTops[cursorTops.Count - 1])
                        {
                            Console.SetCursorPosition(1, cursorTops[cursorTops.IndexOf(Console.CursorTop) + 1]);
                        }
                        else
                        {
                            Console.SetCursorPosition(1, Console.CursorTop);
                        }
                        break;

                    case ConsoleKey.W:
                        if (Console.CursorTop != cursorTops[0])
                        {
                            Console.SetCursorPosition(1, cursorTops[cursorTops.IndexOf(Console.CursorTop) - 1]);
                        }
                        else
                        {
                            Console.SetCursorPosition(1, Console.CursorTop);
                        }
                        break;
                    case ConsoleKey.S:
                        if (Console.CursorTop != cursorTops[cursorTops.Count - 1])
                        {
                            Console.SetCursorPosition(1, cursorTops[cursorTops.IndexOf(Console.CursorTop) + 1]);
                        }
                        else
                        {
                            Console.SetCursorPosition(1, Console.CursorTop);
                        }
                        break;
                    default:
                        break;
                }
            }
            return consoleKey;
        }
        static string hossz(int hosz, int kivonando, string kitoltes)
        {
            string vege = "";
            int kulombseg = 0;
            if (hosz > kivonando)
            {
                kulombseg = hosz - kivonando;
            }
            else
            {
                kulombseg = kivonando - hosz;
            }
            for (int i = 0; i < kulombseg; i++)
            {
                vege += kitoltes;
            }
            return vege;
        }
        static ugyfelStruct ugyfel_adatbekeres(string parancs)
        {
            ugyfelStruct ugyfel = new ugyfelStruct();
            if (parancs.Contains("id"))
            {
                Console.WriteLine("adja meg fiók azonosítóját");
                ugyfel.azonosito = int.Parse(Console.ReadLine());
            }
            if (parancs.Contains("name"))
            {
                Console.WriteLine("adja meg felhasználónevét");
                ugyfel.nev = Console.ReadLine();
            }
            if (parancs.Contains("number"))
            {
                Console.WriteLine("adja meg telefonszámát");
                ugyfel.telefonszam = Console.ReadLine();
            }
            return ugyfel;
        }
        static ingatlanStrunct ingatlan_adatbekeres(string parancs)
        {

            ingatlanStrunct ingatlan = new ingatlanStrunct();
            if (parancs.Contains("id"))
            {
                Console.WriteLine("Adja meg az ingatlan azonosítóját");
                ingatlan.azonosito = int.Parse(Console.ReadLine());
            }
            if (parancs.Contains("cim"))
            {
                Console.WriteLine("Adja meg az ingatlan címét");
                ingatlan.cim = Console.ReadLine();
            }
            if (parancs.Contains("alapterület"))
            {
                Console.WriteLine("Adja meg az ingatlan alapterületét");
                ingatlan.alapterulet = int.Parse(Console.ReadLine());
            }
            if (parancs.Contains("ar"))
            {
                Console.WriteLine("Adja meg az ingatlan árát");
                ingatlan.ar = int.Parse(Console.ReadLine());
            }
            return ingatlan;
        }
        static void ugyfelDelete(ugyfelStruct ugyfel_delete)
        {
            for (int i = 0; i < ugyfelek.Count; i++)
            {
                if (ugyfelek[i].azonosito == ugyfel_delete.azonosito)
                {
                    ugyfelek.RemoveAt(i);
                }
            }
        }
        static void ugyfelNew(ugyfelStruct ugyfel_new)
        {
            int index = 0;
            bool van = true;
            int szam = 0;
            while (van)
            {
                index++;
                szam = 0;
                for (int i = 0; i < ugyfelek.Count; i++)
                {
                    if (ugyfelek[i].azonosito == index)
                    {
                        szam++;
                    }
                }
                if (szam == 0)
                {
                    van = false;
                }
            }
            ugyfel_new.azonosito = index;
            ugyfelek.Add(ugyfel_new);
        }
        static void ingatlanNew(ingatlanStrunct ingatlan_new)
        {
            int index = 0;
            bool van = true;
            int szam = 0;
            while (van)
            {
                index++;
                for (int i = 0; i < ingatlanok.Count; i++)
                {
                    if (ingatlanok[i].azonosito == index)
                    {
                        szam++;
                    }
                }
                if (szam == 0)
                {
                    van = false;
                }
            }
            ingatlan_new.azonosito = index;
            ingatlanok.Add(ingatlan_new);
        }
        static void ingatlanDelete(ingatlanStrunct ingatlan_delete)
        {
            for (int i = 0; i < ingatlanok.Count; i++)
            {
                if (ingatlanok[i].azonosito == ingatlan_delete.azonosito)
                {
                    ingatlanok.RemoveAt(i);
                }
            }
        }
        static void filekezeles(string parancs)
        {
            Console.WriteLine("mentés");
            if (parancs == "save")
            {
                StreamWriter streamWriter_ugyfelek = new StreamWriter("ugyfelek.txt");
                Console.WriteLine("ugyfelek mentése");
                for (int i = 0; i < ugyfelek.Count; i++)
                {
                    Console.WriteLine(i);
                    streamWriter_ugyfelek.WriteLine(ugyfelek[i].azonosito + "\t" + ugyfelek[i].nev + "\t" + ugyfelek[i].telefonszam);
                }
                streamWriter_ugyfelek.Close();
                Console.WriteLine("ingatlanok mentése");
                StreamWriter streamWriter_ingatlanok = new StreamWriter("ingatlanok.txt");
                for (int i = 0; i < ingatlanok.Count; i++)
                {
                    Console.WriteLine(i);
                    streamWriter_ingatlanok.WriteLine(ingatlanok[i].azonosito + "\t" + ingatlanok[i].cim + "\t" + ingatlanok[i].alapterulet + "\t" + ingatlanok[i].ar);
                }
                streamWriter_ingatlanok.Close();
                Console.WriteLine("MAIN mentése");
                StreamWriter streamWriter_MAIN = new StreamWriter("MAIN.txt");
                for (int i = 0; i < main.Count; i++)
                {

                    string szoveg = main[i].UgyfelAzonosito + "_" + main[i].datum.ToString("yy.MM.dd");
                    Console.WriteLine("" + i + "   " + szoveg);
                    streamWriter_MAIN.WriteLine(szoveg);
                    if (File.Exists(szoveg))
                    {
                        Console.WriteLine("file létrehozása");
                        StreamWriter streamWriter = new StreamWriter(szoveg);
                        for (int j = 0; j < main[i].ingatlanok.Count; j++)
                        {
                            Console.WriteLine(j);
                            streamWriter.WriteLine(main[i].ingatlanok[j].azonosito + "\t" + main[i].ingatlanok[j].cim + "\t" + main[i].ingatlanok[j].alapterulet + "\t" + main[i].ingatlanok[j].ar);
                        }
                        streamWriter.Close();
                    }
                }
                streamWriter_MAIN.Close();


            }
            else if (parancs == "load")
            {
                string[] allomanyUgyfel = File.ReadAllLines("ugyfelek.txt");

                for (int i = 0; i < allomanyUgyfel.Length; i++)
                {
                    ugyfelStruct ugyfel = new ugyfelStruct();
                    ugyfel.azonosito = int.Parse(allomanyUgyfel[i].Split('\t')[0]);
                    ugyfel.nev = allomanyUgyfel[i].Split('\t')[1];
                    ugyfel.telefonszam = allomanyUgyfel[i].Split('\t')[2];
                    ugyfelek.Add(ugyfel);
                }

                string[] alllomanyIngatlan = File.ReadAllLines("ingatlanok.txt");

                for (int i = 0; i < alllomanyIngatlan.Length; i++)
                {
                    ingatlanStrunct ingatlan = new ingatlanStrunct();
                    ingatlan.azonosito = int.Parse(alllomanyIngatlan[i].Split('\t')[0]);
                    ingatlan.cim = alllomanyIngatlan[i].Split('\t')[1];
                    ingatlan.alapterulet = int.Parse(alllomanyIngatlan[i].Split('\t')[2]);
                    ingatlan.ar = int.Parse(alllomanyIngatlan[i].Split('\t')[3]);
                    ingatlanok.Add(ingatlan);
                }

                string[] allomanyMAIN = File.ReadAllLines("MAIN.txt");

                for (int i = 0; i < allomanyMAIN.Length; i++)
                {
                    MAINStrunct ujmain = new MAINStrunct();
                    ujmain.UgyfelAzonosito = int.Parse(allomanyMAIN[i].Split('\t')[0]);
                    ujmain.datum = Convert.ToDateTime(allomanyMAIN[i].Split('\t')[1]);
                    string filename = "" + ujmain.UgyfelAzonosito + "_" + allomanyMAIN[i].Split('\t')[1];
                    if (File.Exists(filename))
                    {
                        string[] ideiglenesallomany = File.ReadAllLines(filename);
                        for (int j = 0; j < ideiglenesallomany.Length; j++)
                        {
                            ingatlanStrunct ideiglenesIngatlan = new ingatlanStrunct();
                            ideiglenesIngatlan.azonosito = int.Parse(alllomanyIngatlan[i].Split('\t')[0]);
                            ideiglenesIngatlan.cim = alllomanyIngatlan[i].Split('\t')[1];
                            ideiglenesIngatlan.alapterulet = int.Parse(alllomanyIngatlan[i].Split('\t')[2]);
                            ideiglenesIngatlan.ar = int.Parse(alllomanyIngatlan[i].Split('\t')[3]);
                            ujmain.ingatlanok.Add(ideiglenesIngatlan);
                        }
                        main.Add(ujmain);
                    }
                }
            }
            else
            {
                if (!File.Exists("ingatlanok.txt"))
                {
                    StreamWriter streamWriter = new StreamWriter("ingatlanok.txt");
                    streamWriter.Close();
                }
                if (!File.Exists("ugyfelek.txt"))
                {
                    StreamWriter streamWriter = new StreamWriter("ugyfelek.txt");
                    streamWriter.Close();
                }
                if (!File.Exists("MAIN.txt"))
                {
                    StreamWriter streamWriter = new StreamWriter("MAIN.txt");
                    streamWriter.Close();
                }
            }
        }
    }
}