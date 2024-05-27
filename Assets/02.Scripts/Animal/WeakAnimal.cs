using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �������� ������ �������� �κ� -> �������� ������ ���� ��) Fox, 
public class WeakAnimal : Animal
{
    // �ٱ�
    public void Run(Vector2 _targetPos)
    {
        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;

        animalAnimator.SetBool("Run", isRunning);
    }

    public override void Damage(int _damage, Vector2 _targetPos)
    {
        base.Damage(_damage, _targetPos);

        if(!isDead )
        {
            Run(_targetPos);
        }
    }
}
