using UnityEngine;
using TMPro;


public class TextTMPViewerTop : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;   // Text - TextMeshPro UI [플레이어의 체력]
    [SerializeField]
    private TextMeshProUGUI textWave;       // Text - TextMeshPro UI [현재 웨이브 / 총 웨이브]

    [SerializeField]
    private PlayerHP playerHP;       // 플레이어의 체력 정보
    [SerializeField]
    private WaveSystem waveSystem;     // 웨이브 정보

    void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;
        textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
}
