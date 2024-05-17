using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Sheep : MonoBehaviour
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
        Rotation();
    }

    public void Move()
    {
        
    }

    public void Rotation()
    {

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

    }

    // ���
    public void Wait()
    {

    }

    // �ٱ�
    public void Bouncing()
    {

    }

    // �ȱ�
    public void Walk()
    {

    }
}
