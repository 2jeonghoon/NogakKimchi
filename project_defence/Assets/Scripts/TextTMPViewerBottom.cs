using UnityEngine;
using TMPro;

public class TextTMPViewerBottom : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; // Text - TextMeshPro UI [플레이어의 골드]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount; // Text - TextMeshPro UI [현재 적 숫자 / 최대 적 숫자]

    [SerializeField]
    private PlayerGold playerGold;     // 플레이어의 골드 정보
    [SerializeField]
    private EnemySpawner enemySpawner;   // 적 정보

    private void Update()
    {
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;
    }
}


/*
 * File : TextTMPViewer.cs
 * Desc
 *	: Text-TextMeshPro UI로 표현되는 여러 정보 업데이트
 *	
 */