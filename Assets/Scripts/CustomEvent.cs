using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
