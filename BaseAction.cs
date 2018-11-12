using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionVisualScripting
{
    public abstract class BaseAction : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Rect _rect = new Rect(2500, 2500, 100, 100);
        public Rect Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }
#endif

        [SerializeField] private List<BaseAction> _actions = new List<BaseAction>();
        public List<BaseAction> Actions { get { return _actions; } }


        protected abstract void Function();

        public void Perform()
        {
            Function();

            for (int i = 0; i < Actions.Count; i++)
                Actions[i].Perform();
        }

#if UNITY_EDITOR

        public void Connect(BaseAction action)
        {
            Actions.Add(action);
        }

        public void Disconect(BaseAction action)
        {
            Actions.Remove(action);
        }

#endif
    }
}