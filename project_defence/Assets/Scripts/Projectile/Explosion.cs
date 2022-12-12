using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float damage;

    private float fTime = 0;

    // ���� ����
    public AudioClip clip;

    public void Setup(float damage,float scale)
    {
        // ���� ���� ���
        SoundManager.instance.SFXPlay("boom", clip);
<<<<<<< HEAD
        Debug.Log("����");
=======
        //Debug.Log("����");
>>>>>>> origin/Jeonghoon
        this.damage = damage;                       // Ÿ���� ���ݷ�
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
        if (!collision.CompareTag("Enemy")) return;         // ���� �ƴ� ���� �ε�����
        collision.GetComponent<EnemyHP>().TakeDamage(damage);   // �� ü���� damage��ŭ ����
        // ���� ���� ��� �ش� ��ġ���� ����
    }
}
