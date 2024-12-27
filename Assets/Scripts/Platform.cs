using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Vector3 origin;
    [SerializeField] Vector3 addLocation;

    Vector3 arrivalLocation;

    void Start()
    {
        origin = transform.position;
        arrivalLocation = origin + addLocation;
    }

    //need to set time
    public void Activate(bool activate)
    {
        transform.DOPause();
        if (activate)
        {
            transform.DOLocalMove(arrivalLocation, 2).SetEase(Ease.Linear);
        }
        else
        {
            transform.DOLocalMove(origin, 2).SetEase(Ease.Linear);
        }
    }
}
