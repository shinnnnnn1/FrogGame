using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] bool isTrigger;
    
    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.Fade(false);
        Invoke("Credit", 5f);
    }

    void Credit()
    {
        SceneManager.LoadScene("EndingCredit");
    }

    void Start()
    {
        if (isTrigger) { return; }

    }
}
