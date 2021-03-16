using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    [SerializeField]
    public GameManager GM;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public Vector2 RawShotDirectionInput { get; private set; }
    public Vector2Int ShotDirectionInput { get; private set; }
    public int InputXNormalized { get; private set; }

    public int InputYNormalized { get; private set; }

    public bool JumpInput { get; private set; }

    public bool JumpInputStop { get; private set; }

    public bool GrabInput { get; private set; }

    public bool DashInput { get; private set; }

    public bool DashInputStop { get; private set; }

    public bool AttackInput { get; private set; }

    public bool AttackInputStop { get; private set; }

    public bool RollInput { get; private set; }

    public bool RollInputStop { get; private set; }

    public bool PauseInput { get; private set; }

    public bool KeyboardAimToggle { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime,
        attackInputStartTime,
        rollInputStartTime,
        dashInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
        AttackInputStop = true;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckAttackInputHoldTime();
        CheckRollInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            InputXNormalized = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            InputXNormalized = 0;
        }


        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            InputYNormalized = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            InputYNormalized = 0;
        }
        
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
            AttackInputStop = false;
            attackInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            AttackInputStop = true;
        }
    }

    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RollInput = true;
            RollInputStop = false;
            rollInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            RollInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        
        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if(playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;

        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void OnShotDirectionInput(InputAction.CallbackContext context)
    {
        if (playerInput.currentControlScheme == "Keyboard" && KeyboardAimToggle)
        {
            RawShotDirectionInput = context.ReadValue<Vector2>();
            RawShotDirectionInput = cam.ScreenToWorldPoint((Vector3)RawShotDirectionInput) - transform.position;
            ShotDirectionInput = Vector2Int.RoundToInt(RawShotDirectionInput.normalized);

        }
        else if(playerInput.currentControlScheme == "Keyboard" && !KeyboardAimToggle)
        {
            RawShotDirectionInput = Vector2.zero;
            ShotDirectionInput = Vector2Int.RoundToInt(RawShotDirectionInput.normalized);
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            RawShotDirectionInput = context.ReadValue<Vector2>();
            ShotDirectionInput = Vector2Int.RoundToInt(RawShotDirectionInput.normalized);
        }
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GM.TogglePause();
        }
    }

    public void OnKeyboardAimToggle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            KeyboardAimToggle = true;
            Debug.Log("Keyboard Aim toggle on");
        }
        if (context.canceled)
        {
            KeyboardAimToggle = false;
            Debug.Log("Keyboard Aim toggle on");
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
    public void UseAttackInput() => AttackInput = false;
    public void UseRollInput() => RollInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if(Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }

    private void CheckAttackInputHoldTime()
    {
        if(Time.time >= attackInputStartTime + inputHoldTime)
        {
            AttackInput = false;
        }
    }

    private void CheckRollInputHoldTime()
    {
        if(Time.time >= rollInputStartTime + inputHoldTime)
        {
            RollInput = false;
        }
    }
}
   
