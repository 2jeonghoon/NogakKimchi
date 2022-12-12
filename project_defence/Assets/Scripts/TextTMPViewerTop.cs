using UnityEngine;
using TMPro;


public class TextTMPViewerTop : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;   // Text - TextMeshPro UI [?�레?�어??체력]
    [SerializeField]
    private TextMeshProUGUI textWave;       // Text - TextMeshPro UI [?�재 ?�이�?/ �??�이�?

    [SerializeField]
    private PlayerHP        playerHP;       // ?�레?�어??체력 ?�보
    [SerializeField]
    private WaveSystem      waveSystem;     // ?�이�??�보

    void Update()
    {
        textPlayerHP.text   = playerHP.CurrentHP + "/" + playerHP.MaxHP;
        textWave.text       = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
}
