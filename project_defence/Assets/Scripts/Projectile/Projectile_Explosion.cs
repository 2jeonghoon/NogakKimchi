using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Explosion : Projectile
{
    //폭발 범위
    private float explosionRange;
    [SerializeField]
    private GameObject explosionPrefab;

    private int pool_idx = 2;

    // 사거리가 벗어나면 터지도록 사거리 가져오기
    float range;
    float move_distance;
    Vector3 direction;
    Vector3 start_position;


    public void Setup(Transform target, float damage, float range, float explosionRange)
    {
        // 발사 사운드 재생
        //SoundManager.instance.SFXPlay("ExplosionShot", clip);
        //Debug.Log("발사");
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // 타워의 공격력
        this.target = target;                       // 타워가 설정해준 target
        this.range = range;                         // 타워가 설정해준 range
        this.explosionRange = explosionRange;
        start_position = transform.position;
        direction = (target.position - transform.position).normalized;
        gameObject.SetActive(true);					// ObjectPool을 사용하면서 SetActive(true)가 필요해짐
    }

    private void Update()
    {
        move_distance = (transform.position - start_position).magnitude;
        if(move_distance > range)
        {
            boom();
        }
        if (target != null) // target이 존재하면
        {
            // 발사체를 target의 위치로 이동
            movement2D.MoveTo(direction);
        }
        else                    // 여러 이유로 target이 사라지면
        {
            //boom();                    
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
        ProjectileReturn(pool_idx);
    }

}
