using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : StrongAnimal
{
    protected override  void Update()
    {
        base.Update();
    }

   protected override void Reset()
    {
        base.Reset();
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 3);

        if(_random == 0 )
        {
            Wait();
        }
        else if(_random == 1 )
        {
            Wait();
        }
        else if( _random == 2 )
        {
            Walk();
        }
    }

    private void Wait()
    {
        currentChaseTIme = waitTime;
        Debug.Log("´ë±â Áß....");
    }
}
