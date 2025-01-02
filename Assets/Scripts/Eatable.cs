using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour, IInteractable
{
    Collider coll;
    Rigidbody rigid;

    void Start()
    {
        coll = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
    }

    public void Interact(PlayerCtrl player)
    {
        player.isTransforming = true;
        player.anim.SetBool("Action", true);
        coll.enabled = false;
        rigid.isKinematic = true;

        StartCoroutine(EatObject(player));
    }

    IEnumerator EatObject(PlayerCtrl player)
    {
        yield return new WaitForSeconds(0.15f);
        transform.SetParent(player.mouth);
        
        Vector3 center = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.5f) + Vector3.up;
        transform.DOLocalPath(new[]
        {
            center, Vector3.zero
        },
        0.3f, PathType.CatmullRom).SetEase(Ease.OutQuart);
        transform.DOScale(0.6f, 0.3f).SetEase(Ease.OutQuart);
    }

    public void Fire(PlayerCtrl player)
    {
        transform.SetParent(null);
        rigid.isKinematic = false;
        transform.DOScale(1f, 0.3f);

        StartCoroutine(FireObject(player));
    }

    IEnumerator FireObject(PlayerCtrl player)
    {
        rigid.AddForce(player.transform.forward * 13 + Vector3.up * 7, ForceMode.Impulse);

        yield return new WaitForSeconds(0.05f);
        coll.enabled = true;
    }

}
