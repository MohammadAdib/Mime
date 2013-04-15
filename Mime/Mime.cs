using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

namespace Mime
{
    class Mime
    {
        private Boolean running = false;
        private List<String> recording = new List<String>();

        public Mime()
        {

            GlobalHooker hooker = new GlobalHooker();
            MouseHookListener listener = new MouseHookListener(hooker);
            listener.Start();
            listener.MouseDown += new MouseEventHandler(MouseDown);
            listener.MouseUp += new MouseEventHandler(MouseUp);
        }

        public void Start()
        {
            running = true;
            Thread thread = new Thread(new ThreadStart(Record));
            thread.Start();
        }

        public Boolean getStatus()
        {
            return running;
        }

        private void Record()
        {
            while (running)
            {
                int x = Cursor.Position.X;
                int y = Cursor.Position.Y;
                Console.WriteLine("MouseMove(" + x + "," + y + ")");
                recording.Add("MouseMove(" + x + "," + y + ")");
                Thread.Sleep(10);
            }
        }

        public String[] GetRecording()
        {
            return recording.ToArray();
        }

        public void Stop()
        {
            running = false;
        }

        public void MouseDown(object sender, MouseEventArgs args)
        {
            Console.WriteLine("MouseDown(" + Cursor.Position.X + "," + Cursor.Position.Y + ")");
            recording.Add("MouseDown(" + Cursor.Position.X + "," + Cursor.Position.Y + ")");
        }

        public void MouseUp(object sender, MouseEventArgs args)
        {
            Console.WriteLine("MouseUp(" + Cursor.Position.X + "," + Cursor.Position.Y + ")");
            recording.Add("MouseUp(" + Cursor.Position.X + "," + Cursor.Position.Y + ")");
        }
    }
}
