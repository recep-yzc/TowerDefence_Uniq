using DG.Tweening;
using Game.Interface;
using UnityEngine;

public class MoneyActor : MonoBehaviour, IPoolItem
{
    [Header("Properties")]
    [SerializeField] private float collectAndSellSpeedDuration = 0.3f;

    [Header("References")] 
    [SerializeField] private Rigidbody rb;

    public bool IsCollected { get; private set; }
    public bool IsAvailableForSpawn { get; set; }
    public float Money { get; private set; }

    #region private

    private Transform parent;

    private Tween tweenMove;

    #endregion

    private void Awake()
    {
        parent = transform.parent;
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetMoney(float money)
    {
        Money = money;
    }

    public void DropIt()
    {
        IsCollected = false;
        IsAvailableForSpawn = false;

        rb.isKinematic = false;

        Vector3 force = (Vector3.up * 2) + Random.insideUnitSphere;

        rb.AddForce(force, ForceMode.Impulse);
    }

    public void CollectIt(Transform stackMoneyParent, Vector3 point)
    {
        IsCollected = true;
        rb.isKinematic = true;

        transform.SetParent(stackMoneyParent);

        #region Movement
        tweenMove.Kill();
        Rotate(Vector3.zero);
        Move(point, false);
        #endregion
    }

    public void SellIt(Vector3 point, CallBack moneyCallBack)
    {
        transform.SetParent(parent);

        #region Movement
        tweenMove.Kill();
        Rotate(Vector3.zero);
        Move(point, true, moneyCallBack);
        #endregion
    }

    private void Move(Vector3 point, bool isSell, CallBack moneyCallBack = null)
    {
        if (isSell)
        {
            tweenMove = transform.DOMove(point, collectAndSellSpeedDuration).SetLink(gameObject).OnComplete(() =>
            {
                moneyCallBack?.Invoke();
                SetLocalPosition(Vector3.zero);

                IsAvailableForSpawn = true;
            });
        }
        else
        {
            tweenMove = transform.DOLocalMove(point, collectAndSellSpeedDuration).SetLink(gameObject);
        }
    }

    private void Rotate(Vector3 point)
    {
        tweenMove = transform.DOLocalRotate(point, collectAndSellSpeedDuration).SetLink(gameObject);
    }
}