using UnityEngine;
using TMPro;

public class TextTMPViewerBottom : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; // Text - TextMeshPro UI [?Œë ˆ?´ì–´??ê³¨ë“œ]
    [SerializeField]
    private TextMeshProUGUI textEnemyCount; // Text - TextMeshPro UI [?„ì¬ ???«ì / ìµœë? ???«ì]

    [SerializeField]
    private PlayerGold      playerGold;     // ?Œë ˆ?´ì–´??ê³¨ë“œ ?•ë³´
    [SerializeField]
    private EnemySpawner    enemySpawner;   // ???•ë³´

    private void Update()
    {
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;
    }
}


/*
 * File : TextTMPViewer.cs
 * Desc
 *	: Text-TextMeshPro UIë¡??œí˜„?˜ëŠ” ?¬ëŸ¬ ?•ë³´ ?…ë°?´íŠ¸
 *	
 */