using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class CustomStart : MonoBehaviour
{
    [SerializeField] int index;

    void Start()
    {
        switch (index)
        {
            case 0:
                Event0();
                break;

        }
    }

    void Event0()
    {
        transform.DOLocalMove(transform.position + transform.right * 15, 5f)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

}
