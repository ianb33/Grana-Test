using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmitManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(() => SubmitPressedCallback());
    }

    public void SubmitPressedCallback()
    {
        gameManager.submitWord(inputText.text);
    }
    
    public IEnumerator FadeOutCR(Color color)
    {
        float duration = .5f; //.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            inputText.GetComponent<TextMeshProUGUI>().color = color;
            inputText.GetComponent<TextMeshProUGUI>().alpha = alpha;
            color.a -= 0.1f;
            currentTime += Time.deltaTime;
            yield return null;
        }
        inputText.text = "";
        inputText.color = new Color(0, 0, 0.2078432f, 1);
        yield break;
    }

}
