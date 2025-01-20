using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindField : MonoBehaviour
{
    Rigidbody rigid;
    ParticleSystem particle;

    [SerializeField] float power = 500f;
    [SerializeField] bool canActivate;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        if(canActivate)
        {
            particle.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        rigid = other.GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(canActivate)
        {
            rigid.AddForce(this.transform.forward * power);
        }
    }

    public void Toggle()
    {
        canActivate = !canActivate;
        if(!canActivate)
        {
            particle.Stop();
        }
        else
        {
            particle.Play();
        }
    }
}
