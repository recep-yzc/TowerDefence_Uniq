using Bullet;
using Characters.Enemy;
using Game.Interface;
using Game.Manager;
using Game.Particle;
using UnityEngine;

namespace Characters.TowerPlayer
{
    public class TowerCharacterAttackActor : MonoBehaviour, IAttackable
    {
        [Header("References")]
        [SerializeField] private TowerCharacterComponentActor towerCharacterComponentActor;
        [SerializeField] private Transform muzzleTransform;

        #region private

        private float Damage => towerCharacterComponentActor.TowerCharacterPropertyData.Damage;
        private float ShotSpeedDuration => towerCharacterComponentActor.TowerCharacterPropertyData.ShotSpeedDuration;
        private float currentShotSpeedDuration = 0;

        #endregion

        private void Update()
        {
            if (!towerCharacterComponentActor.towerCharacterModelActor) return;

            currentShotSpeedDuration += Time.deltaTime;
            if (currentShotSpeedDuration > ShotSpeedDuration)
            {
                currentShotSpeedDuration = 0;

                Shot();
            }
        }

        private void Shot()
        {
            EnemyComponentActor enemyComponentActorTemp = towerCharacterComponentActor.CurrentEnemyComponentActor;
            if (enemyComponentActorTemp && !enemyComponentActorTemp.IsDead)
            {
                CreateShotParticle();
                CreateBullet(enemyComponentActorTemp);
            }
        }

        public void Hit(IDamageable damageable, float damage)
        {
            damageable.TakeDamage(damage);
        }

        private void CreateBullet(EnemyComponentActor enemyComponentActor)
        {
            BulletActor bulletActorTemp = PoolManager.Instance.GetPoolItem<BulletActor>(typeof(BulletActor).ToString());
            bulletActorTemp.SetPosition(muzzleTransform.position);

            Vector3 targetPoint = enemyComponentActor.transform.position + Vector3.up;
            IDamageable damageable = enemyComponentActor.GetComponent<IDamageable>();

            bulletActorTemp.Shot(targetPoint, () => Hit(damageable, Damage));
        }

        private void CreateShotParticle()
        {
            ShotParticleActor shotParticleActorTemp =
                PoolManager.Instance.GetPoolItem<ShotParticleActor>(typeof(ShotParticleActor).ToString());
            shotParticleActorTemp.SetPosition(muzzleTransform.position);
            shotParticleActorTemp.Play();
        }

        public void SetMuzzleTransform(Transform muzzleTransform)
        {
            this.muzzleTransform = muzzleTransform;
        }
    }
}