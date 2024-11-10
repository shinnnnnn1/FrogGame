using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerCtrl : MonoBehaviour
{
    Animator anim;
    Rigidbody rigid;

    Vector2 inputDirection;

    [Header("Camera")]
    [SerializeField] GameObject mainCam;
    [SerializeField] Transform camFollow;
    [SerializeField] Vector2 look;
    float xRot;
    float yRot;

    [Space(10f)] [Header("Movement")]
    [SerializeField] float sensitivity = 1f;
    [SerializeField] float moveSpd = 1f;
    [SerializeField] float jumpPow = 1f;

    [SerializeField] bool canMove = true;
    [SerializeField] bool canLook = true;

    [Space(10f)] [Header("Action")]
    [SerializeField] Transform tongue;
    [SerializeField] Transform tongueStart;
    [SerializeField] Transform tongueMiddle;
    [SerializeField] LayerMask actionLayer;
    [SerializeField] Vector3 actionOffset;
    [SerializeField] float radius = 1f;
    [SerializeField] float distance = 2f;
    bool search;
    bool isLockOn;
    bool isTransforming;
    [SerializeField] Vector3 tongueRotate;

    RaycastHit actionHit;

    [Space(10f)] [Header("Ground")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gRadius = 1f;
    [SerializeField] float gDistance = 1f;
    RaycastHit groundHit;

    void Start()
    {
        anim = gameObject.transform.GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        anim.SetBool("Jump", !OnGround());
        CanTouch();
    }

    void FixedUpdate()
    {
        Move();
;    }

    void LateUpdate()
    {
        CameraRotation();
        //tongueMiddle.localEulerAngles = qqq;
        if(search)
        {
            SetTongue();
        }
    }

    void Move()
    {
        if (!CanMove()) { return; }

        float speed = 0;
        float targetRotation = 0;
        Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        if (inputDirection != Vector2.zero)
        {
            speed = moveSpd;
            targetRotation = Quaternion.LookRotation(direction).eulerAngles.y + mainCam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 8 * Time.deltaTime);
        }
        Vector3 TargetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        rigid.velocity = TargetDirection * speed + new Vector3(0, rigid.velocity.y, 0);
        Vector2 velocity = new Vector2(rigid.velocity.x, rigid.velocity.z).normalized;
        anim.SetFloat("Move", velocity.magnitude);
    }

    void CameraRotation()
    {
        if (!canLook) { return; }
        xRot -= look.y * sensitivity;
        yRot += look.x * sensitivity;
        xRot = Mathf.Clamp(xRot, -10f, 85f);
        Quaternion rot = Quaternion.Euler(xRot, yRot, 0);
        camFollow.rotation = rot;
    }

    void Jump()
    {
        if(OnGround())
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            rigid.AddForce(Vector3.up * jumpPow);
            anim.SetBool("Jump", true);
        }
        else
        {

        }
    }

    bool OnGround()
    {
        if (Physics.SphereCast(transform.position + transform.forward * -0.1f, gRadius, Vector3.down, out groundHit, gDistance, groundLayer))
        {
            return true;
        }
        else { return false; }
    }

    void ActionCheck()
    {
        StartCoroutine(Action());

    }

    IEnumerator Action()
    {
        canMove = false;
        if (isTransforming)
        {
            isTransforming = false;
        }
        else
        {
            anim.SetTrigger("Action1");

            yield return new WaitForSeconds(0.15f);
            search = true;
            tongue.DOLocalMove(new Vector3(0, 0.05f, -0.043f), 0.1f);
            tongueStart.DOLocalMove(new Vector3(0, 0.008f, 0), 0.1f);

            yield return new WaitForSeconds(0.15f);
            tongue.DOLocalMove(new Vector3(-3.308722e-25f, 0.004158414f, 2.980232e-10f), 0.02f).SetEase(Ease.OutQuart);
            tongueStart.DOLocalMove(new Vector3(-8.349354e-26f, 0.004507747f, 6.519258e-10f), 0.1f);

            search = false;
            isLockOn = false;
            tongueRotate = new Vector3(32.198f, 0, 0);

            yield return new WaitForSeconds(0.1f);
        }
        canMove = true;
    }

    
    void SetTongue()
    {
        if (Physics.SphereCast(transform.position, radius, transform.forward, out actionHit, distance, actionLayer) && !isLockOn)
        {
            Debug.Log("asdsad");
            //need to create tongueRotate vector :)
            isLockOn = true;
        }
        else if(isLockOn)
        {
            tongueMiddle.localEulerAngles = tongueRotate;
        }
        else
        {
            tongueMiddle.localEulerAngles = new Vector3(32.198f, 0, 0);
        }
    }

    bool CanTouch()
    {
        if (Physics.SphereCast(transform.position, radius, transform.forward, out actionHit, distance, actionLayer))
        {
            //Debug.Log("Candd");
            return true;
        }
        else { return false; }
    }
    bool CanInteract()
    {
        if (Physics.SphereCast(transform.position, radius, Vector3.down, out actionHit, distance, actionLayer))
        {
            return true;
        }
        else { return false; }
    }

    bool CanMove()
    {
        if (canMove)
        {
            return true;
        }
        else
        {
            rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            return false;
        }
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
        if(context.performed)
        {
            //Debug.Log("Jump");
            Jump();
        }
    }
    public void InputAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log("Action");
            ActionCheck();
        }
    }
    public void InputPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log("Pause");
        }
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + actionOffset, transform.position + actionOffset + transform.forward * (distance + 0.4f));
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance + actionOffset, radius);
        Gizmos.DrawWireSphere(transform.position + transform.forward * -0.1f, 0.5f);
        if(OnGround())
        {
            Gizmos.color = Color.cyan;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        //Gizmos.DrawSphere(transform.position + Vector3.down * gDistance + transform.forward * -0.1f, gRadius);
    }
}
