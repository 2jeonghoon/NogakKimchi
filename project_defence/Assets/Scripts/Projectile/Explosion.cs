using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float damage;

    private float fTime = 0;
    Animator animator;

    // ���� ����
    public AudioClip clip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Setup(float damage,float scale)
    {
        // ���� ���� ���
        SoundManager.instance.SFXPlay("boom", clip);
        //Debug.Log("����");
        this.damage = damage;                       // Ÿ���� ���ݷ�
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
        if (!collision.CompareTag("Enemy")) return;         // ���� �ƴ� ���� �ε�����
        collision.GetComponent<EnemyHP>().TakeDamage(damage);   // �� ü���� damage��ŭ ����
        Boom();
        StartCoroutine("WaitForAnimation");
        // ���� ���� ��� �ش� ��ġ���� ����
    }

    public void Boom()
    {
        //Debug.Log("Boom");
        animator.SetTrigger("Boom");
    }

    // �ִϸ��̼� ���� �� ���� ��� (1��)
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
