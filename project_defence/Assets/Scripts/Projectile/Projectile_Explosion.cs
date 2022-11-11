using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Explosion : Projectile
{
    //폭발 범위
    [SerializeField]
    private float explosionRange;
    [SerializeField]
    private GameObject explosionPrefab;


    public void Setup(Transform target, float damage)
    {
        // 발사 사운드 재생
        SoundManager.instance.SFXPlay("ExplosionShot", clip);
        Debug.Log("발사");
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // 타워의 공격력
        this.target = target;                       // 타워가 설정해준 target
    }

    private void Update()
    {
        if (target != null) // target이 존재하면
        {
            // 발사체를 target의 위치로 이동
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else                    // 여러 이유로 target이 사라지면
        {
            boom();                    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;         // 적이 아닌 대상과 부딪히면
        if (collision.transform != target) return;          // 현재 target인 적이 아닐 때

        boom();
    }

    // 폭발
    private void boom()
    {
        // 적을 맞춘 경우 해당 위치에서 폭발
        GameObject clone = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        clone.GetComponent<Explosion>().Setup(damage, explosionRange);
        Destroy(gameObject);                                    // 발사체 오브젝트 삭제
    }

}
