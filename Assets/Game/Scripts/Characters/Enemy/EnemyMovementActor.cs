using Game.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemy
{
    public class EnemyMovementActor : MonoBehaviour, IMoveable
    {
        [Header("References")] 
        [SerializeField] private EnemyComponentActor enemyComponentActor;
        [SerializeField] private NavMeshAgent navMeshAgent;

        #region private

        private Transform target;

        #endregion

        private void Start()
        {
            navMeshAgent.enabled = true;
        }

        public void FetchData()
        {
            navMeshAgent.speed = enemyComponentActor.Property.MovementSpeed;
        }

        public void SetEnemyTargetTransform(Transform target)
        {
            this.target = target;
        }

        public void StartMove()
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);
        }

        public void StopMove()
        {
            navMeshAgent.isStopped = true;
        }
    }
}