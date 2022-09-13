using Game.Interface;
using UnityEngine;

namespace Game.Helper
{
    public class ParticleEventHelper : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject script;

        #region private

        private IParticleItem particleItem;

        #endregion

        private void Awake()
        {
            particleItem = script.GetComponent<IParticleItem>();
        }

        private void OnParticleSystemStopped()
        {
            particleItem?.Stop();
        }
    }
}