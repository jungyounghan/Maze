using System;

namespace Maze
{
    internal class Program
    {

        static void Main()
        {
            Screen.SetConsolePreferences();
            Player player = new Player(0, 0, 0);
            int x = 5;
            int y = 6;
            Block[,] blocks = Block.GetRandomBlocks(x, y, ref player);
            while (true)
            {
                Screen.Print(player, blocks);
                Input(ref player, blocks);
                //최종 도착점이 되었을 때 탈출
            }
        }

        static void Input(ref Player player, Block[,] blocks)
        {
            if (Console.KeyAvailable == true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        player.Input(Player.Direction.Up, blocks);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        player.Input(Player.Direction.Down, blocks);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        player.Input(Player.Direction.Left, blocks);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        player.Input(Player.Direction.Right, blocks);
                        break;
                    case ConsoleKey.M:
                        Screen.View();
                        break;
                }
            }
        }
    }
}