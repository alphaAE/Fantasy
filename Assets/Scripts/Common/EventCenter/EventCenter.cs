using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter {
    private static Dictionary<EventType, Delegate> _eventTable = new Dictionary<EventType, Delegate>();

    //添加监听
    private static void OnListenerAdding(EventType eventType, Delegate @delegate) {
        if (!_eventTable.ContainsKey(eventType)) {
            _eventTable.Add(eventType, null);
        }

        Delegate d = _eventTable[eventType];
        if (d != null && d.GetType() != @delegate.GetType()) {
            throw new Exception($"尝试为事件{eventType}添加不同类型的委托：当前事件委托类型是{d.GetType()}，添加事件委托类型是{@delegate.GetType()}");
        }
    }

    public static void AddListener(EventType eventType, CallBack callBack) {
        OnListenerAdding(eventType, callBack);
        _eventTable[eventType] = (CallBack)_eventTable[eventType] + callBack;
    }

    public static void AddListener<T>(EventType eventType, CallBack<T> callBack) {
        OnListenerAdding(eventType, callBack);
        _eventTable[eventType] = (CallBack<T>)_eventTable[eventType] + callBack;
    }

    public static void AddListener<T1, T2>(EventType eventType, CallBack<T1, T2> callBack) {
        OnListenerAdding(eventType, callBack);
        _eventTable[eventType] = (CallBack<T1, T2>)_eventTable[eventType] + callBack;
    }

    public static void AddListener<T1, T2, T3>(EventType eventType, CallBack<T1, T2, T3> callBack) {
        OnListenerAdding(eventType, callBack);
        _eventTable[eventType] = (CallBack<T1, T2, T3>)_eventTable[eventType] + callBack;
    }

    public static void AddListener<T1, T2, T3, T4>(EventType eventType, CallBack<T1, T2, T3, T4> callBack) {
        OnListenerAdding(eventType, callBack);
        _eventTable[eventType] = (CallBack<T1, T2, T3, T4>)_eventTable[eventType] + callBack;
    }

    public static void AddListener<T1, T2, T3, T4, T5>(EventType eventType, CallBack<T1, T2, T3, T4, T5> callBack) {
        OnListenerAdding(eventType, callBack);
        _eventTable[eventType] = (CallBack<T1, T2, T3, T4, T5>)_eventTable[eventType] + callBack;
    }

    //移除监听
    private static void OnListenerRemoving(EventType eventType, Delegate @delegate) {
        if (_eventTable.ContainsKey(eventType)) {
            Delegate d = _eventTable[eventType];
            if (d is null) {
                throw new Exception($"移除监听错误：事件{eventType}没有对应委托");
            }
            else if (d.GetType() != @delegate.GetType()) {
                throw new Exception(
                    $"移除监听错误：尝试为事件{eventType}移除不同类型的委托，存在的委托类型为{d.GetType()}，要移除的委托类型为{@delegate.GetType()}");
            }
        }
        else {
            throw new Exception($"移除监听错误：事件{eventType}不存在");
        }
    }

    private static void OnListenerRemoved(EventType eventType) {
        if (_eventTable[eventType] is null) {
            _eventTable.Remove(eventType);
        }
    }

    public static void RemoveListener(EventType eventType, CallBack callBack) {
        OnListenerRemoving(eventType, callBack);
        _eventTable[eventType] = (CallBack)_eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    public static void RemoveListener<T>(EventType eventType, CallBack<T> callBack) {
        OnListenerRemoving(eventType, callBack);
        _eventTable[eventType] = (CallBack<T>)_eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    public static void RemoveListener<T1, T2>(EventType eventType, CallBack<T1, T2> callBack) {
        OnListenerRemoving(eventType, callBack);
        _eventTable[eventType] = (CallBack<T1, T2>)_eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    public static void RemoveListener<T1, T2, T3>(EventType eventType, CallBack<T1, T2, T3> callBack) {
        OnListenerRemoving(eventType, callBack);
        _eventTable[eventType] = (CallBack<T1, T2, T3>)_eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    public static void RemoveListener<T1, T2, T3, T4>(EventType eventType, CallBack<T1, T2, T3, T4> callBack) {
        OnListenerRemoving(eventType, callBack);
        _eventTable[eventType] = (CallBack<T1, T2, T3, T4>)_eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    public static void RemoveListener<T1, T2, T3, T4, T5>(EventType eventType, CallBack<T1, T2, T3, T4, T5> callBack) {
        OnListenerRemoving(eventType, callBack);
        _eventTable[eventType] = (CallBack<T1, T2, T3, T4, T5>)_eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    //发送广播
    public static void Broadcast(EventType eventType) {
        Delegate d;
        if (_eventTable.TryGetValue(eventType, out d)) {
            CallBack callBack = d as CallBack;
            if (callBack is null) {
                throw new Exception($"广播事件出错：事件{eventType}对应委托为空活具有不同类型");
            }

            callBack();
        }
    }

    public static void Broadcast<T>(EventType eventType, T arg) {
        Delegate d;
        if (_eventTable.TryGetValue(eventType, out d)) {
            CallBack<T> callBack = d as CallBack<T>;
            if (callBack is null) {
                throw new Exception($"广播事件出错：事件{eventType}对应委托为空活具有不同类型");
            }

            callBack(arg);
        }
    }

    public static void Broadcast<T1, T2>(EventType eventType, T1 arg1, T2 arg2) {
        Delegate d;
        if (_eventTable.TryGetValue(eventType, out d)) {
            CallBack<T1, T2> callBack = d as CallBack<T1, T2>;
            if (callBack is null) {
                throw new Exception($"广播事件出错：事件{eventType}对应委托为空活具有不同类型");
            }

            callBack(arg1, arg2);
        }
    }

    public static void Broadcast<T1, T2, T3>(EventType eventType, T1 arg1, T2 arg2, T3 arg3) {
        Delegate d;
        if (_eventTable.TryGetValue(eventType, out d)) {
            CallBack<T1, T2, T3> callBack = d as CallBack<T1, T2, T3>;
            if (callBack is null) {
                throw new Exception($"广播事件出错：事件{eventType}对应委托为空活具有不同类型");
            }

            callBack(arg1, arg2, arg3);
        }
    }

    public static void Broadcast<T1, T2, T3, T4>(EventType eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
        Delegate d;
        if (_eventTable.TryGetValue(eventType, out d)) {
            CallBack<T1, T2, T3, T4> callBack = d as CallBack<T1, T2, T3, T4>;
            if (callBack is null) {
                throw new Exception($"广播事件出错：事件{eventType}对应委托为空活具有不同类型");
            }

            callBack(arg1, arg2, arg3, arg4);
        }
    }

    public static void Broadcast<T1, T2, T3, T4, T5>(EventType eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
        Delegate d;
        if (_eventTable.TryGetValue(eventType, out d)) {
            CallBack<T1, T2, T3, T4, T5> callBack = d as CallBack<T1, T2, T3, T4, T5>;
            if (callBack is null) {
                throw new Exception($"广播事件出错：事件{eventType}对应委托为空活具有不同类型");
            }

            callBack(arg1, arg2, arg3, arg4, arg5);
        }
    }
}