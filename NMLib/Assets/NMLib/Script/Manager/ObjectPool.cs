using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NMLib
{
    public abstract class ObjectPool<T> : IDisposable 
    {
        private bool isDisposed = false;
        private Stack<T> stack;
        protected int MaxPoolCount { get { return int.MaxValue; } }
        public int PoolCount { get { return stack.Count; } }

        protected abstract T CreateInstance();
        protected virtual void OnPop(T instance){}
        protected virtual void OnPush(T instance){}
        protected virtual void OnClear(T instance){}

        public ObjectPool() {
            stack = new Stack<T>();
        }
        public T Pop()
        {
            if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
            var instance = (stack.Count > 0) 
                ? stack.Pop() 
                : CreateInstance();
            OnPop(instance);
            return instance;
        }
        public void Push(T instance)
        {
            if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
            if ((stack.Count + 1) == MaxPoolCount) {
                throw new InvalidOperationException("Reached Max PoolSize");
            }
            OnPush(instance);
            stack.Push(instance);
        }
        public void Clear(bool callOnPop = false)
        {
            while (stack.Count != 0) {
                var instance = stack.Pop();
                if (callOnPop) {
                    OnPop(instance);
                }
                OnClear(instance);
            }
        }
        public void Sweep(float instanceCountRatio, int minSize, bool callOnPop = false)
        {
            if (instanceCountRatio <= 0) instanceCountRatio = 0;
            if (instanceCountRatio >= 1.0f) instanceCountRatio = 1.0f;

            var size = (int)(stack.Count * instanceCountRatio);
            size = Math.Max(minSize, size);

            while (stack.Count > size) {
                var instance = stack.Pop();
                if (callOnPop) {
                    OnPop(instance);
                }
                OnClear(instance);
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed) {
                if (disposing) {
                    Clear(false);
                }
                isDisposed = true;
            }
        }
        public void Dispose() { Dispose(true); }
        #endregion

    }


}






#if false
    //UniRX版ならこう
    using UniRx.Toolkit;

    /// <summary>
    /// 汎用オブジェクトプール
    /// </summary>
    public class GenericObjectPool<T> : ObjectPool<T> where T : Component
    {
        private GameObject prefab;
        private Transform parent;
        public GenericObjectPool(GameObject _prefab,Transform _parent) {
            prefab = _prefab;
            parent = _parent;
        }
        protected override T CreateInstance() {
            return GameObject.Instantiate(prefab,parent).GetComponent<T>();
        }
    }
#endif

