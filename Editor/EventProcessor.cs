using UnityEngine;

namespace ActionVisualScripting
{
    internal abstract class EventProcessor
    {
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
    }
}