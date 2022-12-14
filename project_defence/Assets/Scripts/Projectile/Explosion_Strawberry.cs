using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Strawberry: Explosion
{
    public void Setup(float scale)
    {
        // ���� ���� ���
        SoundManager.instance.SFXPlay("boom", clip);
        this.transform.localScale = new Vector3(0.2f, 0.2f, 1) * scale;
    }


    private IEnumerator StrawberryJam(Collider2D collision, float enemyCurrentSpeed)
    {
        collision.GetComponent<Movement2D>().MoveSpeed = enemyCurrentSpeed * 0.5f;
        yield return new WaitForSeconds(1f);
        if (collision.GetComponent<Enemy>().isInstagramOn)
            yield break;                                    // ���� Instagram Skill�� ����ߴٸ� �ӵ��� �ٲپ����� ����, Instagram ��ų�� �� ������ �ֱ� ����
        collision.GetComponent<Movement2D>().MoveSpeed = enemyCurrentSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;         // ���� �ƴ� ���� �ε�����

        //float enemyBaseSpeed = collision.GetComponent<Movement2D>().BaseMoveSpeed;   // ���� BaseMoveSpeed�� ������
        float enemyCurrentSpeed = collision.GetComponent<Movement2D>().MoveSpeed;   // ���� CurrentMoveSpeed�� ������

        if (enemyCurrentSpeed == 0) 
            return; // ���� �������� ���� �ƹ��� ȿ���� ���� ���Ѵ�.
        else 
            StartCoroutine(StrawberryJam(collision, enemyCurrentSpeed));


        Debug.Log(" current : " + enemyCurrentSpeed);
        Boom();
        StartCoroutine("WaitForAnimation");
        // ���� ���� ��� �ش� ��ġ���� ����
    }
}
