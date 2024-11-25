using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    [Space(10f)] [SerializeField] bool isFloor = true;

    [Space(10f)]
    [SerializeField] UnityEvent on;
    [SerializeField] UnityEvent off;

}
