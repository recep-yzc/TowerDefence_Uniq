using DG.Tweening;
using Game.Interface;
using Game.Manager;
using Game.Particle;
using Game.Scriptable;
using UnityEngine;

namespace Bullet
{
    public class BulletActor : MonoBehaviour, IPoolItem
    {
        [Header("Data")] 
        public BulletData BulletData;

        public bool IsAvailableForSpawn { get; set; }

        private void Awake()
        {
            IsAvailableForSpawn = true;
        }

        public void Shot(Vector3 targetPoint, CallBack shotCallBack)
        {
            IsAvailableForSpawn = false;

            transform.DOMove(targetPoint, BulletData.Speed).SetSpeedBased().SetEase(Ease.Linear).SetLink(gameObject)
                .OnComplete(() =>
                {
                    CreateHitParticle();
                    shotCallBack?.Invoke();

                    IsAvailableForSpawn = true;
                    SetLocalPosition(Vector3.zero);
                });
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        private void CreateHitParticle()
        {
            #region particle

            BulletHitParticleActor bulletHitParticleActorTemp =
                PoolManager.Instance.GetPoolItem<BulletHitParticleActor>(typeof(BulletHitParticleActor).ToString());
            bulletHitParticleActorTemp.SetPosition(transform.position);
            bulletHitParticleActorTemp.Play();

            #endregion
        }
    }
}