using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMove : MonoBehaviour
{
    public int firstExp;
    public int secondExp;
    public int thirdExp;
    private bool thirdBool;
    private PlayerDate pd;
   
    [SerializeField] private GameObject thirdSkill;
    [SerializeField] private Animator ani;
    [SerializeField] Camera _cam;
    [SerializeField] private float firstDistance;
    [SerializeField] private float freezTime;
    [SerializeField] LayerMask lm;
    [SerializeField] GameObject oneb;
    [SerializeField] GameObject twob;
    [SerializeField] GameObject freeb;
   
    
    // Start is called before the first frame update
    void Start()
    {
        firstExp = 0;
        secondExp = 0;
        thirdExp = 0;
        pd = GetComponent<PlayerDate>();
        thirdSkill.SetActive(false);
        thirdBool = false;
        lm = 2;
        oneb.SetActive(false);
        twob.SetActive(false);
        freeb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if (pd.lvlPoint > 0)
       {
            LvlUp();
       }

        if (firstExp > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FirstSkill();
                oneb.SetActive(true);
            }            
        }

       if (secondExp > 0)
       {
            SecondSkill();
            twob.SetActive(true);
        }

       if (thirdExp > 0)
       {
            ThirdSkill();
            freeb.SetActive(true);
       }

    }
    void LvlUp()
    {
      
            if (Input.GetKeyDown(KeyCode.F1))
            {
                firstExp++;
                pd.lvlPoint--;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                secondExp++;
                pd.lvlPoint--;
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                thirdExp++;
                pd.lvlPoint--;
            }
        
    }

    void FirstSkill()
    {
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(new Vector3(_cam.pixelWidth * 0.5f, _cam.pixelHeight * 0.5f , 0));
        if (Physics.Raycast(ray, out hit, firstDistance))
        {
           

        }
    }

   

   
    void SecondSkill()
    {

    }
    void ThirdSkill()
    {
        thirdSkill.SetActive(true);
        thirdSkill.GetComponent<HP>().factor = thirdSkill.GetComponent<HP>().factor - 0.1f * (thirdExp - 1) - pd.reloadSkill*0.01f;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            thirdBool = !thirdBool;
            ani.SetBool("OnOff",thirdBool);
        }
    }
}
