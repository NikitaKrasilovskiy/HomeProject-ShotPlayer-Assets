using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turell : MonoBehaviour
{
    [SerializeField] private Transform tr;    
    [SerializeField] private bool enemy;
    [SerializeField] private GameObject targetPoint;
    public List<GameObject> gameObjectIntrigger = new List<GameObject>();
    private string enemyText;
    private bool haveTargetPoint;
   
    [SerializeField] private GunFire[] gunFire;
    // Start is called before the first frame update
    void Start()
    {
        haveTargetPoint = false;
        if (enemy == false)
        {
            enemyText = "Enemy";
        }
        else
        {
            enemyText = "";
        }
      
        
        for (int i = 0; i < gunFire.Length; i++)
        {
                gunFire[i].needFire = false;
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPoint == null)
        {
            transform.LookAt(tr);
            Debug.Log("work search");
            ClearListAndSearchNewTarget();
            for (int i = 0; i < gunFire.Length; i++)
            {
                gunFire[i].needFire = false;
                gunFire[i].fireLight.SetActive(false);
            }
        }
        else
        {
            transform.LookAt(new Vector3 (targetPoint.transform.position.x,0.8f, targetPoint.transform.position.z) );
            for (int i = 0; i < gunFire.Length; i++)
            {
                gunFire[i].needFire = true;
                gunFire[i].fireLight.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        
            if (other.gameObject.CompareTag(enemyText + "Mob") || other.gameObject.CompareTag(enemyText + "Player"))
            {
                gameObjectIntrigger.Add(other.gameObject);
            }
            if (haveTargetPoint == false && (other.gameObject.CompareTag("EnemyMob")))
            {
                targetPoint = other.gameObject;
                haveTargetPoint = true;
            }
            else if (haveTargetPoint == false && (other.gameObject.CompareTag(enemyText + "Player")))
            {
                targetPoint = other.gameObject;
                haveTargetPoint = true;
            }
        

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 2)
        {
            if (other.gameObject == targetPoint)
            {
                targetPoint = null;
            }
            else if (other.gameObject.CompareTag(enemyText + "Mob") || other.gameObject.CompareTag(enemyText + "Player"))
            {
                gameObjectIntrigger.Remove(other.gameObject);
            }

        }
    }

    private void ClearListAndSearchNewTarget()
    {
        Debug.Log("SSSSSSSSSSS");
        for (int i = 0; i < gameObjectIntrigger.Count; i++)
        {
            if (gameObjectIntrigger[i] == null)
            {
                gameObjectIntrigger.Remove(gameObjectIntrigger[i]);
            }
        }        
        for (int i = 0; i < gameObjectIntrigger.Count; i++)
        {
            if (gameObjectIntrigger[i].CompareTag(enemyText + "Mob") && haveTargetPoint == false)
            {
                targetPoint = gameObjectIntrigger[i];
                haveTargetPoint = true;
                Debug.Log(i);
            }
        }
        if (haveTargetPoint == false)
        {
            for (int i = 0; i < gameObjectIntrigger.Count; i++)
            {
                if (gameObjectIntrigger[i].CompareTag(enemyText + "Player") && haveTargetPoint == false)
                {
                    targetPoint = gameObjectIntrigger[i];
                    haveTargetPoint = true;
                }
            }
        }

    }
    
}
