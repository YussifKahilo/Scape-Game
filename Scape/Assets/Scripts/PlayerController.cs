using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    private Rigidbody rb;
    private Vector3 inputMovement;
    private Vector2 inputLooking;
    private int jumpForce = 5;
    private float speed = 4;
    private PlayerPower power;
    private int senstivity;
    private float verticalLookRotation;
    private bool isGrounded = true;

    internal PlayerPower Power { get => power; set => power = value; }

    internal int Senstivity { get => senstivity; set => senstivity = value; }

    internal int JumpForce { get => jumpForce; set => jumpForce = value; }

    internal float Speed { get => speed; set => speed = value; }

    internal GameObject CameraHolder { get => cameraHolder; set => cameraHolder = value; }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        inputMovement = new Vector3(input.x , 0 , input.y);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started && isGrounded)
        {
            Jump();
        }
    }

    public void OnPower1(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Power1();
        }
    }

    public void OnPower2(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Power2();
        }
    }

    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            PauseManager.instance.Pause();
        }
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        inputLooking = value.ReadValue<Vector2>();
    }

    public void OnUse(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Use();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {   
        transform.Translate(inputMovement * speed * Time.fixedDeltaTime);
        GetComponent<PlayerAnimation>().SetAnimation(inputMovement);
    }

    private void LateUpdate()
    {
        Look();
    }

    public void SetGroundedState(bool state)
    {
        GetComponent<PlayerAnimation>().SetJumping(!state);
        isGrounded = state;
    }

    void Look()
    {
        transform.Rotate(Vector3.up * inputLooking.normalized.x * senstivity);

        verticalLookRotation += inputLooking.normalized.y * senstivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -70f, 70f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Power1()
    {
        power.Power1();
    }

    void Power2()
    {
        power.Power2();
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        SoundManager.instance.PlaySound(SoundManager.instance.jump);
    }

    void Use()
    {
        GetComponent<PlayerManager>().UseKey();
    }
    
}
