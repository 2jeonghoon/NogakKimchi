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
        "���� ���ظ� ������ ���ں��� �����մϴ�.\n �������� ���� Ÿ���� �Ǵ� ���ĸ��� ��Ÿ Ÿ������ ������ �� �ֽ��ϴ�. ",

        "������ �� �������� ������ �����մϴ�.\n ���ϸ��� Ÿ���� �Ǵ� ����� ���� Ÿ������ ������ �� �ֽ��ϴ�.",

        "���� ���ظ� ������ ���̽�ũ���� �����մϴ�.\n�� Ÿ���� 2ĭ�� Ÿ���� �ʿ�� �մϴ�.\n������ �� Ÿ���� �Ǵ� ������ �� Ÿ������ ������ �� �ֽ��ϴ�.",

        "���� ���ظ� ������ �޷� �Ҵٸ� �����մϴ�. \n�� Ÿ���� 2ĭ�� Ÿ���� �ʿ�� �մϴ�.\n'ü���� Ÿ���� �Ǵ� ������ ���̵� Ÿ������ ������ �� �ֽ��ϴ�.",

        "��Ÿ����� ���� ���ظ� ������ ������ �����մϴ�.\n���ٳ��� ���� Ÿ���� �Ǵ� ��Ŀ�� ���� Ÿ������ ������ �� �ֽ��ϴ�.",

        "�ֺ��� �ִ� Ÿ������ ���� �ӵ��� ������ŵ�ϴ�.",

        "�̵��ӵ��� ���ҽ�Ű�� �������� �����մϴ�.\n�����ڶ� Ÿ���� �Ǵ� ������ ���� Ÿ������ ������ �� �ֽ��ϴ�.",

        "�ټ��� ������ �����ϴ� ������ �ܹ������� �����մϴ�.\n����� ���� ������ Ÿ���� �Ǵ� ������Ƽ Ÿ������ ������ �� �ֽ��ϴ�.",
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
 *	: ?�???�보�?보여주는 ?�팁 출력
 *
 */