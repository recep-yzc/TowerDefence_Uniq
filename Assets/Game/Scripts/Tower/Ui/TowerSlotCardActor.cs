using UnityEngine;
using UnityEngine.UI;

namespace Characters.Tower.Ui
{
    public class TowerSlotCardActor : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Image imgCharacterCard;
        [SerializeField] private RectTransform rectTransform;

        public TowerSlotActor TowerSlotActor { get; private set; }
        public TowerCharacter TowerCharacter { get; private set; }

        #region private

        private bool isSetLayer;

        #endregion

        public void UpdateSiblingIndex()
        {
            if (!isSetLayer)
            {
                isSetLayer = true;
                int lastIndex = rectTransform.parent.childCount;
                rectTransform.SetSiblingIndex(lastIndex);
            }
        }

        public void SetTowerCharacterData(TowerCharacter towerCharacter)
        {
            TowerCharacter = towerCharacter;
        }

        public void UpdateTowerCharacterSlotIndex(int slotIndex)
        {       
            TowerCharacter.SlotIndex = slotIndex;
        }

        public void SetTowerMergeSlotActor(TowerSlotActor towerSlotActor)
        {
            TowerSlotActor = towerSlotActor;

            isSetLayer = false;
        }

        public void SetCharacterCardSprite(Sprite sprite)
        {
            imgCharacterCard.sprite = sprite;
        }

        public void SetLocalPosition(Vector3 localPosition)
        {
            rectTransform.localPosition = localPosition;
        }

        public void SetSize(Vector2 size)
        {
            rectTransform.sizeDelta = size - new Vector2(50, 50);
        }

        public void SetPosition(Vector3 position)
        {
            rectTransform.position = position;
        }

        public Vector2 GetPosition()
        {
            return rectTransform.position;
        }
    }
}