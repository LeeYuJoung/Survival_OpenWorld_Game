using SurvivalGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject dustGameObject;
    private GameObject jumpDustGameObject;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private StatusController statusController;
    private PlayerController playerController;
    private Collider2D polygonCollider;

    // 스피드 변수
    public float moveSpeed;
    public float runSpeed;
    public float climbSpeed;
    public float currentSpeed;

    // 점프 변수
    public float jumpForce;
    public int currentJumpCount;
    public int maxJumpCount;

    // 상태 변수
    public bool isWalk = false;
    public bool isRun = false;
    public bool isClimb = false;
    public bool isGround = true;

    public Vector2 playerDirection;

    void Start()
    {
        dustGameObject = transform.GetChild(0).gameObject;
        jumpDustGameObject = transform.GetChild(1).gameObject;  
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        statusController = GetComponent<StatusController>();
        playerController = GetComponent<PlayerController>();
        polygonCollider = GameObject.Find("Grid").GetComponent<Collider2D>();

        currentSpeed = moveSpeed;
    }

    void Update()
    {
        if (!isRun)
            dustGameObject.SetActive(false);

        Move();
        Run();
        Climb();
        StartCoroutine(Jump());
    }

    public void Move()
    {
        if (playerController.isAx || playerController.isHammer)
            return;

        if (Input.GetAxis("Horizontal") > 0 && polygonCollider.bounds.extents.x - 0.5f > transform.position.x)
        {
            isWalk = true;
            playerDirection = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0 && -polygonCollider.bounds.extents.x + 0.5f < transform.position.x)
        {
            isWalk = true;
            playerDirection = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        }
        else
        {
            isWalk = false;
            isRun = false;
        }

        playerAnimator.SetBool("Run", isRun);
        playerAnimator.SetBool("Walk", isWalk);
    }

    public void Run()
    {
        if (!isGround || !isWalk)
            return;

        if (Input.GetKey(KeyCode.LeftShift) && statusController.GetCurrentSP() > 0)
        {
            isRun = true;
            currentSpeed = runSpeed;
            statusController.DecreaseSP(0.1f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            currentSpeed = moveSpeed;
            statusController.isSPUse = false;
        }
        else if(statusController.GetCurrentSP() <= 0)
        {
            isRun = false;
            currentSpeed = moveSpeed;
        }

        dustGameObject.SetActive(isRun);
        playerAnimator.SetBool("Run", isRun);
    }

    public void Climb()
    {
        if (Input.GetKey(KeyCode.UpArrow) && isClimb)
        {
            isGround = false;
            isWalk = false;
            isRun = false;

            currentSpeed = climbSpeed;
            playerRigidbody.gravityScale = 0.0f;

            playerAnimator.SetBool("Climb", isClimb);
            transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && isClimb)
        {
            isGround = false;
            isWalk = false;
            isRun = false;

            currentSpeed = climbSpeed;
            playerRigidbody.gravityScale = 0.0f;

            playerAnimator.SetBool("Climb", isClimb);
            transform.Translate(Vector2.down * currentSpeed * Time.deltaTime);
        }
    }

    IEnumerator Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentJumpCount < maxJumpCount)
        {
            isGround = false;
            currentJumpCount++;

            dustGameObject.SetActive(false);
            jumpDustGameObject.SetActive(true);

            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.517f);

            jumpDustGameObject.SetActive(false);
        }

        playerAnimator.SetBool("Ground", isGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥과 충돌 처리
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
            isWalk = false;
            isRun = false;
            isClimb = false;
            statusController.isSPUse = false;

            currentJumpCount = 0;
            currentSpeed = moveSpeed;
            playerRigidbody.gravityScale = 2.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 사다리와 충돌 처리
        if (collision.CompareTag("Ladder"))
        {
            Debug.Log("사다리 충돌...");
            isClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 사라디와 떨어짐 
        if (collision.CompareTag("Ladder"))
        {
            Debug.Log("사다리 충돌 끝...");
            isClimb = false;
            isGround = true;
            isWalk = false;
            isRun = false;

            currentSpeed = moveSpeed;
            playerRigidbody.gravityScale = 2.0f;
            playerAnimator.SetBool("Climb", isClimb);
        }
    }
}
