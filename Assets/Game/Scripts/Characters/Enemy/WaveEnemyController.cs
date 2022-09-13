using DG.Tweening;
using Game.Event;
using Game.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Enemy
{
    public class WaveEnemyController : MonoBehaviour
    {
        [Header("Properties")] 
        [SerializeField] private int enemyCount = 10;

        [Header("References")] 
        [SerializeField] private Transform enemySpawnTransform;
        [SerializeField] private EnemyComponentActor enemyComponentActorPrefab;

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();
        private List<EnemyComponentActor> enemyComponentActors = new();

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
            gameEventSystem.SaveEvent(GameEvents.CreateEnemy, status, CreateEnemy);
            gameEventSystem.SaveEvent(GameEvents.FinishWave, status, FinishWave);

            gameEventSystem.SaveEvent(GameEvents.SendEnemySpawnTransform, status, GetEnemySpawnTransform);
        }

        #endregion

        private void FinishWave(object[] a)
        {
            bool isSuccess = (bool)a[0];

            if (!isSuccess)
            {
                DOVirtual.DelayedCall(0.1f, () =>
                {
                    foreach (EnemyComponentActor enemyCompenentHelperTemp in enemyComponentActors)
                    {
                        Destroy(enemyCompenentHelperTemp.gameObject);
                    }

                    enemyComponentActors.Clear();
                });
            }
        }

        private void GetEnemySpawnTransform(object[] a)
        {
            enemySpawnTransform.position = ((Transform)a[0]).position;
        }

        private void CreateEnemy(object[] a)
        {
            Vector3[] randomPoints = GeneratePoints(enemyCount);

            for (int i = 0; i < enemyCount; i++)
            {
                EnemyComponentActor enemyComponentActorTemp =
                    Instantiate(enemyComponentActorPrefab, enemySpawnTransform);
                enemyComponentActorTemp.transform.localPosition = randomPoints[i];

                enemyComponentActorTemp.SetDeadCallBack(() =>
                {
                    enemyComponentActors.Remove(enemyComponentActorTemp);
                    CheckEnemiesAreDead();
                });

                enemyComponentActors.Add(enemyComponentActorTemp);
            }
        }

        private Vector3[] GeneratePoints(int size)
        {
            Vector3[] randomPoints = new Vector3[size];

            if (size <= 1)
            {
                randomPoints[0] = Vector2.zero;
            }
            else
            {
                for (int i = 0; i < randomPoints.Length; i++)
                {
                    float k = i + 1f;
                    float r = Mathf.Sqrt((k) / size);
                    float theta = Mathf.PI * Mathf.Sqrt(6) * k;
                    float x = r * Mathf.Cos(theta) * 1;
                    float y = r * Mathf.Sin(theta) * 1;

                    Vector3 tmpRandomPoint = new Vector3(x, 0f, y);
                    randomPoints[i] = tmpRandomPoint;
                }
            }

            return randomPoints;
        }

        private void CheckEnemiesAreDead()
        {
            if (enemyComponentActors.Count == 0)
            {
                gameEventSystem.Publish(GameEvents.FinishWave, true);
            }
        }
    }
}