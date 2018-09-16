﻿using UnityEngine;
using TMPro;
using Zenject;

public class ResourceView : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;

    public int resourceViewType = 0;

    public int resourceAmount;

    public TextMeshProUGUI amount;

    public void Awake()
    {
        _signalBus.Subscribe<ResourceModelUpdatedSignal>(OnResourcesUpdated);
        _signalBus.Subscribe<AddMoneySignal>(OnAddMoney);
        _signalBus.Subscribe<AddWorkerSignal>(OnAddWorker);
        _signalBus.Subscribe<WorkerPercentageCalculatedSignal>(OnTransactionsAcquired);
    }

    public void OnDestroy()
    {
        _signalBus.Unsubscribe<ResourceModelUpdatedSignal>(OnResourcesUpdated);
        _signalBus.Unsubscribe<WorkerPercentageCalculatedSignal>(OnTransactionsAcquired);
    }

    public void OnAddMoney(AddMoneySignal signal)
    {
        if (resourceViewType == 1) return;
        resourceAmount += signal.amount;
        amount.SetText("" + resourceAmount);
    }

    public void OnAddWorker(AddWorkerSignal signal)
    {
        if (resourceViewType == 0) return;
        resourceAmount += signal.amount;
        amount.SetText("" + resourceAmount);

    }

    public void OnTransactionsAcquired(WorkerPercentageCalculatedSignal signal)
    {
        if (resourceViewType == 0) return;


        amount.SetText("" + (int)signal.workerPercentage);

        _signalBus.Fire(new NotificationSignal() { rewardType = 0, rewardCount = (int)signal.workerPercentage });

    }

    public void OnResourcesUpdated(ResourceModelUpdatedSignal signal)
    {
        int resourceValue = 0;

        switch(resourceViewType)
        {
            case 0:
                resourceValue = signal.ResourceModel.Currency;
                break;
            case 1:
                resourceValue = signal.ResourceModel.Workers;
                break;
        }

        //TextMeshPro.SetText("" + resourceValue);
    }
}
