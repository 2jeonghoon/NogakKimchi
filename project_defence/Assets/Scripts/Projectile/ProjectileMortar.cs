using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ڰ��� �Ѿ�
public class ProjectileMortar : Projectile
{
    //���� ����
    [SerializeField]
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
    private float fMaxTime = 1f; // �ִ���̱��� ���� �ð�

    private SpriteRenderer sr; // Ÿ�� ���� ����
    private EnemySpawner enemySpawner;
    private Enemy[] targetEnemy;

    public void Setup(Transform target, float damage, EnemySpawner enemySpawner)
    {
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // Ÿ���� ���ݷ�


        // ������ �̵��� ���� �ʿ��� ����
        vStartPos = this.transform.position; // ��������
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
        Debug.Log("startPos : " + vStartPos);
        this.enemySpawner = enemySpawner;
        // ���� Ÿ��
        Tile = FindTile(target);
    }

    private void Update()
    {
        // �߻�ü�� target�� ��ġ�� �̵�
        fTime += Time.deltaTime;
        vPos.x = vStartPos.x + fV_X * fTime;
        vPos.y = vStartPos.y + (fV_Y * fTime) - (1f * fg * fTime * fTime); // �������� �ӵ�
        vPos.z = 0;
        this.transform.position = vPos;


        if (fTime >= fMaxTime && this.transform.position.y <= vEndPos.y && Tile != null)
        {
            // Ȥ�� �����߰� �������� �������� ����, �Ѿ� �������
            sr = Tile.GetComponent<SpriteRenderer>();

            // Ÿ�� Ÿ�� ���� ����
            sr.color = new Color(255, 0, 0, 0);

            FindEnemy(Tile);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (vPos.y >= 0) return;    // �Ѿ��� �������� ���� �ƴ϶��..?
        if (Tile == null) return;
        if (!collision.CompareTag("TileRoad")) return;         // ��Ÿ���� �ƴ� ���� �ε�����;
        if (collision.transform != Tile.transform) return;          // ���� ������ Ÿ��Ÿ���� �ƴϸ�

        // Ÿ�� Ÿ�� ���� ����
        sr.color = new Color(255, 0, 0, 0);
        FindEnemy(Tile);
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

        // Ÿ��Ÿ�� ���� ��������
        if(tile != null)
        {
            Debug.Log("Tile");
            sr = tile.GetComponent<SpriteRenderer>();
            sr.color = new Color(1, 0, 0, 0.5f);
        }
        return tile;
    }

    private void FindEnemy(GameObject tile)
    {
        Enemy enemy;
        int count = 0;
        // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
        {
            enemy = enemySpawner.EnemyList[i];
            // ���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
            if (tile.transform.position.x + explosionRange >= enemy.transform.position.x &&
                tile.transform.position.x - explosionRange <= enemy.transform.position.x &&
                tile.transform.position.y + explosionRange >= enemy.transform.position.y &&
                tile.transform.position.y - explosionRange <= enemy.transform.position.y)
            {
                Debug.Log(damage);
                enemy.GetComponent<EnemyHP>().TakeDamage(damage);
            }
        }
    }
}