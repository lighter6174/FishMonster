using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public sealed class GEventAttribute : Attribute
{
    public string Type { get; private set; }

    public GEventAttribute(string type)
    {
        Type = type;
    }
}

// 标记在需要监听事件的方法上面，前提是该包含此方法的对象需要先注册
[AttributeUsage(AttributeTargets.Method)]
public sealed class GEventMethodAttribute : Attribute
{
    public string Type { get; private set; }

    public GEventMethodAttribute(string type)
    {
        Type = type;
    }
}

public interface IEvent
{
    void Handle();

    void Handle(object a);
}

public abstract class AEvent : IEvent
{
    public void Handle()
    {
        this.Run();
    }

    public void Handle(object a)
    {
        throw new NotImplementedException();
    }

    public abstract void Run();
}

public abstract class AEvent<A> : IEvent
{
    public void Handle()
    {
        throw new NotImplementedException();
    }

    public void Handle(object a)
    {
        this.Run((A)a);
    }

    public abstract void Run(A a);
}

// 以事件处理类的方式接收消息
[GEvent(EventName.Test)]
public class TestEvent : AEvent
{
    public override void Run()
    {
        Debug.Log("test");
    }
}

public delegate void EventDelegate();

public delegate void EventOneParamDelegate(object eventData);

internal class EventListener
{
    public object obj;
    public string type;
    public EventDelegate funcDelegate;
    public EventOneParamDelegate funcOneParamDelegate;

    public bool IsEqual(EventListener l, int paramNum)
    {
        bool isFuncEqual = false;
        if (paramNum == 0)
        {
            isFuncEqual = funcDelegate == l.funcDelegate;
        }
        else if (paramNum == 1)
        {
            isFuncEqual = funcOneParamDelegate == l.funcOneParamDelegate;
        }

        if (obj == l.obj && type == l.type && isFuncEqual)
        {
            return true;
        }
        return false;
    }
}

public class EventManager
{
    private readonly Dictionary<string, List<IEvent>> allEvents = new Dictionary<string, List<IEvent>>();
    private static readonly Dictionary<string, List<EventListener>> listeners = new Dictionary<string, List<EventListener>>();

    private EventManager()
    {
        Init();
    }

    public void Init()
    {
        allEvents.Clear();
        listeners.Clear();

        Type[] types = typeof(EventManager).Assembly.GetTypes();

        foreach (Type type in types)
        {
            object[] attrs = type.GetCustomAttributes(typeof(GEventAttribute), false);
            // 有点不严谨
            foreach (object attr in attrs)
            {
                GEventAttribute aEventAttribute = (GEventAttribute)attr;
                object obj = Activator.CreateInstance(type);
                IEvent iEvent = obj as IEvent;
                if (iEvent == null)
                {
                    Debug.LogError(obj.GetType().Name + " 没有继承IEvent");
                }
                else
                {
                    RegisterEvent(aEventAttribute.Type, iEvent);
                }
            }
        }
    }

    private void RegisterEvent(string eventType, IEvent e)
    {
        List<IEvent> lst = null;

        if (!allEvents.TryGetValue(eventType, out lst))
        {
            lst = new List<IEvent>();
            allEvents.Add(eventType, lst);
        }

        if (!lst.Contains(e))
        {
            lst.Add(e);
        }
    }

    public void Register(object obj)
    {
        BindingFlags flag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        MethodInfo[] methods = obj.GetType().GetMethods(flag);

        foreach (var method in methods)
        {
            object[] attrs = method.GetCustomAttributes(typeof(GEventMethodAttribute), false);

            if (attrs.Length <= 0)
                continue;

            EventDelegate funcDelegate = null;
            EventOneParamDelegate funcOneParamDelegate = null;

            int paramNum = method.GetParameters().Length;
            if (paramNum == 0)
            {
                funcDelegate = (EventDelegate)Delegate.CreateDelegate(typeof(EventDelegate), obj, method);
                if (funcDelegate == null)
                    continue;
            }
            else if (paramNum == 1)
            {
                funcOneParamDelegate = (EventOneParamDelegate)Delegate.CreateDelegate(typeof(EventOneParamDelegate), obj, method);
                if (funcOneParamDelegate == null)
                    continue;
            }
            else
            {
                continue;
            }

            // 一个处理函数可以处理多个事件
            foreach (object attr in attrs)
            {
                GEventMethodAttribute aEventAttribute = (GEventMethodAttribute)attr;
                var eventType = aEventAttribute.Type;
                EventListener listener = new EventListener
                {
                    obj = obj,
                    type = eventType,
                    funcDelegate = funcDelegate,
                    funcOneParamDelegate = funcOneParamDelegate
                };

                List<EventListener> lst = null;
                // TODO: 添加去重的逻辑
                if (!listeners.TryGetValue(eventType, out lst))
                {
                    lst = new List<EventListener>();
                    listeners.Add(eventType, lst);
                }

                bool isExist = false;
                foreach (var l in lst)
                {
                    if (l.IsEqual(listener, paramNum))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    lst.Add(listener);
                }
            }
        }
    }

    public void Unregister(object obj)
    {
        foreach (var type in listeners.Keys)
        {
            List<EventListener> lst = listeners[type];

            for (int i = 0; i < lst.Count; i++)
            {
                EventListener l = lst[i];
                if (l.obj == obj || l.obj == null)
                {
                    lst[i] = null;
                    continue;
                }
                MonoBehaviour mono = (MonoBehaviour)l.obj;
                if (mono != null && mono.gameObject == null)
                {
                    lst[i] = null;
                }
            }

            lst.RemoveAll(l => l == null);
        }
    }

    public void Send(string type)
    {
        List<IEvent> iEvents;
        if (allEvents.TryGetValue(type, out iEvents))
        {
            foreach (IEvent iEvent in iEvents)
            {
                try
                {
                    if (iEvent != null)
                    {
                        iEvent.Handle();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
        }

        List<EventListener> lst = null;
        if (listeners.TryGetValue(type, out lst))
        {
            foreach (EventListener listener in lst)
            {
                try
                {
                    listener.funcDelegate();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
        }
    }

    public void Send<A>(string type, A a)
    {
        List<IEvent> iEvents;
        if (allEvents.TryGetValue(type, out iEvents))
        {
            foreach (IEvent iEvent in iEvents)
            {
                try
                {
                    if (iEvent != null)
                    {
                        iEvent.Handle(a);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
        }

        List<EventListener> lst = null;
        if (listeners.TryGetValue(type, out lst))
        {
            foreach (EventListener listener in lst)
            {
                try
                {
                    listener.funcOneParamDelegate(a);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
        }
    }
}