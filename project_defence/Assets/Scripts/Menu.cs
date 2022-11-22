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

    public void StartButton()
    {
        Time.timeScale = 1;
        // ��ư ��Ȱ��ȭ �� IntroBook ����
        gameStartButton.interactable = false;
        gameQuitButton.interactable = false;

        SceneManager.LoadScene("Book");
    }
}
