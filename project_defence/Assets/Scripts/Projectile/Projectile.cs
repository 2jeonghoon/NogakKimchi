﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	protected	Movement2D	movement2D;
	protected	Transform	target;
	protected float		damage;

	// 총알 발사 사운드
	public AudioClip clip;

	public virtual void Setup(Transform target, float damage)
	{
		// 발사 사운드 재생
		SoundManager.instance.SFXPlay("shot", clip);
		movement2D	= GetComponent<Movement2D>();
		this.target	= target;						// 타워가 설정해준 target
		this.damage	= damage;						// 타워의 공격력
	}

	private void Start()
	{
		StartCoroutine("Destroy_Projectile");
	}

	private void Update()
	{
		if ( target != null )	// target이 존재하면
		{
			// 발사체를 target의 위치로 이동
			Vector3 direction = (target.position-transform.position).normalized;
			movement2D.MoveTo(direction);
		}
	}
    private IEnumerator Destroy_Projectile()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( !collision.CompareTag("Enemy") )	return;			// 적이 아닌 대상과 부딪히면
		if ( collision.transform != target )	return;			// 현재 target인 적이 아닐 때

		collision.GetComponent<EnemyHP>().TakeDamage(damage);	// 적 체력을 damage만큼 감소
		Destroy(gameObject);									// 발사체 오브젝트 삭제
	}
}


/*
 * File : Projectile
 * .cs
 * Desc
 *	: 타워가 발사하는 기본 발사체에 부착
 *	
 * Functions
 *	: Update() - 타겟이 존재하면 타겟 방향으로 이동하고, 타겟이 존재하지 않으면 발사체 삭제
 *	: OnTriggerEnter2D() - 타겟으로 설정된 적과 부딪혔을 때 둘다 삭제
 *	
 */