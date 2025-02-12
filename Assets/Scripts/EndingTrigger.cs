using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dial;
    [SerializeField] bool[] trigger = new bool[3];
    bool isActivated;

    [SerializeField] Transform door;
    [SerializeField] AudioClip clip;
    [SerializeField] ParticleSystem particle;

    [SerializeField] Transform newDoor;

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
        GameManager.Instance.StartDialogue(dial, null);
        StartCoroutine(EndingDialouge());
    }
    IEnumerator EndingDialouge()
    {
        door.DOMoveY(10, 5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1f);
        door.DOPause(); 
        door.DOMoveY(1, 0.3f).SetEase(Ease.Linear);
        particle.Play();
        GameManager.Instance.PlaySE(clip);
        yield return new WaitForSeconds(3f);
        newDoor.DORotate(new Vector3(0, 0, 300), 60).SetEase(Ease.Linear);
    }

}
