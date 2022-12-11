using UnityEngine;
using TMPro;


public class TextTMPViewerTop : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;   // Text - TextMeshPro UI [?åÎ†à?¥Ïñ¥??Ï≤¥Î†•]
    [SerializeField]
    private TextMeshProUGUI textWave;       // Text - TextMeshPro UI [?ÑÏû¨ ?®Ïù¥Î∏?/ Ï¥??®Ïù¥Î∏?

    [SerializeField]
    private PlayerHP        playerHP;       // ?åÎ†à?¥Ïñ¥??Ï≤¥Î†• ?ïÎ≥¥
    [SerializeField]
    private WaveSystem      waveSystem;     // ?®Ïù¥Î∏??ïÎ≥¥

    void Update()
    {
        textPlayerHP.text   = playerHP.CurrentHP + "/" + playerHP.MaxHP;
        textWave.text       = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
}
