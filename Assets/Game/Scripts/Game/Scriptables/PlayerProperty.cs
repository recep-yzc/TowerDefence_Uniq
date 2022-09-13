using System;
using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "PlayerProperty", menuName = "Property/Player")]
    public class PlayerProperty : ScriptableObject, ICloneable
    {
        [Header("Movement")] 
        public float RotateSpeed = 10;
        public float MovementSpeed = 4f;
        public float ActThreshold = 0.1f;

        [Header("Stack")] 
        public Vector3 MoneyStackOffset = new Vector3(0, 0.1f, 0);

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}