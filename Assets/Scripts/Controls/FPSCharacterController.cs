using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using Mirror.Examples.Basic;

public class FPSCharacterController : MonoBehaviour
{
    [HideInInspector] public bool moving = false;
    public Rigidbody playerRB;
    public Vector3 cameraOffset = Vector3.zero;
    public float baseSpeed = 1;
    public float baseSensitivity = 1;
    public float speed = 1;
    public float sensitivity = 1;

    [Header("Events")]
    public FireEvent fireEvent;

    bool freezeControls = false;
    bool wasFiring = false;

    // Start is called before the first frame update
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerRB)
            transform.position = playerRB.transform.position + cameraOffset;
    }

    private void FixedUpdate()
    {
        if (!freezeControls && playerRB)
            Move();
        if (!freezeControls)
            Fire();
    }

    void Fire()
    {
        float input = GetComponent<PlayerInput>().actions
            .FindAction("Fire").ReadValue<float>();
        bool isFirstFrame = !wasFiring;

        if (input == 1) {
            fireEvent.Invoke(isFirstFrame, transform);
            wasFiring = true;
        }
        else
            wasFiring = false;
    }

    public void OnLook(InputValue value)
    {
        Vector2 pos = value.Get<Vector2>();
        Vector3 eulerAngles;

        if (playerRB) {
            eulerAngles = transform.rotation.eulerAngles
                + new Vector3(-pos.y * baseSensitivity * sensitivity,
                pos.x * baseSensitivity * sensitivity, 0);
            if (eulerAngles.x > 180)
                eulerAngles.x = Mathf.Max(270f, eulerAngles.x);
            else
                eulerAngles.x = Mathf.Min(90f, eulerAngles.x);
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }

    public void Move()
    {
        Vector2 input = GetComponent<PlayerInput>().actions
            .FindAction("Move").ReadValue<Vector2>();
        Vector3 direction = new Vector3(transform.forward.x * input.y, 0,
            transform.forward.z * input.y);
        direction += new Vector3(transform.right.x * input.x, 0,
            transform.right.z * input.x);
        direction.Normalize();
        Vector3 movement = new Vector3(direction.x * baseSpeed * speed *
            Time.deltaTime, 0, direction.z * baseSpeed * speed *
            Time.deltaTime);
        playerRB.AddForce(movement);
    }

    public void OnClick()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
    }

    public void OnPause()
    {
        Cursor.lockState = CursorLockMode.None;
        GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
    }
}
