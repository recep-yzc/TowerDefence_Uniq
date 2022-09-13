using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.Event;
using Game.Interface;
using Game.Scriptable;
using Game.Manager;

namespace Characters.Tower.Ui
{
    public class TowerMergeMenuController : MonoBehaviour
    {
        [Header("Slots")]
        [SerializeField] private List<TowerSlotActor> towerSlotActors = new();

        [Header("References")]
        [SerializeField] private Transform menu;
        [SerializeField] private Transform slotCardParent;
        [SerializeField] private TextMeshProUGUI txtCost;
        [SerializeField] private Button btnBuy;
        [SerializeField] private Button btnExit;

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();
        private TowerReferenceData towerReferenceData => TowerManager.Instance.TowerReferenceData;

        private TowerData towerData;
        private List<TowerSlotCardActor> towerSlotCardActors = new();

        private int maxSlotCount = 7;

        #endregion

        #region Event

        private void OnEnable()
        {
            btnExit.onClick.AddListener(BtnExit);
            btnBuy.onClick.AddListener(BtnBuy);
            Listen(true);
        }

        private void OnDisable()
        {
            btnExit.onClick.RemoveListener(BtnExit);
            btnBuy.onClick.RemoveListener(BtnBuy);
            Listen(false);
        }

        private void Listen(bool status)
        {
            gameEventSystem.SaveEvent(GameEvents.MergeMenu, status, MergeMenu);
            gameEventSystem.SaveEvent(GameEvents.ClickUpSlot, status, ClickUpSlot);
        }

        #endregion

        private void Awake()
        {
            SetSlotIndex();
        }

        private void MergeMenu(object[] a)
        {
            MergeMenuStates mergeMenuState = (MergeMenuStates)a[0];

            Vector3 scale = Vector3.zero;
            Ease ease = Ease.Linear;

            switch (mergeMenuState)
            {
                case MergeMenuStates.Open:

                    towerData = (TowerData)a[1];
                    UpdateSlots();

                    scale = Vector3.one;
                    ease = Ease.OutBack;

                    break;
                case MergeMenuStates.Close:

                    scale = Vector3.zero;
                    ease = Ease.InBack;

                    break;
            }

            menu.DOScale(scale, 0.3f).SetEase(ease).SetLink(gameObject);
        }

        private void ClickUpSlot(object[] a)
        {
            TowerSlotCardActor towerSlotCardActorTemp1 = (TowerSlotCardActor)a[0];
            TowerSlotActor towerSlotActorTemp1 = (TowerSlotActor)a[1];

            TowerSlotCardActor towerSlotCardActorTemp2 = towerSlotActorTemp1.TowerSlotCardActor;
            TowerSlotActor towerSlotActorTemp2 = towerSlotCardActorTemp1.TowerSlotActor;

            if (!towerSlotCardActorTemp2)
            {
                TransferSlotCard(towerSlotActorTemp1, towerSlotCardActorTemp1);
            }
            else
            {
                if (towerSlotCardActorTemp2.TowerCharacter.Level == towerSlotCardActorTemp1.TowerCharacter.Level)
                {
                    MergeCards(towerSlotCardActorTemp1, towerSlotActorTemp1, towerSlotCardActorTemp2);
                }
                else
                {
                    TransferSlotCard(towerSlotActorTemp1, towerSlotCardActorTemp1);
                    TransferSlotCard(towerSlotActorTemp2, towerSlotCardActorTemp2);
                }
            }
            
            UpdateSlots();
            UpdateTower();
        }

        private void MergeCards(TowerSlotCardActor towerSlotCardActorTemp1, TowerSlotActor towerSlotActorTemp1, TowerSlotCardActor towerSlotCardActorTemp2)
        {
            int level = towerSlotCardActorTemp2.TowerCharacter.Level + 1;
            int slotIndex = towerSlotActorTemp1.SlotIndex;

            #region ClearOldSlotCard

            towerData.TowerCharacters.Remove(towerSlotCardActorTemp1.TowerCharacter);
            towerData.TowerCharacters.Remove(towerSlotCardActorTemp2.TowerCharacter);

            Destroy(towerSlotCardActorTemp1.gameObject);
            Destroy(towerSlotCardActorTemp2.gameObject);

            #endregion

            #region CreateNewSlotCard

            TowerCharacter towerCharacterDataTemp = Codes.CreateNewTowerCharacter(level, slotIndex);
            towerData.TowerCharacters.Add(towerCharacterDataTemp);

            #endregion
        }

        private void UpdateSlots()
        {
            ClearTowerCharacterCards();

            for (int i = 0; i < towerData.TowerCharacters.Count; i++)
            {
                TowerCharacter towerCharacterTemp = towerData.TowerCharacters[i];

                TowerSlotCardActor towerSlotCardActorTemp = CreateSlotCardData(towerCharacterTemp);
                TowerSlotActor towerSlotActorTemp = towerSlotActors.FirstOrDefault(x => x.SlotIndex == towerSlotCardActorTemp.TowerCharacter.SlotIndex);

                TransferSlotCard(towerSlotActorTemp, towerSlotCardActorTemp);

                towerSlotCardActors.Add(towerSlotCardActorTemp);
            }

            UpdateBuyCardCost();
        }

        private TowerSlotCardActor CreateSlotCardData(TowerCharacter towerCharacterTemp)
        {
            TowerSlotCardActor towerCharacterCardActorTemp = Instantiate(towerReferenceData.TowerSlotCardActorPrefab, slotCardParent);

            TowerCharacterCard towerCharacterCardTemp = towerReferenceData.TowerCharacterCards
                .Where(x => x.CharacterLevel == towerCharacterTemp.Level)
                .FirstOrDefault();

            towerCharacterCardActorTemp.SetCharacterCardSprite(towerCharacterCardTemp.CharacterCardSprite);
            towerCharacterCardActorTemp.SetTowerCharacterData(towerCharacterTemp);

            return towerCharacterCardActorTemp;
        }

        private void UpdateTower()
        {
            gameEventSystem.Publish(GameEvents.UpdateTower, towerData);
        }

        private void FetchSlotCard(TowerSlotActor towerSlotActor, TowerSlotCardActor towerSlotCardActor)
        {
            towerSlotCardActor.SetTowerMergeSlotActor(towerSlotActor);
            towerSlotCardActor.UpdateTowerCharacterSlotIndex(towerSlotActor.SlotIndex);

            towerSlotActor.SetCharacterCardActor(towerSlotCardActor);
            towerSlotActor.SetSize();
            towerSlotActor.SetPosition();
        }

        private void TransferSlotCard(TowerSlotActor towerSlotActor, TowerSlotCardActor towerSlotCardActor)
        {
            FetchSlotCard(towerSlotActor, towerSlotCardActor);
        }

        private void ClearTowerCharacterCards()
        {
            for (int i = 0; i < towerSlotCardActors.Count; i++)
            {
                TowerSlotCardActor towerSlotCardActorTemp = towerSlotCardActors[i];
                Destroy(towerSlotCardActorTemp.gameObject);
            }

            towerSlotCardActors.Clear();
        }

        private void UpdateBuyCardCost()
        {
            txtCost.text = IsFull() ? "Full" : $"{(int)GetCardCost()} <sprite=0>";
        }

        private void SetSlotIndex()
        {
            for (int i = 0; i < towerSlotActors.Count; i++)
            {
                towerSlotActors[i].SetSlotIndex(i);
            }
        }

        private int GetFirstEmptySlotIndex()
        {
            return towerSlotActors.Where(x => !x.TowerSlotCardActor).Select(x => x.SlotIndex).FirstOrDefault();
        }

        private bool IsFull()
        {
            return towerData.TowerCharacters.Count == maxSlotCount;
        }

        private float GetCardCost()
        {
            return towerReferenceData.CostPerCard;
        }

        private void BtnExit()
        {
            gameEventSystem.Publish(GameEvents.MergeMenu, MergeMenuStates.Close);
        }

        private void BtnBuy()
        {
            if (!IsFull())
            {
                float currentMoney = DataManager.Instance.GameData.Money;
                float currentCost = GetCardCost();

                if ((int)currentMoney >= (int)currentCost)
                {
                    DataManager.Instance.DecrementMoney(currentCost);

                    int level = 1;
                    int slotIndex = GetFirstEmptySlotIndex();

                    TowerCharacter towerCharacterTemp = Codes.CreateNewTowerCharacter(level, slotIndex);
                    towerData.TowerCharacters.Add(towerCharacterTemp);

                    UpdateSlots();
                    UpdateTower();
                }
            }
        }
    }
}