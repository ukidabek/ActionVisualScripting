using UnityEngine;

namespace ActionVisualScripting
{
    internal class SelectNode : EventProcessor
    {
        private Vector2 _position = Vector3.zero;

        public SelectNode(ActionGraphWindow actionGraph) : base(actionGraph) {}

        public override EventType EventType { get { return EventType.MouseDown; } }

        public override int Priority { get { return 1000; } }

        protected override bool Conditions(Event @event)
        {
            return base.Conditions(@event) && (@event.button == (int)MouseButton.Left);// || @event.button == (int)MouseButton.Right);
        }

        protected override bool ProcessEvent(Event @event)
        {
            _position = actionGraph.ScrollPositon + @event.mousePosition;
           actionGraph.GraphWorker.SelectAction(_position);

            //if(selectedAction != actionGraph.SelectedAction)
            //{
            //    actionGraph.SelectedAction = selectedAction;
                actionGraph.Repaint();
            //}

            return false;//actionGraph.GraphWorker.SelectedAction != null;
        }
    }
}