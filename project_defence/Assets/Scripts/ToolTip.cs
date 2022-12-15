using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ToolTip : MonoBehaviour, 
                                    IPointerEnterHandler, 
                                    IPointerExitHandler
{
    enum Tower { COCOBALL, JELLY, ICECREAM, MELONSODA, MILK, COFFEE, STRAWBERRY, SMOOTHIE };

    [SerializeField]
    private GameObject tooltip;
    [SerializeField]
    private TextMeshProUGUI textTooltip;
    [SerializeField]
    private Tower type;

    private string[] tooltipstr =
    {
        "단일 피해를 입히는 초코볼을 투사합니다.\n ‘오레오 오즈 타워’ 또는 ‘후르츠 스타 타워’로 전직할 수 있습니다. ",

        "젤리를 세 방향으로 일제히 투사합니다.\n ‘하리보 타워’ 또는 ‘곤약 젤리 타워’로 전직할 수 있습니다.",

        "광역 피해를 입히는 아이스크림을 투사합니다.\n이 타워는 2칸의 타일을 필요로 합니다.\n‘더블 콘 타워’ 또는 ‘토핑 콘 타워’로 전직할 수 있습니다.",

        "지속 피해를 입히는 메론 소다를 투사합니다. \n이 타워는 2칸의 타일을 필요로 합니다.\n'체리콕 타워’ 또는 ‘레몬 에이드 타워’로 전직할 수 있습니다.",

        "장거리에서 광역 피해를 입히는 우유를 포격합니다.\n‘바나나 우유 타워’ 또는 ‘커피 우유 타워’로 전직할 수 있습니다.",

        "주변에 있는 타워들의 투사 속도를 증가시킵니다.",

        "이동속도를 감소시키는 딸기잼을 투사합니다.\n‘누텔라 타워’ 또는 ‘땅콩 버터 타워’로 전직할 수 있습니다.",

        "다수의 빵들을 관통하는 스무디를 단방향으로 투사합니다.\n‘블루 베리 스무디 타워’ 또는 ‘버블티 타워’로 전직할 수 있습니다.",
     };

    public void printTooltip()
    {
        textTooltip.text = tooltipstr[(int)type];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        printTooltip();
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
 *	: ????뺣낫瑜?蹂댁뿬二쇰뒗 ?댄똻 異쒕젰
 *
 */