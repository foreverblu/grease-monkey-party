using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
  private const string HORIZONTAL = "Horizontal";
  private const string VERTICAL = "Vertical";

  private float horizontal_input;
  private float vertical_input;
  private bool is_braking;
  private float brake_torque;
  private float steer_ang;

  public bool hasDriver;
  public bool hasDoor;
  public bool hasWindow;
  public bool hasWheel;
  public bool isBroken;

  [SerializeField] private float acc_force;
  [SerializeField] private float brake_force;
  [SerializeField] private float max_steer_ang;

  [SerializeField] private WheelCollider FRWC;
  [SerializeField] private WheelCollider FLWC;
  [SerializeField] private WheelCollider RRWC;
  [SerializeField] private WheelCollider RLWC;

  [SerializeField] private Transform FRWT;
  [SerializeField] private Transform FLWT;
  [SerializeField] private Transform RRWT;
  [SerializeField] private Transform RLWT;

  [SerializeField] private GameObject door;
  [SerializeField] private GameObject window;
  [SerializeField] private GameObject wheel;

  public void SetBroken()
  {
    hasDriver = false;
    hasDoor = false;
    hasWindow = false;
    hasWheel = false;
    isBroken = true;
    UpdateParts();
  }

  public void UpdatePart(string p)
  {
    if (p == "tire")
    {
      hasWheel = true;
    }
    else if (p == "window")
    {
      hasWindow = true;
    }
    else if (p == "door")
    {
      hasDoor = true;
    }
    isBroken = !(hasWheel && hasWindow && hasDoor);
    UpdateParts();
  }

  private void UpdateParts()
  {
    door.SetActive(hasDoor);
    window.SetActive(hasWindow);
    wheel.SetActive(hasWheel);
  }

  private void FixedUpdate()
  {
    if (hasDriver)
    {
      GetInput();
      HandleMotor();
      HandleSteering();
      UpdateWheels();
    }
  }

  private void GetInput()
  {
    horizontal_input = Input.GetAxis(HORIZONTAL);
    vertical_input = Input.GetAxis(VERTICAL);
    is_braking = Input.GetKey(KeyCode.Space);
  }

  private void HandleMotor()
  {
    FRWC.motorTorque = vertical_input * acc_force;
    FLWC.motorTorque = vertical_input * acc_force;
    brake_torque = is_braking ? brake_force : 0f;

    FRWC.brakeTorque = brake_torque;
    FLWC.brakeTorque = brake_torque;
    RRWC.brakeTorque = brake_torque;
    RLWC.brakeTorque = brake_torque;
  }

  private void HandleSteering()
  {
    steer_ang = max_steer_ang * horizontal_input;
    FRWC.steerAngle = steer_ang;
    FLWC.steerAngle = steer_ang;
  }

  private void UpdateWheels()
  {
    UpdateOneWheel(FRWC, FRWT);
    UpdateOneWheel(FLWC, FLWT);
    UpdateOneWheel(RRWC, RRWT);
    UpdateOneWheel(RLWC, RLWT);
  }

  private void UpdateOneWheel(WheelCollider wc, Transform t)
  {
    Vector3 pos;
    Quaternion rot;
    wc.GetWorldPose(out pos, out rot);
    t.position = pos;
    t.rotation = rot;
  }

  private void OnTriggerEnter(Collider c)
  {
    if (c.tag == "Endzone")
    {
      GameObject.Find("GameController").GetComponent<GameController>().GameOver(true);
    }
  }
}
