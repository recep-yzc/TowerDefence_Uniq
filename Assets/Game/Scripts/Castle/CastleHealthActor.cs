using Game.Event;
using Game.Interface;
using UnityEngine;

namespace Castle
{
    public class CastleHealthActor : MonoBehaviour, IDamageable, IHealable
    {
        [Header("References")] 
        [SerializeField] private CastleComponentActor castleComponentActor;

        #region private

        private float Health
        {
            get => castleComponentActor.Property.Health;
            set => castleComponentActor.Property.Health = value;
        }

        #endregion

        public void TakeDamage(float damage)
        {
            Health = Mathf.Clamp(Health - damage, 0f, 100f);

            if (Health <= 0)
            {
                castleComponentActor.Dead();
            }

            UpdateHealthUI();
        }

        public void TakeHeal(float health)
        {
            Health = Mathf.Clamp(Health + health, 0f, 100f);

            UpdateHealthUI();
        }

        public void UpdateHealthUI()
        {
            castleComponentActor.GameEventSystem.Publish(GameEvents.UpdateCastleHealth, Health);
        }
    }
}