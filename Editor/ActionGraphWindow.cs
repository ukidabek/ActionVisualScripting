using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ActionVisualScripting
{

    public class ActionGraphWindow : EditorWindow
    {
        private ActionsPerformer _actionsPerformer = null;
        public ActionsPerformer ActionsPerformer
        {
            get { return _actionsPerformer; }
            set
            {
                _actionsPerformer = value;
                _actionsList.Clear();
                CrateActionList(_actionsPerformer.RootAction, _actionsList);
            }
        }

        private List<BaseAction> _actionsList = new List<BaseAction>();

        private Rect _graphAreaRect;
        private Vector2 _scrollPositon;
        private Rect _graphAreaWorkRect = new Rect(Vector2.zero, new Vector2(5000, 5000));

        public ActionGraphWindow()
        {
            titleContent = new GUIContent("Actions Graph");
            _graphAreaRect.size = minSize = new Vector2(800, 600);
            _scrollPositon = new Vector2(_graphAreaWorkRect.width / 2, _graphAreaWorkRect.height / 2);
        }

        private void CrateActionList(BaseAction action, List<BaseAction> list)
        {
            if(!list.Contains(action))
                list.Add(action);

            for (int i = 0; i < action.Actions.Count; i++)
                CrateActionList(action.Actions[i], list);
        }

        private void OnGUI()
        {
            DrawGraphArea();
        }

        private void DrawGrid(Rect rect, float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(rect.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(rect.height / gridSpacing);

            Handles.BeginGUI();
            {
                Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

                for (int i = 0; i < widthDivs; i++)
                {
                    Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0), new Vector3(gridSpacing * i, rect.height, 0f));
                }

                for (int j = 0; j < heightDivs; j++)
                {
                    Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0), new Vector3(rect.width, gridSpacing * j, 0f));
                }

                Handles.color = Color.white;
            }
            Handles.EndGUI();
        }

        private void DrawGraphArea()
        {
            GUILayout.BeginArea(_graphAreaRect);
            {
                Rect scrollArea = new Rect(_graphAreaRect);
                _scrollPositon = GUI.BeginScrollView(scrollArea, _scrollPositon, _graphAreaWorkRect, false, false, GUIStyle.none, GUIStyle.none);
                {
                    DrawGrid(_graphAreaWorkRect, 10, 0.2f, Color.gray);
                    DrawGrid(_graphAreaWorkRect, 100, 0.4f, Color.gray);

                    for (int i = 0; i < _actionsList.Count; i++)
                    {
                        GUI.Box(_actionsList[i].Rect, new GUIContent(_actionsList[i].GetType().Name));
                    }
                }
                GUI.EndScrollView();
            }
            GUILayout.EndArea();
        }
    }
}