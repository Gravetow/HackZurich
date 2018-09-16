using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class NotificationView : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;

    private void Awake()
    {
        _signalBus.Subscribe<NotificationSignal>(DisplayNotification);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<NotificationSignal>(DisplayNotification);

    }

    public void DisplayNotification(NotificationSignal notificationSignal)
    {
        Sequence displayNotificationSequence = DOTween.Sequence();
        displayNotificationSequence.Append(transform.DOMoveY(0, 1f));
        displayNotificationSequence.Append(transform.DOLocalMoveY(300, 1f).SetDelay(5));
    }

}
