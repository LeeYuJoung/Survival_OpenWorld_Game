using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 강한 동물들이 가지는 공통적인 부분 -> 데미지 입으면 공격 예) 
public class StrongAnimal : Animal
{
    [SerializeField] protected int attackDamage;     // 공격 데미지
    [SerializeField] protected int damage;           // 공격 딜레이
    [SerializeField] protected float attackDistance; // 공격 사정 거리
    [SerializeField] protected LayerMask targetMask;

    protected float currentChaseTIme;
    [SerializeField] protected float chaseTime;        // 총 추격 시간
    [SerializeField] protected float chaseDelayTime;   //  추격 딜레이 시간

    public void Chase(Vector2 _targetPos)
    {
        isChasing = true;
        isWalking = false;
        isRunning = true;
        animalAnimator.SetBool("Run", isRunning);

        if (!isDead)
        {
            // 추적 시작

        }
    }

    public override void Damage(int _damage, Vector2 _targetPos)
    {
        base.Damage(_damage, _targetPos);

        if (!isDead)
        {
            Chase(_targetPos);
        }
    }

    protected IEnumerator ChaseTargetCoroutine()
    {
        yield return null;
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttacking = true;


        currentChaseTIme = chaseTime;

        yield return null;
    }
}
