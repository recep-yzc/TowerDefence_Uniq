using Characters.Enemy;
using Game.Helper;
using Game.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters.Tower
{
    public class TowerDetectActor : MonoBehaviour, ITriggerEnter, ITriggerExit
    {
        [Header("References")]
        [SerializeField] private TowerComponentActor towerComponentActor;
        [SerializeField] private SphereCollider sphereCollider;

        #region private

        private List<EnemyComponentActor> enemyComponentActors = new();

        #endregion

        public void FinishWave()
        {
            enemyComponentActors.Clear();
        }

        public void TriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyComponentActor enemyComponentActorTemp = GetEnemyComponentActor(other);
                enemyComponentActorTemp.SetDeadCallBack(() => WhenEnemyDead(enemyComponentActorTemp));

                enemyComponentActors.Add(enemyComponentActorTemp);

                UpdateTarget();
            }
        }

        public void TriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyComponentActor enemyComponentActorTemp = GetEnemyComponentActor(other);
                enemyComponentActors.Remove(enemyComponentActorTemp);

                UpdateTarget();
            }
        }

        private EnemyComponentActor GetCurrentEnemyComponentActor()
        {
            return enemyComponentActors.FirstOrDefault();
        }

        private EnemyComponentActor GetEnemyComponentActor(Collider other)
        {
            return other.GetComponent<ParentFindHelper>().GetParent<EnemyComponentActor>();
        }

        private void UpdateTarget()
        {
            EnemyComponentActor enemyComponentActorTemp = GetCurrentEnemyComponentActor();
            towerComponentActor.TowerCharacterComponentActor.SetEnemyComponentActor(enemyComponentActorTemp);
        }

        private void WhenEnemyDead(EnemyComponentActor enemyComponentActor)
        {
            enemyComponentActors.Remove(enemyComponentActor);
            UpdateTarget();
        }

        public void UpdateRadius()
        {
            TowerCharacterModelActor towerCharacterModelActorTemp = towerComponentActor.TowerCharacterComponentActor.towerCharacterModelActor;
            if (towerCharacterModelActorTemp)
            {
                sphereCollider.radius = towerCharacterModelActorTemp.TowerCharacterPropertyData.Radius;
            }
        }
    }
}