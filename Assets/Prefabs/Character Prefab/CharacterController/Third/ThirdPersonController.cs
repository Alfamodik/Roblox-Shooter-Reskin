using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using YG;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class ThirdPersonController : MonoBehaviour, IPausable
{
    public enum PlayerState { Idle, Crafting, InNpcTrigger, Busy }
    public PlayerState playerState = PlayerState.Idle;

    [SerializeField] private Joystick joystick;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -30f;
    public float jumpHeight = 1.5f;

    [Header("Camera Settings")] public Transform cameraPivot;
    public float cameraDistance = 8f;
    public float cameraSensitivity = 3.5f;
    public float minVerticalAngle = -25f;
    public float maxVerticalAngle = 60f;

    [Header("Camera Collision")]
    [SerializeField] private Transform mainCamera;
    public LayerMask collisionMask;
    public float cameraRadius = 0.3f;
    public float collisionOffset = 1.2f;
    public float cameraAdaptSpeed = 10f;
    public float minCameraDistance = 1f;

    private float currentCameraDistance;
    private Vector3[] cameraClipPoints;

    [Header("Touch Controls")] public float touchSensitivity = 3.5f;
    [Range(0.1f, 0.9f)] public float splitScreenRatio = 0.5f;
    public bool invertVertical = false;

    [Header("Animation")] 
    public Animator animator;
    public CharacterSkinChanger characterSkinChanger;

    [Header("Sounds")]
    public AudioClip[] stepSounds;

    private Vector2 previousTouchPosition;
    private Vector3 moveDirection;
    private bool isCameraDragging;

    private CharacterController controller;

    private float verticalVelocity;
    private float currentHorizontalAngle;
    private float currentVerticalAngle;
    private AudioSource audioSource;
    private float _timer;
    private bool canMove = true;

    public bool inNpcTrigger = false;
    public bool inResourceTrigger = false;

    public bool OnPause { get; private set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        InitializeCamera();

        characterSkinChanger.AnimatorChanged += UpdateAnimator;

        currentCameraDistance = cameraDistance;
        InitializeClipPoints();

        PauseHandler.Add(this);
    }

    private void UpdateAnimator(Animator animator)
    {
        this.animator = animator;
    }

    private void Update()
    {
        if (OnPause) 
            return;

        if (playerState == PlayerState.Busy)
        {
            moveDirection = Vector3.zero;
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsIdle", true);
            return;
        }

        // Блокируем движение, если не Idle
        if (playerState != PlayerState.Idle)
        {
            moveDirection = Vector3.zero;
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsIdle", true);
        }
        else
        {
            HandleMovement();
        }

        UpdateCameraPosition();
        CheckCameraCollisions();
        HandleCameraInput();
        UpdateAnimations();
        UpdateSounds();
    }

    private void HandleCameraInput()
    {
        if (YG2.envir.isDesktop)
            HandleCameraRotation();
        else
            HandleTouchInput();
    }

    public void EnableController()
    {
        this.enabled = true;
        canMove = true;
        SetPlayerState(PlayerState.Idle);
    }

    //Mobile
    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (IsTouchInRightScreen(touch.position))
                    {
                        isCameraDragging = true;
                        previousTouchPosition = touch.position;
                    }
                }
                else if (touch.phase == TouchPhase.Moved && isCameraDragging && IsTouchInRightScreen(touch.position))
                {
                    Vector2 delta = touch.position - previousTouchPosition;
                    float mouseX = delta.x * touchSensitivity * Time.deltaTime;
                    float mouseY = delta.y * touchSensitivity * Time.deltaTime;
                    UpdateCameraAngles(mouseX, mouseY);
                    previousTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isCameraDragging = false;
                }
            }
        }
    }

    private bool IsTouchInRightScreen(Vector2 touchPosition)
    {
        return touchPosition.x > Screen.width * splitScreenRatio;
    }

    private void UpdateCameraAngles(float mouseX, float mouseY)
    {
        currentHorizontalAngle += mouseX * cameraSensitivity;

        float verticalModifier = invertVertical ? 1f : -1f;
        currentVerticalAngle += mouseY * verticalModifier * cameraSensitivity;
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);
    }

    //PC
    private void CheckCameraCollisions()
    {
        Vector3 desiredPosition = cameraPivot.position +
                                  cameraPivot.rotation * new Vector3(0, 0, -cameraDistance);

        float targetDistance = cameraDistance;
        RaycastHit hit;

        if (Physics.SphereCast(cameraPivot.position, cameraRadius,
                (desiredPosition - cameraPivot.position).normalized,
                out hit, cameraDistance, collisionMask))
        {
            targetDistance = hit.distance - collisionOffset;
        }

        currentCameraDistance = Mathf.Lerp(
            currentCameraDistance,
            Mathf.Max(targetDistance, minCameraDistance),
            Time.deltaTime * cameraAdaptSpeed
        );
    }


    private void InitializeClipPoints()
    {
        cameraClipPoints = new Vector3[5];
    }

    private void InitializeCamera()
    {
        //mainCamera = Camera.main.transform;
        currentHorizontalAngle = transform.eulerAngles.y;
        currentVerticalAngle = 15f;
        UpdateCameraPosition();
    }

    private void HandleMovement()
    {
        if (!canMove)
        {
            moveDirection = Vector3.zero;
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsIdle", true);
            return;
        }
        // Проверка нахождения на земле
        bool isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        float horizontal;
        float vertical;

        if (YG2.envir.isDesktop)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = 0; //joystick.Horizontal; zxc
            vertical = 0; //joystick.Vertical; zxc
        }

        // Создание вектора движения относительно камеры
        Vector3 forward = mainCamera.forward;
        Vector3 right = mainCamera.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        // Применение движения
        Vector3 moveVelocity = moveDirection * moveSpeed;
        moveVelocity.y = 0f;


        verticalVelocity += gravity * Time.deltaTime;
        moveVelocity.y = verticalVelocity;

        controller.Move(moveVelocity * Time.deltaTime);

        // Плавный поворот персонажа в направлении движения
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void HandleCameraRotation()
    {
        // Ввод мыши
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        currentHorizontalAngle += mouseX;
        currentVerticalAngle -= mouseY;
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0);
        cameraPivot.rotation = rotation;

        Vector3 cameraOffset = rotation * new Vector3(0, 0, -currentCameraDistance);
        mainCamera.position = cameraPivot.position + cameraOffset;
        mainCamera.LookAt(cameraPivot.position);
    }

    private void UpdateAnimations()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            animator.SetBool("IsWalk", true);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsIdle", true);
        }
    }

    private void UpdateSounds()
    {
        _timer += Time.deltaTime;
        if (moveDirection.magnitude >= 0.1f && _timer >= controller.stepOffset)
        {
            audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
            _timer = 0;
        }
    }

    public void ChangeSpeed(int newSpeed)
    {
        var multiple = moveSpeed / newSpeed;
        moveSpeed = newSpeed;
        animator.speed /= multiple;
        controller.stepOffset *= multiple;
    }

    public void SwitchEnable(bool value)
    {
        if (value == false)
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsIdle", true);
        }
        enabled = value;
    }

    public void SetPlayerState(PlayerState newState)
    {
        playerState = newState;
    }

    public void StartCrafting()
    {
        SetPlayerState(PlayerState.Crafting);
        animator.SetBool("IsCrafting", true);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsIdle", false);
        canMove = false;
    }

    public void StopCrafting()
    {
        animator.SetBool("IsCrafting", false);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsIdle", true);
        animator.Play("Idle", 0, 0f); // Принудительный переход в Idle (укажите точное имя, если другое)
        canMove = true;
        SetPlayerState(PlayerState.Idle);
    }

    // Остановка крафта при входе в триггер NPC
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            SetInNpcTrigger(true);

            // Принудительно сбрасываем все анимационные флаги и переходим в Idle
            animator.SetBool("IsCrafting", false);
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsIdle", true);
            animator.Play("Idle", 0, 0f); // Должно быть "Idle", а не "IsIdle"
            canMove = true;
            playerState = PlayerState.Idle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            SetInNpcTrigger(false);
        }
    }

    public void SetInResourceTrigger(bool value)
    {
        inResourceTrigger = value;
    }

    public void SetInNpcTrigger(bool value)
    {
        inNpcTrigger = value;
    }

    // Пример метода, который должен запускать крафт
    public void TryStartCrafting()
    {
        // Не запускать крафт, если игрок рядом с NPC
        if (inNpcTrigger)
        {
            return;
        }
        // Не запускать крафт, если уже крафтим
        if (playerState == PlayerState.Crafting)
        {
            return;
        }
        // Не запускать крафт, если игрок не в триггере ресурса
        if (!inResourceTrigger)
        {
            return;
        }
        StartCrafting();
    }

    public void Pause()
    {
        OnPause = true;
    }

    public void Play()
    {
        OnPause = false;
    }
}