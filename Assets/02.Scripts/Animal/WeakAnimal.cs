using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 약한 동물들이 가지는 공통적인 부분 -> 데미지를 입으면 도망 예) Fox, 
public class WeakAnimal : Animal
{
    // 뛰기
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
