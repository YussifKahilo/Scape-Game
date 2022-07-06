using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject cameraHolder;
    Rigidbody rb;
    [SerializeField] Vector3 inputMovement;
    [SerializeField] Vector2 inputLooking;
    [SerializeField] int speed;
    [SerializeField] int jumpForce;
    float verticalLookRotation;
    public int senstivity;
    bool isGrounded = false;

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
    }

    private void LateUpdate()
    {
        Look();
    }

    public void SetGroundedState(bool state)
    {
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

    }

    void Power2()
    {

    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void Use()
    {

    }
}
