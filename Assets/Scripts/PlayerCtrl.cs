using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using DG.Tweening;

public class PlayerCtrl : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    Rigidbody rigid;
    Eatable interactingObject;

    Vector2 inputDirection;

    [Space(10f)] [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera virtualCam;
    CinemachineComponentBase componentBase;
    [SerializeField] GameObject mainCam;
    [SerializeField] Transform camFollow;
    [SerializeField] Vector2 look;
    [SerializeField] Vector2 scroll;
    float xRot;
    float yRot;

    [Space(10f)] [Header("Movement")]
    [SerializeField] float sensitivity = 1f;
    [SerializeField] float moveSpd = 1f;
    [SerializeField] float jumpPow = 1f;

    public bool canMove = true;
    [SerializeField] bool canLook = true;

    [Space(10f)] [Header("Action")]
    [SerializeField] Transform tongue;
    [SerializeField] Transform tongueStart;
    [SerializeField] Transform tongueMiddle;
    [SerializeField] Transform lookObj;
    public Transform mouth;
    [SerializeField] LayerMask actionLayer;
    [SerializeField] float radius = 1f;
    [SerializeField] float distance = 2f;
    float rotY;
    float eatTime;
    bool lockOn;

    [HideInInspector] public bool isTransforming;

    RaycastHit actionHit;

    [Space(10f)] [Header("Ground")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gRadius = 1f;
    [SerializeField] float gDistance = 1f;
    RaycastHit groundHit;

    void Awake()
    {
        anim = gameObject.transform.GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        if (virtualCam != null)
        {
            componentBase = virtualCam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
        //Time.timeScale = 0.5f;
    }

    void Update()
    {
        anim.SetBool("Jump", !OnGround());
    }

    void FixedUpdate()
    {
        Move();
;    }

    void LateUpdate()
    {
        CameraRotation();
        //tongueMiddle.localEulerAngles = qqq;
        if (lockOn)
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
            float jump = isTransforming ? jumpPow * 0.7f : jumpPow;
            rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            rigid.AddForce(Vector3.up * jump);
            anim.SetBool("Jump", true);
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
        if (!CanMove()) { return; }
        StartCoroutine(Action());
    }

    public IEnumerator Action()
    {
        canMove = false;
        anim.SetFloat("Move", 0);
        if (isTransforming)
        {
            isTransforming = false;
            anim.SetBool("Action", false);
            anim.SetTrigger("Action2");

            interactingObject.Fire(this);

            yield return new WaitForSeconds(0.4f);
        }
        else
        {
            anim.SetTrigger("Action1");

            yield return new WaitForSeconds(0.15f);
            Search();
            tongue.DOLocalMove(new Vector3(0, 0.05f, -0.043f), 0.1f);
            tongueStart.DOLocalMove(new Vector3(0, 0.008f, 0), 0.1f);

            yield return new WaitForSeconds(0.15f);
            if(isTransforming)
            {
                tongue.DOLocalMove(new Vector3(-3.308722e-25f, 0.004158414f, 2.980232e-10f), 0.02f).SetEase(Ease.OutQuart);
                tongueStart.DOLocalMove(new Vector3(-8.349354e-26f, 0.004507747f, 6.519258e-10f), 0.1f);
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                
                tongue.DOLocalMove(new Vector3(-3.308722e-25f, 0.004158414f, 2.980232e-10f), 0.02f).SetEase(Ease.OutQuart);
                tongueStart.DOLocalMove(new Vector3(-8.349354e-26f, 0.004507747f, 6.519258e-10f), 0.1f);
                yield return new WaitForSeconds(0.05f);
            }
            lockOn = false;

        }
        canMove = true;
    }

    void Search()
    {
        if (Physics.SphereCast(transform.position, radius, transform.forward, out actionHit, distance, actionLayer))
        {
            lockOn = true;
            lookObj.LookAt(actionHit.transform.position);
            rotY = lookObj.localEulerAngles.y;
            eatTime = actionHit.distance / distance * 0.14f;
            Invoke("Interacting", eatTime);
            actionHit.collider.gameObject.GetComponent<IInteractable>()?.Interact(this);
        }
        else
        {
            anim.SetBool("Action", false);
        }
    }

    void SetTongue()
    {
        tongueMiddle.localEulerAngles = new Vector3(tongueMiddle.localEulerAngles.x, rotY, 0);
    }

    void Interacting()
    {
        tongue.DOKill();
        tongueStart.DOKill();
        interactingObject = actionHit.collider.gameObject.GetComponent<Eatable>();
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
    public void InputScroll(InputAction.CallbackContext context)
    {
        scroll = context.ReadValue<Vector2>() / 1200;
        if (componentBase is Cinemachine3rdPersonFollow)
        {
            (componentBase as Cinemachine3rdPersonFollow).CameraDistance -= scroll.y;
            (componentBase as Cinemachine3rdPersonFollow).CameraDistance = Mathf.Clamp
                ((componentBase as Cinemachine3rdPersonFollow).CameraDistance, 3f, 10f);
        }
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        if (OnGround())
        {
            Gizmos.color = Color.cyan;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position + Vector3.down * gDistance + transform.forward * -0.1f, gRadius);
    }
}
