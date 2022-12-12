using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
<<<<<<< HEAD
=======
using UnityEngine.UI;
using TMPro;
>>>>>>> origin/Jeonghoon

public class ToolTip : MonoBehaviour, 
                                    IPointerEnterHandler, 
                                    IPointerExitHandler
{
<<<<<<< HEAD
    private GameObject tooltip;

    void Start() {
        tooltip = transform.GetChild(2).gameObject; // tooltip objectëŠ” ë²„íŠ¼ì— idx num 2ë²ˆì— ìœ„ì¹˜í•˜ê²Œ ì„¸íŒ…
=======
    enum Tower { COCOBALL, JELLY, ICECREAM, MELONSODA, MILK, COFFEE, STRAWBERRY, SMOOTHIE };

    [SerializeField]
    private GameObject tooltip;
    [SerializeField]
    private TextMeshProUGUI textTooltip;
    [SerializeField]
    private Tower type;

    private string[] tooltipstr =
    {
        "´ÜÀÏ ÇÇÇØ¸¦ ÀÔÈ÷´Â ÃÊÄÚº¼À» Åõ»çÇÕ´Ï´Ù.\n ¡®¿À·¹¿À ¿ÀÁî Å¸¿ö¡¯ ¶Ç´Â ¡®ÈÄ¸£Ã÷ ½ºÅ¸ Å¸¿ö¡¯·Î ÀüÁ÷ÇÒ ¼ö ÀÖ½À´Ï´Ù. ",

        "Á©¸®¸¦ ¼¼ ¹æÇâÀ¸·Î ÀÏÁ¦È÷ Åõ»çÇÕ´Ï´Ù.\n ¡®ÇÏ¸®º¸ Å¸¿ö¡¯ ¶Ç´Â ¡®°ï¾à Á©¸® Å¸¿ö¡¯·Î ÀüÁ÷ÇÒ ¼ö ÀÖ½À´Ï´Ù.",

        "±¤¿ª ÇÇÇØ¸¦ ÀÔÈ÷´Â ¾ÆÀÌ½ºÅ©¸²À» Åõ»çÇÕ´Ï´Ù.\nÀÌ Å¸¿ö´Â 2Ä­ÀÇ Å¸ÀÏÀ» ÇÊ¿ä·Î ÇÕ´Ï´Ù.\n¡®´õºí ÄÜ Å¸¿ö¡¯ ¶Ç´Â ¡®ÅäÇÎ ÄÜ Å¸¿ö¡¯·Î ÀüÁ÷ÇÒ ¼ö ÀÖ½À´Ï´Ù.",

        "Áö¼Ó ÇÇÇØ¸¦ ÀÔÈ÷´Â ¸Þ·Ð ¼Ò´Ù¸¦ Åõ»çÇÕ´Ï´Ù. \nÀÌ Å¸¿ö´Â 2Ä­ÀÇ Å¸ÀÏÀ» ÇÊ¿ä·Î ÇÕ´Ï´Ù.\n'Ã¼¸®ÄÛ Å¸¿ö¡¯ ¶Ç´Â ¡®·¹¸ó ¿¡ÀÌµå Å¸¿ö¡¯·Î ÀüÁ÷ÇÒ ¼ö ÀÖ½À´Ï´Ù.",

        "Àå°Å¸®¿¡¼­ ±¤¿ª ÇÇÇØ¸¦ ÀÔÈ÷´Â ¿ìÀ¯¸¦ Æ÷°ÝÇÕ´Ï´Ù.\n¡®¹Ù³ª³ª ¿ìÀ¯ Å¸¿ö¡¯ ¶Ç´Â ¡®Ä¿ÇÇ ¿ìÀ¯ Å¸¿ö¡¯·Î ÀüÁ÷ÇÒ ¼ö ÀÖ½À´Ï´Ù.",

        "ÁÖº¯¿¡ ÀÖ´Â Å¸¿öµéÀÇ Åõ»ç ¼Óµµ¸¦ Áõ°¡½ÃÅµ´Ï´Ù.",

        "ÀÌµ¿¼Óµµ¸¦ °¨¼Ò½ÃÅ°´Â µþ±âÀëÀ» Åõ»çÇÕ´Ï´Ù.\n¡®´©ÅÚ¶ó Å¸¿ö¡¯ ¶Ç´Â ¡®¶¥Äá ¹öÅÍ Å¸¿ö¡¯·Î ÀüÁ÷ÇÒ ¼ö ÀÖ½À´Ï´Ù.",

        "´Ù¼öÀÇ »§µéÀ» °üÅëÇÏ´Â ½º¹«µð¸¦ ´Ü¹æÇâÀ¸·Î Åõ»çÇÕ´Ï´Ù.\n¡®ºí·ç º£¸® ½º¹«µð Å¸¿ö¡¯ ¶Ç´Â ¡®¹öºíÆ¼ Å¸¿ö¡¯·Î ÀüÁ÷ÇÒ ¼ö ÀÖ½À´Ï´Ù.",
     };

    public void printTooltip()
    {
        textTooltip.text = tooltipstr[(int)type];
>>>>>>> origin/Jeonghoon
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
<<<<<<< HEAD
=======
        printTooltip();
>>>>>>> origin/Jeonghoon
        tooltip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }

}

/*
 * File : ToolTip.cs
 * Desc
<<<<<<< HEAD
 *	: íƒ€ì›Œ ì •ë³´ë¥¼ ë³´ì—¬ì£¼ëŠ” íˆ´íŒ ì¶œë ¥
=======
 *	: ?€???•ë³´ë¥?ë³´ì—¬ì£¼ëŠ” ?´íŒ ì¶œë ¥
>>>>>>> origin/Jeonghoon
 *
 */