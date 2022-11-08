using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("Player Component")]
    public Animator anim;
    public Rigidbody2D rb;
    public AnimatorStateInfo animCurState;

    [Header("State List")]
    public FSM curState;
    public string lastState;
    public FSM idleState = new idleState();
    public FSM runState = new RunState();
    public FSM jumpState = new JumpState();
    public FSM fallState = new FallState();
    public FSM onWallState = new OnWallState();
    public FSM climbState = new ClimbState();
    public FSM wallJumpState = new WallJumpState();
    public FSM deadState = new DeadState();
    public FSM cotoyeState = new CotoyeState();


    [Header("Enteral Input")]
    public Transform groundCheck;
    public float checkRange;
    public LayerMask checkLayer;

    public Vector2 leftWall;
    public Vector2 rightWall;
    public float checkRangeForWall;
    public LayerMask grabWall;

    [Header("Move Parameter")]
    public float dir;
    public float speed;
    public float acceleration;
    public float deceleration;


    private float targetSpeed;
    private float speedDif;
    private float accelRate;
    private float trueSpeed;


    [Header("Jump Parameter")]
    private float jumpDir;
    public float jumpForce;
    public float wallJumpVertical;
    public float walljumpHorizontal;
    public float jumpSpeed;
    public float fallingMultiplier;
    public float lowJumpMultiplier;
    public float JumpLerp;
    public int MaxJump = 2;
    public int curJump = 0;
    public ParticleSystem jumpParticle;

    [Header("Player Check")]
    public bool isGround;
    public bool isJump;
    public bool isGrab;
    public bool isLeftGrab;
    public bool isRightGrab;

    [Header("Wall Climb Parameter")]
    public float climbSpeed;
    public float climbDir;

    [Header("Coyote Time")]
    public float Time;

    [SerializeField] public Transform RespawPoint;



    private void Awake()
    {
        jumpParticle.Stop();
        switchState(idleState);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        animCurState = anim.GetCurrentAnimatorStateInfo(0);
        inputSystem();
        curState.onChange();
    }

    private void FixedUpdate()
    {
        curState.onUpdate();
    }









    //Implement Function
    public void switchState(FSM changeState) {
        curState = changeState;
        curState.enterState(this);
    }

    public void MovementMethod() {

        transform.localRotation = dir > 0 ? Quaternion.Euler(0, 0, 0) :
            (dir < 0 ? Quaternion.Euler(0, 180, 0) : transform.localRotation);

        targetSpeed = dir * speed;
        speedDif = targetSpeed - rb.velocity.x;
        accelRate = (Mathf.Abs(targetSpeed) >= 0.01f) ? acceleration : deceleration;
        trueSpeed = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 1.5f) * Mathf.Sign(speedDif);
        rb.AddForce(trueSpeed * Vector2.right, ForceMode2D.Force);

    }


    public void Jump() {
        anim.Play("Jump");
        isJump = false;
        curJump++;
        rb.velocity = new Vector2(jumpDir * jumpSpeed, 0) + Vector2.up * jumpForce;
    }

    public void wallJump(Vector2 jumpDir) {
        anim.Play("Jump");
        curJump++;
        rb.velocity = jumpDir * walljumpHorizontal + Vector2.up * wallJumpVertical;
    }


    private void inputSystem()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRange, checkLayer);

        isGrab = Physics2D.OverlapCircle((Vector2)transform.position + leftWall, checkRangeForWall, grabWall) |
            Physics2D.OverlapCircle((Vector2)transform.position + rightWall, checkRangeForWall, grabWall);

        isLeftGrab = Physics2D.OverlapCircle((Vector2)transform.position + leftWall, checkRangeForWall, grabWall);
        isRightGrab = Physics2D.OverlapCircle((Vector2)transform.position + rightWall, checkRangeForWall, grabWall);

        dir = Input.GetAxis("Horizontal");
        jumpDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            isJump = true;

        climbDir = Input.GetAxis("Vertical");


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRange);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftWall, checkRangeForWall);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightWall, checkRangeForWall);
    }

    public void dirFilp() {
        transform.localRotation = transform.localRotation.y == 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    public void Grabing() {
        rb.velocity = Vector2.zero;
        anim.Play("OnWall");
    }


    public void Climbing() {

        if (climbDir != 0) {
            anim.Play("Climb");
            rb.velocity = Vector2.up * climbDir * climbSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hit")) {
            switchState(deadState);
            anim.Play("hurt");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("checkPoint")) {
            RespawPoint = other.transform;
        }
    }
}
