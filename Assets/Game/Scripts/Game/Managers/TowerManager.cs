using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Characters.Tower;
using Game.Interface;
using Game.Event;
using Game.Scriptable;
using System;
using System.Linq;

namespace Game.Manager
{
    public class TowerManager : MonoBehaviour
    {
        public static TowerManager Instance;

        [Header("Data")]
        public TowerReferenceData TowerReferenceData;

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();

        private List<TowerComponentActor> towerComponentActors = new();

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
            gameEventSystem.SaveEvent(GameEvents.SendTowerComponentActor, status, SaveTowerComponentActor);
            gameEventSystem.SaveEvent(GameEvents.UpdateTower, status, UpdateTower);
        }

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
        }

        private void Start()
        {
            DOVirtual.DelayedCall(0.2f, FetchTowerDatas);
        }

        private void SaveTowerComponentActor(object[] a)
        {
            TowerComponentActor towerComponentActorTemp = (TowerComponentActor)a[0];
            towerComponentActors.Add(towerComponentActorTemp);
        }

        private void UpdateTower(object[] a)
        {
            TowerData towerDataTemp = (TowerData)a[0];

            TowerComponentActor towerComponentActorTemp = towerComponentActors.Where(x => x.TowerData == towerDataTemp).FirstOrDefault();
            if (towerComponentActorTemp)
            {
                UpdateTowerComponent(towerComponentActorTemp);
            }
        }

        private void FetchTowerDatas()
        {
            List<TowerData> towerDatas = DataManager.Instance.GameData.TowerDatas;

            for (int i = 0; i < towerComponentActors.Count; i++)
            {
                TowerData towerDataTemp = towerDatas[i];
                TowerComponentActor towerComponentActorTemp = towerComponentActors[i];

                towerComponentActorTemp.SetTowerData(towerDataTemp);
                UpdateTowerComponent(towerComponentActorTemp);
            }
        }

        private void UpdateTowerComponent(TowerComponentActor towerComponentActor)
        {
            towerComponentActor.UpdateCharacter();
            towerComponentActor.UpdateTowerLevel();
            towerComponentActor.UpdateRadius();
        }
    }
}