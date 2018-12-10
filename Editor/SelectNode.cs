using UnityEngine;

namespace ActionVisualScripting
{
    internal class SelectNode : EventProcessor
    {
        private Vector2 _position = Vector3.zero;

        public SelectNode(ActionGraphWindow actionGraph) : base(actionGraph) {}

        public override EventType EventType { get { return EventType.MouseDown; } }

        protected override bool Conditions(Event @event)
        {
            return base.Conditions(@event) && (@event.button == 0 || @event.button == 1);
        }

        protected override bool ProcessEvent(Event @event)
        {
            _position = actionGraph.ScrollPositon + @event.mousePosition;
            BaseAction selectedAction = actionGraph.SelectAction(_position);

            if(selectedAction != actionGraph.SelectedAction)
            {
                actionGraph.SelectedAction = selectedAction;
                actionGraph.Repaint();
            }

            return actionGraph.SelectedAction != null;
        }
    }
}