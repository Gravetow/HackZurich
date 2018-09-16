using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HouseController : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    //4 : farm, 5: House, 6: Hospital
    public int houseModelId;

    public int Cost;
    public int CurrentPaid;

    public int Profit;
    public int WorkerPlus;

    public int UpgradeCost;
    public int CurrentUpgradeCount;

    public int Capacity;
    public int CurrentWorkerCount;

    public GameObject dropIndicator;
    public GameObject CostIndicator;
    public GameObject WorkerIndicator;
    public GameObject WorkerPlusIndicator;
    public GameObject ProfitIndicator;
    public GameObject UpgradeIndicator;

    public GameObject UpgradedModel;

    public SignalEmitter SignalEmitter;

    public void Construct()
    {
        switch (houseModelId)
        {
            case 4:
                Profit = 2;
                Capacity = 3;
                ProfitIndicator = Instantiate(GameObject.Find("ProfitIndicator"), transform);
                ProfitIndicator.transform.position = transform.position + Vector3.up * 50;
                WorkerIndicator = Instantiate(GameObject.Find("WorkerIndicator"), transform);
                WorkerIndicator.transform.position = transform.position + Vector3.up * 40;
                break;
            case 5:
                WorkerPlus = 5;
                //UpgradeCost = 3;
                WorkerPlusIndicator = Instantiate(GameObject.Find("WorkerPlusIndicator"), transform);
                WorkerPlusIndicator.transform.position = transform.position + Vector3.up * 50;
                //UpgradeIndicator = Instantiate(GameObject.Find("UpgradeIndicator"), transform);
                //UpgradeIndicator.transform.position = transform.position + Vector3.up * 40;
                SignalEmitter.AddToWorkers(5);
                WorkerPlusIndicator.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(WorkerPlusIndicator));
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

        if (WorkerCoin.currentType == 0 && CurrentWorkerCount < Capacity)
        {
            dropIndicator.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
            dropIndicator.SetActive(true);
            WorkerCoin.houseCountroller = this;
        }

        if (WorkerCoin.currentType == 1 && CurrentPaid < Cost)
        {
            dropIndicator.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
            dropIndicator.SetActive(true);
            WorkerCoin.houseCountroller = this;
        }

        if (WorkerCoin.currentType == 2 && CurrentUpgradeCount < UpgradeCost)
        {
            dropIndicator.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
            dropIndicator.SetActive(true);
            WorkerCoin.houseCountroller = this;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!WorkerCoin.dragging) return;
        dropIndicator.SetActive(false);
        WorkerCoin.houseCountroller = null;
    }

    public void Drop(WorkerCoin coin)
    {
        if (WorkerCoin.currentType == 0)
        {
            CurrentWorkerCount++;

            if (CurrentWorkerCount < Capacity)
            {
                WorkerIndicator.transform.GetChild(CurrentWorkerCount - 1).GetComponent<Image>().color = Color.white;
            }
            else
            {
                WorkerIndicator.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(WorkerIndicator));
                ProfitIndicator.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(ProfitIndicator));
                SignalEmitter.AddToMoney(Profit);
            }
        }

        if (WorkerCoin.currentType == 1)
        {
            CurrentPaid++;
            if (CurrentPaid < Cost)
            {
                CostIndicator.transform.GetChild(CurrentPaid - 1).GetComponent<Image>().color = Color.white;
            }
            else
            {
                CostIndicator.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(CostIndicator));
                WorkerPlusIndicator.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(WorkerPlusIndicator));
                SignalEmitter.AddToWorkers(WorkerPlus);
            }
        }

        if (WorkerCoin.currentType == 2)
        {
            CurrentUpgradeCount++;
            if (CurrentUpgradeCount < UpgradeCost)
            {
                UpgradeIndicator.transform.GetChild(CurrentUpgradeCount - 1).GetComponent<Image>().color = Color.white;
            }
            else
            {
                Instantiate(UpgradedModel, transform);

                UpgradeIndicator.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(UpgradeIndicator));

                if (CostIndicator != null)
                {
                    CostIndicator.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(CostIndicator));
                }

                if (WorkerPlusIndicator != null)
                {
                    WorkerPlusIndicator.transform.DOScale(0, 0.5f).OnComplete(() => Destroy(WorkerPlusIndicator));

                }

                Profit = 10;
                Capacity = 2;

                WorkerIndicator = Instantiate(GameObject.Find("UpgradedWorkerIndicator"), transform);
                WorkerIndicator.transform.position = transform.position + Vector3.up * 40;
                ProfitIndicator = Instantiate(GameObject.Find("UpgradedProfitIndicator"), transform);
                ProfitIndicator.transform.position = transform.position + Vector3.up * 50;
            }
        }
    }

}
