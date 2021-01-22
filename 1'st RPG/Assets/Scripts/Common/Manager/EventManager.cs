using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace common
{
    public class EventManager : Singleton<EventManager>
    {
        private IDictionary<string, List<Action<object>>> _eventDatabase;
        IDictionary<string, List<Action<object>>> EventDatabase =>
            _eventDatabase ?? (_eventDatabase = new Dictionary<string, List<Action<object>>>());

        public static void On(string eventName, Action<object> subscriber)
        {
            if(!Instance.EventDatabase.ContainsKey(eventName))
                Instance.EventDatabase.Add(eventName,new List<Action<object>>());
            Instance.EventDatabase[eventName].Add(subscriber);
        }

        public static void Emit(string eventName, object parameter = null)
        {
            if (Instance.EventDatabase.ContainsKey(eventName))
                foreach(var action in Instance.EventDatabase[eventName])
                {
                    action?.Invoke(parameter);
                }
        }
    }
}