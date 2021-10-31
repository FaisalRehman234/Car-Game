using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float motorForce;
    private float m_steerAngle;

    public float maxMotorSpeed = 1000;
    public float maxSteerAngle = 40;

    public Transform wheelFL, wheelFR;
    public Transform wheelRL, wheelRR;
    public WheelCollider wheelCFL, wheelCFR;
    public WheelCollider wheelCRL, wheelCRR;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        GetInput();
        UpdatePoses();
        Steer();
        Acceleration();
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void UpdatePoses()
    {
        UpdateWheelPose(wheelCFL, wheelFL);
		UpdateWheelPose(wheelCFR, wheelFR);
		UpdateWheelPose(wheelCRL, wheelRL);
		UpdateWheelPose(wheelCRR, wheelRR);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}


    void Steer()
    {
        m_steerAngle = maxSteerAngle * horizontalInput;
        wheelCFL.steerAngle = m_steerAngle;
        wheelCFR.steerAngle = m_steerAngle;
    }

    void Acceleration()
    {
        motorForce = maxMotorSpeed * verticalInput;

        wheelCFL.motorTorque = motorForce;
        wheelCFR.motorTorque = motorForce;
        wheelCRL.motorTorque = motorForce;
        wheelCRR.motorTorque = motorForce;
    }
}
