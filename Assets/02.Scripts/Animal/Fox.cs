using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Fox : WeakAnimal
{
    [SerializeField] private Animator animalAnimator;
    [SerializeField] private Rigidbody2D animalRigidbody;
    [SerializeField] private Collider2D animalCollider;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private string animalName;
    [SerializeField] private int hp;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float applySpeed;

    private int direction;

    // 상태 변수
    private bool isAction;
    private bool isWalking;
    private bool isRunning;
    private bool isDead;

    [SerializeField] private float walkTime;
    [SerializeField] private float runTime;
    [SerializeField] private float waitTime;
    private float currentTime;

    [SerializeField] private AudioClip[] normal_Sounds; // 일상 소리
    [SerializeField] private AudioClip hurt_Sound;
    [SerializeField] private AudioClip dead_Sounds;

    void Start()
    {
        animalAnimator = GetComponent<Animator>();
        animalRigidbody = GetComponent<Rigidbody2D>();
        animalCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        currentTime = waitTime;  // 대기 시작
        isAction = true;         // 대기도 행동으로 처리
    }

    void Update()
    {
        if (isDead)
        {
            Move();
            ElapseTime();
        }
    }

    public void Move()
    {
        if (isWalking || isRunning)
        {
            if(direction == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                animalRigidbody.MovePosition(transform.position + (transform.right * applySpeed * Time.deltaTime));
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                animalRigidbody.MovePosition(transform.position + (transform.right * applySpeed * Time.deltaTime));
            }
        }
    }

    public void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                Reset();
            }
        }
    }

    // 다음 행동 준비
    public void Reset()
    {
        isAction = true;
        isWalking = false;
        animalAnimator.SetBool("Walk", isWalking);
        isRunning = false;
        animalAnimator.SetBool("Run", isRunning);
        applySpeed = walkSpeed;

        direction = Random.Range(0, 2);

        RandomAction();
    }

    public void RandomAction()
    {
        int _random = Random.Range(0, 3);

        if(_random == 0)
        {
            Wait();
        }
        else if(_random == 1)
        {
            Peek();
        }
        else if( _random == 2)
        {
            Walk();
        }
        else if( _random == 3)
        {
            Sleep();
        }
    }

    // 대기
    public void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기 중....");
    }

    // 두리번
    public void Peek()
    {
        currentTime = waitTime;
        animalAnimator.SetTrigger("Peek");
        Debug.Log("두리번....");
    }

    // 잠자기
    public void Sleep()
    {
        currentTime = waitTime;
        animalAnimator.SetBool("Sleep", true);
        Debug.Log("자는 중....");
    }

    // 걷기
    public void Walk()
    {
        currentTime = walkTime;
        isWalking = true;
        applySpeed = walkSpeed;
        animalAnimator.SetBool("Walk", isWalking);
        Debug.Log("걷는 중....");
    }

    // 뛰기
    public void Run(Vector2 _targetPos)
    {
        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;

        animalAnimator.SetBool("Run",isRunning);
    }

    public void Damage (int _damage, Vector2 _targetPos)
    {
        if (!isDead)
        {
            hp -= _damage;

            if(hp <= 0)
            {
                Dead();
                return;
            }

            PlaySE(hurt_Sound);
            animalAnimator.SetTrigger("Hurt");
            Run(_targetPos);
        }
    }

    private void Dead()
    {
        isWalking = false;
        isRunning = false;
        isDead = true;

        animalAnimator.SetTrigger("Dead");
    }

    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
