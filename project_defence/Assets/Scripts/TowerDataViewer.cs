using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private TextMeshProUGUI textLevel;
    [SerializeField]
    private TextMeshProUGUI textUpgradeCost;
    [SerializeField]
    private TextMeshProUGUI textUpgradeCost2;
    [SerializeField]
    private TextMeshProUGUI textSellCost;
    [SerializeField]
    private TowerAttackRange towerAttackRange;
    [SerializeField]
    private Button buttonUpgrade;
    [SerializeField]
    private Button buttonUpgrade2;
    [SerializeField]
    private SystemTextViewer systemTextViewer;
    [SerializeField]
    private GameObject PanelUpgrade;

    private TowerWeapon currentTower;

    private void Awake()
    {
        OffPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerWeapon)
    {
        // 출력해야하는 타워 정보를 받아와서 저장
        currentTower = towerWeapon.GetComponent<TowerWeapon>();
        // 타워 정보 Panel On, 타워업그레이드 Panel on
        PanelUpgrade.SetActive(true);
        gameObject.SetActive(true);
        // 타워 정보를 갱신
        UpdateTowerData();
        // 타워 오브젝트 주변에 표시되는 타워 공격범위 Sprite On
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }

    public void OffPanel()
    {
        // 타워 정보 Panel Off, 타워업그레이드 Panel off
        PanelUpgrade.SetActive(false);
        gameObject.SetActive(false);
        // 타워 공격범위 Sprite Off
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        if (currentTower.WeaponType == WeaponType.Gun || currentTower.WeaponType == WeaponType.Laser ||
            currentTower.WeaponType == WeaponType.Explosion || currentTower.WeaponType == WeaponType.Mortar ||
            currentTower.WeaponType == WeaponType.Shotgun || currentTower.WeaponType == WeaponType.Spear)
        {
            //imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
            textDamage.text = "Damage : " + currentTower.Damage
                            + "+" + "<color=red>" + currentTower.AddedDamage.ToString("F1") + "</color>";
        }
        else
        {
            //imageTower.rectTransform.sizeDelta = new Vector2(59, 59);

            if (currentTower.WeaponType == WeaponType.Slow)
            {
                textDamage.text = "Slow : " + currentTower.Slow * 100 + "%";
            }
            else if (currentTower.WeaponType == WeaponType.Buff)
            {
                textDamage.text = "Buff : " + currentTower.Buff * 100 + "%";
            }
        }
        //Debug.Log(currentTower.TileType);
        if (currentTower.TileType == TileType.One)
        {
            imageTower.rectTransform.sizeDelta = new Vector2(60, 120);
        }
        else if (currentTower.TileType == TileType.Two)
        {
            imageTower.rectTransform.sizeDelta = new Vector2(120, 120);
        }

        imageTower.sprite = currentTower.TowerSprite;
        textRate.text = "Rate : " + currentTower.Rate;
        textRange.text = "Range : " + currentTower.Range;
        textLevel.text = "Level : " + currentTower.Level;
        textUpgradeCost.text = currentTower.UpgradeCost.ToString();
        if (currentTower.Level == 2)
            textUpgradeCost2.text = currentTower.UpgradeCost2.ToString();
        textSellCost.text = currentTower.SellCost.ToString();


        // 업그레이드가 불가능해지면 버튼 비활성화
        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
        if(currentTower.UpgradeImage1 != null)
            buttonUpgrade.image.sprite = currentTower.UpgradeImage1;
        buttonUpgrade2.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
        if (currentTower.UpgradeImage2 != null)
            buttonUpgrade2.image.sprite = currentTower.UpgradeImage2;
        if(currentTower.Level == 3)
        {
            buttonUpgrade.transform.localScale = new Vector3(0.5f, 1, 1);
            buttonUpgrade2.transform.localScale = new Vector3(0.5f, 1, 1);
        }
        else
        {
            buttonUpgrade.transform.localScale = new Vector3(1, 1, 1);
            buttonUpgrade2.transform.localScale = new Vector3(1, 1, 1);
        }
        if (currentTower.Level != 3)
        {
            buttonUpgrade2.interactable = false;
        }

    }

    public void OnClickEventTowerUpgrade_1()
    {
        // 타워 업그레이드 시도 (성공:true, 실패:false)
        bool isSuccess = currentTower.Upgrade_1();

        if (isSuccess == true)
        {
            // 타워가 업그레이드 되었기 때문에 타워 정보 갱신
            UpdateTowerData();
            // 타워 주변에 보이는 공격범위도 갱신
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
        }
        else
        {
            // 타워 업그레이드에 필요한 비용이 부족하다고 출력
            systemTextViewer.PrintText(SystemType.Money);
        }
    }

    public void OnClickEventTowerUpgrade_2()
    {
        // 타워 업그레이드 시도 (성공:true, 실패:false)
        bool isSuccess = currentTower.Upgrade_2();

        if (isSuccess == true)
        {
            // 타워가 업그레이드 되었기 때문에 타워 정보 갱신
            UpdateTowerData();
            // 타워 주변에 보이는 공격범위도 갱신
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
        }
        else
        {
            // 타워 업그레이드에 필요한 비용이 부족하다고 출력
            systemTextViewer.PrintText(SystemType.Money);
        }
    }

    public void OnClickEventTowerSell()
    {
        // 타워 판매
        currentTower.Sell();
        // 선택한 타워가 사라져서 Panel, 공격범위 Off
        OffPanel();
    }
}


/*
 * File : TowerDataViewer.cs
 * Desc
 *	: 선택한 타워 정보 출력
 * 
 * Functions
 *	: OnPanel() - 타워 정보 패널 UI 활성화
 *	: OffPanel() - 타워 정보 패널 UI 비활성화
 *	: UpdateTowerData() - 타워 정보를 갱신해서 UI에 표시
 *	: OnClickEventTowerUpgrade() - 타워 업그레이드 버튼을 눌렀을 때 호출
 *	: OnClickEventTowerSell() - 타워 판매 버튼을 눌렀을 때 호출
 * 
 */