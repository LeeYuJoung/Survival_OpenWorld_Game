using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject dustGameObject;
    public GameObject jumpDustGameObject;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    public Collider2D polygonCollider;

    // 스피드 변수
    public float moveSpeed;
    public float runSpeed;
    public float climbSpeed;
    public float currentSpeed;

    // 점프 변수
    public float jumpForce;
    public int currentJumpCount;
    public int maxJumpCount;

    // 스태미나 변수
    public float staminaTime;
    public float currentTime;

    // 상태 변수
    private bool isWalk = false;
    private bool isRun = false;
    private bool isClimb = false;
    private bool isGround = true;

    void Start()
    {
        dustGameObject = transform.GetChild(0).gameObject;
        jumpDustGameObject = transform.GetChild(1).gameObject;  
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        polygonCollider = GameObject.Find("Grid").GetComponent<Collider2D>();

        currentTime = staminaTime;
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        if (!isRun)
            dustGameObject.SetActive(false);

        Move();
        Run();
        Climb();
        RunningTime();
        StartCoroutine(Jump());
    }

    public void Move()
    {
        if (Input.GetAxis("Horizontal") > 0 && polygonCollider.bounds.extents.x - 0.5f > transform.position.x)
        {
            isWalk = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0 && -polygonCollider.bounds.extents.x + 0.5f < transform.position.x)
        {
            isWalk = true;
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

    public void RunningTime()
    {
        if (isRun)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UIManager.Instance().RunningBarUpdate(currentTime, staminaTime);
            }
            else
            {
                currentTime = 0;
                currentSpeed = moveSpeed;
                dustGameObject.SetActive(false);
                UIManager.Instance().RunningBarUpdate(currentTime, staminaTime);
            }
        }
        else
        {
            if (currentTime < staminaTime)
            {
                currentTime += Time.deltaTime;
                UIManager.Instance().RunningBarUpdate(currentTime, staminaTime);
            }
            else
            {
                currentTime = staminaTime;
                UIManager.Instance().RunningBarUpdate(currentTime, staminaTime);
            }
        }
    }

    public void Run()
    {
        if (!isGround || !isWalk)
            return;

        if (Input.GetKey(KeyCode.LeftShift) && currentTime > 0)
        {
            isRun = true;
            currentSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
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
