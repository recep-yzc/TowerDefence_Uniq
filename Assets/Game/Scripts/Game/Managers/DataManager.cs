using Game.Entity;
using Game.Event;
using Game.Interface;
using UnityEngine;

namespace Game.Manager
{
    [ScriptOrder(-9998)]
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;
        public GameData GameData { get; private set; }

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();

        #endregion

        private void Awake()
        {
            #region singleton
            if (!Instance)
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            #endregion

            InitData();
        }

        #region Event

        private void OnEnable()
        {
            Listen(true);
        }

        private void OnDisable()
        {
            GameData.Save();
            Listen(false);
        }

        private void Listen(bool status)
        {
            gameEventSystem.SaveEvent(GameEvents.GameInit, status, GameInit);
        }

        #endregion

        private void GameInit(object[] a)
        {
            gameEventSystem.Publish(GameEvents.UpdateMoney, GameData.Money);
            gameEventSystem.Publish(GameEvents.UpdateWaveLevel, GameData.Wave);
        }

        public void IncrementWave()
        {
            GameData.Wave++;

            gameEventSystem.Publish(GameEvents.UpdateWaveLevel, GameData.Wave);
        }

        public void DecrementWave()
        {
            if (GameData.Wave > 0)
            {
                GameData.Wave--;
            }

            gameEventSystem.Publish(GameEvents.UpdateWaveLevel, GameData.Wave);
        }

        public void IncrementMoney(float value)
        {
            GameData.Money += value;

            gameEventSystem.Publish(GameEvents.UpdateMoney, GameData.Money);
        }

        public void DecrementMoney(float value)
        {
            GameData.Money -= value;

            gameEventSystem.Publish(GameEvents.UpdateMoney, GameData.Money);
        }

        private void InitData()
        {
            GameData = GameData.Get();
            if (GameData == null)
            {
                GameData = new GameData();
                bool isSuccess = GameData.Register();
                if (!isSuccess) Debug.LogError("Currency Data Entity register error!");
            }

            GameData.Load();
        }
    }
}