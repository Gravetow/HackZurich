using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SignalEmitter : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;

    public void AddToMoney(int amount)
    {
        _signalBus.Fire(new AddMoneySignal(){ amount = amount});
        _signalBus.Fire(new NotificationSignal() { rewardType = 1, rewardCount = amount });
    }

    public void AddToWorkers(int amount)
    {
        _signalBus.Fire(new AddWorkerSignal() { amount = amount });
        _signalBus.Fire(new NotificationSignal() { rewardType = 0, rewardCount = amount });

    }

}
