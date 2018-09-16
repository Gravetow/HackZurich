using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;
using Zenject;

public class CameraMovement : MonoBehaviour, UnityEngine.EventSystems.IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float RotationSpeed;

    public Vector3 LookAtPosition = Vector3.zero;

    public Transform CameraTransform;
    public Camera MainCamera;

    private bool zoomed = false;

    [Inject]
    readonly SignalBus _signalBus;

    private void Awake()
    {
        _signalBus.Subscribe<TileClickedSignal>(ZoomTo);
        _signalBus.Subscribe<LeaveConstructionSignal>(ResetZoom);

        MainCamera.transform.LookAt(LookAtPosition);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<TileClickedSignal>(ZoomTo);
        _signalBus.Unsubscribe<LeaveConstructionSignal>(ResetZoom);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        CameraTransform.RotateAround(LookAtPosition, -Vector3.up, eventData.delta.x * RotationSpeed *  Time.deltaTime);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void ZoomTo(TileClickedSignal tileClicked)
    {
        if (zoomed)
        {
            return;
        }
        LookAtPosition = tileClicked.position;
        CameraTransform.DOLookAt(LookAtPosition, 0.5f).SetEase(Ease.InOutQuad);
        MainCamera.DOFieldOfView(30f, 0.5f).SetEase(Ease.InOutQuad);

        zoomed = true;
    }

    public void ResetZoom()
    {
        LookAtPosition = Vector3.zero;
        CameraTransform.DOLookAt(Vector3.zero, 0.5f).SetEase(Ease.InOutQuad);
        MainCamera.DOFieldOfView(60f, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() => zoomed = false);

    }

}
