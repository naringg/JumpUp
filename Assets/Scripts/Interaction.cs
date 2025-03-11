using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float firstPersonCheckDistance = 3f;  // 1인칭 체크 거리
    public float thirdPersonCheckDistance = 10f; // 3인칭 체크 거리
    private float currentCheckDistance; // 현재 체크 거리
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private PlayerController playerController; // 플레이어 컨트롤러 참조
    private bool isThirdPerson = false; // 현재 카메라 상태

    [Header("Jump Boost Settings")]
    public string interactTag = "Carrot"; // 적용할 태그
    public float jumpMultiplier = 2f; // 점프력 증가 배율
    public float boostDuration = 5f; // 점프력 증가 지속 시간

    void Start()
    {
        camera = Camera.main;
        playerController = FindObjectOfType<PlayerController>(); // 플레이어 컨트롤러 찾기
        currentCheckDistance = firstPersonCheckDistance; // 기본 거리 설정
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // Y키를 눌러 Ray 거리 변경
        {
            ToggleRayDistance();
        }

        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            CheckForInteractable();
        }
    }

    void ToggleRayDistance()
    {
        isThirdPerson = !isThirdPerson;
        currentCheckDistance = isThirdPerson ? thirdPersonCheckDistance : firstPersonCheckDistance;
    }

    void CheckForInteractable()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // Debug.DrawRay 추가 (빨간색)
        Debug.DrawRay(ray.origin, ray.direction * currentCheckDistance, Color.red);

        if (Physics.Raycast(ray, out hit, currentCheckDistance, layerMask))
        {
            if (hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponent<IInteractable>();
                SetPromptText();
            }
        }
        else
        {
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText()
    {
        if (curInteractable != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curInteractable.GetInteractPrompt();
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            if (curInteractGameObject.CompareTag(interactTag))
            {
                StartCoroutine(BoostPlayerJump()); // 점프 부스트 실행
            }

            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    IEnumerator BoostPlayerJump()
    {
        if (playerController != null)
        {
            float originalJumpPower = playerController.jumpPower; // 기존 점프력 저장
            playerController.jumpPower *= jumpMultiplier; // 점프력 증가
            yield return new WaitForSeconds(boostDuration); // 지속 시간 후
            playerController.jumpPower = originalJumpPower; // 원래 점프력 복구
        }
    }
}
