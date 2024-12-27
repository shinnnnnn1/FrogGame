using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Unity.VisualScripting;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TriggerButton : MonoBehaviour, IInteractable
{
    AudioSource audios;

    [Space(10f)]
    [SerializeField] UnityEvent[] on;
    [SerializeField] UnityEvent[] off;

    [SerializeField] Transform button;

    [SerializeField] bool isFloor;
    [SerializeField] Transform down;

    Vector3 origin;
    float value;

    bool isActivated;

    ParticleSystem particle;

    void Start()
    {
        audios = GetComponent<AudioSource>();
        origin = button.localPosition;
        particle = GetComponentInChildren<ParticleSystem>();
    }

    
    void Update()
    {
        if (!isFloor) { return; }

        if (isActivated && !Physics.SphereCast(down.position, 1, Vector3.up, out RaycastHit hit, 2, 1 << 3 | 1 << 8))
        {
            Trigger(false);

            float time = Mathf.Abs(button.transform.localPosition.y - origin.y) * 5;

            button.DOPause();
            button.DOLocalMove(origin, time).SetEase(Ease.Linear);

            isActivated = false;
        }

    }
    



    public void Interact(PlayerCtrl player)
    {
        particle.Play();
        audios.Play();
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
        if(Physics.SphereCast(down.position, 1, Vector3.up, out RaycastHit hit, 100, 1 << 3 | 1 << 8))
        {
            return;
        }
        if (isActivated)
        {
            Trigger(false);
            isActivated = false;
        }

        float time = Mathf.Abs(button.transform.localPosition.y - origin.y) * 5;

        button.DOPause();
        button.DOLocalMove(origin, time).SetEase(Ease.Linear);
    }

    void IsArrival()
    {
        if(button.localPosition.y <= 0.0301f && !isActivated)
        {
            audios.Play();
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
