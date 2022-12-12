using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPViewer : MonoBehaviour
{
    private PlayerHP playerHP;
    private Slider hpSlider;
    // Start is called before the first frame update

    public void setUp(PlayerHP playerHP)
    {
        this.playerHP = playerHP;
        hpSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
        hpSlider.value = playerHP.CurrentHP / playerHP.MaxHP;
    }
}
