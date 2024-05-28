using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �������� ������ �������� �κ� -> ������ ������ ���� ��) 
public class StrongAnimal : Animal
{
    [SerializeField] protected int attackDamage;     // ���� ������
    [SerializeField] protected int damage;           // ���� ������
    [SerializeField] protected float attackDistance; // ���� ���� �Ÿ�
    [SerializeField] protected LayerMask targetMask;

    protected float currentChaseTIme;
    [SerializeField] protected float chaseTime;        // �� �߰� �ð�
    [SerializeField] protected float chaseDelayTime;   //  �߰� ������ �ð�

    public void Chase(Vector2 _targetPos)
    {
        isChasing = true;
        isWalking = false;
        isRunning = true;
        animalAnimator.SetBool("Run", isRunning);

        if (!isDead)
        {
            // ���� ����

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
