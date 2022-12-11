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
        tooltip = transform.GetChild(2).gameObject; // tooltip object??버튼??idx num 2번에 ?�치?�게 ?�팅
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
 *	: ?�???�보�?보여주는 ?�팁 출력
 *
 */