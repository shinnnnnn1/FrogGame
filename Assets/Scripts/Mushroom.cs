using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] float pow = 100f;
    [SerializeField] AudioClip clip;

    void OnCollisionEnter(Collision collision)
    {
        rigid = collision.gameObject.GetComponent<Rigidbody>();
        if(rigid.position.y > transform.position.y)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            rigid.AddForce(this.transform.up * pow, ForceMode.Impulse);
            GameManager.Instance.PlaySE(clip);
        }
    }
}
