using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Slider[] statusSliders;

    // �� ���� ��ǥ �ε���
    private const int HP = 0, MP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;

    // ü�� ����
    [SerializeField]
    private int hp;
    private int currentHP;

    // ���� ����
    [SerializeField]
    private int mp;
    private int currentMP;

    // ���¹̳� ����
    [SerializeField]
    private int sp;
    private int currentSP;

    [SerializeField]
    private int spInCreaseSpeed;    // ���¹̳� ������ 

    [SerializeField]
    private int spChargeTime;       // ���¹̳� ���� ������ �ð� 
    private int currentSPChargeTime;

    private bool isSPUse = false;

    // ����� ����
    [SerializeField]
    private int hungry;
    private int currentHungry;

    [SerializeField]
    private int hungryDecreaseTime;  // ����� �پ��� �ӵ�
    private int currentHungryDecreaseTime;

    // �񸶸� ����
    [SerializeField]
    private int thirsty;
    private int currentThirsty;

    [SerializeField]
    private int thirstyDecreaseTime; // �񸶸� �پ��� �ӵ�
    private int currentThirstyDecreaseTime;

    // ������ ����
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
}
