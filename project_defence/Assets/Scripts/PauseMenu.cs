using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI; // �Ͻ� ���� UI �г�

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

    public void CloseMenu()
    {
        isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f; // 1��� (���� �ӵ�)
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
