using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Vector3 originPos;   // ���� �� ��ġ (������ ���� �� ���ƿ� ��ġ)

    [SerializeField]
    private Gun currentGun;
    private float currentFireRate;

    private bool isReload = false;
    private bool isFindSightMode = false; // ������ ������ Ȯ��

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

    // �߻�
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
        Debug.Log("::: �Ѿ� �߻� :::");
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;

        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();

        // �ѱ� �ݵ� �ڷ�ƾ ����
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    // ������
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
            Debug.Log("::: �Ѿ� ���� :::");
        }
    }

    // ������
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

    // �ݵ�
    IEnumerator RetroActionCoroutine()
    {
        // ������ �� ���� �� �ִ� �ݵ� 
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        // ������ ���� �� �ִ� �ݵ�
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.findSightOriginPos.y, currentGun.findSightOriginPos.z);

        if (!isFindSightMode) // ������ �ƴ� ����
        {
            currentGun.transform.localPosition = originPos;

            // �ݵ� ����
            while(currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            // ����ġ
            while(currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        else // ������ ����
        {
            currentGun.transform.localPosition = currentGun.findSightOriginPos;

            // �ݵ� ����
            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            // ����ġ
            while (currentGun.transform.localPosition != currentGun.findSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.findSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }

    // �߻� �Ҹ� ���
    private void PlaySE(AudioClip _clip)
    {
        gunAudioSource.clip = _clip;
        gunAudioSource.Play();
    }
}