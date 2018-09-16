using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using System;

public class NotificationView : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;

    public GameObject[] Notifications;


    private void Awake()
    {
        _signalBus.Subscribe<NotificationSignal>(DisplayNotification);
        _signalBus.Subscribe<LeaveConstructionSignal>(OnLeaveConstruct);
        _signalBus.Subscribe<TileClickedSignal>(OnConstructMode);
    }

    private void OnLeaveConstruct()
    {
        GetComponent<CanvasGroup>().alpha = 1;

    }

    private void OnConstructMode()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<NotificationSignal>(DisplayNotification);

    }

    public void DisplayNotification(NotificationSignal notificationSignal)
    {
        for(int i = 0; i < notificationSignal.rewardCount; i++) {
            GameObject Notification = Instantiate(Notifications[notificationSignal.rewardType], transform);
            Notification.transform.localScale = Vector3.zero;
            Notification.SetActive(true);
            Notification.transform.DOScale(1, 0.5f).SetEase(Ease.InOutQuad);
        }
    }



}
