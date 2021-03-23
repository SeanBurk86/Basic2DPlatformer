using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerInjuredState InjuredState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerKickState KickState { get; private set; }
    public PlayerSlideKickState SlideKickState { get; private set; }
    public PlayerRollState RollState { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private GameObject deathChunkParticle,
        deathBloodParticle,
        useBox,
        grabber;

    private GameManager GM;

    [SerializeField]
    public PhysicsMaterial2D frictionlessMaterial,
        fullFrictionMaterial;

    [SerializeField]
    private PauseMenu pauseMenu;

    #endregion

    #region Components
    public Animator Anim { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public Transform ShotDirectionIndicator { get; private set; }
    public CapsuleCollider2D MovementCollider { get; private set; }
    public float ghostDelaySeconds,
        boxDetectDistance;
    public PsychicBullet LoadedBullet;
    public Transform EmissionPoint,
        kickCheck,
        slideKickCheck;
    private float lastShotTime;
    
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform frontGroundCheck,
        backGroundCheck,
        wallCheck,
        ledgeCheck,
        ceilingCheck,
        squishChecksTransform;

    [SerializeField]
    private SquishCheck topSquishCheck,
        bottomSquishCheck,
        frontSquishCheck,
        backSquishCheck;

    #endregion

    #region Misc Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool CanBeHurt { get; private set; }
    public bool CanMelee { get; private set; }
    public bool CanFlip { get; private set; }
    public bool CanShoot { get; private set; }
    public Vector3 MovingPlatformPositionOffset { get; private set; }
    private Vector2 workspace;
    private Vector2 shotDirection;
    private bool isHoldingBox;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();


        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        InjuredState = new PlayerInjuredState(this, StateMachine, playerData, "injured");
        DeadState = new PlayerDeadState(this, StateMachine, playerData, "dead");
        KickState = new PlayerKickState(this, StateMachine, playerData, "kick");
        SlideKickState = new PlayerSlideKickState(this, StateMachine, playerData, "slideKick");
        RollState = new PlayerRollState(this, StateMachine, playerData, "roll");

    }

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        ShotDirectionIndicator = transform.Find("ShotDirectionIndicator");
        MovementCollider = GetComponent<CapsuleCollider2D>();
        EmissionPoint = ShotDirectionIndicator.Find("IndicatorPoint");

        FacingDirection = 1;
        CanBeHurt = true;
        CanShoot = true;
        isHoldingBox = false;
        EnableFlip();
        DisableUseBox();
        CurrentHealth = playerData.startingHealth;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if (!pauseMenu.pauseToggle)
        {
            CurrentVelocity = RB.velocity;

            if ((topSquishCheck.isSquished == true && bottomSquishCheck.isSquished == true)
                || (frontSquishCheck.isSquished == true && backSquishCheck.isSquished == true))
            {
                CurrentHealth -= 10;
            }

            if (CurrentHealth <= 0)
            {
                Die();
            }

            StateMachine.CurrentState.LogicUpdate();

            if (InputHandler.GrabInput) EnableGrabber();
            else DisableGrabber();

            isHoldingBox = CheckIfHoldingBox();

            if (InputHandler.ShotDirectionInput != Vector2.zero)
            {
                DisableMelee();
                ShotDirectionIndicator.gameObject.SetActive(true);
                shotDirection = InputHandler.ShotDirectionInput;
                bool isFiring = !InputHandler.AttackInputStop;
                float angleWorkspace = Vector2.SignedAngle(Vector2.right, shotDirection);
                ShotDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angleWorkspace - 45f);
                if (isFiring && shotDirection != Vector2.zero && CanShoot)
                {
                    Shoot();
                }
            }
            else
            {
                EnableMelee();
                ShotDirectionIndicator.gameObject.SetActive(false);
            }
            if (Time.time >= lastShotTime + LoadedBullet.timeBetweenFiring)
            {
                CanShoot = true;
            }
        }
        

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(frontGroundCheck.position.x, frontGroundCheck.position.y, frontGroundCheck.position.z), playerData.groundChecksRadius);
        Gizmos.DrawWireSphere(new Vector3(backGroundCheck.position.x, backGroundCheck.position.y, backGroundCheck.position.z), playerData.groundChecksRadius);
        Gizmos.DrawLine(new Vector3(wallCheck.position.x, wallCheck.position.y, wallCheck.position.z), new Vector3((wallCheck.position.x + playerData.wallCheckDistance * FacingDirection), wallCheck.position.y, wallCheck.position.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ceilingCheck.position, playerData.ceilingCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ledgeCheck.position, new Vector3((ledgeCheck.position.x + playerData.ledgeCheckDistance*FacingDirection), ledgeCheck.position.y, ledgeCheck.position.z));
        Gizmos.DrawWireSphere(kickCheck.position, playerData.kickCheckRadius);
        Gizmos.DrawWireSphere(slideKickCheck.position, playerData.slideKickCheckRadius);
    }
    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Functions

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, playerData.ceilingCheckRadius, playerData.whatIsCeiling);
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(frontGroundCheck.position, playerData.groundChecksRadius, playerData.whatIsGround)
            || Physics2D.OverlapCircle(backGroundCheck.position, playerData.groundChecksRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        if (isHoldingBox)
        {
            return false;
        }
        else
        {
            return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
        }
        
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.ledgeCheckDistance, playerData.whatIsWall);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection && CanFlip)
        {
            Flip();
        }
    }

    public Collider2D[] CheckIfKickbox()
    {
        return Physics2D.OverlapCircleAll(kickCheck.position, playerData.kickCheckRadius);
    }

    public Collider2D[] CheckIfSlideKickbox()
    {
        return Physics2D.OverlapCircleAll(slideKickCheck.position, playerData.slideKickCheckRadius);
    }

    public bool CheckIfTouchingMovingPlatform()
    {
        return transform.parent != null && transform.parent.CompareTag("Moving Platform");
    }

    public bool CheckIfHoldingBox()
    {
        Kickable[] ts = GetComponentsInChildren<Kickable>();
        if(ts.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }



#endregion

    #region Misc Functions

    public void CheckIfDamage(AttackDetails attackDetails)
    {
        if (CanBeHurt)
        {
            CurrentHealth -= attackDetails.damageAmount;
            Debug.Log("Current Health is " + CurrentHealth);
        }
        
    }

    public void SetColliderHeightAndOffset(float height, float yOffset)
    {
        workspace.Set(MovementCollider.size.x, height);
        MovementCollider.size = workspace;
        MovementCollider.offset = new Vector2(MovementCollider.offset.x, yOffset);
    }

    public void SetSquishChecksYScaleAndYPosition(float yScale, float yPosition)
    {
        workspace.Set(squishChecksTransform.localScale.x, yScale);
        squishChecksTransform.localScale = workspace;
        squishChecksTransform.localPosition = new Vector3(squishChecksTransform.localPosition.x, yPosition, squishChecksTransform.localPosition.z);
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
        float xDist = xHit.distance;
        workspace.Set(xDist * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsWall);
        float yDist = yHit.distance;

        workspace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist);
        return workspace;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void DisableFlip()
    {
        CanFlip = false;
        Debug.Log("Flip disabled");
    }

    public void EnableFlip()
    {
        CanFlip = true;
        Debug.Log("Flip re-enabled");
    }

    public void EnablePlayerDamage()
    {
        CanBeHurt = true;
    }

    public void DisablePlayerDamage()
    {
        CanBeHurt = false;
    }

    private void EnableMelee()
    {
        CanMelee = true;
    }

    private void DisableMelee()
    {
        CanMelee = false;
    }

    public void ApplyKickThrust()
    {
        SetVelocityX(playerData.kickThrustVelocity * FacingDirection);
    }

    public void ApplySlideKickThrust()
    {
        SetVelocityX(playerData.slideKickThrustVelocity * FacingDirection);
    }

    public void ApplyRollThrust()
    {
        SetVelocityX(playerData.rollThrustVelocity * FacingDirection);
    }

    private void Die()
    {
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }

    public void UpdateMovingPlatformPositionOffset()
    {
        if (CheckIfTouchingMovingPlatform())
        {
            float movingPlatformPositionOffsetX = transform.position.x - transform.parent.transform.position.x;
            float movingPlatformPositionOffsetY = transform.position.y - transform.parent.transform.position.y;
            MovingPlatformPositionOffset = new Vector3(movingPlatformPositionOffsetX, movingPlatformPositionOffsetY);
        }
    }

    public void SetMovingPlatformOffsetPosition()
    {
        if (CheckIfTouchingMovingPlatform())
        {
            Debug.Log("Updating dynamic transform position to " + new Vector3(transform.parent.transform.position.x + MovingPlatformPositionOffset.x,
                transform.parent.transform.position.y + MovingPlatformPositionOffset.y));
            transform.position = new Vector3(transform.parent.transform.position.x + MovingPlatformPositionOffset.x, 
                transform.parent.transform.position.y + MovingPlatformPositionOffset.y);
        }
    }

    public void UpdateAndSetMovingPlatformOffsetPosition()
    {
        UpdateMovingPlatformPositionOffset();
        SetMovingPlatformOffsetPosition();
    }

    private void Shoot()
    {
        float angleWorkspace = Vector2.SignedAngle(Vector2.right, shotDirection) + LoadedBullet.spread/2;

        for(int i = 0; i < LoadedBullet.numberOfBullets; i++)
        {
            
            Vector2 angleWorkspaceVector = new Vector2(Mathf.Cos(angleWorkspace * Mathf.Deg2Rad), Mathf.Sin(angleWorkspace * Mathf.Deg2Rad));

            GameObject emittedBullet = GameObject.Instantiate(LoadedBullet.gameObject, EmissionPoint.position, Quaternion.Euler(0f, 0f, angleWorkspace));
            Rigidbody2D emittedBulletRB = emittedBullet.GetComponent<Rigidbody2D>();
            emittedBulletRB.AddForce(angleWorkspaceVector * LoadedBullet.BulletForce, ForceMode2D.Impulse);
            angleWorkspace -= LoadedBullet.spread/LoadedBullet.numberOfBullets;
        }
        
        
        CanShoot = false;
        lastShotTime = Time.time;
    }

    public void CreateGhostTrail()
    {
        if (ghostDelaySeconds > 0)
        {
            ghostDelaySeconds -= Time.deltaTime;
        }
        else
        {
            GameObject currentGhost = GameObject.Instantiate(playerData.ghost, transform.position, transform.rotation);
            ghostDelaySeconds = playerData.ghostDelaySeconds;
            Destroy(currentGhost, 1f);
        }
    }

    public void EnableUseBox()
    {
        useBox.SetActive(true);
    }

    public void DisableUseBox()
    {
        useBox.SetActive(false);
    }

    public void EnableGrabber()
    {
        grabber.SetActive(true);
    }

    public void DisableGrabber()
    {
        grabber.SetActive(false);
    }

    #endregion
}
