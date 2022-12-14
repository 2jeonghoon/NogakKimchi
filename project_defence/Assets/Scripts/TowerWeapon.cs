using UnityEngine;
using System.Collections;

public enum WeaponType { Gun = 0, Laser, Slow, Buff, Mortar, Shotgun, Spear, Explosion, Strawberry }
public enum WeaponState
{
    SearchTarget = 0, TryAttackGun, TryAttackLaser, TryAttackMortar,
    TryAttackShotgun, TryAttackSpaer, TryAttackExplosion, TryAttackStrawberry
}
public enum TileType { One, Two };

public class TowerWeapon : MonoBehaviour
{
    [Header("Commons")]
    [SerializeField]
    private TowerTemplate towerTemplate;                            // Ÿ�� ���� (���ݷ�, ���ݼӵ� ��)
    [SerializeField]
    private Transform spawnPoint;                               // �߻�ü ���� ��ġ
    [SerializeField]
    private WeaponType weaponType;                              // ���� �Ӽ� ����

    [SerializeField]
    private TileType tileType;
    public TowerWeapon buffTower;

    [Header("Gun")]
    [SerializeField]
    private GameObject projectilePrefab;                        // �߻�ü ������

    [Header("Laser")]
    [SerializeField]
    private LineRenderer lineRenderer;                          // �������� ���Ǵ� ��(LineRenderer)
    [SerializeField]
    private Transform hitEffect;                                // Ÿ�� ȿ��
    [SerializeField]
    private LayerMask targetLayer;                          // ������ �ε����� ���̾� ����

    private int level = 0;                              // Ÿ�� ����
    private WeaponState weaponState = WeaponState.SearchTarget; // Ÿ�� ������ ����
    private Transform attackTarget = null;                    // ���� ���
    private SpriteRenderer spriteRenderer;                          // Ÿ�� ������Ʈ �̹��� �����
    private TowerSpawner towerSpawner;
    private EnemySpawner enemySpawner;                          // ���ӿ� �����ϴ� �� ���� ȹ���
    private PlayerGold playerGold;                              // �÷��̾��� ��� ���� ȹ�� �� ����
    private Tile ownerTile;                             // ���� Ÿ���� ��ġ�Ǿ� �ִ� Ÿ��

    private float addedDamage;                          // ������ ���� �߰��� ������
    private int buffLevel;                              // ������ �޴��� ���� ���� (0 : ����X, 1~3 : �޴� ���� ����)

    // Ÿ�� ��ġ ����� Ŭ��
    public AudioClip buildClip;
    // Ÿ�� ���׷��̵� ����� Ŭ��
    public AudioClip upgradeClip;
    // Ÿ�� �Ǹ� ����� Ŭ��
    public AudioClip sellClip;

    public Sprite TowerSprite => towerTemplate.weapon[level].sprite;
    public float Damage => towerTemplate.weapon[level].damage;
    public float Rate => towerTemplate.weapon[level].rate;
    public float Range => towerTemplate.weapon[level].range;

    public float ExplosionRange => towerTemplate.weapon[level].explosionRange;
    public int UpgradeCost => Level < MaxLevel ? towerTemplate.weapon[level + 1].cost : 0;
    public int UpgradeCost2 => Level < MaxLevel ? towerTemplate.weapon[level + 2].cost : 0;
    public int SellCost => towerTemplate.weapon[level].sell;
    public int Level => level + 1;
    public int MaxLevel => towerTemplate.maxTowerLV;
    public float Slow => towerTemplate.weapon[level].slow;
    public float Buff => towerTemplate.weapon[level].buff;
    public WeaponType WeaponType => weaponType;
    public TileType TileType => tileType;

    public float AddedDamage
    {
        set => addedDamage = Mathf.Max(0, value);
        get => addedDamage;
    }
    public int BuffLevel
    {
        set => buffLevel = Mathf.Max(0, value);
        get => buffLevel;
    }

    public void Setup(TowerSpawner towerSpawner, EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
        // Ÿ�� ��ġ ���� ���
        SoundManager.instance.SFXPlay("TowerSetUp", buildClip);
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.towerSpawner = towerSpawner;
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;
        //y��ǥ�� �������� ������ ������
        this.GetComponent<SpriteRenderer>().sortingOrder = -(int)this.transform.position.y + 10;
        // ���� �Ӽ��� ĳ��, �������� ��
        if (weaponType == WeaponType.Gun || weaponType == WeaponType.Laser ||
            weaponType == WeaponType.Mortar || weaponType == WeaponType.Shotgun ||
            weaponType == WeaponType.Spear || weaponType == WeaponType.Explosion || weaponType == WeaponType.Strawberry)
        {
            // ���� ���¸� WeaponState.SearchTarget���� ����
            ChangeState(WeaponState.SearchTarget);
        }
    }

    public void ChangeState(WeaponState newState)
    {
        // ������ ������̴� ���� ����
        //Debug.Log(weaponState.ToString());
        StopCoroutine(weaponState.ToString());
        // ���� ����
        weaponState = newState;

        //Debug.Log(weaponState.ToString());
        // ���ο� ���� ���
        StartCoroutine(weaponState.ToString());
    }


    private void RotateToTarget()
    {
        // �������κ����� �Ÿ��� ���������κ����� ������ �̿��� ��ġ�� ���ϴ� �� ��ǥ�� �̿�
        // ���� = arctan(y/x)
        // x, y ������ ���ϱ�
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;
        // x, y �������� �������� ���� ���ϱ�
        // ������ radian �����̱� ������ Mathf.Rad2Deg�� ���� �� ������ ����
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            // ���� Ÿ���� ���� ������ �ִ� ���� ���(��) Ž��
            attackTarget = FindClosestAttackTarget();
            if (attackTarget != null && attackTarget.gameObject.activeSelf)
            {
                if (weaponType == WeaponType.Gun)
                {
                    ChangeState(WeaponState.TryAttackGun);
                }
                else if (weaponType == WeaponType.Laser)
                {
                    ChangeState(WeaponState.TryAttackLaser);
                }
                else if (weaponType == WeaponType.Mortar)
                {
                    ChangeState(WeaponState.TryAttackMortar);
                }
                else if (weaponType == WeaponType.Shotgun)
                {
                    ChangeState(WeaponState.TryAttackShotgun);
                }
                else if (weaponType == WeaponType.Spear)
                {
                    ChangeState(WeaponState.TryAttackSpaer);
                }
                else if (weaponType == WeaponType.Explosion)
                {
                    ChangeState(WeaponState.TryAttackExplosion);
                }
                else if (weaponType == WeaponType.Strawberry)
                {
                    ChangeState(WeaponState.TryAttackStrawberry);
                }
            }
            yield return null;
        }
    }

    private IEnumerator TryAttackGun()
    {
        while (true)
        {
            // target�� �����ϴ°� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // ĳ�� ���� (�߻�ü ����)
            SpawnProjectile();
            // attackRate �ð���ŭ ���
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);

        }
    }

    private IEnumerator TryAttackLaser()
    {
        // ������, ������ Ÿ�� ȿ�� Ȱ��ȭ
        EnableLaser();

        while (true)
        {
            // target�� �����ϴ°� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                // ������, ������ Ÿ�� ȿ�� ��Ȱ��ȭ
                DisableLaser();
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // ������ ����
            SpawnLaser();

            yield return null;
        }
    }

    private IEnumerator TryAttackMortar()
    {
        while (true)
        {
            // target�� �����ϴ°� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // �ڰ��� ���� (�߻�ü ����)
            if (attackTarget != null)
            {
                SpawnMortarProjectile();
            }
            // attackRate �ð���ŭ ���
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);

        }
    }

    // ���� Ÿ�� ����
    private IEnumerator TryAttackShotgun()
    {
        while (true)
        {
            // target�� �����ϴ°� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // ���� ���� (�߻�ü ����)
            SpawnProjectile_Multiple();
            // attackRate �ð���ŭ ���
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);
        }
    }

    // ���� Ÿ�� ����
    private IEnumerator TryAttackSpaer()
    {
        while (true)
        {
            // target�� �����ϴ°� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // ���� ���� (�߻�ü ����)
            SpawnProjectile_Spear();
            // attackRate �ð���ŭ ���
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);
        }
    }

    // ���� Ÿ�� ����
    private IEnumerator TryAttackExplosion()
    {
        while (true)
        {
            // target�� �����ϴ°� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // ���� ���� (�߻�ü ����)
            SpawnProjectile_Explosion();
            // attackRate �ð���ŭ ���
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);
        }
    }

    private IEnumerator TryAttackStrawberry()
    {
        while (true)
        {
            // target�� �����ϴ°� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // ���� ���� (�߻�ü ����)
            SpawnProjectile_Strawberry();
            // attackRate �ð���ŭ ���
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);
        }
    }

    public void OnBuffAroundTower()
    {
        // ���� �ʿ� ��ġ�� "Tower" �±׸� ���� ��� ������Ʈ Ž��
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        for (int i = 0; i < towers.Length; ++i)
        {
            TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();

            // �̹� ������ �ް� �ְ�, ���� ���� Ÿ���� �������� ���� �����̸� �н�
            if (weapon.BuffLevel > Level)
            {
                continue;
            }

            // ���� ���� Ÿ���� �ٸ� Ÿ���� �Ÿ��� �˻��ؼ� ���� �ȿ� Ÿ���� ������
            if (Vector3.Distance(weapon.transform.position, transform.position) <= towerTemplate.weapon[level].range)
            {
                // ������ ������ ĳ��, ������ Ÿ���̸�
                if (weapon.WeaponType == WeaponType.Gun || weapon.WeaponType == WeaponType.Laser ||
                    weapon.WeaponType == WeaponType.Explosion || weapon.WeaponType == WeaponType.Mortar ||
                    weapon.WeaponType == WeaponType.Shotgun || weapon.WeaponType == WeaponType.Spear)
                {
                    // ������ ���� ���ݷ� ����
                    weapon.AddedDamage = weapon.Damage * (towerTemplate.weapon[level].buff);
                    // Ÿ���� �ް� �ִ� ���� ���� ����
                    weapon.BuffLevel = Level;
                    weapon.buffTower = this;
                }
            }
        }
    }

    private Transform FindClosestAttackTarget()
    {
        // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
        // float closestDistSqr = Mathf.Infinity;
        // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
        //Debug.Log(enemySpawner.EnemyList.Count);
        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
        {
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
            // ���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
            if (distance <= towerTemplate.weapon[level].range)// && distance <= closestDistSqr)
            {
                // closestDistSqr = distance;
                attackTarget = enemySpawner.EnemyList[i].transform;
                break;
            }
        }


        return attackTarget;
    }

    private bool IsPossibleToAttackTarget()
    {
        // target�� �ִ��� �˻� (�ٸ� �߻�ü�� ���� ����, Goal �������� �̵��� ���� ��)
        if (attackTarget == null || !attackTarget.gameObject.activeSelf)
        {
            return false;
        }
        // target�� ���� ���� �ȿ� �ִ��� �˻� (���� ������ ����� ���ο� �� Ž��)
        float distance = Vector3.Distance(attackTarget.position, transform.position);
        if (distance > towerTemplate.weapon[level].range || !attackTarget.gameObject.activeSelf)
        {
            attackTarget = null;
            return false;
        }
        return true;
    }



    private void SpawnProjectile()
    {
        if (attackTarget != null)
        {
            GameObject clone;                                                                   // ������Ʈ Pool���� Dequeue�ؼ� ������, 0�� : coco
            if (!ObjectPool.instance.objectPoolList[0].TryDequeue(out clone))
            {
                clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            }
            clone.transform.position = spawnPoint.position;                                     // Dequeue�ؼ� ������ Projectile�� position�� SpawnPoint�� �ٲپ� ��
            // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����   
            // ���ݷ� = Ÿ�� �⺻ ���ݷ� + ������ ���� �߰��� ���ݷ�
            float damage = towerTemplate.weapon[level].damage + AddedDamage;
            if (IsPossibleToAttackTarget())
                clone.GetComponent<Projectile>().Setup(attackTarget, damage);
            else
                clone.GetComponent<Projectile>().Setup(FindClosestAttackTarget(), damage);
        }
    }

    private void SpawnProjectile_Multiple()
    {
        if (attackTarget != null)
        {
            GameObject clone1;                                                                   // ������Ʈ Pool���� Dequeue�ؼ� ������, 1�� : jelly
            GameObject clone2;
            GameObject clone3;

            if (!ObjectPool.instance.objectPoolList[1].TryDequeue(out clone1))
                clone1 = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            if (!ObjectPool.instance.objectPoolList[1].TryDequeue(out clone2))
                clone2 = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            if (!ObjectPool.instance.objectPoolList[1].TryDequeue(out clone3))
                clone3 = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            clone1.transform.position = spawnPoint.position;
            clone2.transform.position = spawnPoint.position;
            clone3.transform.position = spawnPoint.position;

            // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
            // ���ݷ� = Ÿ�� �⺻ ���ݷ� + ������ ���� �߰��� ���ݷ�
            float damage = towerTemplate.weapon[level].damage + AddedDamage;
            // �� ������ ���������� ������ ���� Vector3.left, right�� ������
            if (IsPossibleToAttackTarget())
            {
                Vector3 targetPos = attackTarget.position;
                clone1.GetComponent<Projectile_Multiple>().Setup(targetPos, damage, -1);
                clone2.GetComponent<Projectile_Multiple>().Setup(targetPos, damage);
                clone3.GetComponent<Projectile_Multiple>().Setup(targetPos, damage, 1);
            }
            else
            {
                Debug.Log("Find");
                Vector3 targetPos = FindClosestAttackTarget().position;
                clone1.GetComponent<Projectile_Multiple>().Setup(targetPos, damage, -1);
                clone2.GetComponent<Projectile_Multiple>().Setup(targetPos, damage);
                clone3.GetComponent<Projectile_Multiple>().Setup(targetPos, damage, 1);
            }
        }
    }

    // ���� �Ѿ� ����
    private void SpawnProjectile_Explosion()
    {
        //Debug.Log("�߻�");
        if (attackTarget != null)
        {
            GameObject clone;                                                                   // ������Ʈ Pool���� Dequeue�ؼ� ������, 2�� : icecream
            if (!ObjectPool.instance.objectPoolList[2].TryDequeue(out clone))
            {
                clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            }
            clone.transform.position = spawnPoint.position;                                     // Dequeue�ؼ� ������ Projectile�� position�� SpawnPoint�� �ٲپ� ��
            // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
            // ���ݷ� = Ÿ�� �⺻ ���ݷ� + ������ ���� �߰��� ���ݷ�
            float damage = towerTemplate.weapon[level].damage + AddedDamage;
            if (IsPossibleToAttackTarget())
                clone.GetComponent<Projectile_Explosion>().Setup(attackTarget, damage, Range, ExplosionRange);
            else
                clone.GetComponent<Projectile_Explosion>().Setup(FindClosestAttackTarget(), damage, Range, ExplosionRange);
        }
    }

    // �ڰ��� �Ѿ� ����
    private void SpawnMortarProjectile()
    {
        //GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        GameObject clone;                                                                   // ������Ʈ Pool���� Dequeue�ؼ� ������, 3�� : milk
        if (!ObjectPool.instance.objectPoolList[3].TryDequeue(out clone))
        {
            clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        }
        clone.transform.position = spawnPoint.position;                                     // Dequeue�ؼ� ������ Projectile�� position�� SpawnPoint�� �ٲپ� ��
        // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
        // ���ݷ� = Ÿ�� �⺻ ���ݷ� + ������ ���� �߰��� ���ݷ�
        float damage = towerTemplate.weapon[level].damage + AddedDamage;
        if (IsPossibleToAttackTarget())
            clone.GetComponent<ProjectileMortar>().Setup(attackTarget, damage, enemySpawner, ExplosionRange);
        else
            clone.GetComponent<ProjectileMortar>().Setup(FindClosestAttackTarget(), damage, enemySpawner, ExplosionRange);
    }

    private void SpawnProjectile_Strawberry()
    {
        GameObject clone;                                                                   // ������Ʈ Pool���� Dequeue�ؼ� ������, 4�� : strawberry
        if (!ObjectPool.instance.objectPoolList[4].TryDequeue(out clone))
        {
            clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        }
        clone.transform.position = spawnPoint.position;                                     // Dequeue�ؼ� ������ Projectile�� position�� SpawnPoint�� �ٲپ� ��
        // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
        // ���ݷ� = Ÿ�� �⺻ ���ݷ� + ������ ���� �߰��� ���ݷ�
        float damage = towerTemplate.weapon[level].damage + AddedDamage;
        if (IsPossibleToAttackTarget())
            clone.GetComponent<ProjectileStrawberry>().Setup(attackTarget, enemySpawner, ExplosionRange);
        else
            clone.GetComponent<ProjectileStrawberry>().Setup(FindClosestAttackTarget(), enemySpawner, ExplosionRange);
    }

    private void SpawnProjectile_Spear()
    {
        if (attackTarget != null)
        {
            GameObject clone;                                                                   // ������Ʈ Pool���� Dequeue�ؼ� ������, 5�� : spear
            if (!ObjectPool.instance.objectPoolList[5].TryDequeue(out clone))
            {
                clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            }
            clone.transform.position = spawnPoint.position;                                     // Dequeue�ؼ� ������ Projectile�� position�� SpawnPoint�� �ٲپ� ��
            // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
            // ���ݷ� = Ÿ�� �⺻ ���ݷ� + ������ ���� �߰��� ���ݷ�
            float damage = towerTemplate.weapon[level].damage + AddedDamage;
            if (IsPossibleToAttackTarget())
                clone.GetComponent<Projectile_Spear>().Setup(attackTarget, damage, Range);
            else
                clone.GetComponent<Projectile_Spear>().Setup(FindClosestAttackTarget(), damage, Range);
        }
    }

    private void EnableLaser()
    {
        lineRenderer.gameObject.SetActive(true);
        hitEffect.gameObject.SetActive(true);
    }

    private void DisableLaser()
    {
        lineRenderer.gameObject.SetActive(false);
        hitEffect.gameObject.SetActive(false);
    }

    private void SpawnLaser()
    {
        Vector3 direction = attackTarget.position - spawnPoint.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll(spawnPoint.position, direction, towerTemplate.weapon[level].range, targetLayer);

        // ���� �������� ���� ���� ������ ���� �� �� ���� attackTarget�� ������ ������Ʈ�� ����
        for (int i = 0; i < hit.Length; ++i)
        {
            if (hit[i].transform == attackTarget)
            {
                // ���� ��������
                lineRenderer.SetPosition(0, spawnPoint.position);
                // ���� ��ǥ����
                lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);
                // Ÿ�� ȿ�� ��ġ ����
                hitEffect.position = hit[i].point;
                // �� ü�� ���� (1�ʿ� damage��ŭ ����)
                // ���ݷ� = Ÿ�� �⺻ ���ݷ� + ������ ���� �߰��� ���ݷ�
                float damage = towerTemplate.weapon[level].damage + AddedDamage;

                attackTarget.GetComponent<EnemyHP>().TakeLaserDamage(damage * Time.deltaTime);
            }
        }
    }


    public bool Upgrade_1()
    {
        // Ÿ�� ��ġ ���� ���
        SoundManager.instance.SFXPlay("TowerUpgrade", upgradeClip);
        // Ÿ�� ���׷��̵忡 �ʿ��� ��尡 ������� �˻�
        if (playerGold.CurrentGold < towerTemplate.weapon[level + 1].cost)
        {
            return false;
        }

        // Ÿ�� ���� ����
        level++;
        // Ÿ�� ���� ���� (Sprite)
        spriteRenderer.sprite = towerTemplate.weapon[level].sprite;
        // ��� ����
        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;

        // ���� �Ӽ��� �������̸�
        if (weaponType == WeaponType.Laser)
        {
            // ������ ���� �������� ���� ����
            lineRenderer.startWidth = 0.05f + level * 0.05f;
            lineRenderer.endWidth = 0.05f;
        }

        // Ÿ���� ���׷��̵� �� �� ��� ���� Ÿ���� ���� ȿ�� ����
        // ���� Ÿ���� ���� Ÿ���� ���, ���� Ÿ���� ���� Ÿ���� ���
        towerSpawner.OnBuffAllBuffTowers();

        return true;
    }

    public bool Upgrade_2()
    {
        // Ÿ�� ��ġ ���� ���
        SoundManager.instance.SFXPlay("TowerUpgrade", upgradeClip);
        // Ÿ�� ���׷��̵忡 �ʿ��� ��尡 ������� �˻�
        if (playerGold.CurrentGold < towerTemplate.weapon[level + 2].cost)
        {
            return false;
        }

        // Ÿ�� ���� ����
        level += 2;
        // Ÿ�� ���� ���� (Sprite)
        spriteRenderer.sprite = towerTemplate.weapon[level].sprite;
        // ��� ����
        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;

        // ���� �Ӽ��� �������̸�
        if (weaponType == WeaponType.Laser)
        {
            // ������ ���� �������� ���� ����
            lineRenderer.startWidth = 0.05f + (level) * 0.05f;
            lineRenderer.endWidth = 0.05f;
        }

        // Ÿ���� ���׷��̵� �� �� ��� ���� Ÿ���� ���� ȿ�� ����
        // ���� Ÿ���� ���� Ÿ���� ���, ���� Ÿ���� ���� Ÿ���� ���
        towerSpawner.OnBuffAllBuffTowers();

        return true;
    }

    public void Sell()
    {
        // Ÿ�� �Ǹ� ���� ���
        SoundManager.instance.SFXPlay("TowerSell", sellClip);
        // ��� ����
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        // ���� Ÿ�Ͽ� �ٽ� Ÿ�� �Ǽ��� �����ϵ��� ����
        ownerTile.IsBuildTower = false;

        // ���� �ʿ� ��ġ�� "Tower" �±׸� ���� ��� ������Ʈ Ž��
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        for (int i = 0; i < towers.Length; ++i)
        {
            towers[i].GetComponent<TowerWeapon>().BuffLevel = 0;
            towers[i].GetComponent<TowerWeapon>().AddedDamage = 0;
            //Debug.Log(towers[i].GetComponent<TowerWeapon>().AddedDamage);
        }
        towerSpawner.OnBuffAllBuffTowers();

        // �Ǹ� ����Ʈ �����ֱ�
        towerSpawner.sellEffect.SetActive(true);
        towerSpawner.sellEffect.GetComponent<TowerBuildEffect>().Boom();
        towerSpawner.sellEffect.transform.position = transform.position + Vector3.down / 2;

        // Ÿ�� �ı�
        Destroy(gameObject);
    }
}


/*
 * File : TowerWeapon.cs
 * Desc
 *	: ���� �����ϴ� Ÿ�� ����
 *	
 * Functions
 *	: ChangeState() - �ڷ�ƾ�� �̿��� FSM���� ���� ���� �Լ�
 *	: RotateToTarget() - target �������� o
 *	: SearchTarget() - ���� Ÿ���� ���� ������ �� Ž��
 *	: TryAttackGun() - target���� ������ ��󿡰� ĳ�� ����
 *	: TryAttackLaser() - target���� ������ ��󿡰� ������ ����
 *	: FindClosestAttackTarget() - ���� Ÿ���� ���� ������ ���� ���(��) Ž��
 *	: IsPossibleToAttackTarget() - AttackTarget�� �ִ���, ���� �������� �˻�
 *	: SpawnProjectile() - ĳ�� �߻�ü ����
 *	: EnableLaser() - ������, ������ Ÿ�� ȿ�� Ȱ��ȭ
 *	: DisableLaser() - ������, ������ Ÿ�� ȿ�� ��Ȱ��ȭ
 *	: SpawnLaser() - ������ ����, ������ Ÿ�� ȿ��, �� ü�� ����
 *	: Upgrade() - Ÿ�� ���׷��̵�
 *	: Sell() - Ÿ�� �Ǹ�
 *	
 */