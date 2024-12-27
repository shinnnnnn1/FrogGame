using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEvent : MonoBehaviour
{

    [SerializeField] Dialogue dialogue;
    [SerializeField] TriggerEvent trigger;

    [SerializeField] Transform instantiatePos;
    [SerializeField] GameObject instantiateObj;

    [SerializeField] int intager;
    [SerializeField] bool boolean;

    GameObject Instant;

    
    public void StartEvent(int index)
    {
        switch(index)
        {
            case 0:
                Event0();
                break;
            case 1:
                Event1();
                break;
        }
    }
    void Event0()
    {
        Destroy(Instant);
        Instant = Instantiate(instantiateObj, instantiatePos.position, Quaternion.identity);
    }

    void Event1()
    {
        Instantiate(instantiateObj, instantiatePos.position, Quaternion.identity);
        intager++;
        if(intager == 3)
        {
            GameManager.Instance.StartDialogue(dialogue, trigger);
        }
    }
}
