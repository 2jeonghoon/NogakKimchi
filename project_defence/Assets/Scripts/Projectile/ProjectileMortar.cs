using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ڰ��� �Ѿ�
public class ProjectileMortar : Projectile
{
    //���� ����
    private float explosionRange;

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
    private float fMaxTime = 0.3f; // �ִ���̱��� ���� �ð�

    public override void Setup(Transform target, float damage)
    {
        // ���� Ÿ��
        Tile = FindTile(target);

        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // Ÿ���� ���ݷ�


        // ������ �̵��� ���� �ʿ��� ����
        vStartPos = this.transform.position; // ��������
        vEndPos = target.position;      // ��������
        fMaxHeight = vEndPos.y + 1.5f; // ������ �ִ����

        fEndHeight = vEndPos.y - vStartPos.y;
        fHeight = fMaxHeight - vStartPos.y;
        fg = 2 * fHeight / (fMaxTime * fMaxTime);
        fV_Y = Mathf.Sqrt(2 * fg * fHeight);

        float a = fg;
        float b = -2 * fV_Y;
        float c = 2 * fEndHeight;

        fEndTime = Mathf.Abs((-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));


        fV_X = -(vStartPos.x - vEndPos.x) / fEndTime;
        fV_Z = -(vStartPos.x - vEndPos.x) / fEndTime;

    }

    private void Update()
    {
        // �߻�ü�� target�� ��ġ�� �̵�
        fTime += Time.deltaTime;
        vPos.x = vStartPos.x + fV_X * fTime;
        vPos.y = vStartPos.y + (fV_Y * fTime) - (1f * fg * fTime * fTime); // �������� �ӵ�
        vPos.z = 0;
        this.transform.position = vPos;


        if (this.transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹");
        //if (vPos.y >= 0) return;    // �Ѿ��� �������� ���� �ƴ϶��..?
        if (!collision.CompareTag("TileRoad")) return;         // ��Ÿ���� �ƴ� ���� �ε�����
        if (collision.transform != Tile.transform) return;          // ���� ������ Ÿ��Ÿ���� �ƴϸ�

        Destroy(gameObject);                                    // �߻�ü ������Ʈ ����
    }

    private GameObject FindTile(Transform target)
    {
        List<GameObject> FoundObjects;
        float shortDis;
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TileRoad"));
        shortDis = Vector3.Distance(target.transform.position, FoundObjects[0].transform.position); // ù��°�� �������� ����ֱ� 
        GameObject tile = FoundObjects[0]; // ù��°�� ���� 

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(target.transform.position, found.transform.position);

            if (Distance < shortDis) // ������ ���� �������� �Ÿ� ���
            {
                tile = found;
                shortDis = Distance;
            }
        }
        return tile;
    }
}
