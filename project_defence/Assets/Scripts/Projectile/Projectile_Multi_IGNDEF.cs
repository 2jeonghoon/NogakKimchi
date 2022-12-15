using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Multi_IGNDEF : Projectile_Multiple {
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;         // 적이 아닌 대상과 부딪히면

        StopCoroutine("Destory_Projectile");
        collision.GetComponent<EnemyHP>().TakeDamage(damage, true);	// 적 체력을 damage만큼 감소
        Debug.Log("OnTrigerPR");
        ProjectileReturn(pool_idx);
    }
}
