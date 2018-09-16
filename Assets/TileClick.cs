using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TileClick : MonoBehaviour, IPointerClickHandler
{
    [Inject]
    readonly SignalBus _signalBus;

    [Inject]
    public HouseModel houseModel;

    private BoxCollider boxCollider;

    public GameObject editBox;
    private GameObject tile;
    private bool clicked;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        _signalBus.Subscribe<LeaveConstructionSignal>(OnLeaveConstruction);
        _signalBus.Subscribe<TileClickedSignal>(OnTileClicked);

        tile = Instantiate(houseModel.Houses[Random.Range(0,3)]);
        tile.transform.position = transform.position;
        tile.transform.SetParent(transform);
        tile.transform.localPosition +=  0.5f* Vector3.up;

    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<LeaveConstructionSignal>(OnLeaveConstruction);
        _signalBus.Unsubscribe<TileClickedSignal>(OnTileClicked);
    }

    public void OnLeaveConstruction(LeaveConstructionSignal leaveConstructionSignal)
    {
        if (clicked && leaveConstructionSignal.buildingBuilt != null) {
            tile = leaveConstructionSignal.buildingBuilt;
            BoxCollider box = tile.AddComponent<BoxCollider>();
            box.center = boxCollider.center;
            box.size = boxCollider.size;
            Destroy(boxCollider);
        } else
        {
            tile.SetActive(true);
        }
        boxCollider.enabled = true;
        editBox.SetActive(false);
        clicked = false;
    }

    public void OnTileClicked(TileClickedSignal tileClickedSignal)
    {
        boxCollider.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _signalBus.Fire(new TileClickedSignal() { position = transform.position });
        clicked = true;
        editBox.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
        tile.SetActive(false);
        editBox.SetActive(true);

    }

}
