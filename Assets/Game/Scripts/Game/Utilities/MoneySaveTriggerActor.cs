using UnityEngine;
using Characters.Player;
using Game.Interface;
using Game.Event;

public class MoneySaveTriggerActor : MonoBehaviour, ITriggerEnter, ITriggerExit
{
    [Header("References")] 
    [SerializeField] private Transform moneyTargetTransform;
    [SerializeField] private Canvas canvas;

    #region private

    private IGameEventSystem gameEventSystem = new GameEventSystem();
    private PlayerStackActor playerStackActor;

    private bool playerIsIn;
    private readonly float perTakeDuration = 0.1f;
    private float currentPerTakeDuration;

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
        gameEventSystem.SaveEvent(GameEvents.SendPlayerComponentActor, status, GetPlayerComponentActor);
    }

    #endregion

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    private void GetPlayerComponentActor(object[] a)
    {
        PlayerComponentActor playerComponentActorTemp = (PlayerComponentActor)a[0];
        playerStackActor = playerComponentActorTemp.PlayerStackActor;
    }

    public void TriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = true;
        }
    }

    public void TriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = false;
        }
    }

    private void Update()
    {
        if (playerIsIn)
        {
            currentPerTakeDuration += Time.deltaTime;
            if (currentPerTakeDuration >= perTakeDuration)
            {
                TakeMoney();
                currentPerTakeDuration = 0;
            }
        }
    }

    private void TakeMoney()
    {
        playerStackActor.SellMoney(moneyTargetTransform);
    }
}