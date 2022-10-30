using UnityEngine;
using TMPro;

public enum SystemType { Money = 0, Build }

public class SystemTextViewer : MonoBehaviour
{
	private TextMeshProUGUI	textSystem;
	private	TMPAlpha		tmpAlpha;

	private void Awake()
	{
		textSystem	= GetComponent<TextMeshProUGUI>();
		tmpAlpha	= GetComponent<TMPAlpha>();
	}

	public void PrintText(SystemType type)
	{
		switch ( type )
		{
			case SystemType.Money:
				textSystem.text = "System : Not enough money...";
				break;
			case SystemType.Build:
				textSystem.text = "System : Invalid build tower...";
				break;
		}

		tmpAlpha.FadeOut();
	}
}


/*
 * File : SystemTextViewer.cs
 * Desc
 *	: 시스템 메시지 출력
 *	  - 타워 건설/타워 업그레이드에 필요한 골드가 부족할 때
 *	  - 타워를 건설할 수 없는 위치에 타워 건설을 하려고 할 때
 *
 */