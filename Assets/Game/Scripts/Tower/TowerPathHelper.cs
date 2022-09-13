using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Characters.Tower
{
    public class TowerPathHelper : MonoBehaviour
    {
        #region Path

        [Header("PathSetup")] 
        [SerializeField] private PathCreator pathCreator;
        [SerializeField] private Vector3 offSet;

        [ShowInInspector, GUIColor(1f, 0.5f, 0)]
        public float Distance
        {
            get => distance;
            set
            {
                distance = value;

                transform.position = pathCreator.path.GetPointAtDistance(distance, EndOfPathInstruction.Stop) + offSet;
                transform.rotation = pathCreator.path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop);
            }
        }

        #region private

        private float distance;

        #endregion

        #endregion
    }
}