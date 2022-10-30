using UnityEngine;

public class PlayerGold : MonoBehaviour
{
	[SerializeField]
	private	int	currentGold = 100;

	public int CurrentGold
	{
		set => currentGold = Mathf.Max(0, value);
		get => currentGold;
	}
}


/*
 * File : PlayerGold.cs
 * Desc
 *	: 플레이어의 소지 골드 정보
 *	
 */