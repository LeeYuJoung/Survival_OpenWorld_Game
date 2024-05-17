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

    // 상태 변수
    private bool isAction;
    private bool isWalking;

    [SerializeField]
    private float walkTime;
    [SerializeField]
    private float waitTime;
    private float currentTime;

    void Start()
    {
        currentTime = waitTime;  // 대기 시작
        isAction = true;         // 대기도 행동으로 처리
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

    // 다음 행동 준비
    public void Reset()
    {
        
    }

    public void RandomAction()
    {

    }

    // 대기
    public void Wait()
    {

    }

    // 뛰기
    public void Bouncing()
    {

    }

    // 걷기
    public void Walk()
    {

    }
}
