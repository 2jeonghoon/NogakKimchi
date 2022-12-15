using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Spear : Projectile
{
	private float range;
	private Vector3 direction;		// 가는 방향

	private Vector3 start_pos;	// 시작 위치
	private Vector3 cur_pos;        // 현재 위치

	public void Setup(Transform target, float damage, float range)
	{
		// 발사 사운드 재생
		SoundManager.instance.SFXPlay("Spear", clip);
		movement2D = GetComponent<Movement2D>();
		this.target = target;                       // 타워가 설정해준 target
		this.damage = damage;                       // 타워의 공격력
		this.range = range;                         // 타워 사거리

		direction = (target.position - transform.position).normalized;
		start_pos = this.transform.position;
		this.pool_idx = 5;
        gameObject.SetActive(true);                 // ObjectPool을 사용하면서 SetActive(true)가 필요해짐
    }

	private void Update()
	{
		cur_pos = this.transform.position;

		// 발사체를 target의 위치로 이동
		movement2D.MoveTo(direction);

		// 사거리를 벗어났으면 반납
		if (Vector3.Distance(start_pos, cur_pos) >= range)
        {
			ProjectileReturn(pool_idx);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("충돌");
		if (!collision.CompareTag("Enemy")) return;         // 적이 아닌 대상과 부딪히면
		collision.GetComponent<EnemyHP>().TakeDamage(damage, false);   // 적 체력을 damage만큼 감소
	}
}
