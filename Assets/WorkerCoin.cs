using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class WorkerCoin : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public static bool dragging = false;
    public static bool currentlyActive = true;


    public Vector3 startPosition;

    private void OnDestroy()
    {
        dragging = false;
        currentlyActive = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragging) return;
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
        if(eventData.pointerCurrentRaycast.isValid)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name, eventData.pointerCurrentRaycast.gameObject);
        }
        GetComponent<Image>().raycastTarget = true;

        transform.DOMove(startPosition, 0.5f).SetDelay(0.5f).OnComplete(() => dragging = false); ;

    }


}
