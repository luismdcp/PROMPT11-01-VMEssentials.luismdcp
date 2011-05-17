using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Sessao2
{
    public partial class Form1 : Form
    {
        private SessionRecorder recorder;

        public Form1()
        {
            InitializeComponent();
            recorder = new SessionRecorder(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var formEvents = this.GetType().GetEvents();

            this.listBox1.BeginUpdate();

            foreach (var formEvent in formEvents)
            {
                this.listBox1.Items.Add(new Tuple<Control, EventInfo>(this, formEvent));
            }

            BuildEventList(this.listBox1, this.Controls);

            this.listBox1.EndUpdate();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Start Recording....");
            recorder.StartRecorder();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Stop Recording....");
            recorder.StopRecorder();
        }

        private void isel_Click(object sender, EventArgs e)
        {
            textbox.Text = ((Button)sender).Text;
        }

        private void prompt_Click(object sender, EventArgs e)
        {
            textbox.Text = ((Button)sender).Text;
        }

        private void saveEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recorder.SaveEvents(@"c:\events.log");
        }

        private void BuildEventList(ListBox lb, System.Windows.Forms.Control.ControlCollection formControls)
        {
            if (formControls.Count == 0)
            {
                return;
            }

            foreach (var formControl in formControls)
            {
                var childControlsEvents = formControl.GetType().GetEvents();

                foreach (var childControlEvent in childControlsEvents)
                {
                    lb.Items.Add(new Tuple<Control, EventInfo>((Control) formControl, childControlEvent));
                }

                BuildEventList(lb, ((Control) formControl).Controls);
            }
        }
    }
}