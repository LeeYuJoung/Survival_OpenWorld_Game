using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���ǵ� ����
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;

    // ���� ����
    [SerializeField]
    private float jumpForce;

    // ���� ����
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    // ���� ����
    private bool isRun = false;
    private bool isGround = false;
    private bool isCrouch = false;

    // ī�޶� �Ѱ� ����
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

        // �ʱ�ȭ
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

    // ���� üũ
    public void IsGround()
    {
        // collider.bounds.extens => �ݶ��̴� ũ���� ����
        isGround = Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f);
    }

    // ����
    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            if(isCrouch)
                Crouch();

            playerRigidbody.velocity = transform.up * jumpForce;
        }
    }

    // �޸��� 
    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isCrouch)
                Crouch();

            // �޸��� ������ ���� Ǯ��
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

    // �ɱ�
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

    // �ε巯�� �ɱ� ����
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

    // Ű���� �Է¿� ���� �̵�
    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveVelocity = ((transform.right * moveX) + (transform.forward * moveY)).normalized * applySpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + moveVelocity);
    }

    // ���콺 ���Ʒ�(Y) �����ӿ� ���� ī�޶� X�� ȸ��
    public void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;

        // ������ �ִ�/�ּ� ���� �̿� �� ���� �ʰ� ����
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        mainCam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0.0f, 0.0f);
    }

    // ���콺 �¿�(X) �����ӿ� ���� ĳ���� Y�� ȸ�� 
    public void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");

        // Quaternion�� ���� ���ϱ� ���ؼ��� ���� �����־�� ��
        Vector3 playerRotationY = new Vector3(0.0f, yRotation * lookSensitivity, 0.0f);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(playerRotationY));
    }
}
