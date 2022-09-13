using Game.Event;
using Game.Interface;
using Game.Scriptable;
using UnityEngine;

namespace Castle
{
    public class CastleComponentActor : MonoBehaviour
    {       
        [Header("Properties")] 
        public CastleProperty Property;

        [Header("References")] 
        public CastleHealthActor CastleHealthActor;

        public IGameEventSystem GameEventSystem = new GameEventSystem();

        #region Event

        private void OnEnable()
        {
            Listen(true);
        }

        private void OnDisable()
        {
            Listen(false);
        }

        private void Listen(bool status)
        {
            GameEventSystem.SaveEvent(GameEvents.FinishWave, status, FinishWave);
        }

        #endregion

        private void Start()
        {
            CreateData();
            CastleHealthActor.UpdateHealthUI();
        }

        private void FinishWave(object[] a)
        {
            bool isSuccess = (bool)a[0];

            if (!isSuccess)
            {
                Property.Health = Property.MaxHealth;
                CastleHealthActor.UpdateHealthUI();
            }
        }

        public void Dead()
        {
            GameEventSystem.Publish(GameEvents.CastleDead);
            GameEventSystem.Publish(GameEvents.FinishWave, false);
        }

        private void CreateData()
        {
            Property = Property.Clone() as CastleProperty;
        }
    }
}