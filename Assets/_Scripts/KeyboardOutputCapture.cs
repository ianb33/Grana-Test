using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyboardOutputCapture : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TMP;
    public void AppendText(string letter)
    {
        TMP.text += letter;
    }

    public void DeleteCharacter()
    {
        TMP.text = TMP.text.Remove(TMP.text.Length - 1);
    }
}
