using Game.Event;
using Game.Interface;
using Game.Manager;
using Game.Scriptable;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyComponentActor : MonoBehaviour
    {
        [Header("Properties")] 
        public EnemyProperty Property;

        [Header("References")] 
        public EnemyHealthActor EnemyHealthActor;
        public EnemyMovementActor EnemyMovementActor;
        public EnemyAttackActor EnemyAttackActor;

        public bool IsDead { get; private set; }

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();

        private CallBack enemyDeadCallBack;

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
            gameEventSystem.SaveEvent(GameEvents.BtnClickPlay, status, BtnClickPlay);
            gameEventSystem.SaveEvent(GameEvents.SendEnemyTargetTransform, status, GetEnemyTargetTransform);
        }

        #endregion

        private void Start()
        {
            CreateData();
            FetchData();
        }

        private void GetEnemyTargetTransform(object[] a)
        {
            Transform enemyTargetTransform = (Transform)a[0];
            EnemyMovementActor.SetEnemyTargetTransform(enemyTargetTransform);
        }

        private void FinishWave(object[] a)
        {
            EnemyMovementActor.StopMove();
        }

        private void BtnClickPlay(object[] a)
        {
            EnemyMovementActor.StartMove();
        }

        public void Dead(bool createMoney)
        {
            if (IsDead) return;
            IsDead = true;

            enemyDeadCallBack?.Invoke();
            if (createMoney)
            {
                CreateMoney();
            }

            gameObject.SetActive(false);
        }

        public void SetDeadCallBack(CallBack enemyDeadCallBack)
        {
            this.enemyDeadCallBack += enemyDeadCallBack;
        }

        private void CreateMoney()
        {
            MoneyActor moneyActorTemp = PoolManager.Instance.GetPoolItem<MoneyActor>(typeof(MoneyActor).ToString());
            moneyActorTemp.SetMoney(Property.Earn);
            moneyActorTemp.SetPosition(transform.position + Vector3.up);
            moneyActorTemp.DropIt();
        }

        private void FetchData()
        {
            EnemyMovementActor.FetchData();
        }

        private void CreateData()
        {
            Property = Property.Clone() as EnemyProperty;
        }
    }
}