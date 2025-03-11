using UnityEngine;
using TMPro;

public class ClearZone : MonoBehaviour
{
    public AudioClip enterSound; // Ʈ���ſ� ������ �� ����� �Ҹ�
    public TextMeshProUGUI messageText; // ȭ�鿡 ǥ���� �ؽ�Ʈ
    private AudioSource audioSource;

    private void Start()
    {
        // Empty ������Ʈ�� AudioSource�� �ִ��� Ȯ���ϰ� ��������
        audioSource = GetComponent<AudioSource>();


        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������ ��
        {
            // ���� ���
            if (enterSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(enterSound);
            }

            // �ؽ�Ʈ ǥ��
            if (messageText != null)
            {
                messageText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������
        {
            // �ؽ�Ʈ �����
            if (messageText != null)
            {
                messageText.gameObject.SetActive(false);
            }
        }
    }
}
