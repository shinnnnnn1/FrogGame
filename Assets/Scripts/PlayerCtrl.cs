using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    Animator anim;
    Rigidbody rigid;

    [SerializeField] Vector2 inputDirection;

    [Space(10f)]
    [SerializeField] float moveSpd = 1f;
    [SerializeField] float jumpPow = 1f;

    [Space(10f)]
    [SerializeField] GameObject mainCam;
    [SerializeField] Transform camFollow;
    [SerializeField] Vector2 look;
    float xRot;
    float yRot;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();

;    }

    void Move()
    {
        float speed = 0;
        float targetRotation = 0;
        Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        
        if (inputDirection != Vector2.zero)
        {
            speed = moveSpd;
            targetRotation = Quaternion.LookRotation(direction).eulerAngles.y + mainCam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20 * Time.deltaTime);
        }

        Vector3 TargetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        rigid.velocity = TargetDirection * speed + new Vector3(0, rigid.velocity.y, 0);
    }

    void LateUpdate()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        xRot += look.y;
        yRot -= look.x;
        xRot = Mathf.Clamp(xRot, -90, 90);
        Quaternion rot = Quaternion.Euler(xRot, yRot, 0);
        camFollow.rotation = rot;
    }

    #region INPUT
    public void InputMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }
    public void InputLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
    public void InputJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }
    public void InputAction(InputAction.CallbackContext context)
    {
        Debug.Log("Action");
    }
    public void InputPause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause");
    }
    #endregion
}
