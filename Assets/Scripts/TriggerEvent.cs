using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] UnityEvent[] on;
    [SerializeField] UnityEvent[] off;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        
    }

}