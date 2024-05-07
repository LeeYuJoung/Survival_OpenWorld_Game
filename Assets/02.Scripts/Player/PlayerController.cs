using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 공격 및 상태 구현
public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private StatusController statusController;
    private PlayerMovement playerMovement;

    private RaycastHit2D hit;

    // 도끼질 변수
    [SerializeField]
    private float workDelayTime;
    private float currentWorkDelayTime;

    // 망치질 변수

    // 검 변수

    // 활 변수

    // 상태 변수
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
            if (hit.collider.CompareTag("Tree"))
            {
                Debug.Log("나무 제거 중....");
                currentWorkDelayTime += Time.deltaTime;

                if(currentWorkDelayTime >= workDelayTime)
                {
                    currentWorkDelayTime = 0;
                    hit.collider.GetComponent<EnviromentController>();
                    Debug.Log("::: 나뭇가지 획득 :::");
                }
            }
        }
    }

    public void Hammer()
    {
        if (isAx)
            return;

        if(Input.GetKey(KeyCode.Keypad0) || Input.GetKey(KeyCode.Alpha0))
        {
            isHammer = true;
            playerMovement.isWalk = false;
            InteractionHammer();
        }
        else if(Input.GetKeyUp(KeyCode.Keypad0) || Input.GetKeyUp(KeyCode.Alpha0))
        {
            isHammer = false;
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
        Debug.DrawRay(transform.position, playerMovement.playerDirection, Color.red, 1.0f);

        if (hit)
        {
            if (hit.collider.CompareTag("Rock"))
            {
                Debug.Log("돌 제거 중....");
                currentWorkDelayTime += Time.deltaTime;

                if (currentWorkDelayTime >= workDelayTime)
                {
                    currentWorkDelayTime = 0;
                    hit.collider.GetComponent<EnviromentController>();
                    Debug.Log("::: 돌멩이 획득 :::");
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
