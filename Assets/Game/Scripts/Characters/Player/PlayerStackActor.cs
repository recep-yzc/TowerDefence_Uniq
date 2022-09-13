using Game.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerStackActor : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private PlayerComponentActor playerComponentActor;
        [SerializeField] private Transform stackMoneyParent;
        [SerializeField] private Transform stackItemParent;
        
        #region private

        private List<MoneyActor> moneyActors = new();
        private Vector3 MoneyStackOffset => playerComponentActor.Property.MoneyStackOffset;

        #endregion

        public void CollectMoney(MoneyActor moneyActor)
        {
            moneyActor.CollectIt(stackMoneyParent, GetNextMoneyPoint());

            moneyActors.Add(moneyActor);
        }

        public void SellMoney(Transform targetMoneyTransform)
        {
            MoneyActor moneyActorTemp = GetLastMoney();

            if (moneyActorTemp)
            {
                moneyActorTemp.SellIt(targetMoneyTransform.position,
                    () => { DataManager.Instance.IncrementMoney(moneyActorTemp.Money); });

                moneyActors.Remove(moneyActorTemp);
            }
        }

        private MoneyActor GetLastMoney()
        {
            return moneyActors.LastOrDefault();
        }

        private Vector3 GetNextMoneyPoint()
        {
            bool isDuel = (moneyActors.Count % 2 == 0);
            int index = moneyActors.Count / 2;

            Vector3 offset = Vector3.zero;
            offset.x = isDuel ? 0.25f : -0.25f;

            return (index * MoneyStackOffset) + offset;
        }
    }
}