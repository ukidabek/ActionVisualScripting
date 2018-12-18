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

        public void SelectAction(Vector3 position)
        {
            foreach (var item in ActionsList)
            {
                if (item.Rect.Contains(position))
                {
                    SelectedAction = item;
                    return;
                }
            }

            SelectedAction = null;
        }

        private void CrateActionList(BaseAction action, List<BaseAction> list)
        {
            if (!list.Contains(action))
                list.Add(action);

            for (int i = 0; i < action.Actions.Count; i++)
                CrateActionList(action.Actions[i], list);
        }

        public GraphWorker(BaseAction root)
        {
            _rootAction = root;
            _nodeTypes.AddRange(TypeHelper.GetDerivedTypes(typeof(BaseAction)));
            int index = _nodeTypes.IndexOf(typeof(RootAction));
            _nodeTypes.RemoveAt(index);
            //CrateActionList(root, _actionsList);
            _actionsList.AddRange(root.gameObject.GetComponentsInChildren<BaseAction>());
        }

        public void AddAction(Type type, Vector2 mousePosition, Vector2 scrollPosition)
        {
            BaseAction action =  (_rootAction.gameObject.AddComponent(type) as BaseAction);
            Rect rect = action.Rect;
            rect.position = mousePosition + scrollPosition;
            action.Rect = rect;
            _actionsList.Add(action);
        }

        public void RemoveAction(BaseAction baseAction)
        {
            if(baseAction != null)
            {
                _actionsList.Remove(baseAction);
                GameObject.DestroyImmediate(baseAction);
            }
        }
        
    }
}