using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalGame
{
    public class Gun : MonoBehaviour
    {
        /* 모든 종류의 총이 공통적을 갖고 있는 기본 속성 */

        public string gunName;  // 총 이름
        public float range;     // 총 사정거리
        public float accuracy;  // 총의 정확도
        public float fireRate;  // 연사 속도
        public float reloadTime;// 재장전 속도
        public int damage;      // 총의 공격력

        public int reloadBulletCount;  // 재장전 총알 개수
        public int currentBulletCount; // 현재 탄창 안에 남아있는 총알 개수
        public int maxBulletCount;     // 최대 소유 가능한 총알의 개수
        public int carryBulletCount;   // 현재 가지고 있는 총알의 총 개수

        public float retroActionForce; // 반동 세기
        public float retroActionFineSightForce;  // 정조준시 반동 세기

        public Vector3 findSightOriginPos;   // 정조준시 총이 향할 위치

        public Animator anim;
        public ParticleSystem muzzleFlash;
        public AudioClip fire_Sound;
    }
}