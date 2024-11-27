using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] Transform door;

    Vector3 origin = new Vector3(2.5f, 1.3f, -0.2f);
    float value = 2.5f;

    public void Activate(bool activate)
    {
        door.DOPause();
        if (activate)
        {
            float time = Mathf.Abs(door.transform.localPosition.y - value) / value;
            door.DOLocalMove(Vector3.up * value, time).SetEase(Ease.Linear);
        }
        else
        {
            float time = Mathf.Abs(door.transform.localPosition.y) / value;
            door.DOLocalMove(origin, time).SetEase(Ease.Linear);
        }
    }
}
