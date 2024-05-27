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

    // ���� ����
    private bool isAction;
    private bool isWalking;
    private bool isRunning;
    private bool isDead;

    [SerializeField] private float walkTime;
    [SerializeField] private float runTime;
    [SerializeField] private float waitTime;
    private float currentTime;

    [SerializeField] private AudioClip[] normal_Sounds; // �ϻ� �Ҹ�
    [SerializeField] private AudioClip hurt_Sound;
    [SerializeField] private AudioClip dead_Sounds;

    void Start()
    {
        animalAnimator = GetComponent<Animator>();
        animalRigidbody = GetComponent<Rigidbody2D>();
        animalCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        currentTime = waitTime;  // ��� ����
        isAction = true;         // ��⵵ �ൿ���� ó��
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

    // ���� �ൿ �غ�
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

    // ���
    public void Wait()
    {
        currentTime = waitTime;
        Debug.Log("��� ��....");
    }

    // �θ���
    public void Peek()
    {
        currentTime = waitTime;
        animalAnimator.SetTrigger("Peek");
        Debug.Log("�θ���....");
    }

    // ���ڱ�
    public void Sleep()
    {
        currentTime = waitTime;
        animalAnimator.SetBool("Sleep", true);
        Debug.Log("�ڴ� ��....");
    }

    // �ȱ�
    public void Walk()
    {
        currentTime = walkTime;
        isWalking = true;
        applySpeed = walkSpeed;
        animalAnimator.SetBool("Walk", isWalking);
        Debug.Log("�ȴ� ��....");
    }

    // �ٱ�
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
