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

    [Space(10f)] [Header("Action")]
    [SerializeField] LayerMask actionLayer;
    [SerializeField] Vector3 actionOffset;
    [SerializeField] float length = 10f;
    RaycastHit actionHit;

    [Space(10f)] [Header("Ground")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float radius = 1f;
    [SerializeField] float distance = 1f;
    RaycastHit groundHit;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        OnGround();
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
        xRot -= look.y;
        yRot += look.x;
        xRot = Mathf.Clamp(xRot, -10f, 90f);
        Quaternion rot = Quaternion.Euler(xRot, yRot, 0);
        camFollow.rotation = rot;
    }

    void Jump()
    {
        if(OnGround())
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
            rigid.AddForce(Vector3.up * jumpPow);
        }
        else
        {

        }
    }

    bool OnGround()
    {
        if(Physics.SphereCast(transform.position, radius, Vector3.down, out groundHit, distance, groundLayer))
        {
            Debug.Log("OnGround");
            return true;
        }
        else
        {
            return false;
        }
    }

    void Action()
    {
        StartCoroutine(PlayAction());
    }

    IEnumerator PlayAction()
    {
        yield return null;
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
            Debug.Log("Jump");
            Jump();
        }
    }
    public void InputAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Action");
        }
    }
    public void InputPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pause");
        }
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + Vector3.down * distance, radius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + actionOffset, transform.position + actionOffset + transform.forward * length);
    }
}
