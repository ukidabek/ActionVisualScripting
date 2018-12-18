using System;
using UnityEngine;

namespace ActionVisualScripting
{
    internal abstract class EventProcessor : IComparable
    {
        public enum MouseButton
        {
            Left = 0,
            Right = 1,
            Scroll = 2
        }
      
        public virtual int Priority { get { return 0; } }
        protected ActionGraphWindow actionGraph = null;

        public EventProcessor(ActionGraphWindow actionGraph)
        {
            this.actionGraph = actionGraph;
        }

        public abstract EventType EventType { get; }

        protected virtual bool Conditions(Event @event)
        {
            return EventType == @event.type;
        }

        public bool Process(Event @event)
        {
            if(Conditions(@event))
                return ProcessEvent(@event);

            return false;
        }

        protected abstract bool ProcessEvent(Event @event);

        public int CompareTo(object obj)
        {
            return (obj as EventProcessor).Priority.CompareTo(Priority);
        }
    }
}