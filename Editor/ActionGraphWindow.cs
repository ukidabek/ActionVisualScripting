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
        //public RootAction RootAction
        //{
        //    get { return _rootAction; }
        //    set
        //    {
        //        _rootAction = value;
        //        _graphWorker = new GraphWorker(_rootAction);
        //    }
        //}

        private Rect _graphAreaRect;
        public Vector2 ScrollPositon;
        private Rect _graphAreaWorkRect = new Rect(Vector2.zero, new Vector2(5000, 5000));

        private EventProcessorHandler _eventProcessorHandler = null;
        private GraphWorker _graphWorker = null;
        public GraphWorker GraphWorker { get { return _graphWorker; } }

        public ActionGraphWindow()
        {
            titleContent = new GUIContent("Actions Graph");
            _graphAreaRect.size = minSize = new Vector2(800, 600);
            ScrollPositon = new Vector2(_graphAreaWorkRect.width / 2, _graphAreaWorkRect.height / 2);

        }

        public void Initialize(RootAction rootAction)
        {
            _rootAction = rootAction;
            _graphWorker = new GraphWorker(_rootAction);
            _eventProcessorHandler = new EventProcessorHandler(this);
        }

        private void OnGUI()
        {
            ProcessEvents();
            DrawGraphArea();
        }

        private void ProcessEvents()
        {
            _eventProcessorHandler.ProcessEvents(Event.current);
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
                    for (int i = 0; i < _graphWorker.ActionsList.Count; i++)
                    {
                        GUI.backgroundColor = _graphWorker.ActionsList[i] == _graphWorker.SelectedAction ? Color.red : oldColor;
                        GUI.Box(_graphWorker.ActionsList[i].Rect, new GUIContent(_graphWorker.ActionsList[i].GetType().Name));
                    }

                    GUI.backgroundColor = oldColor;
                }
                GUI.EndScrollView();
            }
            GUILayout.EndArea();
        }
    }
}