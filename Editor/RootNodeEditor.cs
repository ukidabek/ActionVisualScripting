using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ActionVisualScripting
{
    [CustomEditor(typeof(RootAction))]
    public class RootNodeEditor : Editor
    {
        private RootAction rootAction = null;

        private void OnEnable()
        {
            rootAction = target as RootAction;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open graph"))
            {
                var instance = (CreateInstance(typeof(ActionGraphWindow)) as ActionGraphWindow);
                instance.Initialize(rootAction);
                instance.Show();
            }
        }
    }
}