using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameAlert : MonoBehaviour
{
    [SerializeField] public GameObject thisAlert;
    [SerializeField] public float duration1;
    [SerializeField] public float duration2;
    [SerializeField] public float inBetween;
    
    // Start is called before the first frame update
    void Start()
    {
        Color currentColor = thisAlert.GetComponent<TextMeshProUGUI>().color;
        thisAlert.GetComponent<TextMeshProUGUI>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);

        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator AnimateFade(float start, float end, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(start, end, currentTime / duration);
            thisAlert.GetComponent<TextMeshProUGUI>().alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    private IEnumerator AnimateMove(Vector2 targetPosition, float duration)
    {
        float currentTime = 0f;
        Vector2 startPosition = thisAlert.GetComponent<Transform>().localPosition;
        
        while (currentTime < duration)
        {
            float x = Mathf.SmoothStep(startPosition.x, targetPosition.x, currentTime / duration);
            float y = Mathf.SmoothStep(startPosition.y, targetPosition.y, currentTime / duration);
            currentTime += Time.deltaTime;
            thisAlert.GetComponent<Transform>().localPosition = new Vector3(x, y, 1);
            
            yield return null;
        }
        yield break;
    }

    private IEnumerator FadeInAndOut()
    {
        StartCoroutine(AnimateMove(new Vector2(thisAlert.GetComponent<Transform>().localPosition.x,thisAlert.GetComponent<Transform>().localPosition.y + 50), duration1));
        yield return AnimateFade(0f, 1f, duration1);
        yield return new WaitForSeconds(inBetween);
        StartCoroutine(AnimateMove(new Vector2(thisAlert.GetComponent<Transform>().localPosition.x,thisAlert.GetComponent<Transform>().localPosition.y - 50), duration2));
        yield return AnimateFade(1f, 0f, duration2);
        
        Destroy(thisAlert);
    }
}
