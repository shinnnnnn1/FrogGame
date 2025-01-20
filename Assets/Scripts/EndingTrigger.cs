using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dial;
    [SerializeField] bool[] trigger = new bool[3];
    bool isActivated;

    public void _Activate(int index)
    {
        switch (index)
        {
            case 0:
                trigger[0] = true;
                break;
            case 1:
                trigger[1] = true;
                break;
            case 2:
                trigger[2] = true;
                break;
        }
    }
    public void _Deactivate(int index)
    {
        switch (index)
        {
            case 0:
                trigger[0] = false;
                break;
            case 1:
                trigger[1] = false;
                break;
            case 2:
                trigger[2] = false;
                break;
        }
    }

    public void _IsActivated()
    {
        for (int i = 0; i < trigger.Length; i++)
        {
            if (trigger[i] == false)
            {
                return;
            }
        }
        Trigger();
        isActivated = true;
    }

    void Trigger()
    {
        if(isActivated) {return;}
        Debug.Log("Event");
    }
}
