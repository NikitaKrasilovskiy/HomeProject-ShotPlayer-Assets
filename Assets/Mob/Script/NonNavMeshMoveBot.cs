using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonNavMeshMoveBot : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoint;
    [SerializeField] private float stopDistance;
    [SerializeField] private float acselerate;
    [SerializeField] private float realMaxSpeed;
    [SerializeField] private GunFire gunFire;
    [SerializeField] private GunFireEnemy gunFireEnemy;
    [SerializeField] private bool enemy;
    private string enemyText;
    private GameObject saveGameObject;
    private bool dontSearchTarget;
    private bool inTrigger;
    private float localX;    
    private float localZ;
    private float lockX;
    private float lockY;
    private float lockZ;
    private bool stop;
    private Animator ani;
    private Rigidbody rb;
    [SerializeField] private int i = 0;

   
    // Start is called before the first frame update
    void Start()
    {        
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        stop = false;
        inTrigger = false;
        i = 0;
        dontSearchTarget = false;
        if (enemy == false)
        {
            enemyText = "Enemy";
        }
        else
        {
            enemyText = "";
        }
    }

    private void Update()
    {
        localX = (transform.InverseTransformDirection(rb.velocity)).x;
        localZ = (transform.InverseTransformDirection(rb.velocity)).z;
        
        if (saveGameObject == null)
        {
            inTrigger = false;
            ani.SetBool("fire", false);           
            gunFire.needFire = false;
            gunFireEnemy.needFire = false;
            dontSearchTarget = false;
        }
        if (inTrigger == false && stop == false)
        {
            Navigation();
            transform.LookAt(new Vector3(checkPoint[i].position.x,0, checkPoint[i].position.z));
        }
        if (dontSearchTarget == true) TriggerRun(saveGameObject);
        Animation();
    }

    // Update is called once per frame

    #region Navigation
    void Navigation()
    {      
        var distance = checkPoint[i].transform.position - this.transform.position;
        
      
        if (distance.sqrMagnitude <= stopDistance*stopDistance)
        {
            i++;
            rb.velocity = new Vector3(0, 0, 0);          
        }
        else if (i >= checkPoint.Length-1)
        {
            rb.velocity = new Vector3(0, 0, 0);
            stop = true;
        }
        else
        {
            if (localX * localX <= realMaxSpeed*realMaxSpeed && localZ * localZ <= realMaxSpeed*realMaxSpeed)
            {
                rb.AddForce(this.transform.forward * acselerate, ForceMode.Impulse);
            }
            else
            {
                lockX = rb.velocity.x; 
                lockY = rb.velocity.y;
                lockZ = rb.velocity.z;
                rb.velocity = new Vector3(lockX, lockY, lockZ);
            }
        }
       
    }
    #endregion Navigation

    #region Animation

    void Animation()
    {
        if (rb.velocity.z*rb.velocity.z !=0.4 || rb.velocity.x * rb.velocity.x != 0.4)
        {
            ani.SetFloat("moveX", localX / realMaxSpeed);
            ani.SetFloat("moveZ", localZ / realMaxSpeed);
            ani.SetBool("Run", true);
        }
        else 
        {
            ani.SetBool("Run", false);
            ani.SetFloat("moveX", 0);
            ani.SetFloat("moveZ", 0);
        }
    }

    #endregion Animation

    #region Fire and EnemyEnter
    private void OnTriggerEnter(Collider other)
    {
        if (dontSearchTarget == false)
        {
            if (other.gameObject.CompareTag(enemyText+"Turell"))
            {
                TriggerRun(other.gameObject);
            }
            else if (other.gameObject.CompareTag(enemyText + "Mob"))
            {
                TriggerRun(other.gameObject);
            }
            else if (other.gameObject.CompareTag(enemyText + "Player"))
            {
                TriggerRun(other.gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(enemyText + "Turell"))
        {
            TriggerRun(other.gameObject);
        }
        else if (other.gameObject.CompareTag(enemyText + "Mob"))
        {
            TriggerRun(other.gameObject);
        }
        else if (other.gameObject.CompareTag(enemyText + "Player"))
        {
            TriggerRun(other.gameObject);
        }
    }
        

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == saveGameObject)
        {
            saveGameObject = null;
            inTrigger = false;
            ani.SetBool("fire", false);
            if (gunFire != null) gunFire.needFire = false;
            if (gunFireEnemy != null) gunFireEnemy.needFire = false;
        }
    }
   
    #endregion Fire and EnemyEnter
  
    #region Programs
    private void TriggerRun(GameObject other)
    {
        inTrigger = true;
        ani.SetBool("fire", true);
        transform.LookAt(new Vector3(other.transform.position.x,0,other.transform.position.z));

        var distance = other.transform.position - this.transform.position;

        saveGameObject = other;     


        if (distance.sqrMagnitude <= stopDistance * stopDistance)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        else if (localX * localX <= realMaxSpeed * realMaxSpeed && localZ * localZ <= realMaxSpeed * realMaxSpeed)
        {
            rb.AddForce(this.transform.forward * acselerate, ForceMode.Impulse);
        }
        else
        {
            lockX = rb.velocity.x;
            lockY = rb.velocity.y;
            lockZ = rb.velocity.z;
            rb.velocity = new Vector3(lockX, lockY, lockZ);
        }
        if (gunFire != null) gunFire.needFire = true;
        if (gunFireEnemy != null) gunFireEnemy.needFire = true;
        dontSearchTarget = true;
    }
   
    #endregion Programs
   
}

