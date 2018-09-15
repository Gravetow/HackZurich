using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OverlayView : MonoBehaviour
{

    [Inject]
    readonly SignalBus _signalBus;

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
        Debug.Log("aha");
        gameObject.SetActive(true);
    }

    public void DeactivateOverlay()
    {
        _signalBus.Fire<LeaveConstructionSignal>();
        gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
