using UnityEngine;

namespace PunchFeature.Infrastructure.ObjectPools
{
    public interface IObjectPool<T> where T : Component
    {
        T Get();
        T Create();
        void Release(T obj);
    }
}