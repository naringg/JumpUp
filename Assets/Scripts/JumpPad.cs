using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // ���� �� ����

    private void OnCollisionEnter(Collision collision)
    {
        // ĳ���Ͱ� ���� �е忡 ��Ҵ��� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // ���� Y�� �ӵ��� �ʱ�ȭ �� ���� �� �߰�
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
