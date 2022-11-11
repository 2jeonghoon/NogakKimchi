using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Button gameStartButton;
    [SerializeField]
    Button gameQuitButton;

    //[SerializeField]
    //Button SettingButton;
    // Start is called before the first frame update
    void Awake()
    {
        // ������ ���۵Ǹ� Ÿ��Ʋ ������ ���� ������ ��ư ��Ȱ��ȭ
        gameStartButton.interactable = false;
        gameQuitButton.interactable = false;


        StartCoroutine("ButtonOn");
    }

    // 5 + �����ð�(0.5��)�� ��ư Ȱ��ȭ 
    private IEnumerator ButtonOn()
    {
        yield return new WaitForSeconds(3.5f);
        gameStartButton.interactable = true;
        gameQuitButton.interactable = true;
        gameStartButton.gameObject.SetActive(true);
        gameQuitButton.gameObject.SetActive(true);
    }


    public void StartButton()
    {
        // ��ư ��Ȱ��ȭ �� IntroBook ����
        gameStartButton.interactable = false;
        gameQuitButton.interactable = false;

        SceneManager.LoadScene("Book");
    }
}
