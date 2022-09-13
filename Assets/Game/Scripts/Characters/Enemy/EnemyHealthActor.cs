using Game.Interface;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyHealthActor : MonoBehaviour, IDamageable
    {
        [Header("References")] 
        [SerializeField] private EnemyComponentActor enemyComponentActor;

        #region private

        private float Health
        {
            get => enemyComponentActor.Property.Health;
            set => enemyComponentActor.Property.Health = value;
        }

        #endregion

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Dead();
            }
        }

        private void Dead()
        {
            enemyComponentActor.Dead(true);
        }
    }
}