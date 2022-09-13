using Game.Event;
using Game.Interface;
using UnityEngine;

namespace Characters.Tower
{
    public class TowerMergeTriggerActor : MonoBehaviour, ITriggerEnter, ITriggerExit
    {
        [Header("References")] 
        [SerializeField] private TowerComponentActor towerComponentActor;
        [SerializeField] private TriggerBarActor triggerBarActor;
        [SerializeField] private Canvas canvas;

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();

        #endregion

        private void Start()
        {
            canvas.worldCamera = Camera.main;
        }

        public void TriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                triggerBarActor.SetOpenState(true, OpenMergeMenu);
            }
        }

        public void TriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                triggerBarActor.SetOpenState(false, null);
            }
        }

        private void OpenMergeMenu()
        {
            gameEventSystem.Publish(GameEvents.MergeMenu, MergeMenuStates.Open, towerComponentActor.TowerData);
        }
    }
}