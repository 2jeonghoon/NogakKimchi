using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ڰ��� �Ѿ�
public class ProjectileMortar : Projectile
{
    //���� ����
    private float explosionRange;
    [SerializeField]
    private GameObject explosionPrefab;

    //���� Ÿ��
    private GameObject Tile;

    // �����
    private Vector3 vStartPos;
    // ������
    private Vector3 vEndPos;
    // ���� ��ġ
    private Vector3 vPos;

    private float fV_X; // x������ �ӵ�
    private float fV_Y; // y������ �ӵ�
    private float fV_Z; // z������ �ӵ�

    private float fg; // Y�������� �߷°��ӵ�
    private float fEndTime; // �������� ���� �ð�
    private float fMaxHeight; // �ִ� ����
    private float fHeight; // �ִ� ������ Y - ���۳����� Y
    private float fEndHeight; // �������� ���� Y - �������� ���� Y
    private float fTime = 0f; // �帣�� �ð�
    private float fMaxTime = 1f; // �ִ���̱��� ���� �ð�


    public void Setup(Transform target, float damage, EnemySpawner enemySpawner, float explosionRange)
    {
        // �߻� ���� ���
        //SoundManager.instance.SFXPlay("Mortar", clip);
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // Ÿ���� ���ݷ�
        this.explosionRange = explosionRange;

        // ������ �̵��� ���� �ʿ��� ����
        vStartPos = this.transform.position; // ��������

        //Ÿ���� ��Ȱ��ȭ ���¸� ���������� ���
        if (target == null || !target.gameObject.activeSelf) 
            vEndPos = new Vector3(-10.5f, -1.5f, 0);
        else
            vEndPos = target.position;      // ��������
        fMaxHeight = vEndPos.y + 10f; // ������ �ִ����

        fEndHeight = vEndPos.y - vStartPos.y;
        fHeight = fMaxHeight - vStartPos.y;
        fg = 2 * fHeight / (fMaxTime * fMaxTime);
        fV_Y = Mathf.Sqrt(2 * fg * fHeight);

        float a = fg;
        float b = -2 * fV_Y;
        float c = 2 * fEndHeight;

        fEndTime = Mathf.Abs((-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));


        fV_X = -(vStartPos.x - vEndPos.x)*2.04f / fEndTime;
        fV_Z = -(vStartPos.x - vEndPos.x) / fEndTime;
        this.pool_idx = 3;
        gameObject.SetActive(true);					// ObjectPool�� ����ϸ鼭 SetActive(true)�� �ʿ�����
    }

    private void Update()
    {
        // �߻�ü�� target�� ��ġ�� �̵�
        fTime += Time.deltaTime;
        vPos.x = vStartPos.x + fV_X * fTime;
        vPos.y = vStartPos.y + (fV_Y * fTime) - (1f * fg * fTime * fTime); // �������� �ӵ�
        vPos.z = 0;
        this.transform.position = vPos;


        if (fTime >= fMaxTime-0.5f && this.transform.position.y <= vEndPos.y)
        {
            Debug.Log("������Ʈ�� Mortar ��!");
            GameObject clone = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            clone.GetComponent<Explosion>().Setup(damage, explosionRange);
            ProjectileReturn(pool_idx);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (vPos.y >= 0) return;    // �Ѿ��� �������� ���� �ƴ϶��..?
        if (Tile == null) return;
        if (!collision.CompareTag("TileRoad")) return;         // ��Ÿ���� �ƴ� ���� �ε�����;
        if (collision.transform != Tile.transform) return;          // ���� ������ Ÿ��Ÿ���� �ƴϸ�

        Debug.Log("Ʈ���Ź� Mortar ��!");
        GameObject clone = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        clone.GetComponent<Explosion>().Setup(damage, explosionRange);
        ProjectileReturn(pool_idx);
    }

}