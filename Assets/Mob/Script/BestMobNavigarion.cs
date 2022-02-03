using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestMobNavigarion : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoint;
    [SerializeField] private float stopDistance;
    [SerializeField] private float speed;    
    [SerializeField] private GunFire gunFire;
    [SerializeField] private GunFireEnemy gunFireEnemy;
    [SerializeField] private bool enemy;
    
   
        
    public float gravity = 20f;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController controller;
    private string enemyText;
    private GameObject saveGameObject;
    private bool dontSearchTarget;
    private bool inTrigger;    
    private bool stop;
    private Animator ani;   
    [SerializeField] private int i = 0;


    // Start is called before the first frame update
    void Start()
    {        
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
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {      

        if (saveGameObject == null)
        {
            inTrigger = false;
            ani.SetBool("fire", false);
            gunFire.needFire = false;
            gunFireEnemy.needFire = false;
            dontSearchTarget = false;
        }
        if (inTrigger == false && stop == false && controller.isGrounded)
        {
            Navigation();
            transform.LookAt(new Vector3(checkPoint[i].position.x, 0, checkPoint[i].position.z));
        }
        if (dontSearchTarget == true) TriggerRun(saveGameObject);
       
    }

    // Update is called once per frame

    #region Navigation
    void Navigation()
    {
        var distance = checkPoint[i].transform.position - this.transform.position;


        if (distance.sqrMagnitude <= stopDistance * stopDistance)
        {
            i++;
            ani.SetBool("Run", false);
            ani.SetFloat("moveX", 0);
            ani.SetFloat("moveZ", 0);
        }
        else if (i >= checkPoint.Length - 1)
        {
            moveDir = new Vector3(0, 0, 0);
            ani.SetBool("Run", false);
            ani.SetFloat("moveX", 0);
            ani.SetFloat("moveZ", 0);
        }
        else
        {
            moveDir = new Vector3(0, 0, 1);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
            ani.SetFloat("moveZ", 1);
            ani.SetBool("Run", true);
        }
        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);            

    }
    #endregion Navigation

   

    #region Fire and EnemyEnter
    private void OnTriggerEnter(Collider other)
    {
        if (dontSearchTarget == false)
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
    }
    /*private void OnTriggerStay(Collider other)
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
    }*/


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
        transform.LookAt(new Vector3(other.transform.position.x, 0, other.transform.position.z));

        var distance = other.transform.position - this.transform.position;

        saveGameObject = other;


        if (distance.sqrMagnitude <= stopDistance * stopDistance)
        {
            moveDir = new Vector3(0, 0, 0);
            ani.SetBool("Run", false);
            ani.SetFloat("moveX", 0);
            ani.SetFloat("moveZ", 0);
        }
        else 
        {
            moveDir = new Vector3(0, 0, 1);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;           
            ani.SetFloat("moveZ", 1);
            ani.SetBool("Run", true);
        }
        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);

        if (gunFire != null) gunFire.needFire = true;
        if (gunFireEnemy != null) gunFireEnemy.needFire = true;
        dontSearchTarget = true;
    }

    #endregion Programs
}
