using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, 
                                    IPointerEnterHandler, 
                                    IPointerExitHandler
{
    private GameObject tooltip;

    void Start() {
        tooltip = transform.GetChild(2).gameObject; // tooltip object는 버튼에 idx num 2번에 위치하게 세팅
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
 *	: 타워 정보를 보여주는 툴팁 출력
 *
 */