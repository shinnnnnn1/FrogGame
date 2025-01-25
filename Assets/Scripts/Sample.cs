using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{

    [SerializeField] Transform obj;
    [SerializeField] Vector3 sss;
    [SerializeField] float a;

    void Update()
    {
        Vector3 aaa = transform.position - obj.position;
        //a = Mathf.Atan2(transform.position.x , obj.position.x) * Mathf.Rad2Deg;
        //a = Mathf.Atan2(aaa.z, aaa.x) * Mathf.Rad2Deg;
        //Debug.DrawLine(transform.position, -aaa + transform.position);
        transform.LookAt(obj.transform);
    }
}
