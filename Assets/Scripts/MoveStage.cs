using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveStage : MonoBehaviour
{
    [SerializeField] bool isStart;

    [SerializeField] Transform[] doors;
    [SerializeField] AudioClip[] clips;

    bool isActivated;

    void Start()
    {
        if(isStart)
        {
            transform.DOMoveY(-3, 3).SetRelative(true).SetEase(Ease.Linear);
            GameManager.Instance.Fade(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isActivated) { return; }
        isActivated = true;

        Vector3 pos1 = doors[0].position + doors[0].right * -3.5f;
        Vector3 pos2 = doors[1].position + doors[1].right * 3.5f;

        doors[0].DOMove(pos1, 0.5f).SetEase(Ease.OutQuart);
        doors[1].DOMove(pos2, 0.5f).SetEase(Ease.OutQuart);

        transform.DOMoveY(-20, 20).SetRelative(true).SetEase(Ease.Linear).SetDelay(2f);
        GameManager.Instance.Fade(false);
        Invoke("NextScene", 7.5f);
    }

    void NextScene()
    {
        GameManager.Instance.ChangeScene();
    }
}
