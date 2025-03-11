using UnityEngine;

public class ViewPoint : MonoBehaviour
{
    public Transform cameraTransform;  // ī�޶� ��ġ�� ������ ���
    public Vector3 thirdPersonOffset = new Vector3(0, 4, -2);  // 3��Ī ī�޶� ��ġ ������
    private Vector3 originalPosition; // ���� 1��Ī ��ġ ����
    private bool isThirdPerson = false; // ���� ���� Ȯ��

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;  // �⺻������ ���� ī�޶� �Ҵ�
        }
        originalPosition = cameraTransform.localPosition; // ������ �� ī�޶� ��ġ ����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // YŰ�� ������ ��ȯ
        {
            ToggleCameraPosition();
        }
    }

    void ToggleCameraPosition()
    {
        if (isThirdPerson)
        {
            // 1��Ī���� ����
            cameraTransform.localPosition = originalPosition;
        }
        else
        {
            // 3��Ī ��ġ�� �̵�
            cameraTransform.localPosition = originalPosition + thirdPersonOffset;
        }

        isThirdPerson = !isThirdPerson; // ���� ����
    }
}
