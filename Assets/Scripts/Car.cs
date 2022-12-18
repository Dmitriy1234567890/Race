using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int currentCheckpoint=-1;

    [SerializeField] Rigidbody sphere;
    [SerializeField] float forwardAccel=8;
    [SerializeField] float backAccel=4;
    [SerializeField] float turnStrength=180;
    [SerializeField] float gravityForce = 9.78f;
    [SerializeField] float groundDistance = 0.45f;
    [SerializeField] Transform pistol;
    [SerializeField] LayerMask groundMask;

    [Header("Wheels")]
    [SerializeField] float maxTurn=25;
    [SerializeField] Transform leftWheel;
    [SerializeField] Transform rightWheel;

    private bool onGround;
    private float speed;
    private float speedInput;
    private float turnInput;

    RaycastHit hit;


    void Start()
    {
        sphere.transform.SetParent(null);
    }

    void Update()
    {

        onGround = Physics.Raycast(pistol.position, -transform.up, out hit, groundDistance, groundMask);
        //Debug.Log(onGround);

        if (onGround)
        {
            //transform.rotation *= Quaternion.FromToRotation(transform.up, hit.normal);
        }

        speedInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        Accelerate();
        Rotate();
        PlaceSphere();
        RotateWheels();
    }

    void FixedUpdate()
    {
        if (!onGround)
        {
            sphere.AddForce(Vector3.down * gravityForce * 300);
            return;
        }
        if (speed!=0)
        {
            sphere.AddForce(transform.forward * speed);
        }
    }

    void Rotate()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y += turnInput * turnStrength * speedInput * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
    }

    void Accelerate()
    {
        float accel = speedInput > 0 ? forwardAccel : backAccel;
        speed = accel * speedInput * 1000;
    }

    void PlaceSphere()
    {
        Vector3 spherePosition = sphere.transform.position;
        spherePosition.y -= 0.5f;
        transform.position = spherePosition;
    }
    void RotateWheels()
    {
        rightWheel.localRotation = Quaternion.Euler(
            rightWheel.localRotation.eulerAngles.x,
            turnInput*maxTurn,
            rightWheel.localRotation.eulerAngles.z);
        leftWheel.localRotation = Quaternion.Euler(
            leftWheel.localRotation.eulerAngles.x,
            turnInput * maxTurn-180,
            leftWheel.localRotation.eulerAngles.z);
    }
}
