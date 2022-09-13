using Game.Interface;
using UnityEngine;

namespace Game.Particle
{
    public class ShotParticleActor : MonoBehaviour, IPoolItem, IParticleItem
    {
        [Header("References")] 
        [SerializeField] private ParticleSystem vfxParticle;

        public bool IsAvailableForSpawn { get; set; }

        private void Awake()
        {
            IsAvailableForSpawn = true;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        public void Play()
        {
            vfxParticle.Play();

            IsAvailableForSpawn = false;
        }

        public void Stop()
        {
            IsAvailableForSpawn = true;
            SetPosition(Vector3.zero);
        }
    }
}