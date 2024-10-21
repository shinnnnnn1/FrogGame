using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    Animator anim;
    Rigidbody rigid;

    [SerializeField] Vector3 inputDirection;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    #region INPUT
    public void InputMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }
    public void InputJump(InputAction.CallbackContext context)
    {

    }
    public void InputAction(InputAction.CallbackContext context)
    {

    }
    #endregion
}
