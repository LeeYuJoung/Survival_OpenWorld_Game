using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Fox : MonoBehaviour
{
    [SerializeField]
    private Animator animalAnimator;
    [SerializeField]
    private Rigidbody2D animalRigidbody;
    [SerializeField]
    private Collider2D animalCollider;

    [SerializeField]
    private string animalName;
    [SerializeField]
    private int hp;
    [SerializeField]
    private float walkSpeed;

    private Vector2 direction;

    // ���� ����
    private bool isAction;
    private bool isWalking;

    [SerializeField]
    private float walkTime;
    [SerializeField]
    private float waitTime;
    private float currentTime;

    void Start()
    {
        currentTime = waitTime;  // ��� ����
        isAction = true;         // ��⵵ �ൿ���� ó��
    }

    void Update()
    {
        Move();
        ElapseTime();
    }

    public void Move()
    {
        if (isWalking)
        {
            animalRigidbody.MovePosition(transform.position + transform.right * walkSpeed * Time.deltaTime);
        }
    }

    public void ElapseTime()
    {

    }

    // ���� �ൿ �غ�
    public void Reset()
    {
        
    }

    public void RandomAction()
    {
        int _random = Random.Range(0, 5);

        if(_random == 0)
        {

        }
        else if(_random == 1)
        {

        }
        else if( _random == 2)
        {

        }
        else if( _random == 3)
        {

        }
        else if(_random == 4)
        {

        }
    }

    // ���
    public void Wait()
    {

    }

    // �θ���
    public void Peek()
    {

    }

    // �ȱ�
    public void Walk()
    {

    }

    // �ٱ�
    public void Run()
    {

    }
}
