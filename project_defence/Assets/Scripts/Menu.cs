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
    [SerializeField]
    GameObject introBook;

    //[SerializeField]
    //Button SettingButton;
    // Start is called before the first frame update
    void Awake()
    {
        // 게임이 시작되면 타이틀 영상이 끝날 때까지 버튼 비활성화
        gameStartButton.interactable = false;
        gameQuitButton.interactable = false;



        StartCoroutine("ButtonOn");
    }

    // 5 + 여유시간(0.5초)뒤 버튼 활성화 
    private IEnumerator ButtonOn()
    {
        yield return new WaitForSeconds(3.5f);
        gameStartButton.interactable = true;
        gameQuitButton.interactable = true;
    }


    private void IntroBook()
    {
        introBook.SetActive(true);
    }

    public void StartButton()
    {
        // 버튼 비활성화 후 IntroBook 실행
        gameStartButton.interactable = false;
        gameQuitButton.interactable = false;

        IntroBook();
    }
}
