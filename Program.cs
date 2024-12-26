using System;
using System.Threading;

namespace Maze
{
    internal class Program
    {
        enum Selection
        { 
            First,
            Second,
            Third
        }

        static readonly int MinSizeX = 3;
        static readonly int MinSizeY = 2;

        static void Main()
        {
            Screen.SetConsolePreferences();
            bool exit = false;
            int x = MinSizeX;
            int y = MinSizeY;
            Selection selection = Selection.First;
            while (exit == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    \r\n         @@@@@@                #@@@         @@@@@@       -@@@@@@@@@@@@@*        +@@@@@@#            \r\n        -@@@@@@@%             @@@@@%       @@@@@@@@     @@@         #@@@@@*  @@@@@@@@@@@@@@.        \r\n        =@@@@@@@@@           @@@@@@%      @@@@@@@@@    @@@ *@@@@    @@@@@@@@@@@@@@@@@@@@@@@@@       \r\n        +@@@@@@@@@@@        @@@@@@@#     @@@@@@@@@@+   @@@+@@@@@=  @@@@@@@@@@@@@@    @@@@@@@@       \r\n        +@@@@@@@@@@@@      @@@@@@@@-    -@@@@  @@@@@   @@@@@@@@@  %@@@@@@@@@@@@*    *@@@@@@@@       \r\n        =@@@@@@@@@@@@@    @@@@@@@@@     @@@@   @@@@@    *@@@@@@  @@@@@@. @@@@@@      @@@@@@@        \r\n        +@@@@@  -@@@@@@  @@@=.@@@@@    *@@@:   @@@@@            @@@@@@:  @@@@@@.       -%*          \r\n        *@@@@@    @@@@@@@@@@  @@@@@    @@@@    @@@@@@          @@@@@@.    @@@@@@+                   \r\n        *@@@@@     @@@@@@@@   @@@@@   @@@@     @@@@@@         @@@@@@       #@@@@@@@@@@@             \r\n        #@@@@@     +@@@@@@#   @@@@@   @@@       @@@@@        @@@@@@          @@@@@@@@@@@            \r\n        @@@@@@      @@@@@@    @@@@@@@@@@@@@@@@@@@@@@@@     =@@@@@@          @@@@@@@@@@@:            \r\n        @@@@@@       @@@@.   .@@@@@@@@@@@@@@@@@@@@@@@@    @@@@@@@       =# @@@@@@         *#        \r\n        @@@@@@.              .@@@@@  @@@        %@@@@@@  @@@@@@@       @@@@@@@@@         @@@        \r\n        @@@@@@.              :@@@@@ #@@@         @@@@@@@@@@@@@#      @@@@@ @@@@@#      .@@@@        \r\n        @@@@@@               :@@@@@ @@@@          @@@@@@@@@@@@@@@@@@@@@@@   @@@@@@@@@@@@@@@         \r\n        +@@@@@                @@@@%  @@           -@@@@#:@@@@@@@@@@@@@#       %@@@@@@@@@#           \r\n         +@@@                  .#                   -+                                              \r\n                                                                                                    \r\n                                                                                                    \r\n                                                                                                    ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
                string text = "1. 시작";
                Console.SetCursorPosition(Screen.X, Screen.Y - 6);
                Console.Write(text);
                if(selection == Selection.First)
                {
                    Console.Write("\t◀");
                }
                else
                {
                    Console.Write("\t　");
                }
                text = "2. 맵 설정";
                Console.SetCursorPosition(Screen.X, Screen.Y - 4);
                Console.Write(text);
                if (selection == Selection.Second)
                {
                    Console.Write("\t◀");
                }
                else
                {
                    Console.Write("\t　");
                }
                text = "3. 종료";
                Console.SetCursorPosition(Screen.X, Screen.Y - 2);
                Console.Write(text);
                if (selection == Selection.Third)
                {
                    Console.Write("\t◀");
                }
                else
                {
                    Console.Write("\t　");
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (selection > Selection.First)
                        {
                            selection--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (selection < Selection.Third)
                        {
                            selection++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch(selection)
                        {
                            case Selection.First:
                                Play(x, y);
                                selection = Selection.First;
                                break;
                            case Selection.Second:
                                SetMap(ref x, ref y);
                                selection = Selection.First;
                                break;
                            case Selection.Third:
                                exit = true;
                                Console.Clear();
                                Console.ResetColor();
                                break;
                        }
                        break;
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        selection = Selection.First;
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        selection = Selection.Second;
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        selection = Selection.Third;
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        Console.Clear();
                        Console.ResetColor();
                        break;
                }
            }
        }

        static void Play(int x, int y)
        {
            Player player = new Player(0, 0, 0);
            Block[,] blocks = Block.GetRandomBlocks(x, y, ref player, out Point point);
            DateTime dateTimeFrom = DateTime.Now;
            while (true)
            {
                DateTime dateTimeTo = DateTime.Now;
                double elapsedTime = (dateTimeTo - dateTimeFrom).TotalSeconds;
                dateTimeFrom = dateTimeTo;
                Screen.Print(player, blocks, point);
                if (Console.KeyAvailable == true)
                {
                    ConsoleKey consoleKey = Console.ReadKey(true).Key;
                    switch (consoleKey)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            player.Input(Player.Direction.Up, blocks, elapsedTime);
                            break;
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            player.Input(Player.Direction.Down, blocks, elapsedTime);
                            break;
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.A:
                            player.Input(Player.Direction.Left, blocks, elapsedTime);
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                            player.Input(Player.Direction.Right, blocks, elapsedTime);
                            break;
                        case ConsoleKey.M:
                            Screen.View();
                            break;
                        case ConsoleKey.Escape:
                            Screen.Reset();
                            Console.ResetColor();
                            Console.Clear();
                            return;
                    }
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(intercept: true);
                    }
                }
                if (point.x - 1 < player.x && player.x < point.x + 1 && point.y - 1 < player.y && player.y < point.y + 1)
                {
                    Screen.Print(player, blocks, point);
                    Console.ResetColor();
                    string text = "성공!!!";
                    Console.SetCursorPosition(Screen.X - (text.Length / 2), Screen.Y / 2);
                    Console.Write(text);
                    Thread.Sleep(1000);
                    break;
                }
            }
            Screen.Reset();
            Console.ResetColor();
            Console.Clear();
        }

        static void SetMap(ref int x, ref int y)
        {
            Console.Clear();
            string text = "미로의 크기를 설정하세요.";
            Console.SetCursorPosition(Screen.X - (text.Length/ 2), Screen.Y / 2);
            Console.WriteLine(text);
            Selection selection = Selection.First;
            while (true)
            {
                Console.SetCursorPosition(Screen.X, (Screen.Y / 2) + 2);
                Console.Write("1.가로:" + string.Format("{0:00}", x));
                if (selection == Selection.First)
                {
                    Console.Write("\t◀");
                }
                else
                {
                    Console.Write("\t　");
                }
                Console.SetCursorPosition(Screen.X, (Screen.Y / 2) + 4);
                Console.Write("2.세로:" + string.Format("{0:00}", y));
                if (selection == Selection.Second)
                {
                    Console.Write("\t◀");
                }
                else
                {
                    Console.Write("\t　");
                }
                Console.SetCursorPosition(Screen.X, (Screen.Y / 2) + 6);
                Console.Write("3.이전");
                if (selection == Selection.Third)
                {
                    Console.Write("\t◀");
                }
                else
                {
                    Console.Write("\t　");
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (selection > Selection.First)
                        {
                            selection--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (selection < Selection.Third)
                        {
                            selection++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        switch(selection)
                        {
                            case Selection.First:
                                if(x > MinSizeX)
                                {
                                    x--;
                                }
                                break;
                            case Selection.Second:
                                if (y > MinSizeY)
                                {
                                    y--;
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        switch (selection)
                        {
                            case Selection.First:
                                if (x < Screen.X)
                                {
                                    x++;
                                }
                                break;
                            case Selection.Second:
                                if (y < Screen.Y)
                                {
                                    y++;
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        selection = Selection.First;
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        selection = Selection.Second;
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        selection = Selection.Third;
                        break;
                    case ConsoleKey.Enter:
                        switch(selection)
                        {
                            case Selection.First:
                                if (x < Screen.X)
                                {
                                    x++;
                                }
                                break;
                            case Selection.Second:
                                if (y < Screen.Y)
                                {
                                    y++;
                                }
                                break;
                            case Selection.Third:
                                Console.Clear();
                                return;
                        }
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return;
                }
            }
        }
    }
}