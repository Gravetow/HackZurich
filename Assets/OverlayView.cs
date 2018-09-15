using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OverlayView : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;

    [Inject]
    public HouseModel houseModel;

    private Vector3 position;
    private GameObject currentHouse;

    private void Awake()
    {
        gameObject.SetActive(false);
        _signalBus.Subscribe<TileClickedSignal>(ActivateOverlay);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<TileClickedSignal>(ActivateOverlay);
    }

    private void ActivateOverlay(TileClickedSignal tileClickedSignal)
    {
        position = tileClickedSignal.position;
        gameObject.SetActive(true);
    }

    public void CancelConstruction()
    {
        _signalBus.Fire(new LeaveConstructionSignal() { buildingBuilt = false });
        gameObject.SetActive(false);
        Destroy(currentHouse);
    }

    public void ConfirmConstruction()
    {
        _signalBus.Fire(new LeaveConstructionSignal() { buildingBuilt = true });
        gameObject.SetActive(false);
        currentHouse = null;
    }

    public void ShowHouse(int houseId)
    {
        if(currentHouse != null)
        {
            Destroy(currentHouse);
        }

        currentHouse = Instantiate(houseModel.Houses[houseId]);
        currentHouse.transform.position = position;
    }

}
