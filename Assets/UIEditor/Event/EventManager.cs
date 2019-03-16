/*
author： Lai Zhang Yin，
description ： If you have any question, please add QQ/Wechat : 782966734
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EventManager
{
    public static Dictionary<string, Delegate> eventList = new Dictionary<string, Delegate>();//用来遍历的

    private static void ThrowEvenException(string msg)
    {
        throw new Exception(msg);
    }
    private static bool CheckAddEvent(string eventType, Delegate Addinghandler)
    {
        if (!eventList.ContainsKey(eventType))
        {
            eventList.Add(eventType, null);
        }
        Delegate DeleList = eventList[eventType];
        if (DeleList != null && DeleList.GetType() != Addinghandler.GetType())
        {
            ThrowEvenException(string.Format("add event error:{0},must:{1},you add:{2}", eventType, DeleList.GetType(), Addinghandler.GetType()));
            return false;
        }
        if (DeleList != null)
        {
            Delegate[] list = DeleList.GetInvocationList();
            if (Array.IndexOf(list, Addinghandler) != -1)
            {
                return false;
            }
        }
        return true;
    }
    private static bool CheckRemove(string eventType, Delegate delEvent)
    {
        bool result;
        if (!eventList.ContainsKey(eventType))
        {
            result = false;
        }
        else
        {
            Delegate EventObj = eventList[eventType];
            if (EventObj != null && EventObj.GetType() != delEvent.GetType())
            {
                ThrowEvenException(string.Format("del event fail {0}, Current type is {1}, adding type is {2}.", eventType, delEvent.GetType(), EventObj.GetType()));
            }
            result = true;
        }
        return result;
    }
    private static void RemoveEvent(string eventType)
    {
        if (eventList.ContainsKey(eventType) && eventList[eventType] == null)
        {
            eventList.Remove(eventType);
        }
    }

    public static void AddEventListener(string eventType, Action handler)
    {
        bool isCanAdd = CheckAddEvent(eventType, handler);
        if (isCanAdd)
        {
            eventList[eventType] = (Action)Delegate.Combine((Action)eventList[eventType], handler);
        }
    }
    public static void AddEventListener<T>(string eventType, Action<T> handler)
    {
        bool isCanAdd = CheckAddEvent(eventType, handler);
        if (isCanAdd)
        {
            eventList[eventType] = (Action<T>)Delegate.Combine((Action<T>)eventList[eventType], handler);
        }
    }
    public static void AddEventListener<T, U>(string eventType, Action<T, U> handler)
    {
        bool isCanAdd = CheckAddEvent(eventType, handler);
        if (isCanAdd)
        {
            eventList[eventType] = (Action<T, U>)Delegate.Combine((Action<T, U>)eventList[eventType], handler);
        }
    }
    public static void AddEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
    {
        bool isCanAdd = CheckAddEvent(eventType, handler);
        if (isCanAdd)
        {
            eventList[eventType] = (Action<T, U, V>)Delegate.Combine((Action<T, U, V>)eventList[eventType], handler);
        }
    }
    public static void AddEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
    {
        bool isCanAdd = CheckAddEvent(eventType, handler);
        if (isCanAdd)
        {
            eventList[eventType] = (Action<T, U, V, W>)Delegate.Combine((Action<T, U, V, W>)eventList[eventType], handler);
        }
    }

    public static void RemoveEventListener(string eventType, Action handler)
    {
        if (CheckRemove(eventType, handler))
        {
            eventList[eventType] = (Action)Delegate.Remove((Action)eventList[eventType], handler);
            RemoveEvent(eventType);
        }
    }
    public static void RemoveEventListener<T>(string eventType, Action<T> handler)
    {
        if (CheckRemove(eventType, handler))
        {
            eventList[eventType] = (Action<T>)Delegate.Remove((Action<T>)eventList[eventType], handler);
            RemoveEvent(eventType);
        }
    }
    public static void RemoveEventListener<T, U>(string eventType, Action<T, U> handler)
    {
        if (CheckRemove(eventType, handler))
        {
            eventList[eventType] = (Action<T, U>)Delegate.Remove((Action<T, U>)eventList[eventType], handler);
            RemoveEvent(eventType);
        }
    }
    public static void RemoveEventListener<T, U, V>(string eventType, Action<T, U, V> handler)
    {
        if (CheckRemove(eventType, handler))
        {
            eventList[eventType] = (Action<T, U, V>)Delegate.Remove((Action<T, U, V>)eventList[eventType], handler);
            RemoveEvent(eventType);
        }
    }
    public static void RemoveEventListener<T, U, V, W>(string eventType, Action<T, U, V, W> handler)
    {
        if (CheckRemove(eventType, handler))
        {
            eventList[eventType] = (Action<T, U, V, W>)Delegate.Remove((Action<T, U, V, W>)eventList[eventType], handler);
            RemoveEvent(eventType);
        }
    }

    public static void Dispatch(string eventType)
    {
        Delegate delObj;
        if (eventList.TryGetValue(eventType, out delObj))
        {
            Delegate[] invocationList = delObj.GetInvocationList();
            for (int i = 0; i < invocationList.Length; i++)
            {
                Action action = invocationList[i] as Action;
                if (action == null)
                {
                    ThrowEvenException(string.Format("dispatch {0} error: not match.", eventType));
                }
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex, null);
                }
            }
        }
    }
    public static void Dispatch<T>(string eventType, T arg1)
    {
        Action<Delegate> ac = (Delegate eventObj) =>
        {
            Action<T> action = eventObj as Action<T>;
            if (action == null)
            {
                ThrowEvenException(string.Format("Dispatch {0} error: match listener not found.", eventType));
            }
            try
            {
                action(arg1);
            }
            catch (Exception ex)
            {
                Debug.Log(ex, null);
            }
        };
        GetMatchAction(eventType, ac);
    }
    public static void Dispatch<T, U>(string eventType, T arg1, U arg2)
    {
        Action<Delegate> ac = (Delegate eventObj) =>
        {
            Action<T, U> action = eventObj as Action<T, U>;
            if (action == null)
            {
                ThrowEvenException(string.Format("Dispatch {0} error: match listener not found.", eventType));
            }
            try
            {
                action(arg1, arg2);
            }
            catch (Exception ex)
            {
                Debug.Log(ex, null);
            }
        };
        GetMatchAction(eventType, ac);
    }
    public static void Dispatch<T, U, V>(string eventType, T arg1, U arg2, V arg3)
    {
        Action<Delegate> ac = (Delegate eventObj) =>
        {
            Action<T, U, V> action = eventObj as Action<T, U, V>;
            if (action == null)
            {
                ThrowEvenException(string.Format("Dispatch {0} error: match listener not found.", eventType));
            }
            try
            {
                action(arg1, arg2, arg3);
            }
            catch (Exception ex)
            {
                Debug.Log(ex, null);
            }
        };
        GetMatchAction(eventType, ac);
    }
    public static void Dispatch<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
    {
        Action<Delegate> ac = (Delegate eventObj) =>
        {
            Action<T, U, V, W> action = eventObj as Action<T, U, V, W>;
            if (action == null)
            {
                ThrowEvenException(string.Format("Dispatch {0} error: match listener not found.", eventType));
            }
            try
            {
                action(arg1, arg2, arg3, arg4);
            }
            catch (Exception ex)
            {
                Debug.Log(ex, null);
            }
        };
        GetMatchAction(eventType, ac);
    }

    private static void GetMatchAction(string eventType, Action<Delegate> condition)
    {
        Delegate delObj;
        if (eventList.TryGetValue(eventType, out delObj))
        {
            Delegate[] eventList = delObj.GetInvocationList();
            for (int i = 0; i < eventList.Length; i++)
            {
                condition(eventList[i]);
            }
        }
    }
}

/*
author： Lai Zhang Yin，
description ： If you have any question, please add QQ/Wechat : 782966734
*/
