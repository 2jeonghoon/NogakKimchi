using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
	public void OnAttackRange(Vector3 position, float range)
	{
		gameObject.SetActive(true);

		// 공격 범위 크기
		float diameter = range * 2.0f;
		transform.localScale = Vector3.one * diameter;
		// 공격 범위 위치
		transform.position = position;
	}

	public void OffAttackRange()
	{
		gameObject.SetActive(false);
	}
}


/*
 * File : TowerAttackRange.cs
 * Desc
 *	: 타워의 공격 범위 표시
 *	
 * Functions
 *	: OnAttackRange() - 타워 위치, 타워 공격 범위를 바탕으로 타워 공격 범위 활성화
 *	: OffAttackRange() - 타워 공격 범위 비활성화
 *
 */