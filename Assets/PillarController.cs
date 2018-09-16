using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PillarController : MonoBehaviour
{

    private void Awake()
    {
        transform.localPosition += Vector3.down * 400;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(-250, Random.Range(2, 5)).SetEase(Ease.InOutQuad);
    }

}
