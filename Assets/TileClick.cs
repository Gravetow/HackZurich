using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TileClick : MonoBehaviour, IPointerClickHandler
{
    [Inject]
    readonly SignalBus _signalBus;

    public void OnPointerClick(PointerEventData eventData)
    {
        _signalBus.Fire(new TileClickedSignal() { position = transform.position });
    }
}
