using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.ProBuilder.Shapes;
using UnityEditor.ShaderGraph.Internal;

public class Platform : MonoBehaviour
{
    [SerializeField] float addValue;

    float origin;
    float location;

    void Start()
    {
        origin = transform.position.y;
        location = origin + addValue;
    }

    //need to set time
    public void Activate(bool activate)
    {
        transform.DOPause();
        if (activate)
        {
            float time = Mathf.Abs(transform.position.y - location) / 2;
            transform.DOLocalMoveY(location, time).SetEase(Ease.Linear);
        }
        else
        {
            float time = Mathf.Abs(transform.position.y - origin) / 2;
            transform.DOLocalMoveY(origin, time).SetEase(Ease.Linear);
        }
    }

    public void ActivateOnce()
    {
        transform.DOPause();

        float time = Mathf.Abs(transform.position.y - location) / 2;
        transform.DOLocalMoveY(location, time).SetEase(Ease.Linear).OnKill(Down);
    }

    void Down()
    {
        float time = Mathf.Abs(transform.position.y - origin) / 2;
        transform.DOLocalMoveY(origin, time).SetEase(Ease.Linear).SetDelay(1f);
    }
}
