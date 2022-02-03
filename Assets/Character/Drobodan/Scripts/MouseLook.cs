using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Camera-control/MouseLook")]
public class MouseLook : MonoBehaviour
{
    [SerializeField] private enum RotationAxes {MouseXandY = 0, MouseX = 1, MouseY=2 };
    [SerializeField] private RotationAxes axes = RotationAxes.MouseXandY;
    public float sensyvity;
    [SerializeField] public float minimumX = -360;
    [SerializeField] public float maximumX = 360;
    [SerializeField] public float minimumY = -360;
    [SerializeField] public float maximumY = 360;
    private float rotationX;
    private float rotationY;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;            
        }
        originalRotation = transform.localRotation;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseXandY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensyvity;
            rotationY += Input.GetAxis("Mouse Y") * sensyvity;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensyvity;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else if (axes == RotationAxes.MouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensyvity;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }   
}
