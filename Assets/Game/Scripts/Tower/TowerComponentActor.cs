using UnityEngine;
using Characters.TowerPlayer;
using Game.Event;
using Game.Interface;
using TMPro;
using System.Linq;

namespace Characters.Tower
{
    public class TowerComponentActor : MonoBehaviour
    {
        [Header("References")]
        public TowerDetectActor TowerDetectActor;
        public TowerCharacterComponentActor TowerCharacterComponentActor;
        [SerializeField] private TextMeshProUGUI txtTowerLevel;

        public TowerData TowerData { get; private set; }

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();

        #endregion

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
            gameEventSystem.SaveEvent(GameEvents.FinishWave, status, FinishWave);
        }

        #endregion

        private void Start()
        {
            gameEventSystem.Publish(GameEvents.SendTowerComponentActor, this);
        }

        public void SetTowerData(TowerData towerData)
        {
            TowerData = towerData;
        }

        private void FinishWave(object[] a)
        {
            TowerDetectActor.FinishWave();
        }

        public void UpdateTowerLevel()
        {
            TowerCharacter towerCharacterTemp = TowerData.TowerCharacters.Where(x => x.SlotIndex == 0).FirstOrDefault();
            txtTowerLevel.text = (towerCharacterTemp != null) ? towerCharacterTemp.Level.ToString() : "";            
        }

        public void UpdateCharacter()
        {
            TowerCharacterComponentActor.UpdateCharacter();
        }

        public void UpdateRadius()
        {
            TowerDetectActor.UpdateRadius();
        }
    }
}