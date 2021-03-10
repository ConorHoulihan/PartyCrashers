 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Transform cam;
    [SerializeField]
    float speed = .06f;
    [SerializeField]
    float jumpPower;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    //bool isTouching = false;
    

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //rb.AddForce(moveDir.normalized * speed * Time.deltaTime);
            rb.MovePosition(rb.position+moveDir.normalized * speed * Time.deltaTime);

            //Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown("space"))
        { //jump
            rb.AddForce(new Vector3(0, jumpPower, 0) * 50, ForceMode.Acceleration);
        }
    }
}
/*public class PlayerController : MonoBehaviour {
  
      public float moveSpeed = 12;
  
      Rigidbody rigidBody;
      float moveX;
      float moveZ;
  
      bool isTouching = false;
      public float jumpPower;
  
      public Transform _camera;
  
      void Start () {
          rigidBody = this.GetComponent<Rigidbody> ();
  
          if (rigidBody == null)
              Debug.LogError ("RigidBody could not be found.");
      }
  
      void Update () {
          public float moveX = Input.GetAxis ("Horizontal");
          public float moveZ = Input.GetAxis ("Vertical");
 
  
          public Vector3 moveVector = new Vector3(transform.position.x*moveX, 0f, (transform.position.z-camera.transform.position.z)*moveZ);
      }
  
      void FixedUpdate () {
 
          if (rigidBody != null) {
              rigidBody.AddForce (moveVector * moveSpeed, ForceMode.Acceleration);
          }
          if (isTouching && Input.GetKeyDown("space")) { //jump
              rigidBody.AddForce(new Vector3(0, jumpPower, 0) * 50, ForceMode.Acceleration);
          }
      }
  
      private void OnCollisionEnter(Collision collision) {
          isTouching = true;
      }
      private void OnCollisionExit(Collision collision) {
          isTouching = false;
      }
  }*/