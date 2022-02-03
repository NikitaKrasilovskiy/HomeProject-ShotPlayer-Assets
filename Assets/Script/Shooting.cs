using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private ObjectPooler.ObjectInfo.ObjectType bulletType;

 
    private Vector3 spawnPosition;
    [SerializeField] private Transform gunPoint;

    [SerializeField] private float rateOfFire;
    [SerializeField] private GameObject fireLight;   
    private float currentTime;
    private int i;
    
  

    public bool needFire;
    // Start is called before the first frame update
    void Start()
    {
       fireLight.SetActive(false);
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition = gunPoint.transform.position;
        if (needFire == true && i <= 30)
        {
           
            currentTime += Time.deltaTime;
            if (currentTime > rateOfFire)
            {
                Debug.Log("fIRE");
                i++;
                currentTime = 0;
                var bullet = ObjectPooler.Instance.GetObject(bulletType);
                bullet.GetComponent<Bullet>().OnCreate(spawnPosition, transform.rotation);
                fireLight.SetActive(true);
                
            }
            else
            {               
                fireLight.SetActive(false);
                Debug.Log("sTOP");
            }
        }
        else if (i <= 30)
        {
            fireLight.SetActive(false);
        }
        else if (i > 30)
        {
            StartCoroutine(Reload());
            fireLight.SetActive(false);
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(3f);
        i = 0;
    }
}
