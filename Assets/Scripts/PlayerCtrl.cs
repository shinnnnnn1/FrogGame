using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    Animator anim;
    Rigidbody rigid;

    [SerializeField] Vector2 inputDirection;



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
        
    }

    void LateUpdate()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        xRot -= look.y;
        yRot -= look.x;
        xRot = Mathf.Clamp(xRot, 0, 70);
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
