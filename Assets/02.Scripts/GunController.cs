using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Vector3 originPos;   // 원래 총 위치 (정조준 해제 시 돌아올 위치)

    [SerializeField]
    private Gun currentGun;
    private float currentFireRate;

    private bool isReload = false;
    private bool isFindSightMode = false; // 정조준 중인지 확인

    private AudioSource gunAudioSource;

    private void Start()
    {
        gunAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GunFireRateCalc();
        Fire();
        Reload();
        TryFindSight();
    }

    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    // 발사
    private void Fire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            if (currentGun.currentBulletCount > 0)
            {
                Shoot();
            }
            else
            {
                CancelFindSight();  
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    private void Shoot()
    {
        Debug.Log("::: 총알 발사 :::");
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;

        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();

        // 총기 반동 코루틴 실행
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    // 재장전
    private void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            CancelFindSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        if(currentGun.carryBulletCount > 0)
        {
            isReload = true;
            currentGun.anim.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);

            if(currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }

            isReload = false;
        }
        else
        {
            Debug.Log("::: 총알 없음 :::");
        }
    }

    // 정조준
    private void TryFindSight()
    {
        if (Input.GetButtonDown("Fire2") && !isReload)
        {
            FindSight();
        }
    }

    public void CancelFindSight()
    {
        if(isFindSightMode)
        {
            FindSight();
        }
    }

    private void FindSight()
    {
        isFindSightMode = !isFindSightMode;
        currentGun.anim.SetBool("FindSightMode", isFindSightMode);

        if (isFindSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FindSightActivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FindSightDeActivateCoroutine());
        }
    }

    IEnumerator FindSightActivateCoroutine()
    {
        while(currentGun.transform.localPosition != currentGun.findSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.findSightOriginPos, 0.2f);
            yield return null;
        }
    }

    IEnumerator FindSightDeActivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }

    // 반동
    IEnumerator RetroActionCoroutine()
    {
        // 정조준 안 했을 때 최대 반동 
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        // 정조준 했을 때 최대 반동
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.findSightOriginPos.y, currentGun.findSightOriginPos.z);

        if (!isFindSightMode) // 정조준 아닌 상태
        {
            currentGun.transform.localPosition = originPos;

            // 반동 시작
            while(currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            // 원위치
            while(currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        else // 정조준 상태
        {
            currentGun.transform.localPosition = currentGun.findSightOriginPos;

            // 반동 시작
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            // 원위치
            while (currentGun.transform.localPosition != currentGun.findSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.findSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }

    // 발사 소리 재생
    private void PlaySE(AudioClip _clip)
    {
        gunAudioSource.clip = _clip;
        gunAudioSource.Play();
    }
}