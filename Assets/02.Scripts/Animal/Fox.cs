using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Fox : WeakAnimal
{
    protected override void Reset()
    {
        base.Reset();
        RandomAction();
    }

    public void RandomAction()
    {
        int _random = Random.Range(0, 3);

        if (_random == 0)
        {
            Wait();
        }
        else if (_random == 1)
        {
            Peek();
        }
        else if (_random == 2)
        {
            Walk();
        }
        else if (_random == 3)
        {
            Sleep();
        }
    }

    // 대기
    public void Wait()
    {
        currentTime = waitTime;
    }

    // 두리번
    public void Peek()
    {
        currentTime = waitTime;
        animalAnimator.SetTrigger("Peek");
    }

    // 잠자기
    public void Sleep()
    {
        currentTime = waitTime;
        animalAnimator.SetBool("Sleep", true);
    }
}
