using PGGE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController nCharacterController;
    public Animator nAnimator;

    public float nWalkSpeed = 1.5f;
    public float nRotationSpeed = 50.0f;
    public bool nFollowCameraForward = false;
    public float nTurnRate = 10.0f;

    private float hInput;
    private float vInput;
    private float speed;
    private bool jump = false;
    private bool crouch = false;
    public float nGravity = -30.0f;
    public float nJumpHeight = 1.0f;

    private Vector3 nVelocity = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        nCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleInputs();
        Move();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public void HandleInputs()
    {
        // We shall handle our inputs here.

        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        speed = nWalkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = nWalkSpeed * 2.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            crouch = !crouch;
            Crouch();
        }
    }

    public void Move()
    {
        if (crouch) return;

        // We shall apply movement to the game object here.
        if (nAnimator == null) return;
        if (nFollowCameraForward)
        {
            // rotate Player towards the camera forward.
            Vector3 eu = Camera.main.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0.0f, eu.y, 0.0f),
                nTurnRate * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0.0f, hInput * nRotationSpeed * Time.deltaTime, 0.0f);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;

        nCharacterController.Move(forward * vInput * speed * Time.deltaTime);
        nAnimator.SetFloat("PosX", 0);
        nAnimator.SetFloat("PosZ", vInput * speed / (2.0f * nWalkSpeed));

        if (jump)
        {
            Jump();
            jump = false;
        }
    }

    void Jump()
    {
        nAnimator.SetTrigger("Jump");
        nVelocity.y += Mathf.Sqrt(nJumpHeight * -2f * nGravity);
    }

    private Vector3 HalfHeight;
    private Vector3 tempHeight;
    void Crouch()
    {
        nAnimator.SetBool("Crouch", crouch);
        if (crouch)
        {
            //tempHeight = Camera.CameraConstants.CameraPositionOffset;
            HalfHeight = tempHeight;
            HalfHeight.y *= 0.5f;
            CameraConstants.CameraPositionOffset = HalfHeight;
        }
        else
        {
            CameraConstants.CameraPositionOffset = tempHeight;
        }
    }

    void ApplyGravity()
    {
        // apply gravity.
        nVelocity.y += nGravity * Time.deltaTime;
        if (nCharacterController.isGrounded && nVelocity.y < 0)
            nVelocity.y = 0f;
    }
}
