using UnityEngine;
using TMPro;

public class TextTMPViewerBottom : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; // Text - TextMeshPro UI [?�레?�어??골드]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount; // Text - TextMeshPro UI [?�재 ???�자 / 최�? ???�자]

    [SerializeField]
    private PlayerGold      playerGold;     // ?�레?�어??골드 ?�보
    [SerializeField]
    private EnemySpawner    enemySpawner;   // ???�보

    private void Update()
    {
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;
    }
}


/*
 * File : TextTMPViewer.cs
 * Desc
 *	: Text-TextMeshPro UI�??�현?�는 ?�러 ?�보 ?�데?�트
 *	
 */