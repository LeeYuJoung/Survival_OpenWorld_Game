using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 스피드 변수
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;

    // 점프 변수
    [SerializeField]
    private float jumpForce;

    // 앉음 변수
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    // 상태 변수
    private bool isRun = false;
    private bool isGround = false;
    private bool isCrouch = false;

    // 카메라 한계 변수
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private Camera mainCam;
    private Rigidbody playerRigidbody;
    private Collider playerCollider;
    private GunController gunController;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        gunController = FindObjectOfType<GunController>();  

        // 초기화
        applySpeed = moveSpeed;
        originPosY = mainCam.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    void Update()
    {
        IsGround();
        Jump();
        Run();
        TryCrouch();
        Move();
        CameraRotation();
        CharacterRotation();
    }

    // 지면 체크
    public void IsGround()
    {
        // collider.bounds.extens => 콜라이더 크기의 절반
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f);
    }

    // 점프
    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            if(isCrouch)
                Crouch();

            playerRigidbody.velocity = transform.up * jumpForce;
        }
    }

    // 달리기 
    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isCrouch)
                Crouch();

            // 달리면 정조준 상태 풀림
            //gunController.CancelFindSight();

            isRun = true;
            applySpeed = runSpeed;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            applySpeed = moveSpeed;
        }
    }

    // 앉기
    public void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    public void Crouch()
    {
        isCrouch = !isCrouch;

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = moveSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    // 부드러운 앉기 동작
    IEnumerator CrouchCoroutine()
    {
        float _posY = mainCam.transform.localPosition.y;
        int count = 0;

        while (_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.2f);
            mainCam.transform.localPosition = new Vector3(0.0f, _posY, 0.5f);

            if(count > 15)
                break;

            yield return null;
        }

        mainCam.transform.localPosition = new Vector3(0.0f, applyCrouchPosY, 0.5f);
    }

    // 키보드 입력에 따라 이동
    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveVelocity = ((transform.right * moveX) + (transform.forward * moveY)).normalized * applySpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + moveVelocity);
    }

    // 마우스 위아래(Y) 움직임에 따라 카메라 X축 회전
    public void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;

        // 지정한 최대/최소 범위 이외 값 되지 않게 제한
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        mainCam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0.0f, 0.0f);
    }

    // 마우스 좌우(X) 움직임에 따라 캐릭터 Y축 회전 
    public void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");

        // Quaternion은 값을 더하기 위해서는 서로 곱해주어야 됨
        Vector3 playerRotationY = new Vector3(0.0f, yRotation * lookSensitivity, 0.0f);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(playerRotationY));
    }
}
