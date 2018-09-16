using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPillarController : MonoBehaviour
{

    public Button spawnButton;
    public GameObject[] Pillars;
    public GameObject NewStreet;
    public SpawnCars SpawnCars;

    public void SpawnNewRow()
    {
        foreach (GameObject pillar in Pillars)
        {
            pillar.SetActive(true);
        }
        NewStreet.transform.DOLocalMoveY(0, 1f).SetEase(Ease.InOutQuad).SetDelay(4);
        SpawnCars.paths[0].start += new Vector3(0,0,40);
        SpawnCars.paths[1].start += new Vector3(0, 0, 40);
        SpawnCars.paths[2].start -= new Vector3(0, 0, 40);
        SpawnCars.paths[3].start -= new Vector3(0, 0, 40);
    }
}
    