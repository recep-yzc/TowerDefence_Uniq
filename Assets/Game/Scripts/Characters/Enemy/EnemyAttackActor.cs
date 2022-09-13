using Game.Helper;
using Game.Interface;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyAttackActor : MonoBehaviour, IAttackable, ITriggerEnter
    {
        [Header("References")] 
        [SerializeField] private EnemyComponentActor enemyComponentActor;

        private float Damage => enemyComponentActor.Property.Damage;

        public void Hit(IDamageable damageable, float damage)
        {
            damageable.TakeDamage(damage);
        }

        public void TriggerEnter(Collider other)
        {
            if (other.CompareTag("Castle"))
            {
                IDamageable damageable = other.GetComponent<ParentFindHelper>().GetParent<IDamageable>();
                Hit(damageable, Damage);

                enemyComponentActor.Dead(false);
            }
        }
    }
}