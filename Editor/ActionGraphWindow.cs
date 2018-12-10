using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ActionVisualScripting
{
    public class ActionGraphWindow : EditorWindow
    {
        private RootAction _rootAction = null;
        public RootAction RootAction
        {
            get { return _rootAction; }
            set
            {
                _rootAction = value;
                _actionsList.Clear();
                CrateActionList(_rootAction, _actionsList);
            }
        }

        private List<BaseAction> _actionsList = new List<BaseAction>();
        public List<BaseAction> ActionsList { get { return _actionsList; } }

        public BaseAction SelectedAction { get; internal set; }

        private Rect _graphAreaRect;
        public Vector2 ScrollPositon;
        private Rect _graphAreaWorkRect = new Rect(Vector2.zero, new Vector2(5000, 5000));

        private List<EventProcessor> _eventProcessors = new List<EventProcessor>();

        public ActionGraphWindow()
        {
            titleContent = new GUIContent("Actions Graph");
            _graphAreaRect.size = minSize = new Vector2(800, 600);
            ScrollPositon = new Vector2(_graphAreaWorkRect.width / 2, _graphAreaWorkRect.height / 2);

            _eventProcessors.Add(new SelectNode(this));
            _eventProcessors.Add(new MoveNode(this));
            _eventProcessors.Add(new MoveScrollAarea(this));
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
            ProcessEvents();
            DrawGraphArea();
        }

        private void ProcessEvents()
        {
            for (int i = 0; i < _eventProcessors.Count; i++)
                if (_eventProcessors[i].Process(Event.current))
                    break;
        }

        private void DrawGrid(Rect rect, float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(rect.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(rect.height / gridSpacing);

            Handles.BeginGUI();
            {
                Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

                for (int i = 0; i < widthDivs; i++)
                    Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0), new Vector3(gridSpacing * i, rect.height, 0f));

                for (int j = 0; j < heightDivs; j++)
                    Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0), new Vector3(rect.width, gridSpacing * j, 0f));

                Handles.color = Color.white;
            }
            Handles.EndGUI();
        }

        private void DrawGraphArea()
        {
            GUILayout.BeginArea(_graphAreaRect);
            {
                Rect scrollArea = new Rect(_graphAreaRect);
                ScrollPositon = GUI.BeginScrollView(scrollArea, ScrollPositon, _graphAreaWorkRect, false, false, GUIStyle.none, GUIStyle.none);
                {
                    DrawGrid(_graphAreaWorkRect, 10, 0.2f, Color.gray);
                    DrawGrid(_graphAreaWorkRect, 100, 0.4f, Color.gray);

                    Color oldColor = GUI.backgroundColor;
                    for (int i = 0; i < _actionsList.Count; i++)
                    {
                        GUI.backgroundColor = _actionsList[i] == SelectedAction ? Color.red : oldColor;
                        GUI.Box(_actionsList[i].Rect, new GUIContent(_actionsList[i].GetType().Name));
                    }

                    GUI.backgroundColor = oldColor;
                }
                GUI.EndScrollView();
            }
            GUILayout.EndArea();
        }

        public BaseAction SelectAction(Vector3 position)
        {
            foreach (var item in ActionsList)
                if (item.Rect.Contains(position))
                    return item;

            return null;
        }
    }
}