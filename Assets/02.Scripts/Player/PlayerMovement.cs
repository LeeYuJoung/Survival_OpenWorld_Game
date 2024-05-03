using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject dustGameObject;
    public GameObject jumpDustGameObject;

    private Rigidbody2D playerRigidbody;
    private Collider2D playerCollider;
    private Animator playerAnimator;

    // 스피드 변수
    public float moveSpeed;
    public float runSpeed;
    public float climbSpeed;
    public float currentSpeed;

    public float jumpForce;

    // 상태 변수
    private bool isWalk = false;
    private bool isRun = false;
    private bool isGround = true;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();

        currentSpeed = moveSpeed;
    }

    void Update()
    {
        Move();
        Run();
        StartCoroutine(Jump());
    }

    public void Move()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            isWalk = true;

            //transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            isWalk = true;

            //transform.localEulerAngles = new Vector3(0, 180, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
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

    IEnumerator Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGround = false;
            dustGameObject.SetActive(false);
            jumpDustGameObject.SetActive(true);

            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.velocity = transform.up * jumpForce;

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
            currentSpeed = moveSpeed;
        }
    }
}
