using System;
using System.Collections.Generic;
using UnityEngine;

namespace ActionVisualScripting
{
    public class EventProcessorHandler
    {
        private List<EventProcessor> _eventProcessors = new List<EventProcessor>();

        public EventProcessorHandler(ActionGraphWindow actionGraphWindow)
        {
            var types = TypeHelper.GetDerivedTypes(typeof(EventProcessor));
            object[] args = { actionGraphWindow };

            foreach (var type in types)
                _eventProcessors.Add((EventProcessor)Activator.CreateInstance(type, args));

            _eventProcessors.Sort();
        }

        public void ProcessEvents(Event currentEvent)
        {
            for (int i = 0; i < _eventProcessors.Count; i++)
                if (_eventProcessors[i].Process(currentEvent))
                    break;
        }
    }
}