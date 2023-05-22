using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum Axle
{
    Front,
    Back,
}

public struct Wheel
{
    public GameObject model;
    public WheelCollider wheelCollider;
    public Axle axle;
}

[RequireComponent(typeof(Rigidbody))]

public class CarController : MonoBehaviour
{
    [SerializeField] private float maxAcceleration = 60f;
    [SerializeField] private float turnSensitive = 1.0f;
    [SerializeField] private float maxAngleRotate = 60.0f;

    private float inputX;
    private float inputY;

    private Rigidbody rigidbody;

    public List<AxleInfo> axleInfos = new List<AxleInfo>();

    [SerializeField] private GameObject camara1;
    [SerializeField] private GameObject camara2;
    [SerializeField] private GameObject camara3;


    public AudioSource claxon;
    public AudioSource Movimiento;
    public AudioSource Motor;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInputs();
        Move();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            camara1.SetActive(true);
            camara2.SetActive(false);
            camara3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camara1.SetActive(false);
            camara2.SetActive(true);
            camara3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            camara1.SetActive(false);
            camara2.SetActive(false);
            camara3.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            claxon.Play();
        }
    }

    private void Move()
    {
        foreach (AxleInfo info in axleInfos)
        {
            if (info.isBack)
            {
                info.rigthWheel.motorTorque = inputY * maxAcceleration * 2000 * Time.deltaTime;
                info.leftWheel.motorTorque = inputY * maxAcceleration * 2000 * Time.deltaTime;
            }
            if (info.isFront)
            {
                var _steerAngle = inputX * turnSensitive * maxAngleRotate;
                info.rigthWheel.steerAngle = Mathf.Lerp(info.rigthWheel.steerAngle, _steerAngle, 1f);
                info.leftWheel.steerAngle = Mathf.Lerp(info.leftWheel.steerAngle, _steerAngle, 1f);
            }

            AnimateWheels(info.rigthWheel, info.visualRigthWheel);
            AnimateWheels(info.leftWheel, info.visualLeftWheel);
        }
    }

    private void AnimateWheels(WheelCollider Wheel, Transform visualWheel)
    {
        Quaternion _rot;
        Vector3 _pos;

        Vector3 rotate = Vector3.zero;

        Wheel.GetWorldPose(out _pos, out _rot);
        visualWheel.transform.rotation = _rot;
    }

    private void GetInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }
}


[System.Serializable]
public class AxleInfo
{
    public WheelCollider rigthWheel;
    public WheelCollider leftWheel;

    public Transform visualRigthWheel;
    public Transform visualLeftWheel;

    public bool isBack;
    public bool isFront; 
}
