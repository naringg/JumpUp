using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; // TextMeshPro 사용

public class Condition : MonoBehaviour
{
    public float maxValue;
    public float startValue;
    public float curValue;
    public float passiveValue;
    public Image uiBar;
    public TextMeshProUGUI gameOverText; // TextMeshPro 사용

    private void Start()
    {
        curValue = startValue;
        UpdateUI();

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); // 시작 시 비활성화
        }
    }

    private void Update()
    {
        if (passiveValue != 0)
        {
            SetCurValue(curValue - passiveValue * Time.deltaTime);
        }
    }

    public void Add(float amount)
    {
        SetCurValue(curValue + amount);
    }

    public void Subtract(float amount)
    {
        SetCurValue(curValue - amount);
    }

    private void SetCurValue(float value)
    {
        curValue = Mathf.Clamp(value, 0, maxValue);
        UpdateUI();

        if (curValue <= 0)
        {
            TriggerGameOver(); // 체력이 0이 되면 게임 오버 실행
        }
    }

    private void UpdateUI()
    {
        if (uiBar != null)
        {
            uiBar.fillAmount = GetPercentage();
        }
    }

    public float GetPercentage()
    {
        return maxValue > 0 ? curValue / maxValue : 0;
    }

    private void TriggerGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true); // TextMeshPro 텍스트 활성화
        }

        StartCoroutine(RestartScene()); // 4초 뒤 씬 변경 실행
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("JumpMapScene");
    }
}
