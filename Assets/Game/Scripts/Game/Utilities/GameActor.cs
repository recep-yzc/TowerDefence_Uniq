using Game.Event;
using Game.Interface;
using UnityEngine;

public class GameActor : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform enemySpawnTransform;
    [SerializeField] private Transform enemyTargetTransform;

    #region Private

    private IGameEventSystem gameEventSystem = new GameEventSystem();

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
        gameEventSystem.SaveEvent(GameEvents.InitWave, status, InitWave);
    }

    #endregion

    private void InitWave(object[] a)
    {
        gameEventSystem.Publish(GameEvents.CreateEnemy);

        gameEventSystem.Publish(GameEvents.SendEnemySpawnTransform, enemySpawnTransform);
        gameEventSystem.Publish(GameEvents.SendEnemyTargetTransform, enemyTargetTransform);
    }
}