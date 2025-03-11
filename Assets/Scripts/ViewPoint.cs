using UnityEngine;

public class ViewPoint : MonoBehaviour
{
    public Transform cameraTransform;  // 카메라 위치를 변경할 대상
    public Vector3 thirdPersonOffset = new Vector3(0, 4, -2);  // 3인칭 카메라 위치 오프셋
    private Vector3 originalPosition; // 원래 1인칭 위치 저장
    private bool isThirdPerson = false; // 현재 상태 확인

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;  // 기본적으로 메인 카메라 할당
        }
        originalPosition = cameraTransform.localPosition; // 시작할 때 카메라 위치 저장
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // Y키를 누르면 전환
        {
            ToggleCameraPosition();
        }
    }

    void ToggleCameraPosition()
    {
        if (isThirdPerson)
        {
            // 1인칭으로 복귀
            cameraTransform.localPosition = originalPosition;
        }
        else
        {
            // 3인칭 위치로 이동
            cameraTransform.localPosition = originalPosition + thirdPersonOffset;
        }

        isThirdPerson = !isThirdPerson; // 상태 변경
    }
}
