using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Sessao2
{
    class EventDetails
    {
        public string EventName;
        public DateTime GeneratedTime;
        public EventArgs Argument;
        public static List<string> Buffer = new List<string>();

        public void HandleEvent(object sender, EventArgs e)
        {
            string output = String.Format("Sender: {0} | EventName: {1} | Trigger time: {2}", ((Control) sender).Name, EventName, DateTime.Now.ToShortDateString());
            Console.WriteLine(output);
            Buffer.Add(output);
        }
    }
}