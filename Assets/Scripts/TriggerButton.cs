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
        origin = button.localPosition;
    }

    public void Interact(PlayerCtrl player)
    {
        button.DOKill();
        button.DOPunchScale(Vector3.one, 0.3f, 10, 5f);
        Trigger(true);
    }

    void OnTriggerEnter(Collider other)
    {
        float time = (button.transform.localPosition.y - 0.0299f) * 5;

        button.DOPause();
        button.DOLocalMoveY(0.03f, time).SetEase(Ease.Linear).OnUpdate(IsArrival);
    }

    void OnTriggerExit(Collider other)
    {
        isActivated = false;
        if (isActivated)
        {
            Trigger(false);
        }

        float time = Mathf.Abs(button.transform.localPosition.y - origin.y) * 5;

        button.DOPause();
        button.DOLocalMove(origin, time).SetEase(Ease.Linear);
    }

    void IsArrival()
    {
        if(button.localPosition.y <= 0.0301f && !isActivated)
        {
            Debug.Log("Arrrrrrrrrrrr");
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
                off[i].Invoke();
            }
        }
    }

}
