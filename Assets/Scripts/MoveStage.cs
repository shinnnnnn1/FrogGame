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

        doors[0].DOMoveX(-3.5f, 0.5f).SetRelative(true).SetEase(Ease.OutQuart);
        doors[1].DOMoveX(3.5f, 0.5f).SetRelative(true).SetEase(Ease.OutQuart);

        transform.DOMoveY(-20, 20).SetRelative(true).SetEase(Ease.Linear).SetDelay(2f);
        GameManager.Instance.Fade(false);
        Invoke("NextScene", 7.5f);
    }

    void NextScene()
    {
        GameManager.Instance.ChangeScene();
    }
}
