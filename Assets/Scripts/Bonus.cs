using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] int index;
    bool isActivated;

    [Space(20f)]
    [SerializeField] GameObject penguin;
    [SerializeField] ParticleSystem particle;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(!isActivated)
        {
            penguin.SetActive(false);
            particle.Play();
            source.Play();
            GameManager.Instance.Bonus(index);
            isActivated = true;
        }
    }
}
