using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDate : MonoBehaviour
{
    public float exp;
    public int money;
    private int lvl;
    public int lvlPoint;
    public float reloadSkill;
    public float reloadSkillArmor;
    public float dmgUp;
    private float hp;
    private float armor;
    private float maxHp;
    private float maxArmor;
    private HpAndArmor hAP;
    private float needExp;
    private int ammo;
    private int ammoIn;

    private Gun gun;


    [SerializeField] private Slider expS;
    [SerializeField] private Slider hpS;
    [SerializeField] private Slider armorS;

    [SerializeField] private Text expT;
    [SerializeField] private Text hpT;
    [SerializeField] private Text ArmorT;
    [SerializeField] private Text AmmoT;
    
    // Start is called before the first frame update
    void Start()
    {
        exp = 0;
        money = 0;
        lvlPoint = 1;
        reloadSkillArmor = 100;
        reloadSkill = 1;
        hAP = GetComponent<HpAndArmor>();
        maxHp = hAP.maxHp;
        maxArmor = hAP.maxArmor;
        needExp = 50 * (exp + 1);
        gun = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        UpDateData();        
        if (exp >= 50 * (exp+1) && lvl < 20)
        {
            lvl++;
            lvlPoint++;           
        }

        #region UpDate Canvas
        Vector3 hpV = Vector3.one;
        Vector3 armorV = Vector3.one;
        Vector3 expV = Vector3.one;

        hpS.value = hp / maxHp;
        expS.value = exp / needExp;
        armorS.value = armor / maxArmor;

        hpT.text = string.Format("{0:0}", hp) + "/" + string.Format("{0:0}", maxHp);
        ArmorT.text = string.Format("{0:0}", armor) + "/" + string.Format("{0:0}", maxArmor);
        expT.text = string.Format("{0:0}", exp) + "/" + string.Format("{0:0}", needExp);
        AmmoT.text = ammoIn + "/" + ammo;
        #endregion

    }

    private void UpDateData()
    {
        hp = hAP.hp;
        armor = hAP.armor;
        maxHp = hAP.maxHp;
        maxArmor = hAP.maxArmor;
        needExp = 50 * (exp + 1);
        ammo = gun.ammo;
        ammoIn = gun.ammoIn;
    }

}
