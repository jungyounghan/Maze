using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Maze
{
    static class Screen
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        const int MF_BYCOMMAND = 0x00000000;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_SIZE = 0xF000;


        private static bool Map = true;
        private static readonly int Width = Console.WindowWidth;
        private static readonly int Height = Console.WindowHeight;

        public static int X
        {
            get
            {
                return Width / 2;
            }
        }

        public static int Y
        {
            get
            {
                return Height - 1;
            }
        }

        public static void SetConsolePreferences()
        {
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);
            Console.BufferHeight = Height;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static void View()
        {
            Map = !Map;
        }

        public static void Print(Player player, Block[,] blocks)
        {
            Console.SetCursorPosition(0, 0);
            ConsoleColor consoleColor = Console.ForegroundColor;
            if (Map == true)
            {
                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    for (int j = 0; j < blocks.GetLength(1); j++)
                    {
                        if ((int)player.x == j && (int)player.y == i)
                        {
                            player.Print();
                        }
                        else
                        {
                            blocks[i, j].Print();
                        }
                    }
                    Console.WriteLine();
                }
            }
            else
            {

            }
        }
    }
}