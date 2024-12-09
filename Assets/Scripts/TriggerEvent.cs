using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] UnityEvent[] on;
    [SerializeField] UnityEvent[] off;

    [SerializeField] bool onStart;

    void Start()
    {
        if (!TryGetComponent<Collider>(out Collider collider) && onStart)
        {
            GameManager.Instance.StartDialogue(dialogue);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Trigger(true);
    }

    void OnTriggerExit(Collider other)
    {
        Trigger(false);
    }

    public void Trigger(bool activate)
    {
        if (activate)
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