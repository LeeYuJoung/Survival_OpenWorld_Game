using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance()
    {
        return instance;
    }

    public Slider runningBar;
    public Slider HPBar;
    public Slider MPBar;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    public void RunningBarUpdate(float _currentTime, float _runningTime)
    {
        runningBar.value = (_currentTime / _runningTime) * 100.0f;
    }
}
