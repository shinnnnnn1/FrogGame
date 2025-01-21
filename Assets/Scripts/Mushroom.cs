using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] float pow = 100f;
    [SerializeField] AudioClip clip;

    private void OnCollisionEnter(Collision collision)
    {
        rigid = collision.gameObject.GetComponent<Rigidbody>();
        rigid.AddForce(this.transform.up * pow, ForceMode.Impulse);
        GameManager.Instance.PlaySE(clip);
    }
}
