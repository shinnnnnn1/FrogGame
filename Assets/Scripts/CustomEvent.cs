using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomEvent : MonoBehaviour
{

    [SerializeField] Dialogue dialogue;
    [SerializeField] TriggerEvent trigger;

    [SerializeField] Transform instantiatePos;
    [SerializeField] GameObject instantiateObj;
    [SerializeField] AudioClip clip;

    [SerializeField] int intager;
    [SerializeField] bool boolean;

    GameObject Instant;
    GameObject Instant2;

    AudioSource audios;
    
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
            case 2:
                Destroy(Instant);
                Invoke("Event2", 2f);
                break;
            case 3:
                Event3();
                break;
            case 4:
                Event4();
                break;
            case 6:
                Event6();
                break;
            case 7:
                Event7();
                break;
            case 8:
                Event8();
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
    void Event2()
    {
        Instant = Instantiate(instantiateObj, instantiatePos.position, Quaternion.identity);
        Rigidbody rigid = Instant.gameObject.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * 1300);

        audios = GetComponent<AudioSource>();
        audios.PlayOneShot(clip);
    }
    void Event3()
    {
        Instant = Instantiate(instantiateObj, instantiatePos.position, Quaternion.identity);
    }

    void Event4()
    {
        Destroy(Instant2);
        Instant2 = Instantiate(instantiateObj, instantiatePos.position, Quaternion.identity);
        Event5();
    }

    void Event5()
    {
        audios = GetComponent<AudioSource>();
        audios.PlayOneShot(clip);
    }
    void Event6()
    {

        instantiatePos.DOMove(instantiatePos.position - instantiatePos.up, 1);

    }
    void Event7()
    {

         instantiatePos.DOMove(instantiatePos.position + instantiatePos.up, 1);

    }
    void Event8()
    {
        Destroy(Instant2);
    }

    public void Event9(Dialogue dialogue)
    {
        GameManager.Instance.StartDialogue(dialogue, null);
    }
}
