using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour {

	public float speed;
	public float maxSpeed;
	Rigidbody rb;
	Vector3 jumpForceMiddletoLeft = new Vector3(-11,11,0.0f);
	Vector3 jumpForceMiddletoRight = new Vector3(11,11,0.0f);
	Vector3 jumpForceRighttoMiddle = new Vector3(-11,25,0.0f);
	Vector3 jumpForceLefttoMiddle = new Vector3(11,25,0.0f);
	public bool InAir;
	private string currentPlatform = "MiddlePlatform";

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		InAir = false;
	}

	void OnCollisionEnter(Collision other){
		string tag = other.gameObject.tag;
		if ((tag == "LeftPlatform" || tag == "MiddlePlatform" || tag == "RightPlatform") && InAir==true){
			InAir = false;
			// Constrain X Velocity
			rb.velocity = new Vector3(0,0,rb.velocity.z);
		}
	}

	void OnCollisionStay(Collision other){
		if (other.gameObject.tag == "Platform" && InAir == true) {
			InAir = false;
		}
	}

	void OnCollisionExit(Collision other) {
		InAir = true;
	}

	// Update is called once per frame
	void Update () {
		rb.AddForce(new Vector3(0,0, speed * Time.deltaTime));
		if (rb.velocity.z > maxSpeed) {
			rb.AddForce (new Vector3 (0, 0, -speed / 5 * Time.deltaTime));
		}
		Debug.Log (rb.velocity.z);

		// Jump Left
		if(Input.GetKeyDown("a") && InAir==false) {
			if (currentPlatform == "RightPlatform") {
				rb.AddForce (jumpForceRighttoMiddle);
				currentPlatform = "MiddlePlatform";
			} else if (currentPlatform == "MiddlePlatform") {
				rb.AddForce (jumpForceMiddletoLeft);
				currentPlatform = "LeftPlatform";
			} else if (currentPlatform == "LeftPlatform") {
				rb.AddForce (jumpForceMiddletoLeft);
			}
		}

		// Jump Right
		if (Input.GetKeyDown ("d") && InAir == false) {
			if (currentPlatform == "RightPlatform") {
				rb.AddForce (jumpForceMiddletoRight);
			} else if (currentPlatform == "MiddlePlatform") {
				rb.AddForce (jumpForceMiddletoRight);
				currentPlatform = "RightPlatform";
			} else if (currentPlatform == "LeftPlatform") {
				rb.AddForce (jumpForceLefttoMiddle);
				currentPlatform = "MiddlePlatform";
			}
		}

		//Death
		if (transform.position.y <= -1.5) {
			SceneManager.LoadScene("Game");
		}
	}
}