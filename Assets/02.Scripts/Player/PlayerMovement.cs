using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject dustGameObject;
    public GameObject jumpDustGameObject;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

    // ���ǵ� ����
    public float moveSpeed;
    public float runSpeed;
    public float climbSpeed;
    public float currentSpeed;

    // ���� ����
    public float jumpForce;
    public int currentJumpCount;
    public int maxJumpCount;

    // �޸��� ����
    private float currentTime;
    public float runningTime;

    // ���� ����
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

        currentTime = 0.0f;
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        Move();
        Run();
        Climb();
        StartCoroutine(Jump());
    }

    public void Move()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            isWalk = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
            //playerRigidbody.velocity += Vector2.right * currentSpeed * Time.deltaTime;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            isWalk = true;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
            //playerRigidbody.velocity += Vector2.left * currentSpeed * Time.deltaTime;
        }
        else
        {
            isWalk = false;
        }

        playerAnimator.SetBool("Walk", isWalk);
    }

    public void Run()
    {
        if (!isGround || !isWalk)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
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
            transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
            playerAnimator.SetBool("Climb", isClimb);
        }
        else if(Input.GetKey(KeyCode.DownArrow) && isClimb)
        {
            transform.Translate(Vector2.down * currentSpeed * Time.deltaTime);
            playerAnimator.SetBool("Climb", isClimb);
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
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
            isWalk = false;
            isRun = false;
            isClimb = false;

            currentJumpCount = 0;
            currentSpeed = moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimb = true;
            currentSpeed = climbSpeed;
            playerRigidbody.gravityScale = 0.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimb = false;
            currentSpeed = moveSpeed;
            playerRigidbody.gravityScale = 2.0f;
        }
    }
}
