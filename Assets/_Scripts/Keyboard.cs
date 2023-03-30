using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Xml.Serialization;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Keyboard : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Key keyPrefab;
    [SerializeField] private Key backspaceKeyPrefab;

    [Header(" Settings ")]
    [Range(0f, 1f)]
    [SerializeField] private float widthPercent;
    [Range(0f, 1f)]
    [SerializeField] private float heightPercent;
    [Range(0f, .5f)]
    [SerializeField] private float bottomOffset;

    [Header(" Keyboard Lines ")]
    [SerializeField] private KeyboardLine[] lines;

    [Header(" Key Settings ")]
    [SerializeField] private Color keyColor;
    [Range(0f, 1f)]
    [SerializeField] private float keyToLineRatio;
    [Range(0f, 1f)]
    [SerializeField] private float keyXSpacing;

    [SerializeField] private KeyboardOutputCapture KeyboardOutputCapture;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        CreateKeys();

        yield return null;

        UpdateRectTransform();

        /*rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);*/
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRectTransform();
        PlaceKeys();
    }

    private void UpdateRectTransform()
    {
        float width = widthPercent * UnityEngine.Screen.width;
        float height = heightPercent * UnityEngine.Screen.height;

        // Configuring size of the keyboard container
        rectTransform.sizeDelta = new Vector2(width, height);

        //Configure bottom offset
        Vector2 position;

        position.x = UnityEngine.Screen.width / 2;
        position.y = bottomOffset * UnityEngine.Screen.height + height / 2;

        rectTransform.position = position;
    }
    private void CreateKeys()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                char key = lines[i].keys[j];

                if (key == '.')
                {
                    // Its the backspace key
                    Key keyInstance = Instantiate(backspaceKeyPrefab, rectTransform);
                    keyInstance.SetKeyColor(keyColor);
                    keyInstance.GetButton().onClick.AddListener(() => BackspacePressedCallback());
                }
                else
                {
                    // It's a normal key
                    Key keyInstance = Instantiate(keyPrefab, rectTransform);
                    keyInstance.SetKey(key);
                    keyInstance.SetKeyColor(keyColor);
                    keyInstance.GetButton().onClick.AddListener(() => KeyPressedCallback(key));
                    keyInstance.SetKeyColor(keyColor);
                }
            }
        }
    }

    private void PlaceKeys()
    {
        int lineCount = lines.Length;

        float lineHeight = rectTransform.rect.height / lineCount;

        float keyWidth = lineHeight * keyToLineRatio;
        float xSpacing = keyXSpacing * lineHeight;

        int currentKeyIndex = 0;

        for (int i = 0; i < lineCount; i++)
        {
            bool containsBackspace = lines[i].keys.Contains(".");


            float halfKeyCount = (float)lines[i].keys.Length / 2;

            if (containsBackspace)
                halfKeyCount += .5f;

            float startX = -(keyWidth + xSpacing) * halfKeyCount + (keyWidth + xSpacing) / 2;

            float lineY = +rectTransform.rect.height / 2 - lineHeight / 2 - i * lineHeight;

            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                bool isBackspaceKey = lines[i].keys[j] == '.';

                float keyX = startX + j * (keyWidth + xSpacing);

                if (isBackspaceKey)
                    keyX += keyWidth - xSpacing;

                Vector2 keyPosition = new Vector2(keyX, lineY);

                RectTransform keyRectTransform = rectTransform.GetChild(currentKeyIndex).GetComponent<RectTransform>();
                keyRectTransform.localPosition = keyPosition;

                float thisKeyWidth = keyWidth;

                if (isBackspaceKey)
                    thisKeyWidth *= 2;

                keyRectTransform.sizeDelta = new Vector2(thisKeyWidth, keyWidth);

                currentKeyIndex++;
            }
        }
    }

    /*public void ColorUpdate()
    {
        GetComponent<Key>().SetKeyColor(keyColor);
    }*/

    private void BackspacePressedCallback()
    {
        Debug.Log("Backspace Pressed");

        if (KeyboardOutputCapture != null)
        {
            KeyboardOutputCapture.DeleteCharacter();
        }
    }

    private void KeyPressedCallback(char key)
    {
        Debug.Log($"Key Pressed : {key}");

        if (KeyboardOutputCapture != null)
        {
            KeyboardOutputCapture.AppendText(key.ToString().ToUpper());
        }
    }
}



[System.Serializable]
public struct KeyboardLine
{
    public string keys;
}
