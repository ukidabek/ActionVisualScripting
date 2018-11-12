using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionVisualScripting
{
    [RequireComponent(typeof(RootAction))]
    public class ActionsPerformer : MonoBehaviour
    {
        [Serializable]
        public class ReferenceHandler
        {

        }

        [SerializeField] private RootAction _rootAction = null;
        public RootAction RootAction { get { return _rootAction; } }

        /// <summary>
        /// Perform actions
        /// </summary>
        public void Perform()
        {
            RootAction.Perform();
        }

        /// <summary>
        /// Perform actions on game object.
        /// </summary>
        /// <param name="gameObject">Game object that actions will be performed on.</param>
        public void Perform(GameObject gameObject)
        {
        }

        private void Reset()
        {
            _rootAction = GetComponent<RootAction>();
        }

#if UNITY_EDITOR

        public void AddAction(Type type)
        {

        }

#endif
    }
}