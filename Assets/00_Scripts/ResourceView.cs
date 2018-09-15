using UnityEngine;
using TMPro;
using Zenject;

public class ResourceView : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;

    public int resourceViewType = 0;

    [Inject] 
    public TextMeshPro TextMeshPro;

    public void Awake()
    {
        _signalBus.Subscribe<ResourceModelUpdatedSignal>(OnResourcesUpdated);
        _signalBus.Subscribe<TransactionsAcquiredSignal>(OnTransactionsAcquired);
    }

    public void OnDestroy()
    {
        _signalBus.Unsubscribe<ResourceModelUpdatedSignal>(OnResourcesUpdated);
        _signalBus.Unsubscribe<TransactionsAcquiredSignal>(OnTransactionsAcquired);
    }

    public void OnTransactionsAcquired(TransactionsAcquiredSignal signal)
    {
        Debug.LogError(signal.transactions.Length);
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

        TextMeshPro.SetText("" + resourceValue);
    }
}
