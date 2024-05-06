using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Slider[] statusSliders;

    // 각 상태 대표 인덱스
    private const int HP = 0, MP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;

    // 체력 변수
    [SerializeField]
    private int hp;
    private int currentHP;

    // 마나 변수
    [SerializeField]
    private int mp;
    private int currentMP;

    // 스태미나 변수
    [SerializeField]
    private int sp;
    private int currentSP;

    [SerializeField]
    private int spInCreaseSpeed;    // 스태미나 증가량 

    [SerializeField]
    private int spChargeTime;       // 스태미나 충전 딜레이 시간 
    private int currentSPChargeTime;

    private bool isSPUse = false;

    // 배고픔 변수
    [SerializeField]
    private int hungry;
    private int currentHungry;

    [SerializeField]
    private int hungryDecreaseTime;  // 배고픔 줄어드는 속도
    private int currentHungryDecreaseTime;

    // 목마름 변수
    [SerializeField]
    private int thirsty;
    private int currentThirsty;

    [SerializeField]
    private int thirstyDecreaseTime; // 목마름 줄어드는 속도
    private int currentThirstyDecreaseTime;

    // 만족감 변수
    [SerializeField]
    private int satisfy;
    private int currentSatisfy;

    void Start()
    {
        currentHP = hp;
        currentMP = mp;
        currentSP = sp;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentSatisfy = satisfy;
    }

    void Update()
    {
        Hungry();
        Thirsty();

        GaugeUpdate();
    }

    public void GaugeUpdate()
    {
        statusSliders[HP].value = (float)((currentHP / hp) * 100);
        statusSliders[MP].value = (float)((currentMP / mp) * 100);
        statusSliders[SP].value = (float)((currentSP / sp) * 100);
        statusSliders[HUNGRY].value = (float)((currentHungry / hungry) * 100);
        statusSliders[THIRSTY].value = (float)((currentThirsty / thirsty) * 100);
        statusSliders[SATISFY].value = (float)((currentSatisfy / satisfy) * 100);
    }

    public void Hungry()
    {
        if(currentHungry > 0)
        {
            if(currentHungryDecreaseTime <= hungryDecreaseTime)
            {
                currentHungryDecreaseTime++;
            }
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("배고파서 사망.....");
        }
    }

    public void Thirsty()
    {
        if(currentThirsty > 0)
        {
            if(currentThirstyDecreaseTime <= thirstyDecreaseTime)
            {
                currentThirstyDecreaseTime++;
            }
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("목말라서 사망....");
        }
    }
}
