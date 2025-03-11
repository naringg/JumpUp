using UnityEngine;
using UnityEngine.SceneManagement; // 씬 변경을 위해 필요

public class Reset : MonoBehaviour
{
    public float resetHeight = -10f; // 플레이어가 떨어지는 기준 높이

    private void Update()
    {
        if (transform.position.y < resetHeight)
        {
            LoadJumpMapScene();
        }
    }

    private void LoadJumpMapScene()
    {
        SceneManager.LoadScene("JumpMapScene"); // JumpMapScene 씬 로드
    }
}
