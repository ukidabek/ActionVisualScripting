using UnityEngine;

namespace ActionVisualScripting
{
    internal class MoveNode : EventProcessor
    {
        public MoveNode(ActionGraphWindow actionGraph) : base(actionGraph) {}

        public override EventType EventType { get { return EventType.MouseDrag; } }

        public override int Priority { get { return 500; } }

        protected override bool Conditions(Event @event)
        {
            return base.Conditions(@event) && 
                @event.button == (int)MouseButton.Left && 
                actionGraph.GraphWorker.SelectedAction != null && 
                actionGraph.GraphWorker.SelectedAction.Rect.Contains(actionGraph.ScrollPositon + @event.mousePosition);
        }

        protected override bool ProcessEvent(Event @event)
        {
            Rect rect = actionGraph.GraphWorker.SelectedAction.Rect;
            rect.position += @event.delta;
            actionGraph.GraphWorker.SelectedAction.Rect = rect;
            actionGraph.Repaint();
            return true;
        }
    }
}