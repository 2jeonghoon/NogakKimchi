using UnityEngine;
using System.Collections;
using Unity.Profiling;

public class Projectile_Multiple : Projectile
{
    private Vector3 direction;

    public void Setup(Vector3 targetPos, float damage)
	{
        // 발사 ?�운???�생
        SoundManager.instance.SFXPlay("ShotGun", clip);
        movement2D	= GetComponent<Movement2D>();
		this.damage	= damage;						// ?�?�의 공격??
        this.direction = (targetPos - transform.position).normalized;
        this.pool_idx = 1;
        gameObject.SetActive(true);					// ObjectPool???�용?�면??SetActive(true)가 ?�요?�짐
    }

    public void Setup(Vector3 targetPos, float damage, int y)
    {
        movement2D = GetComponent<Movement2D>();
        targetPos.y += y;
        this.damage = damage;           				// ?�?�의 공격??
        this.direction = (targetPos - transform.position);
        Debug.Log(direction);
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
        // 발사체�? target ?�치�??�동
        movement2D.MoveTo(direction);
	}

    // 발사체�? ?�성????2초�? 지?�도 ??��가 ?�된?�면 ??��
    private IEnumerator Destroy_Projectile() {
        yield return new WaitForSeconds(2f);

        // Projectile??Pool?�서 가?�온지 2초�? 지?�면 Destroy ?�??반납
        ProjectileReturn(pool_idx);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( !collision.CompareTag("Enemy") )	return;         // ?�이 ?�닌 ?�?�과 부?�히�?

        StopCoroutine("Destory_Projectile");
        collision.GetComponent<EnemyHP>().TakeDamage(damage);	// ??체력??damage만큼 감소
        ProjectileReturn(pool_idx);                                     // 발사�??�브?�트 ??�� ?�??Pool??반납
    }
}


/*
 * File : Projectile_Multiple.cs
 * Desc
 *	: ?�?��? 발사?�는 기본 발사체에 부�? Projectile�??�르�??�발???�닌 ?�발 ?�격
 *	
 * Functions
 *	: Update() - Setup?�서 매개변?�로 targetPos�?받아 계산??방향 벡터 방향?�로 발사체�? ?�동?�켜�?
 *  : Destory_Projectile() - 발사체�? ?�성????5�??�에 발사체�? ??��?�켜주는 코루??
 *	: OnTriggerEnter2D() - ?�겟으�??�정???�과 부?�혔?????�에�??��?지�?주고 ?�브?�트 ??��
 *	
 */