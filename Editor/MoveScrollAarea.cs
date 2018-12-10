using UnityEngine;

namespace ActionVisualScripting
{
    internal class MoveScrollAarea : EventProcessor
    {
        public MoveScrollAarea(ActionGraphWindow actionGraph) : base(actionGraph) {}

        public override EventType EventType { get { return EventType.MouseDrag; } }

        protected override bool Conditions(Event @event)
        {
            return base.Conditions(@event) && @event.button == 2;
        }

        protected override bool ProcessEvent(Event @event)
        {
            actionGraph.ScrollPositon -= @event.delta;
            actionGraph.Repaint();
            return true;
        }
    }
}