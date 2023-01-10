using System.Runtime.InteropServices;

namespace NetCore
{
    internal class Program
    {
        public struct POINT
        {
            public int X;
            public int Y;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //Console.WriteLine("Type any key to catch cursor position or ESC to exit.");
            //do
            //{
            //    Console.WriteLine($"Keyboard X:{Console.CursorLeft} Y:{Console.CursorTop}");
            //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //    {
            //        GetCursorPos(out var mouse);
            //        Console.WriteLine($"Mouse X:{mouse.X} Y:{mouse.Y}");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Mouse cursor works only on Windows.");
            //    }
            //} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            POINT current_pos, prev_pos;
            List<POINT> coords = new List<POINT>();

            prev_pos.X = 0;
            prev_pos.Y = 0;


            Console.WriteLine("Press any key to start/stop recording mouse movements.");
            Console.ReadKey();
            do
            {
                if (GetCursorPos(out current_pos))
                {

                    if ((current_pos.X != prev_pos.X) || (current_pos.Y != prev_pos.Y))
                    {

                        Console.WriteLine("({0},{1})", current_pos.X, current_pos.Y);
                        coords.Add(current_pos);
                    }

                    prev_pos.X = current_pos.X;
                    prev_pos.Y = current_pos.Y;
                }

            } while (!Console.KeyAvailable);
            Console.ReadKey();

            Console.WriteLine("Press any key to play the recorded mouse positions.");
            Console.ReadKey();
            foreach (POINT coord in coords)
            {
                SetCursorPos(coord.X, coord.Y);
                System.Threading.Thread.Sleep(50);
                if (Console.KeyAvailable) break;
            }
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}