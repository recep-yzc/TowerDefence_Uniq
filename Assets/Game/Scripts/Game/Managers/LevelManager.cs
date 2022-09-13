using Game.Event;
using Game.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager
{
    [ScriptOrder(-9994)]
    public class LevelManager : MonoBehaviour
    {
        [Header("Levels")] 
        [SerializeField] private List<WaveActor> waveActors = new();

        public static LevelManager Instance;
        public static bool IsGameStarted;
        public static bool IsGameFinished;

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();
        private WaveActor waveActorTemp;

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
            gameEventSystem.SaveEvent(GameEvents.CreateWave, status, CreateWave);
        }

        #endregion

        private void Awake()
        {
            #region Singleton

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
        }

        private void CreateWave(object[] a)
        {
            IsGameStarted = false;
            IsGameFinished = false;

            gameEventSystem.Publish(GameEvents.RestartWave);

            if (waveActorTemp)
            {
                Destroy(waveActorTemp.gameObject);
            }

            int levelIndex = (int)Mathf.Repeat(DataManager.Instance.GameData.Wave, waveActors.Count);

            waveActorTemp = Instantiate(waveActors[levelIndex], null);
            waveActorTemp.InitWave();
        }

        private void BtnClickPlay(object[] a)
        {
            IsGameStarted = true;
        }

        private void FinishWave(object[] a)
        {
            IsGameFinished = true;
            bool isSuccess = (bool)a[0];

            if (isSuccess)
            {
                DataManager.Instance.IncrementWave();
            }
        }
    }
}