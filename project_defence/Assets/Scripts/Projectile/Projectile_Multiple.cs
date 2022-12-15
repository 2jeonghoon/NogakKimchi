using UnityEngine;
using System.Collections;
using Unity.Profiling;

public class Projectile_Multiple : Projectile
{
    private Vector3 direction;

    public void Setup(Vector3 targetPos, float damage)
	{
        SoundManager.instance.SFXPlay("ShotGun", clip);
        movement2D	= GetComponent<Movement2D>();
		this.damage	= damage;						// 타워의 공격력
        this.direction = (targetPos - transform.position).normalized;
        this.pool_idx = 1;
        gameObject.SetActive(true);					// ObjectPool을 사용하면서 SetActive(true)가 필요해짐
        StartCoroutine("Destroy_Projectile");
    }

    public void Setup(Vector3 targetPos, float damage, int flags)
    {
        SoundManager.instance.SFXPlay("ShotGun", clip);
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // 타워의 공격력
        this.direction = (targetPos - transform.position);
        if (flags == 1)
        {
            direction.y -= 1;
            direction = direction.normalized;
        }
        else
        {
            direction.y += 1;
            direction = direction.normalized;
        };
        this.pool_idx = 1;
        gameObject.SetActive(true);
        StartCoroutine("Destroy_Projectile");
    }

    public void Setup(float damage, int flags)
    {
        SoundManager.instance.SFXPlay("ShotGun", clip);
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;           				// 타워의 공격력
        this.direction = new Vector3(0,0,0);
        if (flags == 0)
        {
            direction.x += 1;
            direction = direction.normalized;
        }
        else if (flags == 1)
        {
            direction.x -= 1;
            direction = direction.normalized;
        }
        else if (flags == 2)
        {
            direction.y += 1;
            direction = direction.normalized;
        }
        else if (flags == 3)
        {
            direction.y -= 1;
            direction = direction.normalized;
        }
        else if (flags == 4)
        {
            direction.x += 1;
            direction.y += 1;
            direction = direction.normalized;
        }
        else if (flags == 5)
        {
            direction.x += 1;
            direction.y -= 1;
            direction = direction.normalized;
        }
        else if (flags == 6)
        {
            direction.x -= 1;
            direction.y += 1;
            direction = direction.normalized;
        }
        else if (flags == 7)
        {
            direction.x -= 1;
            direction.y -= 1;
            direction = direction.normalized;
        }

        this.pool_idx = 1;
        gameObject.SetActive(true);
        StartCoroutine("Destroy_Projectile");
    }

    private void Start() {
        StartCoroutine("Destroy_Projectile");
    }

	private void Update()
	{
        // 발사체를 target 위치로 이동
        movement2D.MoveTo(direction);
	}

    // 발사체가 생성된 후 2초가 지나도 삭제가 안된다면 삭제
    private IEnumerator Destroy_Projectile() {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("projectile idx:" + pool_idx);
        ProjectileReturn(pool_idx);
    }

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if ( !collision.CompareTag("Enemy") )	return;         // 적이 아닌 대상과 부딪히면

        StopCoroutine("Destory_Projectile");
        collision.GetComponent<EnemyHP>().TakeDamage(damage, false);	// 적 체력을 damage만큼 감소
        Debug.Log("OnTrigerPR");
        ProjectileReturn(pool_idx);                                 
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