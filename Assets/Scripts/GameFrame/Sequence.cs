using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFrame
{
    public class Sequence
    {
        public class Event
        {
            public float eventTime;
            public List<Action> eventList;

            public Event(float eventTime)
            {
                this.eventTime = eventTime;
                eventList = new List<Action>();
            }

            public void Invoke()
            {
                foreach (var e in eventList)
                {
                    e?.Invoke();
                }
            }
        }
        
        // 时间
        public float seqTimer;

        // 事件原始列表
        public Dictionary<float, Event> eventOriginCache = new Dictionary<float, Event>();
        // 时间播放列表
        public Dictionary<float, Event> eventPlayCache = new Dictionary<float, Event>();

        public bool isPlaying;

        // 播放
        public void Play()
        {
            if (isPlaying)
            {
                return;
            }

            CopyEvents();
            seqTimer = 0;
            isPlaying = true;
        }

        public void CopyEvents()
        {
            eventPlayCache.Clear();
            foreach (var temp in eventOriginCache)
            {
                eventPlayCache.Add(temp.Key, temp.Value);
            }
        }

        // 添加事件
        public void AddEvent(float eventTime, Action action)
        {
            if (!eventOriginCache.ContainsKey(eventTime))
            {
                var e = new Event(eventTime);
                eventOriginCache.Add(eventTime, e);
            }
            
            eventOriginCache[eventTime].eventList.Add(action);
        }

        public List<float> delEventCache = new List<float>(); 
        public void Update(float timeDelta)
        {
            if (!isPlaying)
            {
                return;
            }

            seqTimer += timeDelta;
            delEventCache.Clear();
            foreach (var temp in eventPlayCache)
            {
                if (temp.Key >= seqTimer)
                {
                    temp.Value.Invoke();
                    delEventCache.Add(temp.Key);
                }
            }

            foreach (var eventTime in delEventCache)
            {
                eventPlayCache.Remove(eventTime);
            }

            if (eventPlayCache.Count <= 0)
            {
                isPlaying = false;
            }

        }
    }
    
}