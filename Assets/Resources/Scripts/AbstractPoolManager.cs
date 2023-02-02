using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class AbstractPoolManager<T> : MonoBehaviour where T : UnityEngine.Object
{
    protected ObjectPool<T> _pool;
    [SerializeField] protected T _originalObj;

    public ObjectPool<T> Pool { get => _pool; }

    public void InitPool(int defaultCapacity, int maxSize)
    {
        _pool = new ObjectPool<T>(OnCreateObj, OnGetObj, OnReleaseObj, OnDestroyObj, true, defaultCapacity, maxSize);
    }

    protected virtual void OnDestroyObj(T obj)
    {
        Destroy(obj);
    }

    protected abstract void OnReleaseObj(T obj);

    protected abstract void OnGetObj(T obj);

    protected virtual T OnCreateObj()
    {
        T obj = Instantiate(_originalObj, transform);
        return obj;
    }
}
