using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionVisualScripting
{
    public class GraphWorker
    {
        private BaseAction _rootAction = null;
        private List<BaseAction> _actionsList = new List<BaseAction>();
        public List<BaseAction> ActionsList { get { return _actionsList; } }

        public BaseAction SelectedAction { get; internal set; }

        private List<Type> _nodeTypes = new List<Type>();
        public List<Type> NodeTypes { get { return _nodeTypes; } }

        private BaseAction _actionA = null;
        public BaseAction ActionA { get { return _actionA; } }

        private BaseAction _actionB = null;
        public BaseAction ActionB { get { return _actionB; } }

        public bool ActionClicked(Vector3 position)
        {
            return FindAction(position) != null;
        }

        public BaseAction FindAction(Vector3 position)
        {
            foreach (var item in ActionsList)
                if (item.Rect.Contains(position))
                    return item;

            return null;
        }

        public void SelectAction(Vector3 position)
        {
            SelectedAction = FindAction(position);
        }

        public GraphWorker(BaseAction root)
        {
            _rootAction = root;
            _nodeTypes.AddRange(TypeHelper.GetDerivedTypes(typeof(BaseAction)));
            int index = _nodeTypes.IndexOf(typeof(RootAction));
            _nodeTypes.RemoveAt(index);
            _actionsList.AddRange(root.gameObject.GetComponentsInChildren<BaseAction>());
        }

        public void AddAction(Type type, Vector2 position)
        {
            BaseAction action =  (_rootAction.gameObject.AddComponent(type) as BaseAction);
            Rect rect = action.Rect;
            rect.position = position;
            action.Rect = rect;
            _actionsList.Add(action);
        }

        public void RemoveAction(Vector2 position)
        {
            BaseAction baseAction = FindAction(position);
            if(baseAction != null && !(baseAction is RootAction))
            {
                _actionsList.Remove(baseAction);
                GameObject.DestroyImmediate(baseAction);
            }
        }

        public void ConnectAction(Vector2 position)
        {
            if (_actionA == null)
                _actionA = FindAction(position);
            else
                _actionB = FindAction(position);

            if(_actionA != null && _actionB != null)
            {
                _actionA.Actions.Add(_actionB);
                ClearPair();
            }
        }

        public void ClearPair()
        {
            _actionA = _actionB = null;
        }
    }
}