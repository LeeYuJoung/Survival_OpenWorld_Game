using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalGame
{
    public class Gun : MonoBehaviour
    {
        /* ��� ������ ���� �������� ���� �ִ� �⺻ �Ӽ� */

        public string gunName;  // �� �̸�
        public float range;     // �� �����Ÿ�
        public float accuracy;  // ���� ��Ȯ��
        public float fireRate;  // ���� �ӵ�
        public float reloadTime;// ������ �ӵ�
        public int damage;      // ���� ���ݷ�

        public int reloadBulletCount;  // ������ �Ѿ� ����
        public int currentBulletCount; // ���� źâ �ȿ� �����ִ� �Ѿ� ����
        public int maxBulletCount;     // �ִ� ���� ������ �Ѿ��� ����
        public int carryBulletCount;   // ���� ������ �ִ� �Ѿ��� �� ����

        public float retroActionForce; // �ݵ� ����
        public float retroActionFineSightForce;  // �����ؽ� �ݵ� ����

        public Vector3 findSightOriginPos;   // �����ؽ� ���� ���� ��ġ

        public Animator anim;
        public ParticleSystem muzzleFlash;
        public AudioClip fire_Sound;
    }
}