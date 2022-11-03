using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 박격포 총알
public class ProjectileMortar : Projectile
{
    //폭발 범위
    [SerializeField]
    private float explosionRange;
    [SerializeField]
    private GameObject explosionPrefab;

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
    }

    private void Update()
    {
        // 발사체를 target의 위치로 이동
        fTime += Time.deltaTime;
        vPos.x = vStartPos.x + fV_X * fTime;
        vPos.y = vStartPos.y + (fV_Y * fTime) - (1f * fg * fTime * fTime); // 떨어지는 속도
        vPos.z = 0;
        this.transform.position = vPos;


        if (fTime >= fMaxTime-0.5f && this.transform.position.y <= vEndPos.y)
        {
            GameObject clone = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            clone.GetComponent<Explosion>().Setup(damage, explosionRange);
            Destroy(gameObject);                                    // 발사체 오브젝트 삭제
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (vPos.y >= 0) return;    // 총알이 떨어지는 중이 아니라면..?
        if (Tile == null) return;
        if (!collision.CompareTag("TileRoad")) return;         // 길타일이 아닌 대상과 부딪히면;
        if (collision.transform != Tile.transform) return;          // 현재 맞은게 타겟타일이 아니면

        GameObject clone = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        clone.GetComponent<Explosion>().Setup(damage, explosionRange);
        Destroy(gameObject);                                    // 발사체 오브젝트 삭제
    }

}