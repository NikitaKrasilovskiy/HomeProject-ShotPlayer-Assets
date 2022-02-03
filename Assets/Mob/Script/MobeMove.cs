using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobeMove : MonoBehaviour
{
    public List<GameObject> gameObjectIntrigger = new List<GameObject>();
    [SerializeField] private GameObject targetPoint;
    [SerializeField] private GameObject[] checkPoint;
    [SerializeField] private float speed;   
    [SerializeField] private float stopDistance;
    [SerializeField] private GunFire gunFire;
    [SerializeField] private GunFireEnemy gunFireEnemy;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController controller;
    private Animator ani;    
    private bool haveTargetPoint;
    private int checkPointNumber;
    private bool endCheckPoint;
    private Vector3 checkDistance;
    

    [SerializeField] private bool enemy;
    private string enemyText;

    // Start is called before the first frame update
    void Start()
    {
        haveTargetPoint = false;
        endCheckPoint = false;        
        ani = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        checkPointNumber = 0;

        if (enemy == false)
        {
            enemyText = "Enemy";
        }
        else
        {
            enemyText = "";
        }
    }

    // Update is called once per frame
    void Update()
    {

        

        if (targetPoint == null || ReferenceEquals(targetPoint, null))
        {            
            haveTargetPoint = false;
            ClearListAndSearchNewTarget();
        }  
        
        if (targetPoint != null)
        {
            checkDistance = checkPoint[checkPointNumber].transform.position - transform.position;
            if (gunFire != null) gunFire.needFire = true;
            if (gunFireEnemy != null) gunFireEnemy.needFire = true;
            ani.SetBool("fire", true);
            Move(targetPoint);
        }
        else if (endCheckPoint == false && targetPoint == null)
        {
            checkDistance = checkPoint[checkPointNumber].transform.position - transform.position;
            if (gunFire != null) gunFire.needFire = false;
            if (gunFireEnemy != null) gunFireEnemy.needFire = false;
            ani.SetBool("fire", false);
            Move(checkPoint[checkPointNumber]);
        }
        if (haveTargetPoint == false && checkDistance.sqrMagnitude < stopDistance*stopDistance)
        {
            checkPointNumber++;           

            if (checkPointNumber >= checkPoint.Length)
            {
                endCheckPoint = true;
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 2)
        {
            
            if (other.gameObject.CompareTag(enemyText + "Turell"))
            {
                
                TargetPoint(other);
                
            }
            else if (other.gameObject.CompareTag(enemyText + "Mob"))
            {
                TargetPoint(other);
            }
            else if (other.gameObject.CompareTag(enemyText + "Player"))
            {
                TargetPoint(other);
            }
        }
    }
    public void OnTriggerExit (Collider other)
    {
       
        if (other.gameObject.layer == 2)
        {
            if (other.gameObject == targetPoint)
            {
                targetPoint = null;
                gameObjectIntrigger.Remove(other.gameObject);                
            }
            else if (other.gameObject.CompareTag(enemyText + "Turell") || other.gameObject.CompareTag(enemyText + "Mob") || other.gameObject.CompareTag(enemyText + "Player"))
            {
                gameObjectIntrigger.Remove(other.gameObject);
            }

        }
    }

    private void ClearListAndSearchNewTarget()
    {
        for (int i = 0; i < gameObjectIntrigger.Count; i++)
        {
            if (gameObjectIntrigger[i] == null)
            {
                gameObjectIntrigger.Remove(gameObjectIntrigger[i]);
            }            
        }
        for (int i = 0; i < gameObjectIntrigger.Count; i++)
        {
            if (gameObjectIntrigger[i].CompareTag(enemyText + "Turell") && haveTargetPoint == false)
            {
                targetPoint = gameObjectIntrigger[i];
                haveTargetPoint = true;
                return;
            }
        }
        for (int i = 0; i < gameObjectIntrigger.Count; i++)
        {
            if (gameObjectIntrigger[i].CompareTag(enemyText + "Mob") && haveTargetPoint == false)
            {
                targetPoint = gameObjectIntrigger[i];
                haveTargetPoint = true;
                return;
            }
        }
        for (int i = 0; i < gameObjectIntrigger.Count; i++)
        {
            if (gameObjectIntrigger[i].CompareTag(enemyText + "Player") && haveTargetPoint == false)
            {
                targetPoint = gameObjectIntrigger[i];
                haveTargetPoint = true;
                return;
            }
        }

    }

    private void TargetPoint(Collider other)
    {
        gameObjectIntrigger.Add(other.gameObject);
        if (haveTargetPoint == false)
        {
            targetPoint = other.gameObject;
            haveTargetPoint = true;
        }
    }

    private void Move(GameObject target)
    {
        var distance = target.transform.position - this.transform.position;
        transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        
        if (distance.sqrMagnitude >= stopDistance*stopDistance && controller.isGrounded)
        {
            moveDir = new Vector3(0, 0, 1);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
            ani.SetFloat("moveZ", 1);
            ani.SetBool("Run", true);
        }
        else 
        {
            ani.SetBool("Run", false);
            ani.SetFloat("moveX", 0);
            ani.SetFloat("moveZ", 0);
            moveDir = new Vector3(0, 0, 0);            
        }
        moveDir.y -= 30 * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);
    }
}
