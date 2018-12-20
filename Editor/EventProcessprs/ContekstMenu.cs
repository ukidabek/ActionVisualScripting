using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ActionVisualScripting
{
    internal class ContekstMenu : EventProcessor
    {
        private Event _event = null;
        private Vector2 _position = Vector3.zero;

        private GenericMenu genericMenu = new GenericMenu();
        private GenericMenu nodeMenu = new GenericMenu();

        public ContekstMenu(ActionGraphWindow actionGraph) : base(actionGraph)
        {
            foreach (var item in actionGraph.GraphWorker.NodeTypes)
                genericMenu.AddItem(
                    new GUIContent(string.Format("Add/{0}", item.Name)), 
                    false, 
                    (object data) => 
                    {
                        actionGraph.GraphWorker.AddAction(data as Type, _position);
                        actionGraph.Repaint();
                    }, 
                    item);

            nodeMenu.AddItem(
                new GUIContent("Remove"), 
                false, 
                () => 
                {
                    actionGraph.GraphWorker.RemoveAction(_position);
                    actionGraph.Repaint();
                });

            nodeMenu.AddItem(
                new GUIContent("Connect"),
                false,
                () =>
                {
                    actionGraph.GraphWorker.ConnectAction(_position);
                    actionGraph.Repaint();
                });
        }

        public override EventType EventType { get { return EventType.MouseDown; } }

        protected override bool Conditions(Event @event)
        {
            _event = @event;
            return base.Conditions(@event) && (@event.button == (int)MouseButton.Right);
        }

        protected override bool ProcessEvent(Event @event)
        {
            _position = actionGraph.ScrollPositon + @event.mousePosition;

            if (actionGraph.GraphWorker.ActionClicked(_position))
                nodeMenu.ShowAsContext();
            else
                genericMenu.ShowAsContext();

            return true;
        }
    }
}