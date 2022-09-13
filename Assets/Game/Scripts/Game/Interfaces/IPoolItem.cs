using UnityEngine;

namespace Game.Interface
{
    public interface IPoolItem
    {
        public bool IsAvailableForSpawn { get; set; }
        public void SetPosition(Vector3 position);
        public void SetLocalPosition(Vector3 position);
    }
}