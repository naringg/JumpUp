using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �ʿ�

public class Reset : MonoBehaviour
{
    public float resetHeight = -10f; // �÷��̾ �������� ���� ����

    private void Update()
    {
        if (transform.position.y < resetHeight)
        {
            LoadJumpMapScene();
        }
    }

    private void LoadJumpMapScene()
    {
        SceneManager.LoadScene("JumpMapScene"); // JumpMapScene �� �ε�
    }
}
