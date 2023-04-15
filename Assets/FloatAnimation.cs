using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] private float YAmp;
    [SerializeField] private float XAmp;
    [SerializeField] private float speed;
    
    private float y;
    private float x;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        y = rectTransform.position.y;
        x = rectTransform.position.x;
        StartCoroutine(FloatAnimator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator FloatAnimator()
    {
        while (true)
        {
            time += speed * 0.1f;
            x += XAmp * Mathf.Sin(time);
            y += YAmp* Mathf.Sin(time);;
            rectTransform.position = new Vector3(x, y, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
