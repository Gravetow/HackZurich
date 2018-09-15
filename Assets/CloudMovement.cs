using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(transform.localPosition.y + Random.Range(1f, 4f), Random.Range(1f,5f)).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

}
