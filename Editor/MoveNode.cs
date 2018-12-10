using UnityEngine;

namespace ActionVisualScripting
{
    internal class MoveNode : EventProcessor
    {
        public MoveNode(ActionGraphWindow actionGraph) : base(actionGraph) {}

        public override EventType EventType { get { return EventType.MouseDrag; } }

        protected override bool Conditions(Event @event)
        {
            return base.Conditions(@event) && 
                @event.button == 0 && 
                actionGraph.SelectedAction != null && 
                actionGraph.SelectedAction.Rect.Contains(actionGraph.ScrollPositon + @event.mousePosition);
        }

        protected override bool ProcessEvent(Event @event)
        {
            Rect rect = actionGraph.SelectedAction.Rect;
            rect.position += @event.delta;
            actionGraph.SelectedAction.Rect = rect;
            actionGraph.Repaint();
            return true;
        }
    }
}