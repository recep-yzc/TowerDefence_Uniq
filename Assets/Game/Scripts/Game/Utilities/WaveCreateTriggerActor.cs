using DG.Tweening;
using Game.Event;
using Game.Interface;
using UnityEngine;

public class WaveCreateTriggerActor : MonoBehaviour, ITriggerEnter, ITriggerExit
{
    [Header("References")] 
    [SerializeField] private GameObject holder;

    [SerializeField] private TriggerBarActor triggerBarActor;
    [SerializeField] private Canvas canvas;

    #region private

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
        gameEventSystem.SaveEvent(GameEvents.FinishWave, status, FinishWave);
    }

    #endregion

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    private void FinishWave(object[] args)
    {
        holder.SetActive(true);
    }

    public void TriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerBarActor.SetOpenState(true, StartWave);
        }
    }

    public void TriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerBarActor.SetOpenState(false, null);
        }
    }

    private void StartWave()
    {
        holder.SetActive(false);

        gameEventSystem.Publish(GameEvents.CreateWave);

        DOVirtual.DelayedCall(1f, () => { gameEventSystem.Publish(GameEvents.BtnClickPlay); });
    }
}