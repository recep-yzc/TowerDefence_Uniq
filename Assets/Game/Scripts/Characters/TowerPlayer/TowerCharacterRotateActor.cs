using Characters.Enemy;
using UnityEngine;

namespace Characters.TowerPlayer
{
    public class TowerCharacterRotateActor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TowerCharacterComponentActor towerCharacterComponentActor;
        [SerializeField] private Transform rotatePivot;

        #region Private

        private float RotateSpeed => towerCharacterComponentActor.TowerCharacterPropertyData.RotateSpeed;

        #endregion

        public void Update()
        {
            if (!towerCharacterComponentActor.towerCharacterModelActor) return;

            LookTarget();
        }

        private void LookTarget()
        {
            EnemyComponentActor enemyComponentActorTemp = towerCharacterComponentActor.CurrentEnemyComponentActor;
            if (enemyComponentActorTemp && !enemyComponentActorTemp.IsDead)
            {
                Transform enemyTransform = enemyComponentActorTemp.transform;

                Vector3 normalize = (enemyTransform.position - rotatePivot.position).normalized;
                normalize.y = 0;

                Quaternion lookRotation = Quaternion.LookRotation(normalize, Vector3.up);
                rotatePivot.rotation = Quaternion.Lerp(rotatePivot.rotation, lookRotation, Time.deltaTime * RotateSpeed);
            }
        }
    }
}