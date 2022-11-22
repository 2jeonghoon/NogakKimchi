using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBlinkText : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI text;

    // fade Ƚ�� ����
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

        // fade Ƚ����ŭ fade �ݺ� �� gameObject ��Ȱ��ȭ
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
 *	: Text ������ ȿ��
 *	
 * Functions
 *	FadeInCoroutine : FadeIn �� ���� �ڷ�ƾ���� FadeOut�ڷ�ƾ ȣ��
 *	FadeOutCoroutine : FadeOut �� fadeCount��ŭ ���� �ڷ�ƾ���� FadeIn �ڷ�ƾ ȣ�� �� gameObject ��Ȱ��ȭ
 */
