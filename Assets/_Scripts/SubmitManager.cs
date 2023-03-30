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
        inputText.text = "";
    }

}
