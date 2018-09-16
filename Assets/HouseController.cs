using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using UnityEngine.EventSystems;

public class HouseController : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    //4 : farm, 5: House, 6: Hospital
    public int houseModelId;

    public int Cost;

    public int Profit;
    public int WorkerPlus;

    public int UpgradeCost;

    public int Capacity;
    public int CurrentWorkerCount;

    public GameObject dropIndicator;

    private void Start()
    {
        switch (houseModelId)
        {
            case 4:
                Profit = 5;
                Capacity = 3;
                break;
            case 5:
                WorkerPlus = 5;
                UpgradeCost = 3;
                break;
            case 6:
                Cost = 5;
                WorkerPlus = 5;
                UpgradeCost = 3;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!WorkerCoin.dragging) return;
        dropIndicator.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
        dropIndicator.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!WorkerCoin.dragging) return;
        dropIndicator.SetActive(false);
    }

   

}
