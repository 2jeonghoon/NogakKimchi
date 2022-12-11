using UnityEngine;
using TMPro;

public class TextTMPViewerBottom : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; // Text - TextMeshPro UI [?ë ?´ì´??ê³¨ë]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount; // Text - TextMeshPro UI [?ì¬ ???«ì / ìµë? ???«ì]

    [SerializeField]
    private PlayerGold      playerGold;     // ?ë ?´ì´??ê³¨ë ?ë³´
    [SerializeField]
    private EnemySpawner    enemySpawner;   // ???ë³´

    private void Update()
    {
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;
    }
}


/*
 * File : TextTMPViewer.cs
 * Desc
 *	: Text-TextMeshPro UIë¡??í?ë ?¬ë¬ ?ë³´ ?ë°?´í¸
 *	
 */