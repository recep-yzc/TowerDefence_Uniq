using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet")]
    public class BulletData : ScriptableObject
    {
        public float Speed = 60;
    }
}