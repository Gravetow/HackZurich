using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class ResourceModelUpdatedSignal
{
    public ResourceModel ResourceModel;
}

public class TileClickedSignal
{
    public Vector3 position;
}

public class LeaveConstructionSignal
{
    public GameObject buildingBuilt;
}

public class WorkerPercentageCalculatedSignal
{
    public double workerPercentage;
}

public class NotificationSignal
{
    public string notification;
    public int rewardType;
    public int rewardCount;
}

public class AddMoneySignal
{
    public int amount;
}

public class AddWorkerSignal
{
    public int amount;
}

public class SubstractMoneySignal
{
    public int amount;
}

public class SubstractWorkerSignal
{
    public int amount;
}