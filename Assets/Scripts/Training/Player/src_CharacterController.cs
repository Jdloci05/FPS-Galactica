using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static src_Models;
using UnityEngine.Animations.Rigging;

public class src_CharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private InputSystem inputSystem;
    public Vector2 input_Movement;
    public Vector2 input_View;

    private Vector2 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -70;
    public float viewClampYMax = 80;

     public Animator anim;

    private bool hasAnimator;

    private float currentVelocityX;
    private float currentVelocityY;

    private void Awake()
    {

        Cursor.visible = false;
        inputSystem = new InputSystem();

        inputSystem.Player.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        inputSystem.Player.View.performed += e => input_View = e.ReadValue<Vector2>();

        inputSystem.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        hasAnimator = TryGetComponent<Animator>(out anim);
        characterController = GetComponent<CharacterController>();

    }

    private void Update()
    {
        CalculateView();
        CalculateMovement();

        anim.SetFloat("hzInput", currentVelocityX);
        anim.SetFloat("vInput", currentVelocityY);
    }

    private void CalculateView()
    {
        newCharacterRotation.y += playerSettings.ViewXSensitivity * (playerSettings.ViewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(newCharacterRotation);

        newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted ? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);

        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement()
    {
        if (!hasAnimator) return;

        var verticalSpeed = playerSettings.WalkingForwardSpeed * input_Movement.y * Time.deltaTime;
        var horizontalSpeed = playerSettings.WalkingStrateSpeed * input_Movement.x * Time.deltaTime;

        currentVelocityX = input_Movement.x;
        currentVelocityY = input_Movement.y;

        var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        characterController.Move(newMovementSpeed);

    }

    private void changeAnimation()
    {
        inputSystem.Player.Recoger.performed += context =>
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Shooting"), 1);
        };
    }
}
