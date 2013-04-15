using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mime
{
    class Player
    {
        public Player()
        {

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        public void threadedPlay()
        {
            Thread thread = new Thread(new ThreadStart(play));
            thread.Start();
        }

        public void play()
        {
            String[] commands = File.ReadAllLines(@"C:\Users\Mohammad\Dropbox\Transfer\Text\recording.mime");
            foreach (String command in commands)
            {
                Console.WriteLine(command);
                if (command.StartsWith("MouseMove"))
                {
                    String[] cmdParams = command.Replace("MouseMove(", "").Replace(")", "").Split(',');
                    int x = int.Parse(cmdParams[0]);
                    int y = int.Parse(cmdParams[1]);
                    Cursor.Position = new Point(x, y);
                }
                if (command.StartsWith("MouseDown"))
                {
                    try
                    {
                        int MOUSEEVENTF_LEFTDOWN = 0x02;
                        int X = Cursor.Position.X;
                        int Y = Cursor.Position.Y;
                        mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
                    }
                    catch (Exception) { }
                }
                if (command.StartsWith("MouseUp"))
                {
                    try
                    {
                        int MOUSEEVENTF_LEFTUP = 0x04;
                        int X = Cursor.Position.X;
                        int Y = Cursor.Position.Y;
                        mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                    }
                    catch (Exception) { }
                }
                Thread.Sleep(10);
            }
        }
    }
}
