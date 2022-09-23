using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TriggerBarActor : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float activeDuration = 1f;

    [Header("References")]
    [SerializeField] private Image imgBar;

    public void SetOpenState(bool isOpen, CallBack callBack)
    {
        ResetBar();
        if (isOpen)
        {
            FillBar(callBack);
        }
    }

    private void FillBar(CallBack callBack)
    {
        imgBar.DOFillAmount(1, activeDuration).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(() =>
        {
            callBack?.Invoke();
            imgBar.fillAmount = 0;
        });
    }

    private void ResetBar()
    {
        imgBar.DOKill();
        imgBar.fillAmount = 0;
    }
}