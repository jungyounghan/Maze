using System;

namespace Maze
{
    internal class Program
    {
        static void Main()
        {
            Screen.SetConsolePreferences();
            Player player = new Player(0, 0, 0);
            int x = 10;
            int y = 19;
            Block[,] blocks = Block.GetRandomBlocks(x, y, ref player, out Point point);
            DateTime dateTimeFrom = DateTime.Now;
            while (true)
            {
                DateTime dateTimeTo = DateTime.Now;
                double elapsedTime = (dateTimeTo - dateTimeFrom).TotalSeconds;
                dateTimeFrom = dateTimeTo;
                Screen.Print(player, blocks, point);
                Input(ref player, blocks, elapsedTime);
                //최종 도착점이 되었을 때 탈출
            }
        }

        static void Input(ref Player player, Block[,] blocks, double elapsedTime)
        {
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
                }
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(intercept: true);
                }
            }
        }
    }
}