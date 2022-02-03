using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPolledObject
{
    public float dmg = 2;

    public ObjectPooler.ObjectInfo.ObjectType Type => type;

    [SerializeField]
    private ObjectPooler.ObjectInfo.ObjectType type;

    private float lifeTime = 0.5f;
    private float currentLifeTime;
    [SerializeField] private float speed = 10;


    public void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        currentLifeTime = lifeTime;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if ((currentLifeTime -= Time.deltaTime)<0)
        {
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
    }
}
