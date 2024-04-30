using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Animator handAnimator;

    public string handName;   // �̸����� ����. ��) ��Ŭ, �� �� ��...
    public float range;       // ���� ����
    public int damage;        // ���ݷ�
    public float workSpeed;   // �۾� �ӵ�
    public float attackDelay; // ���� ������
    public float attackDelayA;// ���� Ȱ��ȭ ���� (���� �ִϸ��̼� �� �ָ��� �� �������� �� ���� ������ �ֱ�)
    public float attackDelayB;// ���� ��Ȱ��ȭ ����
}
