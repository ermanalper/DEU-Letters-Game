/*
    Her hücreye bir harf verdim: a, b, c, .... , h, i
Row1 = abc, Row2 = def ....

    Puan okurken önce Row1 için abc'yi, sonra Row2 için def'yi ....
en son Diagonal2 için ceg'yi okumak yerine r1 r2 ve r3 diye değişkenler uydurdum
ve her seferinde bunların içeriğini okudum. Tek fark Row1'i okurken r1 = a, r2 = b, r3 = b ;
Row2'yi okurken r1 = d, r2 = e, r3 = f ... oluyor. Böylelikle 8 kere puan okumaktan kurtulmuş olduk

    Her round sonunda puanların geldiği sekansları yazarken önceki versiyonda Row1'de seq1 varsa
.... yaz, Row3te seq2 varsa ... yaz Diagonal2'de seq7 varsa ... yaz gibi yapmıştık.
 Ama 11 puan getiren sekans ve Row1 - Diagonal2 toplam 8 durum için 11 x 8 = 88 durum yazmamız 
 gerekiyordu.
    Bu versiyonda farklı olarak sitWriter diye bir değişken string kullandım. O an okuduğu sıradan
gelen sekansa göre sitWriter içeriğini oluşturuyor. Örn: seq1 geldiyse sitWriter = "Seq1: 120 pts." ya da
seq2 geldiyse sitWriter = "Seq2 : 110 pts." gibi.
    Ardından hangi sırayı okuyorsa o sıraya ait mesaj ile sitWriter'ı birleştiriyor ve o sıraya ait
durum mesajını (sitRow1, sitColumn3 ...) oluşturuyor.
Örn: Diagonal1'in puanını okurken 7. sekans geldi: sitWriter = "Seq 7: 60 pts";
                                                   sitDiagonal1 = "Diagonal 1: " + sitWriter;
                                                   ...
                                                   (ZAMANI GELDİĞİNDE)
                                                   Console.WriteLine(sitDiagonal1);


    Puan okumada kolaylık sağlaması için asal sayıları kullandım
D = 2, E = 3, U = 5; Red = 7, Green = 11, Blue = 13 gibi.
Tam bölünüp bölünememe durumuna göre hangi sekans olduğunu belirliyorum.
Örn: Row1deki sayıların çarpımı 30 ile tam bölünüyorsa D, E, U harflerinin hepsini içeriyordur.
1001 (7x11x13) ile tam bölünüyorsa her harf farklı renktedir gibi.

    Önceki versiyonda 5 roundluk bir loop vardı ve her bir turda tahta basmak, puan okumak, 
sekansları yazmak gibi eylemler bir kez bilgisayar; bir kez de insan için
toplamda iki kez yapılıyordu. Şimdi r ve turn olarak değişkenler atadım. r: -1'den 19'a kadar değişiyor
ve turn 1'den 10'a kadar değişiyor. Böylece tahta basma, puan okuma gibi eylemlerin kodlarını sadece
bir kez yazmış olduk ve r ve turn değişkenleri sayesinde gerektiği zaman gereken eylem yapılıyor
*/
using System;
class Primenumbersdeuletters
{
    static void Main()
    {
        int myRow = 0, myColumn = 0;
        string myLetter = "", myColor = "";
        bool inputforA = false;
        int A = 0, aTemp = 0, bTemp = 0, cTemp = 0, dTemp = 0, eTemp = 0, fTemp = 0, gTemp= 0, hTemp = 0, iTemp = 0;
        while (!inputforA)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Game Mode:"); Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1.Easy"); Console.WriteLine("2.Moderate"); Console.WriteLine("3.Hard");
            Console.ForegroundColor = ConsoleColor.White;
            string? inputdifficulty = Console.ReadLine();
            if (inputdifficulty == "1") {A = 25; inputforA = true;}
            else if (inputdifficulty == "2") {A = 50; inputforA = true;}
            else if (inputdifficulty== "3") { A = 100; inputforA = true;}
            
        }
        int roundondisplay = 0,humanscore = 0, computerscore = 0, r = -1, turn = 1, a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0, i = 0;
        string turnondisplay = "", sitWriter = "", sitRow1 = "", sitRow2 = "", sitRow3 = "", sitColumn1 = "", sitColumn2 = "", sitColumn3 = "", sitDiagonal1 = "", sitDiagonal2 ="";
      
      //ŞİMDİ RASTGELE "İÇERİK" OLUŞTURUCAZ. TAHTAYA BASMIYCAZ. SADECE a : "KIRMIZI D" vs. OLUCAK
      
        Random randomint = new Random();
        for (int cellCreator = 1; cellCreator <= 9; cellCreator++)
        {
            int letterNumber = randomint.Next(3); //HANGİ HARF OLDUĞUNA KARARA VERİYORUZ
            int colorNumber = randomint.Next(3); //HANGİ RENK OLDUĞUNA
            int printedCell = 1;
            switch (letterNumber)
            {
                case 0:
                    printedCell *= 2; //D
                    break;
                case 1:
                    printedCell *= 3; // E 
                    break;
                case 2:
                    printedCell *= 5; // U
                    break;
            }
            switch (colorNumber)
            {
                case 0:
                    printedCell *= 7; //RED
                    break;
                case 1:
                    printedCell *= 11; //GREEN
                    break;
                case 2:
                    printedCell *= 13; // BLUE
                    break;
            }
            switch (cellCreator)
            {
                case 1:
                    a = printedCell;
                    break;
                case 2:
                    b = printedCell;
                    break;
                case 3:
                    c = printedCell;
                    break;
                case 4:
                    d = printedCell;
                    break;
                case 5:
                    e = printedCell;
                    break;
                case 6:
                    f = printedCell;
                    break;
                case 7:
                    g = printedCell;
                    break;
                case 8:
                    h = printedCell;
                    break;
                case 9:
                    i = printedCell;
                    break;
            }
        }
        //RASTGELE İÇERİK OLUŞTURULDU//
        while (r <= 19)
        {
            //PUAN OKUYCAZ//
            int boardscore = 0;
            bool ifRow1 = false, ifRow2 = false, ifRow3 = false, ifColumn1 = false, ifColumn2 = false, ifColumn3 = false, ifDiagonal1 = false, ifDiagonal2 = false;
            for (int eightlines = 1; eightlines <= 8; eightlines++)
            {
                sitWriter = "";
                bool seq1 = false, seq2 = false, seq3 = false, seq4 = false, seq5 = false, seq6 = false, seq7 = false, seq8 = false, seq9 = false, seq10 = false, seq11 = false;
                bool pluspoints = false, sameletters = false, deuletters = false, samecolors = false, alldifferentcolors = false, ordered = false;
                int r1 = 0, r2 = 0, r3 = 0; 

/////// Row1 için abc, Row2 için def ... 8 tane farklı okumak yerine her seferinde r1,r2 ve r3'ü
///okuyoruz. sadece hangi sırayı okuduğuna bağlı olarak r1 r2 ve r3ün içeriği değişiyor
/// örn: Row1 için r1:r2:r3 = a:b:c

                if (eightlines == 1)
                {
                    r1 = a; r2 = b ; r3 = c;
                }
                else if (eightlines == 2)
                {
                    r1 = d; r2 = e ; r3 = f;
                }
                else if (eightlines == 3)
                {
                    r1 = g; r2 = h ; r3 = i;
                }
                else if (eightlines == 4)
                {
                    r1 = a; r2 = d ; r3 = g;
                }
                else if (eightlines == 5)
                {
                    r1 = b; r2 = e ; r3 = h;
                }
                else if (eightlines == 6)
                {
                    r1 = c; r2 = f ; r3 = i;
                }
                else if (eightlines == 7)
                {
                    r1 = a; r2 = e ; r3 = i;
                }
                else if (eightlines == 8)
                {
                    r1 = c; r2 = e ; r3 = g;
                }
                int x = r1 * r2 * r3;
                if (x % 8 == 0 || x % 27 == 0 || x % 125 == 0)
                {
                    sameletters = true;
                }
                if (x % 30 == 0)
                {
                    deuletters = true;
                }
                if (deuletters && r2 % 3 == 0)
                {
                    ordered = true;
                }
                if (x % 343 == 0 || x % 1331 == 0 || x % 2197 == 0)
                {
                    samecolors = true;
                }
                if (x % 1001 == 0)
                {
                    alldifferentcolors = true;
                }
                
                if (deuletters)
                {
                    if (ordered)
                    {
                        if (samecolors)
                        {
                            boardscore += 120;
                            seq1 = true; //o satırda hangi sekans olduğunu belirliyor, 268. satırda kullanıcaz
                        }
                        else if (alldifferentcolors)
                        {
                            boardscore += 110;
                            seq2 = true;
                        }
                        else
                        {
                            boardscore += 100;
                            seq3 = true;
                        }
                    }
                    else
                    {
                        if (samecolors)
                        {
                            boardscore += 90;
                            seq4 = true;
                        }
                        else if(alldifferentcolors)
                        {
                            boardscore += 80;
                            seq5 = true;
                        }
                        else{
                            boardscore += 70;
                            seq6 = true;
                        }
                    }
                }
                else if (sameletters)
                {
                    if (samecolors)
                    {
                        boardscore += 60;
                        seq7 = true;
                    }
                    else if (alldifferentcolors)
                    {
                        boardscore += 50;
                        seq8 = true;
                    }
                    else
                    {
                        boardscore += 40;
                        seq9 = true;
                    }
                }
                else
                {
                    if(samecolors)
                    {
                        boardscore += 30;
                        seq10 = true;
                    }
                    else if (alldifferentcolors)
                    {
                        boardscore += 20;
                        seq11 = true;
                    }
                }
                if (seq1)
                {
                    sitWriter = "Seq.1 - 120 pts"; //yukarda gelen seq'e göre sitWriter içeriğini belirliyoruz
                }                                  // sonrasında hangi sırayı okuduğuna göre sitWriter'ı bir
                                                    //uzantı olarak kullanıcaz (bkz. 318. satır)
                if (seq2)
                {
                    sitWriter = "Seq.2 - 110 pts";
                }
                if (seq3)
                {
                    sitWriter = "Seq.3 - 100 pts";
                }
                if (seq4)
                {
                    sitWriter = "Seq.4 - 90 pts";
                }
                if (seq5)
                {
                    sitWriter = "Seq.5 - 80 pts";
                }
                if (seq6)
                {
                    sitWriter = "Seq.6 - 70 pts";
                }
                if (seq7)
                {
                    sitWriter = "Seq.7 - 60 pts";
                }
                if (seq8)
                {
                    sitWriter = "Seq.8 - 50 pts";
                }
                if (seq9)
                {
                    sitWriter = "Seq.9 - 40 pts";
                }
                if (seq10)
                {
                    sitWriter = "Seq.10 - 30 pts";
                }
                if (seq11)
                {
                    sitWriter = "Seq.11 - 20 pts";
                }
                if (sameletters || deuletters || samecolors || alldifferentcolors)
                {
                    pluspoints = true;
                }
                if (eightlines == 1 & pluspoints)
                {
                    ifRow1 = true;
                    sitRow1 = "Row1: " + sitWriter; // bu şekilde seq mesajlarını hazırlıyoruz
                }
                if (eightlines == 2 & pluspoints)
                {
                    ifRow2 = true;
                    sitRow2 = "Row2: " + sitWriter;
                }
                if (eightlines == 3 & pluspoints)
                {
                    ifRow3 = true;
                    sitRow3 = "Row3: " + sitWriter;
                }
                if (eightlines == 4 & pluspoints)
                {
                    ifColumn1 = true;
                    sitColumn1 = "Column1: " + sitWriter;
                }
                if (eightlines == 5 & pluspoints)
                {
                    ifColumn2 = true;
                    sitColumn2 = "Column2: " + sitWriter;
                }
                if (eightlines == 6 & pluspoints)
                {
                    ifColumn3 = true;
                    sitColumn3 = "Column3: " + sitWriter;
                }
                if (eightlines == 7 & pluspoints)
                {
                    ifDiagonal1 = true;
                    sitDiagonal1 = "Diagonal1: " + sitWriter;
                }
                if (eightlines == 8 & pluspoints)
                {
                    ifDiagonal2 = true;
                    sitDiagonal2 = "Diagonal2: " + sitWriter;
                }
            } // PUAN OKUDUK SEQLERİ HAZIRLADIK
            if (r == -1)
            {
                humanscore = boardscore + 10;
                computerscore = boardscore + 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("- Initial Board -");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            else //sekans yazma değil, hamle yapma zamanıymış
            {
                if (r % 2 == 0)
                {
                    roundondisplay = r / 4 + 1;
                    if (turn % 2 == 0)
                    {
                        turnondisplay = "Computer"; //computer ve human puanlarını yazıyor
                        computerscore -= 10;
                    }
                    else
                    {
                        turnondisplay = "Human";
                        if (r != 0)
                        {
                        humanscore -= 10;
                        }
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                     Console.WriteLine($"- Round {roundondisplay} - Turn: {turnondisplay}");
                     Console.ForegroundColor = ConsoleColor.White;
                     Console.WriteLine();
                }
                else{
                    if (turn % 2 == 0)
                    {
                        computerscore += boardscore;
                    }
                    else{
                        humanscore += boardscore;
                    }
                }
            }
            if (r % 2 == 1 && turn % 2 == 0) //AI EN İYİ HAMLEYE KARAR VERDİKTEN SONRA BU HAMLEYİ BELİRTİYOR
                                            // (HAMLE BULMA FONKSİYONU BİRAZ AŞAĞIDA)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"My row: {myRow}"); 
                Thread.Sleep(500);
                Console.WriteLine($"My column: {myColumn}");
                Thread.Sleep(500);
                Console.WriteLine("Letter: " +myLetter);
                Thread.Sleep(500);
                Console.Write("Color: ");
                if (myColor == "Red") {Console.ForegroundColor = ConsoleColor.Red;}
                else if (myColor == "Green") {Console.ForegroundColor = ConsoleColor.Green;}
                else if (myColor == "Blue") {Console.ForegroundColor = ConsoleColor.Blue;}
                Console.WriteLine(myColor);
                Thread.Sleep(500);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                

            }

            Console.WriteLine("    1   2   3");
            Console.WriteLine("  +-----------+");
            int writtenNumber = 0;
            for (int eachCell = 0; eachCell <= 8; eachCell++)
            {
                switch (eachCell)
                {
                    case 0:
                        writtenNumber = a;
                        break;
                    case 1:
                        writtenNumber = b;
                        break;
                    case 2:
                        writtenNumber = c;
                        break;
                    case 3:
                        writtenNumber = d;
                        break;
                    case 4:
                        writtenNumber = e;
                        break;
                    case 5:
                        writtenNumber = f;
                        break;
                    case 6:
                        writtenNumber = g;
                        break;
                    case 7:
                        writtenNumber = h;
                        break;
                    case 8:
                        writtenNumber = i;
                        break;
                }
                if (eachCell % 3 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(eachCell/3+1 + " | ");
                }
                if (writtenNumber % 7 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (writtenNumber % 11 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                if (writtenNumber % 2 == 0)
                    {
                        Console.Write("D ");
                    }
                    else if(writtenNumber % 3 == 0)
                    {
                        Console.Write("E ");
                    }
                    else
                    {
                        Console.Write("U ");
                    }
                if (eachCell == 0 || eachCell == 1 || eachCell == 3 || eachCell == 4 || eachCell == 6 || eachCell == 7)
                {
                    Console.Write("  ");
                }
                if (eachCell == 2)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (r == -1)
                    {Console.WriteLine("|");}
                    else{
                    Console.WriteLine($"|  H.Score: {humanscore} "); }
                    Console.WriteLine("  |           |");
                }
                if (eachCell == 5)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (r == -1) {Console.WriteLine("|");}
                    else{
                    Console.WriteLine($"|  C.Score: {computerscore} ");}
                    Console.WriteLine("  |           |");
                }
                
                
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("|");
            

            Console.WriteLine("  +-----------+");
            Console.WriteLine();
            
             //TAHTA OLUŞUMU TAMAMLANDI
            if (r == -1)
            {
                if (ifRow1)
                {
                    Console.WriteLine(sitRow1);
                }
                if (ifRow2)
                {
                    Console.WriteLine(sitRow2);
                }
                if (ifRow3)
                {
                    Console.WriteLine(sitRow3);
                }
                if (ifColumn1)
                {
                    Console.WriteLine(sitColumn1);
                }
                if (ifColumn2)
                {
                    Console.WriteLine(sitColumn2);
                }
                if (ifColumn3)
                {
                    Console.WriteLine(sitColumn3);
                }
                if (ifDiagonal1)
                {
                    Console.WriteLine(sitDiagonal1);
                }
                if (ifDiagonal2)
                {
                    Console.WriteLine(sitDiagonal2);
                }
                r++;
                Console.WriteLine();
                Console.WriteLine("Board score: " +boardscore);
                if (r == 0)
            {Console.Write("Press enter to start the game: ");
            Console.ReadLine();}
                
                
            }
            else
            {
                if (r % 2 == 0) //SEQ YAZDIRIP BAŞA MI DÖNÜCEZ YOKSA HAMLE Mİ OLUCAK? ((HAMLE))
                {
                    if (turn % 2 == 0) //SIRA KİMDE ( BİLGİSAYARDA)
                    {
                        r++;
                        int aSave = a, bSave = b, cSave = c, dSave = d, eSave = e, fSave = f, gSave = g, hSave = h, iSave = i;
                        int k = 0;
                        for (int counter = 1; counter <= A; counter++)
                        {
                            a = aSave; b = bSave; c = cSave; d = dSave; e = eSave; f = fSave; g = gSave; h = hSave; i = iSave;
                            int randomcellabouttochange = randomint.Next(9);
                            int newletter = randomint.Next(3);
                            int newcolor = randomint.Next(3);
                            int newletterValue = 0, newcolorValue = 0;
                            if (newletter == 0)
                            {
                                newletterValue = 2;
                            }
                            else if (newletter == 1)
                            {
                                newletterValue = 3;
                            }
                            else if (newletter == 2)
                            {
                                newletterValue = 5;
                            }
                            if (newcolor == 0)
                            {
                                newcolorValue = 7;
                            }
                            else if (newcolor == 1)
                            {
                                newcolorValue = 11;
                            }
                            else if (newcolor == 2)
                            {
                                newcolorValue = 13;
                            }
                            int newcellValue = newletterValue * newcolorValue;
                            switch (randomcellabouttochange)
                            {
                                case 0: //a celli
                                    a = newcellValue;
                                    break;
                                case 1: //b celli
                                    b = newcellValue;
                                    break;
                                case 2: //c celli
                                    c = newcellValue;
                                    break;
                                case 3: //d celli
                                    d = newcellValue;
                                    break;
                                case 4: //e celli
                                    e = newcellValue;
                                    break;
                                case 5: //f celli
                                    f = newcellValue;
                                    break;
                                case 6: //g celli
                                    g = newcellValue;
                                    break;
                                case 7: //h celli
                                    h = newcellValue;
                                    break;
                                case 8: //i celli
                                    i = newcellValue;
                                    break;
                            }
                            int newscore = 0;
                            for (int eightlines = 1; eightlines <= 8; eightlines++)
            {
                bool sameletters = false, deuletters = false, samecolors = false, alldifferentcolors = false, ordered = false;
                int r1 = 0, r2 = 0, r3 = 0;

                //AI'IN EN İYİ HAMLEYİ BULMASI İÇİN BURADA BİR DAHA PUAN OKUMA FONKSİYONUNU 
                // KULLANMAMIZ GEREKİYOR

                if (eightlines == 1)
                {
                    r1 = a; r2 = b ; r3 = c;
                }
                else if (eightlines == 2)
                {
                    r1 = d; r2 = e ; r3 = f;
                }
                else if (eightlines == 3)
                {
                    r1 = g; r2 = h ; r3 = i;
                }
                else if (eightlines == 4)
                {
                    r1 = a; r2 = d ; r3 = g;
                }
                else if (eightlines == 5)
                {
                    r1 = b; r2 = e ; r3 = h;
                }
                else if (eightlines == 6)
                {
                    r1 = c; r2 = f ; r3 = i;
                }
                else if (eightlines == 7)
                {
                    r1 = a; r2 = e ; r3 = i;
                }
                else if (eightlines == 8)
                {
                    r1 = c; r2 = e ; r3 = g;
                }
                int x = r1 * r2 * r3;
                if (x % 8 == 0 || x % 27 == 0 || x % 125 == 0)
                {
                    sameletters = true;
                }
                if (x % 30 == 0)
                {
                    deuletters = true;
                }
                if (deuletters && r2 % 3 == 0)
                {
                    ordered = true;
                }
                if (x % 343 == 0 || x % 1331 == 0 || x % 2197 == 0)
                {
                    samecolors = true;
                }
                if (x % 1001 == 0)
                {
                    alldifferentcolors = true;
                }
                
                if (deuletters)
                {
                    if (ordered)
                    {
                        if (samecolors)
                        {
                            newscore += 120;
                        }
                        else if (alldifferentcolors)
                        {
                            newscore += 110;
                        }
                        else
                        {
                            newscore += 100;
                        }
                    }
                    else
                    {
                        if (samecolors)
                        {
                            newscore += 90;
                        }
                        else if(alldifferentcolors)
                        {
                            newscore += 80;
                       
                        }
                        else
						{
                            newscore += 70;                     
                        }
                    }
                }
                else if (sameletters)
                {
                    if (samecolors)
                    {
                        newscore += 60;
                    }
                    else if (alldifferentcolors)
                    {
                        newscore += 50;
                    }
                    else
                    {
                        newscore += 40;
                    }
                }
                else
                {
                    if(samecolors)
                    {
                        newscore += 30;
                    }
                    else if (alldifferentcolors)
                    {
                        newscore += 20;
                    }
                }
				}
                if (newscore >= k)
                {
                    k = newscore;
                    if(!(a == aTemp && b == bTemp && c == cTemp && d == dTemp && e == eTemp && f == fTemp && g == gTemp && h == hTemp && i == iTemp))
                    {aTemp = a; bTemp = b; cTemp = c; dTemp = d; eTemp = e; fTemp = f; gTemp = g; hTemp = h; iTemp = i;}
                    switch (randomcellabouttochange)
                    {
                        case 0: //a demek
                            myRow = 1; myColumn = 1;
                            break;
                        case 1: //b demek
                            myRow = 1; myColumn = 2;
                            break;
                        case 2: //c demek
                            myRow = 1; myColumn = 3;
                            break;
                        case 3: //d demek
                            myRow = 2; myColumn = 1;
                            break;
                        case 4: //e demek
                            myRow = 2; myColumn = 2;
                            break;
                        case 5: //f demek
                            myRow = 2; myColumn = 3;
                            break;
                        case 6: //g demek
                            myRow = 3; myColumn = 1;
                            break;
                        case 7: //a demek
                            myRow = 3; myColumn = 2;
                            break;
                        case 8: //a demek
                            myRow = 3; myColumn = 3;
                            break;
                    }
                    if (newletter == 0) {myLetter = "D";}
                    else if (newletter == 1) {myLetter = "E";}
                    else {myLetter = "U";}
                    if (newcolor == 0) {myColor = "Red";}
                    else if (newcolor == 1) {myColor = "Green";}
                    else {myColor = "Blue";}
                }       
                        }
                        a = aTemp; b = bTemp; c = cTemp; d = dTemp; e = eTemp; f = fTemp; g = gTemp; h = hTemp; i = iTemp;
                        
                    }
                    else // SIRA İNSANDA
                    {
                        r++;
                        bool validHuman = false;
                        int hSaveA = a, hSaveB = b, hSaveC = c, hSaveD = d, hSaveE = e, hSaveF = f, hSaveG = g, hSaveH = h, hSaveI = i;
                        while (!validHuman)
                        {
                        a = hSaveA; hSaveB = b; hSaveC = c; hSaveD = d; hSaveE = e; hSaveF = f; hSaveG = g; hSaveH = h; hSaveI = i;
                        string? whatrow = "", whatcolumn = "", whatletter = "", whatcolor = "", whatcolorlower = "", whatletterlower= "";
                        int colorValue = 0, letterValue = 0;
                        bool validRow = false, validColumn = false, validColor = false, validLetter = false;
                        while (!validRow)
                        {
                            Console.Write("Enter row: ");
                            whatrow = Console.ReadLine();
                            if (whatrow == "1" || whatrow == "2" || whatrow == "3")
                            {
                                validRow = true;
                            }
                        }
                        while (!validColumn)
                        {
                            Console.Write("Enter column: ");
                            whatcolumn = Console.ReadLine();
                            if (whatcolumn == "1" || whatcolumn == "2" || whatcolumn == "3")
                            {
                                validColumn = true;
                            }
                        }
                        while (!validLetter)
                        {
                            Console.Write("Letter: ");
                            whatletter = Console.ReadLine();
                            whatletterlower = whatletter?.ToLower();
                            if (whatletterlower == "d")
                            {
                                letterValue = 2;
                                validLetter = true;
                            }
                            else if (whatletterlower == "e")
                            {
                                letterValue = 3;
                                validLetter = true;
                            }
                            else if (whatletterlower == "u")
                            {
                                letterValue = 5;
                                validLetter = true;
                            }
                        }
                        while (!validColor)
                        {
                            Console.Write("Color: ");
                            whatcolor = Console.ReadLine();
                            whatcolorlower = whatcolor?.ToLower();
                            if (whatcolorlower == "r")
                            {
                                colorValue = 7;
                                validColor = true;
                            }
                            else if (whatcolorlower == "g")
                            {
                                colorValue = 11;
                                validColor = true;
                            }
                            else if (whatcolorlower == "b")
                            {
                                colorValue = 13;
                                validColor = true;
                            }
                        }
                        int newCell_sValue = letterValue * colorValue;
                        if (whatrow == "1" && whatcolumn == "1") // a seçilmiş demek
                        {
                            a = newCell_sValue;
                        }
                        if (whatrow == "1" && whatcolumn == "2") // b seçilmiş demek
                        {
                            b = newCell_sValue;
                        }
                        if (whatrow == "1" && whatcolumn == "3") // c seçilmiş demek
                        {
                            c = newCell_sValue;
                        }
                        if (whatrow == "2" && whatcolumn == "1") // d seçilmiş demek
                        {
                            d = newCell_sValue;
                        }
                        if (whatrow == "2" && whatcolumn == "2") // e seçilmiş demek
                        {
                            e = newCell_sValue;
                        }
                        if (whatrow == "2" && whatcolumn == "3") // f seçilmiş demek
                        {
                            f = newCell_sValue;
                        }
                        if (whatrow == "3" && whatcolumn == "1") // g seçilmiş demek
                        {
                            g = newCell_sValue;
                        }
                        if (whatrow == "3" && whatcolumn == "2") // h seçilmiş demek
                        {
                            h = newCell_sValue;
                        }
                        if (whatrow == "3" && whatcolumn == "3") // i seçilmiş demek
                        {
                            i = newCell_sValue;
                        }
                        if(a == hSaveA && hSaveB == b && hSaveC == c && hSaveD == d && hSaveE == e && hSaveF == f && hSaveG == g && hSaveH == h && hSaveI == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error, you must make a change!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else{
                            validHuman = true;
                        }
                        }
                        
                    }
                }
                else
                {
                    if (ifRow1)
                {
                    Console.WriteLine(sitRow1);
                }
                if (ifRow2)
                {
                    Console.WriteLine(sitRow2);
                }
                if (ifRow3)
                {
                    Console.WriteLine(sitRow3);
                }
                if (ifColumn1)
                {
                    Console.WriteLine(sitColumn1);
                }
                if (ifColumn2)
                {
                    Console.WriteLine(sitColumn2);
                }
                if (ifColumn3)
                {
                    Console.WriteLine(sitColumn3);
                }
                if (ifDiagonal1)
                {
                    Console.WriteLine(sitDiagonal1);
                }
                if (ifDiagonal2)
                {
                    Console.WriteLine(sitDiagonal2);
                }
                Console.WriteLine();
                Console.WriteLine("Board score: " +boardscore);
                Console.WriteLine();
                if (r < 19){
                Console.Write("Press enter to proceed: ");
                Console.ReadLine();}
                turn++;
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine();
                if (r / 4 + 1 < 5){
                Console.WriteLine("Changing up to four cells randomly...");
                Thread.Sleep(1000);}
                Console.ForegroundColor=ConsoleColor.White;
                
                for (int randomfour = 1; randomfour <= 4; randomfour++)
                {
                    
                    int randomcell = randomint.Next(9);
                    int randomletter = randomint.Next(3);
                    int randomcolor = randomint.Next(3);
                    int letterValue = 0, colorValue = 0;
                    if (randomletter == 0)
                    {
                        letterValue = 2;
                    }
                    else if(randomletter == 1)
                    {
                        letterValue = 3;
                    }
                    else if(randomletter == 2)
                    {
                        letterValue = 5;
                    }
                    if (randomcolor == 0)
                    {
                        colorValue=7;
                    }
                    else if (randomcolor==1)
                    {
                        colorValue=11;
                    }
                    else if(randomcolor==2)
                    {
                        colorValue=13;   
                    }
                    int cellValue = colorValue * letterValue;
                    switch(randomcell)
                    {
                         case 0: 
                            a=cellValue;
                            break;
                        case 1: //b gelmiş
                            b = cellValue;
                            break;
                        case 2:
                            c = cellValue;
                            break;
                        case 3:
                            d = cellValue;
                            break;
                        case 4:
                            e = cellValue;
                            break;
                        case 5:
                            f = cellValue;
                            break;
                        case 6:
                            g = cellValue;
                            break;
                        case 7:
                            h = cellValue;
                            break;
                        case 8:
                            i = cellValue;
                            break;

                    }
                    
                }
                r++;
                
                
                
                }
            }


        }
        Console.ForegroundColor = ConsoleColor.Magenta;
        if (computerscore > humanscore)
        {
            Console.WriteLine("COMPUTER AI WON");
        }
        else if(humanscore > computerscore)
        {
            Console.WriteLine("YOU WIN!");
        }
        else
        {
            Console.WriteLine("DRAW??!?!");
        }
        Console.ReadLine();
    }
}
