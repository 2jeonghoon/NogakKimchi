using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI; // 일시 정지 UI 패널
    [SerializeField] private GameObject sound_BaseUI; // 사운드 UI 패널

    public bool isPause;
    public float gamespeed;

    private void Start()
    {
        isPause = false;
    }

    public void CallMenu()
    {
        isPause = true;
        go_BaseUI.SetActive(true);
        gamespeed = Time.timeScale; // 기존 속도 저장
        Time.timeScale = 0f; // 시간의 흐름 설정. 0배속. 즉 시간을 멈춤.
    }

    public void CallSound()
    {
        // 사운드세팅이 켜져있으면
        if (sound_BaseUI.activeSelf)
        {
            sound_BaseUI.SetActive(false);
        }
        else
        {
            sound_BaseUI.SetActive(true);
        }
        
    }
    
    public void CloseMenu()
    {
        isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = gamespeed; // 저장해둔 게임스피드로
    }
    public void CallReStart()
    {
        isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = gamespeed; // 저장해둔 게임스피드로
        SceneManager.LoadScene("Ingame");

    }
    public void ClickExit()
    {
        Debug.Log("게임 종료");
        SceneManager.LoadScene("MainMenu");
        //Application.Quit();  // 게임 종료 (에디터 상 실행이기 때문에 종료 눌러도 변화 X)
    }
}
