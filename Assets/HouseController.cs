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
    public GameObject CostIndicator;
    public GameObject WorkerIndicator;
    public GameObject WorkerPlusIndicator;
    public GameObject ProfitIndicator;
    public GameObject UpgradeIndicator;

    private void Start()
    {
        switch (houseModelId)
        {
            case 4:
                Profit = 5;
                Capacity = 3;
                ProfitIndicator = Instantiate(GameObject.Find("ProfitIndicator"),transform);
                ProfitIndicator.transform.position = transform.position + Vector3.up * 50;
                WorkerIndicator = Instantiate(GameObject.Find("WorkerIndicator"), transform);
                WorkerIndicator.transform.position = transform.position + Vector3.up * 40;
                break;
            case 5:
                WorkerPlus = 5;
                UpgradeCost = 3;
                WorkerPlusIndicator = Instantiate(GameObject.Find("WorkerPlusIndicator"), transform);
                WorkerPlusIndicator.transform.position = transform.position + Vector3.up * 50;
                UpgradeIndicator = Instantiate(GameObject.Find("UpgradeIndicator"), transform);
                UpgradeIndicator.transform.position = transform.position + Vector3.up * 40;
                break;
            case 6:
                Cost = 5;
                WorkerPlus = 5;
                UpgradeCost = 3;
                CostIndicator = Instantiate(GameObject.Find("CostIndicator"), transform);
                CostIndicator.transform.position = transform.position + Vector3.up * 60;
                WorkerPlusIndicator = Instantiate(GameObject.Find("WorkerPlusIndicator"), transform);
                WorkerPlusIndicator.transform.position = transform.position + Vector3.up * 50;
                UpgradeIndicator = Instantiate(GameObject.Find("UpgradeIndicator"), transform);
                UpgradeIndicator.transform.position = transform.position + Vector3.up * 40;
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
