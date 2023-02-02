using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInstantiator<T> : MonoBehaviour where T : Object
{
    Coroutine _spawnCoroutine;
    [SerializeField] protected AbstractPoolManager<T> _poolManager;
    [SerializeField] protected int _defaultCapacity;
    [SerializeField] protected int _maxSize;
    public virtual void Init()
    {
        _poolManager.InitPool(_defaultCapacity, _maxSize);
    }

    public void StartSpawn()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawn()
    {
        StopCoroutine(_spawnCoroutine);
    }

    protected abstract IEnumerator SpawnCoroutine();
}
