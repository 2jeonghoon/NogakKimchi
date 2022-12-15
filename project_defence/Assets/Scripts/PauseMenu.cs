using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI; // 일시 정지 UI 패널
    [SerializeField] private GameObject sound_BaseUI; // 일시 정지 UI 패널

    public bool isPause;

    private void Start()
    {
        isPause = false;
    }

    public void CallMenu()
    {
        isPause = true;
        go_BaseUI.SetActive(true);
        Time.timeScale = 0f; // 시간의 흐름 설정. 0배속. 즉 시간을 멈춤.
    }

    public void CallSound()
    {
        // soundsetting is On
        if (sound_BaseUI.activeSelf)
        {
            sound_BaseUI.SetActive(false);
        }
        else
        {
            sound_BaseUI.SetActive(true);
        }
        //isPause = true;
        //Time.timeScale = 0f; // 시간의 흐름 설정. 0배속. 즉 시간을 멈춤.
    }

    public void CloseMenu()
    {
        float tmp = Time.timeScale;
        isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = tmp; // 기존 속도로
    }
    public void CallReStart()
    {
        isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f; // 1배속 (정상 속도)
        SceneManager.LoadScene("Ingame");

    }
    public void ClickExit()
    {
        Debug.Log("게임 종료");
        SceneManager.LoadScene("MainMenu");
        //Application.Quit();  // 게임 종료 (에디터 상 실행이기 때문에 종료 눌러도 변화 X)
    }
}
