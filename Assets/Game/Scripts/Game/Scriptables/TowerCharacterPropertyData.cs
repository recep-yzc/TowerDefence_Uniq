using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "TowerCharacterPropertyData", menuName = "Property/TowerCharacter")]
    public class TowerCharacterPropertyData : ScriptableObject
    {
        [Header("Properties")]
        public float Damage = 10;
        public float ShotSpeedDuration = 0.3f;
        public float RotateSpeed = 10;
        public float Radius = 10;
    }
}