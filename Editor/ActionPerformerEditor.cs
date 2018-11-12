using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ActionVisualScripting
{
    [CustomEditor(typeof(ActionsPerformer))]
    public class ActionPerformerEditor : Editor
    {
        private ActionsPerformer actionsPerformer = null;

        private void OnEnable()
        {
            actionsPerformer = target as ActionsPerformer;    
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Open graph"))
            {
                var instance = (Editor.CreateInstance(typeof(ActionGraphWindow)) as ActionGraphWindow);
                instance.ActionsPerformer = actionsPerformer;
                instance.Show();
            }
        }
    }
}