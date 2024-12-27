using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform end;

    void Update()
    {
        var positions = new Vector3[] { transform.position, end.position };
        lineRenderer?.SetPositions(positions);
    }
}
