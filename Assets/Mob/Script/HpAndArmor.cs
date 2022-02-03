using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpAndArmor : MonoBehaviour
{
    public float hp = 100;
    public float armor = 100;
    public float maxHp;
    public float maxArmor;
    private Animator ani;

   
    
    public float dmg = 0;
    public bool haveDmg;
    public float getExp;
    public int getMoney;
    // Start is called before the first frame update
    void Start()
    {
        haveDmg = false;
        maxHp = hp;
        maxArmor = armor;
        ani = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (haveDmg == true)
        {
           StartCoroutine(Damage());
        }

        if (hp<=0)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Damage()
    {

        yield return new WaitForSeconds(0.01f);
        if (armor<=0)
        {
            hp = hp - dmg;
        }
        else if (armor==maxArmor)
        {
            armor = armor - dmg;
        }
        else 
        {
            armor = armor - (1-((maxArmor - armor)/100))* dmg;
            hp = hp - (dmg - (1-((maxArmor - armor) / 100)) * dmg);
        }
        haveDmg = false;
    }
}
