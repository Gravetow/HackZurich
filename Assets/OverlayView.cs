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

    public GameObject[] Descriptions;

    private Vector3 position;
    private GameObject currentHouse;
    private GameObject currentDescription;

    public GameObject DropIndicator;
    public SignalEmitter SignalEmitter;


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
        _signalBus.Fire(new LeaveConstructionSignal() { buildingBuilt = null });
        gameObject.SetActive(false);
        Destroy(currentHouse);
        currentDescription.SetActive(false);
    }

    public void ConfirmConstruction()
    {
        _signalBus.Fire(new LeaveConstructionSignal() { buildingBuilt = currentHouse });
        gameObject.SetActive(false);
        currentHouse.GetComponent<HouseController>().Construct();
        currentHouse = null;
        currentDescription.SetActive(false);


    }

    public void ShowHouse(int houseId)
    {
        if(currentHouse != null)
        {
            Destroy(currentHouse);
            currentDescription.SetActive(false);
        }

        currentDescription = Descriptions[houseId - 4];
        currentDescription.SetActive(true);
        currentHouse = Instantiate(houseModel.Houses[houseId]);
        HouseController house = currentHouse.AddComponent<HouseController>();
        house.houseModelId = houseId;
        house.dropIndicator = DropIndicator;
        house.SignalEmitter = SignalEmitter;
        currentHouse.transform.position = position;
        currentHouse.transform.eulerAngles += new Vector3(0, (float)Random.Range(0, 4) * 90f, 0);
    }

}
