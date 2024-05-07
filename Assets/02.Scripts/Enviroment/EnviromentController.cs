using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public enum EnviromentType
    {
        Tree,
        Rock
    }
    public EnviromentType currentType;

    public GameObject[] treeItems;
    private Vector3 originPos;

    [SerializeField]
    private float hp;
    private float currentHP;

    [SerializeField]
    private float shakeTime;
    private float currentShakeTime;

    void Start()
    {
        originPos = transform.position;
        currentHP = hp;
    }

    void Update()
    {
        
    }

    // ä��
    public void Mining(float _damage)
    {
        SpawnItem();
        hp -= _damage;

        if(hp <= 0)
        {
            Destruction();
        }
    }

    // ������ ����
    public void SpawnItem()
    {
        if(currentType == EnviromentType.Tree)
        {
            GameObject _item = Instantiate(treeItems[Random.Range(0, treeItems.Length)]);
            _item.transform.position = new Vector3(Random.Range(-0.1f, 0.1f), 0, 0);
        }
    }

    // �ı�
    public void Destruction()
    {
        Debug.Log("::: ���� �Ϸ� :::");
        transform.gameObject.SetActive(false);
    }

    // ä�� �۾� �� ��鸲 ȿ��
    public IEnumerator EnviromentShake()
    {
        currentShakeTime = 0;

        while (currentShakeTime < shakeTime)
        {
            transform.position += new Vector3(Random.Range(-0.0025f, 0.0025f), 0, 0);
            yield return new WaitForSeconds(0.01f);
            currentShakeTime += Time.deltaTime;
        }

        transform.position = originPos;
    }
}
