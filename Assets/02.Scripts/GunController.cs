using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun;
    private float currentFireRate;

    private AudioSource gunAudioSource;

    void Update()
    {
        GunFireRateCalc();
        Fire();
    }

    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0)
        {
            currentFireRate = currentGun.fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        // 실제 발사 처리
        Debug.Log("::: 총알 발사 :::");
    }
}
