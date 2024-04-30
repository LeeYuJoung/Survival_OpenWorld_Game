using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Animator handAnimator;

    public string handName;   // 이름으로 구분. 예) 너클, 맨 손 등...
    public float range;       // 공격 범위
    public int damage;        // 공격력
    public float workSpeed;   // 작업 속도
    public float attackDelay; // 공격 딜레이
    public float attackDelayA;// 공격 활성화 시점 (공격 애니메이션 중 주먹이 다 뻗어졌을 때 공격 데미지 넣기)
    public float attackDelayB;// 공격 비활성화 시점
}
