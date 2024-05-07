using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Slider[] statusSliders = new Slider[6];

    // 각 상태 대표 인덱스
    private const int HP = 0, MP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;

    // 체력 변수
    [SerializeField]
    private float hp;
    private float currentHP;

    // 마나 변수
    [SerializeField]
    private float mp;
    private float currentMP;

    // 스태미나 변수
    [SerializeField]
    private float sp;
    private float currentSP;

    [SerializeField]
    private float spInCreaseSpeed;    // 스태미나 증가량 
    public bool isSPUse = false;

    // 배고픔 변수
    [SerializeField]
    private float hungry;
    private float currentHungry;

    [SerializeField]
    private float hungryDecreaseTime;  // 배고픔 줄어드는 속도
    private float currentHungryDecreaseTime;

    // 목마름 변수
    [SerializeField]
    private float thirsty;
    private float currentThirsty;

    [SerializeField]
    private float thirstyDecreaseTime; // 목마름 줄어드는 속도
    private float currentThirstyDecreaseTime;

    // 만족감 변수
    [SerializeField]
    private float satisfy;
    private float currentSatisfy;

    // 사망 변수
    private bool isDead = false;

    void Start()
    {
        statusSliders[HP] = GameObject.Find("Sliders").transform.GetChild(0).GetComponent<Slider>();
        statusSliders[MP] = GameObject.Find("Sliders").transform.GetChild(1).GetComponent<Slider>();
        statusSliders[SP] = GameObject.Find("Sliders").transform.GetChild(2).GetComponent<Slider>();
        statusSliders[HUNGRY] = GameObject.Find("StatusBar").transform.GetChild(0).GetComponent<Slider>();
        statusSliders[THIRSTY] = GameObject.Find("StatusBar").transform.GetChild(2).GetComponent<Slider>();
        statusSliders[SATISFY] = GameObject.Find("StatusBar").transform.GetChild(1).GetComponent<Slider>();

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
        SPRecover();
        GaugeUpdate();
    }

    public void GaugeUpdate()
    {
        statusSliders[HP].value = (currentHP / hp) * 100.0f;
        statusSliders[MP].value = (currentMP / mp) * 100.0f ;
        statusSliders[SP].value = (currentSP / sp) * 100.0f;
        statusSliders[HUNGRY].value = (currentHungry / hungry) * 100.0f;
        statusSliders[THIRSTY].value = (currentThirsty / thirsty) * 100.0f;
        statusSliders[SATISFY].value = (currentSatisfy / satisfy) * 100.0f;
    }

    public void Hungry()
    {
        if(currentHungry > 0)
        {
            if(currentHungryDecreaseTime > 0)
            {
                currentHungryDecreaseTime -= Time.deltaTime;
            }
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = hungryDecreaseTime;
            }
        }
        else
        {
            Debug.Log("배고파서 사망.....");
            isDead = true;
        }
    }

    public void Thirsty()
    {
        if(currentThirsty > 0)
        {
            if(currentThirstyDecreaseTime > 0)
            {
                currentThirstyDecreaseTime -= Time.deltaTime;
            }
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = thirstyDecreaseTime;
            }
        }
        else
        {
            Debug.Log("목말라서 사망....");
            isDead= true;
        }
    }

    // 체력 회복
    public void IncreaseHP(int _count)
    {
        if(currentHP + _count < hp)
        {
            currentHP += _count;
        }
        else
        {
            currentHP = hp;
        }
    }

    // 체력 사용
    public void DecreaseHP(int _count)
    {
        currentHP -= _count;

        if(currentHP <= 0)
        {
            Debug.Log("사망....");
            isDead = true;
        }
    }

    // 마나 회복
    public void IncreaseMP(int _count)
    {
        if(currentMP + _count < mp)
        {
            currentMP += _count;
        }
        else
        {
            currentMP = mp;
        }
    }

    // 마나 사용
    public void DecreaseMP(int _count)
    {
        currentMP -= _count;

        if(currentMP <= 0)
        {
            Debug.Log("마나 고갈....");
        }
    }

    // 배고픔 증가
    public void IncreaseHungry(int _count)
    {
        if(currentHungry + _count < hungry)
        {
            currentHungry += _count;
        }
        else
        {
            currentHungry = hungry;
        }
    }

    // 배고픔 하락
    public void DecreaseHungry(int _count)
    {
        if(currentHungry - _count < 0)
        {
            currentHungry = 0;
            isDead = true;
        }
        else
        {
            currentHungry -= _count;
        }
    }

    // 목마름 증가
    public void IncreaseThirsty(int _count)
    {
        if(currentThirsty + _count < thirsty)
        {
            currentThirsty += _count;
        }
        else
        {
            currentThirsty = thirsty;
        }
    }

    // 목마름 하락
    public void DecreaseThirsty(float _count)
    {
        if(currentThirsty - _count < 0)
        {
            currentThirsty = 0;
            isDead = true;
        }
        else
        {
            currentThirsty -= _count;
        }
    }

    // 스태미나 감소
    public void DecreaseSP(float _count)
    {
        isSPUse = true;

        if(currentSP - _count > 0)
        {
            currentSP -= _count;
        }
        else
        {
            currentSP = 0;
        }
    }

    // 스태미나 회복
    public void SPRecover()
    {
        if(!isSPUse && currentSP < sp)
        {
            currentSP += spInCreaseSpeed;
        }
    }

    public float GetCurrentSP()
    {
        return currentSP;
    }
}
