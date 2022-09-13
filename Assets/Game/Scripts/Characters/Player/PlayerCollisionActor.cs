using Game.Helper;
using Game.Interface;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerCollisionActor : MonoBehaviour, ITriggerEnter
    {
        [Header("References")]
        [SerializeField] private PlayerComponentActor playerComponentActor;

        public void TriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                MoneyActor moneyActorTemp = other.GetComponent<ParentFindHelper>().GetParent<MoneyActor>();

                if (!moneyActorTemp.IsCollected)
                    playerComponentActor.PlayerStackActor.CollectMoney(moneyActorTemp);
            }
        }
    }
}