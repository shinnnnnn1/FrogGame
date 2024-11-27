using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TriggerButton : MonoBehaviour, IInteractable
{
    [Space(10f)] [SerializeField] bool isFloor = true;

    [Space(10f)]
    [SerializeField] UnityEvent[] on;
    [SerializeField] UnityEvent[] off;

    [SerializeField] Transform button;

    Vector3 origin;
    float value;

    bool isActivated;

    void Start()
    {
        origin = button.position;
    }

    public void Interact(PlayerCtrl player)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        float time = (button.transform.localPosition.y + 0.097f) * 5;

        button.DOPause();
        button.DOLocalMove(Vector3.down * 0.097f, time)
            .SetEase(Ease.Linear).OnUpdate(IsArrival);
    }

    void OnTriggerExit(Collider other)
    {
        if(isActivated)
        {
            Trigger(false);
        }

        float time = (button.transform.localPosition.y + 0.097f) * 5;

        button.DOPause();
        button.DOLocalMove(Vector3.down * 0.097f, time)
            .SetEase(Ease.Linear);
    }

    void IsArrival()
    {
        if(button.localPosition.y <= -1 && !isActivated)
        {
            isActivated = true;
            button.DOPause();
            Trigger(true);
        }
    }

    void Trigger(bool activate)
    {
        if(activate)
        {
            for (int i = 0; i < on.Length; i++)
            {
                on[i].Invoke();
            }
        }
        else
        {
            for (int i = 0; i < off.Length; i++)
            {
                on[i].Invoke();
            }
        }
    }

}
