using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] Transform door;

    Vector3 origin = new Vector3(2.5f, 1.3f, -0.2f);
    float value = 3.5f;

    public void Activate(bool activate)
    {
        door.DOPause();
        if (activate)
        {
            float time = Mathf.Abs(door.transform.localPosition.y - value) / 3;
            door.DOLocalMoveY(value, time).SetEase(Ease.Linear);
        }
        else
        {
            float time = Mathf.Abs(door.transform.localPosition.y - origin.y) / 3;
            door.DOLocalMove(origin, time).SetEase(Ease.Linear);
        }
    }
}
