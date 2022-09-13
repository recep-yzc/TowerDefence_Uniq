using Game.Event;
using Game.Interface;
using System.Collections;
using UnityEngine;

public class WaveActor : MonoBehaviour
{
    #region Private

    private IGameEventSystem gameEventSystem = new GameEventSystem();

    #endregion

    public void InitWave()
    {
        StartCoroutine(WaitOneFrame());

        IEnumerator WaitOneFrame()
        {
            yield return null;
            gameEventSystem.Publish(GameEvents.InitWave);
        }
    }
}