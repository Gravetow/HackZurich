using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class WorkerCoin : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public static bool dragging = false;

    private void OnDestroy()
    {
        dragging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.DOMove(eventData.position, 0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        if(eventData.pointerCurrentRaycast.isValid)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name, eventData.pointerCurrentRaycast.gameObject);
        }
        GetComponent<Image>().raycastTarget = true;

    }


}
