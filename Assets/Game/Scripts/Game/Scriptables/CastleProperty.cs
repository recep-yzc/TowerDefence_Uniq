using System;
using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "CastleProperty", menuName = "Property/Castle")]
    public class CastleProperty : ScriptableObject, ICloneable
    {
        [Header("Properties")]
        public float Health = 100;
        public float MaxHealth = 100f;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}