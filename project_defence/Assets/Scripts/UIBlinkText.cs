using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBlinkText : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI text;

    // fade 횟수 지정
    [SerializeField]
    int fadeCount;
    void Start()
    {
       text = gameObject.GetComponent<TextMeshProUGUI>();
       fadeCount = 2;

        StartCoroutine(FadeInCoroutine());
    }

    // Update is called once per frame
    IEnumerator FadeInCoroutine()
    {
        float fadeTime = 0;
        while(fadeTime < 1.0f)
        {
            fadeTime += 0.01f;
            yield return new WaitForSeconds(0.01f);
            text.color = new Color(255, 255, 255, fadeTime);
        }
        
        StartCoroutine(FadeOutCoroutine());
    }
    IEnumerator FadeOutCoroutine()
    {
        float fadeTime = 1f;
        while (fadeTime > 0)
        {
            fadeTime -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            text.color = new Color(255, 255, 255, fadeTime);
        }

        // fade 횟수만큼 fade 반복 후 gameObject 비활성화
        if(fadeCount > 0)
        {
            fadeCount--;
            StartCoroutine(FadeInCoroutine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
/*
 * File : UIBlinkText
 * Desc
 *	: Text 깜빡임 효과
 *	
 * Functions
 *	FadeInCoroutine : FadeIn 후 다음 코루틴으로 FadeOut코루틴 호출
 *	FadeOutCoroutine : FadeOut 후 fadeCount만큼 다음 코루틴으로 FadeIn 코루틴 호출 후 gameObject 비활성화
 */
