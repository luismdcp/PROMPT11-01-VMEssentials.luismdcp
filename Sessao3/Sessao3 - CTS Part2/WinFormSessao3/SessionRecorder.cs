using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Sessao2
{
    class SessionRecorder
    {
        public EventInfo[] formEvents;
        public Form form;
        public StringBuilder buffer;

        public List<Tuple<Object, Delegate, EventInfo>> eventsData;

        public SessionRecorder(Form form)
        {
            this.form = form;
            formEvents = form.GetType().GetEvents();
            buffer = new StringBuilder();
            eventsData = new List<Tuple<Object, Delegate, EventInfo>>();
        }
    
        public void StartRecorder()
        {
            var listBox = form.Controls.Find("listBox1", true);
            var items = ((ListBox) listBox[0]).SelectedItems;

            if (items.Count == 0)
            {
                foreach (var formEvent in formEvents)
                {
                    var eventHandlerType = formEvent.EventHandlerType;
                    var methodInf = typeof(EventDetails).GetMethod("HandleEvent");

                    EventDetails details = new EventDetails
                    {
                        EventName = formEvent.Name
                    };

                    Delegate handler = Delegate.CreateDelegate(eventHandlerType, details, methodInf);
                    formEvent.AddEventHandler(form, handler);
                    eventsData.Add(new Tuple<Object, Delegate, EventInfo>(form, handler, formEvent));
                }

                BuildControlsEvents(form.Controls);
            }
            else
            {
                foreach (var item in items)
                {
                    var tuple = (Tuple<Control, EventInfo>) item;
                    var eventHandlerType = tuple.Item2.EventHandlerType;
                    var methodInf = typeof(EventDetails).GetMethod("HandleEvent");

                    EventDetails childDetails = new EventDetails
                                                    {
                                                        EventName = tuple.Item1.Name
                                                    };

                    Delegate handler = Delegate.CreateDelegate(eventHandlerType, childDetails, methodInf);
                    tuple.Item2.AddEventHandler(tuple.Item1, handler);
                    eventsData.Add(new Tuple<Object, Delegate, EventInfo>(tuple.Item1, handler, tuple.Item2));
                }
            }
        }

        public void StopRecorder()
        {
            foreach (var tuple in eventsData)
            {
                var eventInfo = tuple.Item3;
                eventInfo.RemoveEventHandler(tuple.Item1, tuple.Item2);
            }

            eventsData.Clear();
        }

        public void SaveEvents(string fileName)
        {
            TextWriter writer = new StreamWriter(fileName);

            foreach (var line in EventDetails.Buffer)
            {
                writer.WriteLine(line);
            }

            writer.Close();
            EventDetails.Buffer.Clear();
        }

        private void BuildControlsEvents(System.Windows.Forms.Control.ControlCollection formControls)
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
                    var eventHandlerType = childControlEvent.EventHandlerType;
                    var methodInf = typeof(EventDetails).GetMethod("HandleEvent");

                    EventDetails childDetails = new EventDetails
                    {
                        EventName = childControlEvent.Name
                    };

                    Delegate handler = Delegate.CreateDelegate(eventHandlerType, childDetails, methodInf);
                    childControlEvent.AddEventHandler(formControl, handler);
                    eventsData.Add(new Tuple<Object, Delegate, EventInfo>(formControl, handler, childControlEvent));
                }

                BuildControlsEvents(((Control) formControl).Controls);
            }
        }
    }
}