using System;
using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "EnemyProperty", menuName = "Property/Enemy")]
    public class EnemyProperty : ScriptableObject, ICloneable
    {
        [Header("Health/Damage")] 
        public float Health = 100;
        public float Damage = 15;

        [Header("Movement")] 
        public float MovementSpeed = 2.5f;

        [Header("Earn")] 
        public float Earn = 5;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}