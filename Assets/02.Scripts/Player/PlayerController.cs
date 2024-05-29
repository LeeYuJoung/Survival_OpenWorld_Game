using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

// �÷��̾��� ���� �� ���� ����
public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private StatusController statusController;
    private PlayerMovement playerMovement;

    private RaycastHit2D hit;

    // ������ ����
    [SerializeField]
    private float workDelayTime;
    private float currentWorkDelayTime;

    // ��ġ�� ����

    // �� ����

    // Ȱ ����

    // ���� ����
    public bool isAx = false;
    public bool isHammer = false;
    public bool isSword = false;
    public bool isBow = false;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        statusController = GetComponent<StatusController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Ax();
        Hammer();
        Sword();    
        Bow();  
    }

    // ������
    public void Ax()
    {
        if (isHammer)
            return;

        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1) && statusController.GetCurrentSP() > 0)
        {
            isAx = true;
            playerMovement.isWalk = false;
            statusController.DecreaseSP(0.1f);
            InteractionAx();
        }
        else if(Input.GetKeyUp(KeyCode.Keypad1) || Input.GetKeyUp(KeyCode.Alpha1))
        {
            isAx = false;
            currentWorkDelayTime = 0;
            statusController.isSPUse = false;
        }
        else if(statusController.GetCurrentSP() <= 0)
        {
            isAx = false;
        }

        playerAnimator.SetBool("Ax", isAx);
    }

    public void InteractionAx()
    {
        int layerMask = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Grid")));
        hit = Physics2D.Raycast(transform.position, playerMovement.playerDirection, 1.0f, layerMask);
        Debug.DrawRay(transform.position, playerMovement.playerDirection, Color.red, 1.0f);

        if(hit)
        {
            switch (hit.collider.tag)
            {
                case "Tree":
                    Debug.Log("���� ���� ��....");
                    currentWorkDelayTime += Time.deltaTime;
                    StartCoroutine(hit.collider.GetComponent<EnviromentController>().EnviromentShake());

                    if (currentWorkDelayTime >= workDelayTime)
                    {
                        currentWorkDelayTime = 0;
                        hit.collider.GetComponent<EnviromentController>().Mining(20.0f);
                    }

                    break;
                case "Weak_Animal":
                    Debug.Log("��� ��....");
                    hit.collider.GetComponent<WeakAnimal>().Damage(0, transform.position);

                    break;
                case "Strong_Animal":
                    Debug.Log("��� ��....");
                    hit.collider.GetComponent<StrongAnimal>().Damage(0, transform.position);

                    break;
                default:
                    break;

            }
        }
    }

    // ��ġ��
    public void Hammer()
    {
        if (isAx)
            return;

        if(Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2) && statusController.GetCurrentSP() > 0)
        {
            isHammer = true;
            playerMovement.isWalk = false;
            statusController.DecreaseSP(0.1f);
            InteractionHammer();
        }
        else if(Input.GetKeyUp(KeyCode.Keypad2) || Input.GetKeyUp(KeyCode.Alpha2))
        {
            isHammer = false;
            currentWorkDelayTime = 0;
            statusController.isSPUse = false;
        }
        else if (statusController.GetCurrentSP() <= 0)
        {
            isHammer = false;
        }

        playerAnimator.SetBool("Hammer", isHammer);
    }

    public void InteractionHammer()
    {
        int layerMask = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Grid")));
        hit = Physics2D.Raycast(transform.position, playerMovement.playerDirection, 1.0f, layerMask);

        if (hit)
        {
            if (hit.collider.CompareTag("Rock"))
            {
                Debug.Log("�� ���� ��....");
                currentWorkDelayTime += Time.deltaTime;
                StartCoroutine(hit.collider.GetComponent<EnviromentController>().EnviromentShake());

                if (currentWorkDelayTime >= workDelayTime)
                {
                    currentWorkDelayTime = 0;
                    hit.collider.GetComponent<EnviromentController>().Mining(20.0f);
                }
            }
        }
    }

    public void Sword()
    {

    }

    public void Bow()
    {

    }
}
