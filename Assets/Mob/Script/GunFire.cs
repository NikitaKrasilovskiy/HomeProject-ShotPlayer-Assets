using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public GameObject fireLight;

    [SerializeField] private Transform gunPoint;
    [SerializeField] private float timeout = 0.5f;
    [SerializeField] private float dmg;
    [SerializeField] private Rigidbody bulletEnd;
    // [SerializeField] private Rigidbody bulletEnd;
    private float curTimeout;
    public bool needFire = false;
  //  private AudioSource shoot;
    // Start is called before the first frame update
    void Start()
    {
       // fireLight.SetActive(false);
        //shoot = GetComponent<AudioSource>();
       // shoot.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (needFire == true)
        {
            fireLight.SetActive(true);
            curTimeout += Time.deltaTime;
          
            if (curTimeout > timeout)
            {
                curTimeout = 0;
              //  shoot.Stop();
              //  shoot.Play();
                RaycastHit hit;
                Ray ray = new Ray(gunPoint.position, gunPoint.forward);
                
                

                if (Physics.Raycast(ray, out hit, 20))
                {
                    if (hit.collider.transform.gameObject.CompareTag("EnemyTurell") || hit.collider.transform.gameObject.CompareTag("EnemyMob") || hit.collider.transform.gameObject.CompareTag("EnemyPlayer"))
                    {
                        hit.collider.transform.gameObject.GetComponent<HP>().dmg = dmg;
                        hit.collider.transform.gameObject.GetComponent<HP>().haveDmg = true;
                        
                      //  Rigidbody bulletInstance = Instantiate(bulletEnd, hit.point, Quaternion.identity) as Rigidbody;
                    }                   
                   
                }
            }
        }
        else
        {
            
          //  fireLight.SetActive(false);
        }
    }
}
