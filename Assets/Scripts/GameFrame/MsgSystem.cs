using System;
using System.Collections.Generic;

namespace GameFrame
{
    public class MsgSystem
    {
        public Dictionary<string, List<Delegate>> m_eventDict = new Dictionary<string, List<Delegate>>();
        #region 事件监听
        public void AddListener(string eventName, Delegate callback)
        {
            if (!m_eventDict.ContainsKey(eventName))
            {
                m_eventDict.Add(eventName, new List<Delegate>());
            }
            m_eventDict[eventName].Add(callback);
        }

        public void AddListener(string eventName, Action callback)
        {
            AddListener(eventName, (Delegate)callback);
        }
        
        public void AddListener<T1>(string eventName, Action<T1> callback)
        {
            AddListener(eventName, (Delegate)callback);
        }

        public void AddListener<T1, T2>(string eventName, Action<T1, T2> callback)
        {
            AddListener(eventName, (Delegate)callback);
        }

        #endregion

        #region 事件分发
        public void Dispatch(string eventName)
        {
            if (!m_eventDict.ContainsKey(eventName))
            {
                return;
            }

            var eventList = m_eventDict[eventName];
            foreach (var temp in eventList)
            {
                if (temp is Action)
                {
                    var callback = temp as Action;
                    callback?.Invoke();
                }
            }
        }

        public void Dispatch<T1>(string eventName, T1 arg1)
        {
            if (!m_eventDict.ContainsKey(eventName))
            {
                return;
            }

            var eventList = m_eventDict[eventName];
            foreach (var temp in eventList)
            {
                if (temp is Action<T1>)
                {
                    var callback = temp as Action<T1>;
                    callback?.Invoke(arg1);
                }
            }
        }

        public void Dispatch<T1, T2>(string eventName, T1 arg1, T2 arg2)
        {
            if (!m_eventDict.ContainsKey(eventName))
            {
                return;
            }

            var eventList = m_eventDict[eventName];
            foreach (var temp in eventList)
            {
                if (temp is Action<T1, T2>)
                {
                    var callback = temp as Action<T1, T2>;
                    callback?.Invoke(arg1, arg2);
                }
            }
        }

        #endregion
        
    }
}