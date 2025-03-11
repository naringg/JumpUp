using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; // TextMeshPro ���

public class Condition : MonoBehaviour
{
    public float maxValue;
    public float startValue;
    public float curValue;
    public float passiveValue;
    public Image uiBar;
    public TextMeshProUGUI gameOverText; // TextMeshPro ���

    private void Start()
    {
        curValue = startValue;
        UpdateUI();

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); // ���� �� ��Ȱ��ȭ
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
            TriggerGameOver(); // ü���� 0�� �Ǹ� ���� ���� ����
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
            gameOverText.gameObject.SetActive(true); // TextMeshPro �ؽ�Ʈ Ȱ��ȭ
        }

        StartCoroutine(RestartScene()); // 4�� �� �� ���� ����
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("JumpMapScene");
    }
}
