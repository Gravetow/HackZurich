using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TileClick : MonoBehaviour, IPointerClickHandler
{
    [Inject]
    readonly SignalBus _signalBus;

    private BoxCollider boxCollider;

    public GameObject editBox;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        _signalBus.Subscribe<LeaveConstructionSignal>(OnLeaveConstruction);
        _signalBus.Subscribe<TileClickedSignal>(OnTileClicked);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<LeaveConstructionSignal>(OnLeaveConstruction);
        _signalBus.Unsubscribe<TileClickedSignal>(OnTileClicked);
    }

    public void OnLeaveConstruction(LeaveConstructionSignal leaveConstructionSignal)
    {
        boxCollider.enabled = true;
        editBox.SetActive(false);
    }

    public void OnTileClicked(TileClickedSignal tileClickedSignal)
    {
        boxCollider.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _signalBus.Fire(new TileClickedSignal() { position = transform.position });
        editBox.transform.position = transform.position;
        editBox.SetActive(true);
        editBox.GetComponent<Material>().DOColor(new Color(255,143,0,0) , 0.5f).SetLoops(-1);

    }
}
