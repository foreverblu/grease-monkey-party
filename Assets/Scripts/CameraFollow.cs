using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  [SerializeField] private Vector3 offset;
  [SerializeField] private Transform target;
  [SerializeField] private float translate_speed;
  [SerializeField] private float rotation_speed;

  private void FixedUpdate()
  {
    HandleTranslation();
    HandleRotation();
  }

  private void HandleTranslation()
  {
    var target_pos = target.TransformPoint(offset);
    GetComponent<Transform>().position = Vector3.Lerp(GetComponent<Transform>().position, target_pos, translate_speed * Time.deltaTime);
  }

  private void HandleRotation()
  {
    var direction = target.position - transform.position;
    var rotation = Quaternion.LookRotation(direction, Vector3.up);
    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotation_speed * Time.deltaTime);
  }
}
