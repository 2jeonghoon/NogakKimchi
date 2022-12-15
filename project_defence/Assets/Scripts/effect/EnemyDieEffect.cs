using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public void Set(Transform _transform)
    {
        transform.position = _transform.position;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDie()
    {
        //Debug.Log("Boom");
        animator.SetTrigger("Die");
        StartCoroutine("WaitForAnimation");
    }

    public void BossSkillEffect()
    {
        //Debug.Log("Boom");
        animator.SetTrigger("Boom");
    }


    // 애니메이션 끝날 때 까지 대기 (1초)
    IEnumerator WaitForAnimation()
    {
        float time = 0f;

        //while (true == animator.GetCurrentAnimatorStateInfo(0).IsName(name)) {
        while (time <= 1)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        this.transform.position = ObjectPool.instance.transform.position;
        this.gameObject.SetActive(false);
        ObjectPool.instance.objectPoolList[12].Enqueue(this.gameObject);
    }
}
