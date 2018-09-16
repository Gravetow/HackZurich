using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class WorkerCoin : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public int type = 0;
    public static int currentType = 0;
    public static bool dragging = false;
    public static bool currentlyActive = true;
    public static HouseController houseCountroller = null;

    public Vector3 startPosition;

    private void OnDestroy()
    {
        dragging = false;
        currentlyActive = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragging) return;
        currentType = type;
        dragging = true;
        startPosition = transform.position;

        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.DOMove(eventData.position, 0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (houseCountroller == null)
        {
            GetComponent<Image>().raycastTarget = true;
            transform.DOMove(startPosition, 0.5f).SetDelay(0.5f).OnComplete(() => dragging = false);
        } else
        {
            houseCountroller.Drop(this);
            transform.DOScale(0, 0.5f).OnComplete(() => Destroy(gameObject));
        }




    }


}
