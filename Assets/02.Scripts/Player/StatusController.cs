using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Slider[] statusSliders = new Slider[6];

    // �� ���� ��ǥ �ε���
    private const int HP = 0, MP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;

    // ü�� ����
    [SerializeField]
    private float hp;
    private float currentHP;

    // ���� ����
    [SerializeField]
    private float mp;
    private float currentMP;

    // ���¹̳� ����
    [SerializeField]
    private float sp;
    private float currentSP;

    [SerializeField]
    private float spInCreaseSpeed;    // ���¹̳� ������ 

    [SerializeField]
    private float spChargeTime;       // ���¹̳� ���� ������ �ð� 
    private float currentSPChargeTime;

    private bool isSPUse = false;

    // ����� ����
    [SerializeField]
    private float hungry;
    private float currentHungry;

    [SerializeField]
    private float hungryDecreaseTime;  // ����� �پ��� �ӵ�
    private float currentHungryDecreaseTime;

    // �񸶸� ����
    [SerializeField]
    private float thirsty;
    private float currentThirsty;

    [SerializeField]
    private float thirstyDecreaseTime; // �񸶸� �پ��� �ӵ�
    private float currentThirstyDecreaseTime;

    // ������ ����
    [SerializeField]
    private float satisfy;
    private float currentSatisfy;

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
        SPChargeTime();
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
            Debug.Log("����ļ� ���.....");
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
            Debug.Log("�񸻶� ���....");
        }
    }

    // ü�� ȸ��
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

    // ü�� ���
    public void DecreaseHP(int _count)
    {
        currentHP -= _count;

        if(currentHP <= 0)
        {
            Debug.Log("���....");
        }
    }

    // ���� ȸ��
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

    // ���� ���
    public void DecreaseMP(int _count)
    {
        currentMP -= _count;

        if(currentMP <= 0)
        {
            Debug.Log("���� ��....");
        }
    }

    // ����� ����
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

    // ����� �϶�
    public void DecreaseHungry(int _count)
    {
        if(currentHungry - _count < 0)
        {
            currentHungry = 0;
        }
        else
        {
            currentHungry -= _count;
        }
    }

    // �񸶸� ����
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

    // �񸶸� �϶�
    public void DecreaseThirsty(int _count)
    {
        if(currentThirsty - _count < 0)
        {
            currentThirsty = 0;
        }
        else
        {
            currentThirsty -= _count;
        }
    }

    // ���¹̳� �϶�
    public void DecreaseSP(int _count)
    {
        isSPUse = true;
        currentSPChargeTime = 0;

        if(currentSP - _count > 0)
        {
            currentSP -= _count;
        }
        else
        {
            currentSP = 0;
        }
    }

    // ���¹̳� ȸ���ص� ���� �ð� Ȯ�� 
    public void SPChargeTime()
    {
        if (isSPUse)
        {
            if (currentSPChargeTime < spChargeTime)
            {
                currentSPChargeTime++;
            }
            else
            {
                isSPUse = false;
            }
        }
    }

    // ���¹̳� ȸ��
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
