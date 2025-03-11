using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // 점프 힘 조절
    public AudioClip jumpSound;   // 점프 효과음
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  // 오디오 소스 가져오기
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 캐릭터가 점프 패드에 닿았는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // 기존 Y축 속도를 초기화 후 점프 힘 추가
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                // 효과음 재생
                if (jumpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(jumpSound);
                }
            }
        }
    }
}
