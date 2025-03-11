using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float firstPersonCheckDistance = 3f;  // 1��Ī üũ �Ÿ�
    public float thirdPersonCheckDistance = 10f; // 3��Ī üũ �Ÿ�
    private float currentCheckDistance; // ���� üũ �Ÿ�
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private PlayerController playerController; // �÷��̾� ��Ʈ�ѷ� ����
    private bool isThirdPerson = false; // ���� ī�޶� ����

    [Header("Jump Boost Settings")]
    public string interactTag = "Carrot"; // ������ �±�
    public float jumpMultiplier = 2f; // ������ ���� ����
    public float boostDuration = 5f; // ������ ���� ���� �ð�

    void Start()
    {
        camera = Camera.main;
        playerController = FindObjectOfType<PlayerController>(); // �÷��̾� ��Ʈ�ѷ� ã��
        currentCheckDistance = firstPersonCheckDistance; // �⺻ �Ÿ� ����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // YŰ�� ���� Ray �Ÿ� ����
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

        // Debug.DrawRay �߰� (������)
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
                StartCoroutine(BoostPlayerJump()); // ���� �ν�Ʈ ����
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
            float originalJumpPower = playerController.jumpPower; // ���� ������ ����
            playerController.jumpPower *= jumpMultiplier; // ������ ����
            yield return new WaitForSeconds(boostDuration); // ���� �ð� ��
            playerController.jumpPower = originalJumpPower; // ���� ������ ����
        }
    }
}
