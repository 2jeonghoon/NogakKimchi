using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float damage;

    private float fTime = 0;
    Animator animator;

    // 폭발 사운드
    public AudioClip clip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Setup(float damage,float scale)
    {
        // 폭발 사운드 재생
        SoundManager.instance.SFXPlay("boom", clip);
        //Debug.Log("폭발");
        this.damage = damage;                       // 타워의 공격력
        this.transform.localScale = new Vector3(0.2f, 0.2f, 1) * scale;
    }
    private void Update()
    {
        fTime += Time.deltaTime;
        //if(fTime >= 0.2f)
        //{
            Boom();
            StartCoroutine("WaitForAnimation");
            //Destroy(gameObject);
        //}
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;         // 적이 아닌 대상과 부딪히면
        collision.GetComponent<EnemyHP>().TakeDamage(damage, false);   // 적 체력을 damage만큼 감소
        Boom();
        StartCoroutine("WaitForAnimation");
        // 적을 맞춘 경우 해당 위치에서 폭발
    }

    public void Boom()
    {
        //Debug.Log("Boom");
        animator.SetTrigger("Boom");
    }

    // 애니메이션 끝날 때 까지 대기 (1초)
    IEnumerator WaitForAnimation()
    {
        float time = 0f;

        //while (true == animator.GetCurrentAnimatorStateInfo(0).IsName(name)) {
        while (time <= 0.5)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

}
