using UnityEngine;
using TMPro;
using Zenject;

public class ResourceView : MonoBehaviour
{
    readonly SignalBus _signalBus;

    public int resourceViewType = 0;

    [Inject] 
    public TextMeshPro TextMeshPro;

    public void Awake()
    {
        _signalBus.Subscribe<ResourceModelUpdatedSignal>(OnResourcesUpdated);
    }

    public void OnDestroy()
    {
        _signalBus.Unsubscribe<ResourceModelUpdatedSignal>(OnResourcesUpdated);
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
