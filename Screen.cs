using System;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

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

        private static bool Twinkle = false;
        private static bool Map = true;
        private static readonly int Width = Console.WindowWidth;
        private static readonly int Height = Console.WindowHeight;
        private static double Depth = 16;                   //렌더링 깊이
        private static double FieldOfView = Math.PI / 5;    //화각

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

        public static void Print(Player player, Block[,] blocks, Point point)
        {
            int blockY = blocks.GetLength(0);
            int blockX = blocks.GetLength(1);
            for (int x = 0; x < Width; x++)
            {
                double rayAngle = (player.angle - FieldOfView / 2) + x * FieldOfView / X;
                double rayX = Math.Cos(rayAngle);
                double rayY = Math.Sin(rayAngle);
                double distanceToWall = 0;
                bool hitWall = false;
                bool isBound = false;
                bool destination = false;
                Block block = null;
                while (!hitWall && distanceToWall < Depth)
                {
                    distanceToWall += 0.1;
                    int testX = (int)(player.x + rayX * distanceToWall);
                    int testY = (int)(player.y + rayY * distanceToWall);
                    if (testX < 0 || testX >= blockX || testY < 0 || testY >= blockY)
                    {
                        DivideBoundary();
                    }
                    else
                    {
                        if (testX > blockX - 1)
                        {
                            testX = blockX - 1;
                        }
                        if (testY > blockY - 1)
                        {
                            testY = blockY - 1;
                        }
                        block = blocks[testY, testX];
                        if (block != null)
                        {
                            DivideBoundary();
                        }
                    }
                    if (testX == point.x && testY == point.y)
                    {
                        destination = true;
                    }
                    void DivideBoundary()
                    {
                        hitWall = true;
                        distanceToWall *= Math.Cos(rayAngle - player.angle);
                        List<(double X, double Y)> boundsVectorsList = new List<(double X, double Y)>();
                        for (int tx = 0; tx < 2; tx++)
                        {
                            for (int ty = 0; ty < 2; ty++)
                            {
                                double vx = testX + tx - player.x;
                                double vy = testY + ty - player.y;
                                double vectorModule = Math.Sqrt(vx * vx + vy * vy);
                                double cosAngle = (rayX * vx / vectorModule) + (rayY * vy / vectorModule);
                                boundsVectorsList.Add((vectorModule, cosAngle));
                            }
                        }
                        boundsVectorsList = boundsVectorsList.OrderBy(v => v.X).ToList();
                        double boundAngle = 0.03 / distanceToWall;
                        if (Math.Acos(boundsVectorsList[0].Y) < boundAngle || Math.Acos(boundsVectorsList[1].Y) < boundAngle)
                        {
                            isBound = true;
                        }
                    }
                }
                int ceiling = (int)(Height / 2.0 - Height * FieldOfView / distanceToWall);
                int floor = Height - ceiling;                //ceiling += Height - Height;
                char wallShade;
                if (isBound)
                {
                    wallShade = '|';
                }
                else if (distanceToWall <= Depth / 4.0)
                {
                    wallShade = '\u2588';
                }
                else if (distanceToWall < Depth / 3.0)
                {
                    wallShade = '\u2593';
                }
                else if (distanceToWall < Depth / 2.0)
                {
                    wallShade = '\u2592';
                }
                else if (distanceToWall < Depth)
                {
                    wallShade = '\u2591';
                }
                else
                {
                    wallShade = ' ';
                }
                for (int y = 0; y < Height - 1; y++)
                {
                    if(Map == true && x < blockX * 2 && y < blockY)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        if (x % 2 == 0)
                        {
                            Console.SetCursorPosition(x, y);
                            if (x / 2 == (int)player.x && y == (int)player.y)
                            {
                                player.Print();
                            }
                            else if (x / 2 == point.x && y == point.y)
                            {
                                point.Print();
                            }
                            else
                            {
                                blocks[y, x / 2].Print();
                            }
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(x, y);
                        //천장
                        if(y < ceiling)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.Write(' ');
                        }
                        //벽
                        else if (y > ceiling && y <= floor)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            block.Print(wallShade);
                        }
                        //바닥
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            double bound = 1.0 - (y - Height / 2.0) / (Height / 2.0);
                            if (destination && bound > (y - Height / 2.0))
                            {
                                if (Twinkle == true)
                                {
                                    Console.BackgroundColor = ConsoleColor.Yellow;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                            }
                            if (bound < 0.005)
                            {
                                Console.Write('#');
                            }
                            else if (bound < 0.15)
                            {
                                Console.Write('x');
                            }
                            else if (bound < 0.5)
                            {
                                Console.Write('-');
                            }
                            else if (bound < 0.75)
                            {
                                Console.Write('.');
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }
                    }
                }
            }
            Twinkle = !Twinkle;
        }
    }
}