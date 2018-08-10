using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueList<T>
{
    private List<T> _queue;

    public QueueList(List<T> initialQueue)
    {
        _queue = initialQueue;
    }

    public QueueList()
    {
        _queue = new List<T>();
    }

    public void AddToQueue(T obj)
    {
        var newQueue = new List<T>() {obj};
        newQueue.AddRange(_queue);
        _queue = newQueue;
    }

    public void AddRangeToQueue(List<T> objs)
    {
        var newQueue = new List<T>();
        newQueue.AddRange(objs);
        newQueue.AddRange(_queue);
        _queue = newQueue;
    }

    public T PopFromTop()
    {
        var item = _queue[_queue.Count];
        _queue.RemoveAt(_queue.Count);
        return item;
    }

    public int Count()
    {
        return _queue.Count;
    }
}
