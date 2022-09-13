using Game.Event;
using Game.Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Characters.Tower.Ui
{
    public class TowerSlotActor : MonoBehaviour, IPointerDownHandler
    {
        [Header("References")]
        [SerializeField] private RectTransform rectTransform;

        public TowerSlotCardActor TowerSlotCardActor { get; private set; }
        public int SlotIndex { get; private set; }

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();

        #endregion

        public void SetSlotIndex(int slotIndex)
        {
            SlotIndex = slotIndex;
        }

        public void SetCharacterCardActor(TowerSlotCardActor towerSlotCardActor)
        {
            TowerSlotCardActor = towerSlotCardActor;
        }

        public void SetPosition()
        {
            TowerSlotCardActor.SetLocalPosition(rectTransform.localPosition);
        }

        public void SetSize()
        {
            TowerSlotCardActor.SetSize(rectTransform.sizeDelta);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (TowerSlotCardActor)
            {
                gameEventSystem.Publish(GameEvents.ClickDownSlot, TowerSlotCardActor);
                TowerSlotCardActor = null;
            }
        }
    }
}