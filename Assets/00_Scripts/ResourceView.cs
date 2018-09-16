﻿using UnityEngine;
using TMPro;
using Zenject;

public class ResourceView : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;

    public int resourceViewType = 0;


    public TextMeshProUGUI amount;

    public void Awake()
    {
        _signalBus.Subscribe<ResourceModelUpdatedSignal>(OnResourcesUpdated);
        _signalBus.Subscribe<WorkerPercentageCalculatedSignal>(OnTransactionsAcquired);
    }

    public void OnDestroy()
    {
        _signalBus.Unsubscribe<ResourceModelUpdatedSignal>(OnResourcesUpdated);
        _signalBus.Unsubscribe<WorkerPercentageCalculatedSignal>(OnTransactionsAcquired);
    }

    public void OnTransactionsAcquired(WorkerPercentageCalculatedSignal signal)
    {
        int resourceValue = 0;

        switch (resourceViewType)
        {
            case 0:
                resourceValue = (int)signal.workerPercentage;
                break;
            case 1:
                resourceValue = (int)signal.workerPercentage;
                break;
        }

        amount.SetText("" + resourceValue);

        _signalBus.Fire(new NotificationSignal() { notification = "aha! + " + resourceValue });

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
