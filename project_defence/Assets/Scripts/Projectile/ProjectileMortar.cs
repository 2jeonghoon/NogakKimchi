using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 박격포 총알
public class ProjectileMortar : Projectile
{
    //폭발 범위
    [SerializeField]
    private float explosionRange;

    //도착 타일
    private GameObject Tile;

    // 출발지
    private Vector3 vStartPos;
    // 목적지
    private Vector3 vEndPos;
    // 현재 위치
    private Vector3 vPos;

    private float fV_X; // x축으로 속도
    private float fV_Y; // y축으로 속도
    private float fV_Z; // z축으로 속도

    private float fg; // Y축으로의 중력가속도
    private float fEndTime; // 도착지점 도달 시간
    private float fMaxHeight; // 최대 높이
    private float fHeight; // 최대 높이의 Y - 시작높이의 Y
    private float fEndHeight; // 도착지점 높이 Y - 시작지점 높이 Y
    private float fTime = 0f; // 흐르는 시간
    private float fMaxTime = 1f; // 최대높이까지 가는 시간

    private SpriteRenderer sr; // 타일 색깔 정보
    private EnemySpawner enemySpawner;
    private Enemy[] targetEnemy;

    public void Setup(Transform target, float damage, EnemySpawner enemySpawner)
    {
        movement2D = GetComponent<Movement2D>();
        this.damage = damage;                       // 타워의 공격력


        // 포물선 이동을 위해 필요한 정보
        vStartPos = this.transform.position; // 시작지점
        vEndPos = target.position;      // 도착지점
        fMaxHeight = vEndPos.y + 10f; // 포물선 최대높이

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
        // 도착 타일
        Tile = FindTile(target);
    }

    private void Update()
    {
        // 발사체를 target의 위치로 이동
        fTime += Time.deltaTime;
        vPos.x = vStartPos.x + fV_X * fTime;
        vPos.y = vStartPos.y + (fV_Y * fTime) - (1f * fg * fTime * fTime); // 떨어지는 속도
        vPos.z = 0;
        this.transform.position = vPos;


        if (fTime >= fMaxTime && this.transform.position.y <= vEndPos.y && Tile != null)
        {
            // 혹시 못맞추고 떨어져도 데미지는 들어가게, 총알 사라지기
            sr = Tile.GetComponent<SpriteRenderer>();

            // 타겟 타일 색깔 변경
            sr.color = new Color(255, 0, 0, 0);

            FindEnemy(Tile);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (vPos.y >= 0) return;    // 총알이 떨어지는 중이 아니라면..?
        if (Tile == null) return;
        if (!collision.CompareTag("TileRoad")) return;         // 길타일이 아닌 대상과 부딪히면;
        if (collision.transform != Tile.transform) return;          // 현재 맞은게 타겟타일이 아니면

        // 타겟 타일 색깔 변경
        sr.color = new Color(255, 0, 0, 0);
        FindEnemy(Tile);
        Destroy(gameObject);                                    // 발사체 오브젝트 삭제
    }

    private GameObject FindTile(Transform target)
    {
        List<GameObject> FoundObjects;
        float shortDis;
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TileRoad"));
        shortDis = Vector3.Distance(target.transform.position, FoundObjects[0].transform.position); // 첫번째를 기준으로 잡아주기 
        GameObject tile = FoundObjects[0]; // 첫번째를 먼저 

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(target.transform.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                tile = found;
                shortDis = Distance;
            }
        }

        // 타겟타일 색깔 가져오기
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
        // EnemySpawner의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
        {
            enemy = enemySpawner.EnemyList[i];
            // 현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면
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