using UnityEngine;
using System.Collections;
using Unity.Profiling;

public class Projectile_Multiple : Projectile
{
    private Vector3 direction;

    public void Setup(Vector3 targetPos, float damage)
	{
        // ë°œì‚¬ ?¬ìš´???¬ìƒ
        SoundManager.instance.SFXPlay("ShotGun", clip);
        movement2D	= GetComponent<Movement2D>();
		this.damage	= damage;						// ?€?Œì˜ ê³µê²©??
        this.direction = (targetPos - transform.position).normalized;
        this.pool_idx = 1;
        gameObject.SetActive(true);					// ObjectPool???¬ìš©?˜ë©´??SetActive(true)ê°€ ?„ìš”?´ì§
    }

    public void Setup(Vector3 targetPos, float damage, int y)
    {
        movement2D = GetComponent<Movement2D>();
        targetPos.y += y;
        this.damage = damage;           				// ?€?Œì˜ ê³µê²©??
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
        // ë°œì‚¬ì²´ë? target ?„ì¹˜ë¡??´ë™
        movement2D.MoveTo(direction);
	}

    // ë°œì‚¬ì²´ê? ?ì„±????2ì´ˆê? ì§€?˜ë„ ?? œê°€ ?ˆëœ?¤ë©´ ?? œ
    private IEnumerator Destroy_Projectile() {
        yield return new WaitForSeconds(2f);

        // Projectile??Pool?ì„œ ê°€?¸ì˜¨ì§€ 2ì´ˆê? ì§€?˜ë©´ Destroy ?€??ë°˜ë‚©
        ProjectileReturn(pool_idx);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( !collision.CompareTag("Enemy") )	return;         // ?ì´ ?„ë‹Œ ?€?ê³¼ ë¶€?ªíˆë©?

        StopCoroutine("Destory_Projectile");
        collision.GetComponent<EnemyHP>().TakeDamage(damage);	// ??ì²´ë ¥??damageë§Œí¼ ê°ì†Œ
        ProjectileReturn(pool_idx);                                     // ë°œì‚¬ì²??¤ë¸Œ?íŠ¸ ?? œ ?€??Pool??ë°˜ë‚©
    }
}


/*
 * File : Projectile_Multiple.cs
 * Desc
 *	: ?€?Œê? ë°œì‚¬?˜ëŠ” ê¸°ë³¸ ë°œì‚¬ì²´ì— ë¶€ì°? Projectileê³??¤ë¥´ê²??¨ë°œ???„ë‹Œ ?¤ë°œ ?¬ê²©
 *	
 * Functions
 *	: Update() - Setup?ì„œ ë§¤ê°œë³€?˜ë¡œ targetPosë¥?ë°›ì•„ ê³„ì‚°??ë°©í–¥ ë²¡í„° ë°©í–¥?¼ë¡œ ë°œì‚¬ì²´ë? ?´ë™?œì¼œì¤?
 *  : Destory_Projectile() - ë°œì‚¬ì²´ê? ?ì„±????5ì´??„ì— ë°œì‚¬ì²´ë? ?? œ?œì¼œì£¼ëŠ” ì½”ë£¨??
 *	: OnTriggerEnter2D() - ?€ê²Ÿìœ¼ë¡??¤ì •???ê³¼ ë¶€?ªí˜”?????ì—ê²??°ë?ì§€ë¥?ì£¼ê³  ?¤ë¸Œ?íŠ¸ ?? œ
 *	
 */