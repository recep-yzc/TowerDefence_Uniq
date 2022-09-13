using DG.Tweening;
using Game.Event;
using Game.Interface;
using TMPro;
using UnityEngine;

public class UiActor : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TextMeshProUGUI txtWave;
    [SerializeField] private TextMeshProUGUI txtMoney;
    [SerializeField] private TextMeshProUGUI txtHealth;

    #region private

    private IGameEventSystem gameEventSystem = new GameEventSystem();

    private Tween tweenMoney;
    private Tween healthTween;

    private float beforeHealth;
    private float beforeMoney;

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

    public void Listen(bool status)
    {
        gameEventSystem.SaveEvent(GameEvents.UpdateMoney, status, UpdateMoneyText);
        gameEventSystem.SaveEvent(GameEvents.UpdateWaveLevel, status, UpdateWaveLevel);
        gameEventSystem.SaveEvent(GameEvents.UpdateCastleHealth, status, UpdateCastleHealth);
    }

    #endregion

    private void UpdateWaveLevel(object[] a)
    {
        int wave = (int)a[0];
        txtWave.text = $"Wave {wave + 1}";
    }

    private void UpdateMoneyText(object[] a)
    {
        float targetMoney = (float)a[0];
        tweenMoney.Kill();
        tweenMoney = DOTween.To(() => beforeMoney, x => beforeMoney = x, targetMoney, 1).OnUpdate(() =>
        {
            txtMoney.text = $"{(int)beforeMoney} <sprite=0>";
        });
    }

    private void UpdateCastleHealth(object[] a)
    {
        float targetHealth = (float)a[0];

        healthTween.Kill();
        healthTween = DOTween.To(() => beforeHealth, x => beforeHealth = x, targetHealth, 0.5f).SetEase(Ease.Linear)
            .SetLink(gameObject)
            .OnUpdate(() => { txtHealth.text = $"{(int)beforeHealth} <sprite=0>"; });
    }
}