using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public Transform LookAtTransform;

    public Transform CameraTransform;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        CameraTransform.RotateAround(LookAtTransform.position, -Vector3.up, eventData.delta.x * 5 *  Time.deltaTime);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

}
