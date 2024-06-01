using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �������� ������ �������� �κ� -> �̸�, ü��, �ִϸ��̼�, �ȱ�, ������, ���� 
public class Animal : MonoBehaviour
{
    // �ʿ��� ������Ʈ
    [SerializeField] protected Animator animalAnimator;
    [SerializeField] protected Rigidbody2D animalRigidbody;
    [SerializeField] protected BoxCollider2D animalCollider;
    [SerializeField] protected AudioSource audioSource;

    [SerializeField] protected string animalName;
    [SerializeField] protected int hp;

    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    protected float applySpeed;

    protected int direction;

    // ���� ����
    protected bool isAction;
    protected bool isWalking;
    protected bool isRunning;
    protected bool isChasing;
    protected bool isAttacking;
    protected bool isDead;

    [SerializeField] protected float walkTime;
    [SerializeField] protected float runTime;
    [SerializeField] protected float waitTime;
    protected float currentTime;

    [SerializeField] protected AudioClip[] normal_Sounds; // �ϻ� �Ҹ�
    [SerializeField] protected AudioClip hurt_Sound;
    [SerializeField] protected AudioClip dead_Sounds;

    protected void Start()
    {
        animalAnimator = GetComponent<Animator>();
        animalRigidbody = GetComponent<Rigidbody2D>();
        animalCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

        currentTime = waitTime;  // ��� ����
        isAction = true;         // ��⵵ �ൿ���� ó��
        isDead = false;
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            Move();
            ElapseTime();
        }
    }

    // �̵�
    protected void Move()
    {
        if (isWalking || isRunning)
        {
            if (direction == 0)
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

    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0 && !isChasing && !isAttacking)
            {
                Reset();
            }
        }
    }

    // ���� �ൿ �غ�
    protected virtual void Reset()
    {
        isAction = true;
        isWalking = false;
        isRunning = false;
        applySpeed = walkSpeed;

        animalAnimator.SetBool("Walk", isWalking);
        animalAnimator.SetBool("Run", isRunning);

        direction = Random.Range(0, 2);
    }

    // �ȱ�
    protected void Walk()
    {
        currentTime = walkTime;
        isWalking = true;
        applySpeed = walkSpeed;
        animalAnimator.SetBool("Walk", isWalking);
    }

    public virtual void Damage(int _damage, Vector2 _targetPos)
    {
        if (!isDead)
        {
            hp -= _damage;

            if (hp <= 0)
            {
                Dead();
                return;
            }

            PlaySE(hurt_Sound);
            animalAnimator.SetTrigger("Hurt");
        }
    }

    protected void Dead()
    {
        PlaySE(dead_Sounds);

        isWalking = false;
        isRunning = false;
        isDead = true;

        animalAnimator.SetTrigger("Dead");
    }

    // ���� ���
    protected void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
