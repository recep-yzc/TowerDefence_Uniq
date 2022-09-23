using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Characters.Tower.Ui;
using Game.Event;
using Game.Interface;
using Game.Helper;

public class CharacterCardMouseActor : MonoBehaviour
{
    [Header("References")]
    public GraphicRaycaster Raycaster;
    public EventSystem EventSystem;

    #region private
    private IGameEventSystem gameEventSystem = new GameEventSystem();

    private List<RaycastResult> results = new();

    private TowerSlotCardActor towerSlotCardActor;

    private TowerSlotActor oldTowerSlotActor;
    private TowerSlotActor newTowerSlotActor;

    private PointerEventData pointerEventData;

    private bool isHasItem;
    private bool IsMouseDown => Input.GetMouseButton(0);
    private bool IsMouseUp => Input.GetMouseButtonUp(0);

    private float cardMovementSpeed = 30f;
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
        gameEventSystem.SaveEvent(GameEvents.ClickDownSlot, status, ClickDownSlot);
    }

    #endregion

    private void Awake()
    {
        pointerEventData = new PointerEventData(EventSystem);
    }

    private void ClickDownSlot(object[] a)
    {
        towerSlotCardActor = (TowerSlotCardActor)a[0];
        oldTowerSlotActor = towerSlotCardActor.TowerSlotActor;

        isHasItem = true;
    }

    private void Update()
    {
        if (IsMouseDown && isHasItem)
        {
            HandleMove();
        }

        if (IsMouseUp && isHasItem)
        {
            HandleMouseUp();
        }
    }

    private void HandleMove()
    {
        pointerEventData.position = Input.mousePosition;

        results.Clear();
        Raycaster.Raycast(pointerEventData, results);
        FindMergeSlot();

        towerSlotCardActor.UpdateSiblingIndex();

        Vector2 lerpPos = Vector3.Slerp(towerSlotCardActor.GetPosition(), pointerEventData.position, Time.deltaTime * cardMovementSpeed);
        towerSlotCardActor.SetPosition(lerpPos);
    }

    private void HandleMouseUp()
    {
        TowerSlotActor towerSlotActor;

        if (!newTowerSlotActor)
        {
            towerSlotActor = oldTowerSlotActor;
        }
        else
        {
            towerSlotActor = newTowerSlotActor;
        }

        gameEventSystem.Publish(GameEvents.ClickUpSlot, towerSlotCardActor, towerSlotActor);

        towerSlotCardActor = null;
        isHasItem = false;
    }

    private void FindMergeSlot()
    {
        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("MergeSlot"))
                {
                    newTowerSlotActor = result.gameObject.GetComponent<ParentFindHelper>().GetParent<TowerSlotActor>();
                    break;
                }
                else
                {
                    newTowerSlotActor = null;
                }
            }
        }
        else
        {
            newTowerSlotActor = null;
        }
    }
}