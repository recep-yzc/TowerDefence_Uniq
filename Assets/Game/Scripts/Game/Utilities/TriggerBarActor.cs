using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TriggerBarActor : MonoBehaviour
{
    [Header("Properties")] 
    [SerializeField] private float activeDuration = 1f;

    [Header("References")] 
    [SerializeField] private Transform holder;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image imgBar;

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    public void SetOpenState(bool isOpen, CallBack callBack)
    {
        float duration = 0f;

        ResetBar();
        if (isOpen)
        {
            duration = 0.3f;
        }

        holder.DOKill();
        holder.DOScale(isOpen ? Vector2.one : Vector2.zero, duration).SetEase(isOpen ? Ease.OutBack : Ease.InBack)
            .SetLink(gameObject).OnComplete(() =>
            {
                if (isOpen)
                {
                    FillBar(callBack);
                }
            });
    }

    private void FillBar(CallBack callBack)
    {
        imgBar.DOFillAmount(1, activeDuration).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(() =>
        {
            callBack?.Invoke();
            holder.localScale = Vector3.zero;
        });
    }

    private void ResetBar()
    {
        imgBar.DOKill();
        imgBar.fillAmount = 0;
    }
}