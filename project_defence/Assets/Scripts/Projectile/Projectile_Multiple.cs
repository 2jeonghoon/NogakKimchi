using UnityEngine;
using System.Collections;
using Unity.Profiling;

public class Projectile_Multiple : MonoBehaviour
{
	private	Movement2D	movement2D;
	//private	int			damage;
	private	float		damage;
    private Vector3 direction;
    private Vector3 direction2;

    public void Setup(Vector3 targetPos, float damage)
	{
		movement2D	= GetComponent<Movement2D>();
		this.damage	= damage;						// 타워의 공격력
        this.direction = (targetPos - transform.position).normalized;
        direction2 = direction;
	}

    public void Setup(Vector3 targetPos, float damage, int y)
    {
        movement2D = GetComponent<Movement2D>();
        targetPos.y += y;
        this.damage = damage;           				// 타워의 공격력
        this.direction = (targetPos - transform.position);
        direction2 = direction;
        if (y == 1)
        {
            direction = (direction * Mathf.Cos(45)).normalized;
        }
        else
        {
            direction = (direction * Mathf.Sin(45)).normalized;
        }
    }

    private void Start() {
        StartCoroutine("Destroy_Projectile");
    }

	private void Update()
	{
        // 발사체를 target 위치로 이동
        movement2D.MoveTo(direction);
	}

    // 발사체가 생성된 후 5초가 지나도 삭제가 안된다면 삭제
    private IEnumerator Destroy_Projectile() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( !collision.CompareTag("Enemy") )	return;			// 적이 아닌 대상과 부딪히면
		collision.GetComponent<EnemyHP>().TakeDamage(damage);	// 적 체력을 damage만큼 감소
        StopCoroutine("Destory_Projectile");
		Destroy(gameObject);									// 발사체 오브젝트 삭제
	}
}


/*
 * File : Projectile_Multiple.cs
 * Desc
 *	: 타워가 발사하는 기본 발사체에 부착, Projectile과 다르게 단발이 아닌 다발 사격
 *	
 * Functions
 *	: Update() - Setup에서 매개변수로 targetPos를 받아 계산한 방향 벡터 방향으로 발사체를 이동시켜줌
 *  : Destory_Projectile() - 발사체가 생성된 후 5초 후에 발사체를 삭제시켜주는 코루틴
 *	: OnTriggerEnter2D() - 타겟으로 설정된 적과 부딪혔을 때 적에게 데미지를 주고 오브젝트 삭제
 *	
 */