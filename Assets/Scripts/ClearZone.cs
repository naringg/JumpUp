using UnityEngine;
using TMPro;

public class ClearZone : MonoBehaviour
{
    public AudioClip enterSound; // 트리거에 진입할 때 재생할 소리
    public TextMeshProUGUI messageText; // 화면에 표시할 텍스트
    private AudioSource audioSource;

    private void Start()
    {
        // Empty 오브젝트에 AudioSource가 있는지 확인하고 가져오기
        audioSource = GetComponent<AudioSource>();


        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 들어왔을 때
        {
            // 사운드 재생
            if (enterSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(enterSound);
            }

            // 텍스트 표시
            if (messageText != null)
            {
                messageText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 나가면
        {
            // 텍스트 숨기기
            if (messageText != null)
            {
                messageText.gameObject.SetActive(false);
            }
        }
    }
}
