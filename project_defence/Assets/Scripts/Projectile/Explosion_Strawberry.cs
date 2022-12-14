using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Strawberry: Explosion
{
    public void Setup(float scale)
    {
        // 폭발 사운드 재생
        SoundManager.instance.SFXPlay("boom", clip);
        this.transform.localScale = new Vector3(0.2f, 0.2f, 1) * scale;
    }


    private IEnumerator StrawberryJam(Collider2D collision, float enemyCurrentSpeed)
    {
        collision.GetComponent<Movement2D>().MoveSpeed = enemyCurrentSpeed * 0.5f;
        yield return new WaitForSeconds(1f);
        if (collision.GetComponent<Enemy>().isInstagramOn)
            yield break;                                    // 만약 Instagram Skill을 사용했다면 속도를 바꾸어주지 않음, Instagram 스킬이 더 우위에 있기 때문
        collision.GetComponent<Movement2D>().MoveSpeed = enemyCurrentSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;         // 적이 아닌 대상과 부딪히면

        //float enemyBaseSpeed = collision.GetComponent<Movement2D>().BaseMoveSpeed;   // 적의 BaseMoveSpeed를 가져옴
        float enemyCurrentSpeed = collision.GetComponent<Movement2D>().MoveSpeed;   // 적의 CurrentMoveSpeed를 가져옴

        if (enemyCurrentSpeed == 0) 
            return; // 적이 멈춰있을 때는 아무런 효과를 주지 못한다.
        else 
            StartCoroutine(StrawberryJam(collision, enemyCurrentSpeed));


        Debug.Log(" current : " + enemyCurrentSpeed);
        Boom();
        StartCoroutine("WaitForAnimation");
        // 적을 맞춘 경우 해당 위치에서 폭발
    }
}
