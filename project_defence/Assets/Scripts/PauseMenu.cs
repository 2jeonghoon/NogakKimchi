using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI; // �Ͻ� ���� UI �г�
    [SerializeField] private GameObject sound_BaseUI; // �Ͻ� ���� UI �г�

    public bool isPause;

    private void Start()
    {
        isPause = false;
    }

    public void CallMenu()
    {
        isPause = true;
        go_BaseUI.SetActive(true);
        Time.timeScale = 0f; // �ð��� �帧 ����. 0���. �� �ð��� ����.
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
        //Time.timeScale = 0f; // �ð��� �帧 ����. 0���. �� �ð��� ����.
    }

    public void CloseMenu()
    {
        float tmp = Time.timeScale;
        isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = tmp; // ���� �ӵ���
    }
    public void CallReStart()
    {
        isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f; // 1��� (���� �ӵ�)
        SceneManager.LoadScene("Ingame");

    }
    public void ClickExit()
    {
        Debug.Log("���� ����");
        SceneManager.LoadScene("MainMenu");
        //Application.Quit();  // ���� ���� (������ �� �����̱� ������ ���� ������ ��ȭ X)
    }
}
