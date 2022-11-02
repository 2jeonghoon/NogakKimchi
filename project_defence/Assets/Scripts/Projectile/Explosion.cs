using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float damage;

    private float fTime = 0;

    public void Setup(float damage,float scale)
    {
        Debug.Log("폭발");
        this.damage = damage;                       // 타워의 공격력
        this.transform.localScale = new Vector3(1, 1, 1) * scale;
    }
    private void Update()
    {
        fTime += Time.deltaTime;
        if(fTime >= 0.2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;         // 적이 아닌 대상과 부딪히면
        collision.GetComponent<EnemyHP>().TakeDamage(damage);   // 적 체력을 damage만큼 감소
        // 적을 맞춘 경우 해당 위치에서 폭발
    }
}
