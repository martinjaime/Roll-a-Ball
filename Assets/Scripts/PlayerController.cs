using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float g = 9.8f;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	// Use this for initialization
	void Start () {
		count = 0;
		rb = GetComponent<Rigidbody> ();
		SetCountText ();
		winText.text = "";
		ifMobile_scaleFontSize ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (SystemInfo.deviceType == DeviceType.Desktop) {
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			rb.AddForce (movement * speed);
		}
		else if (isOnMobile()) {
			var gravity = new Vector3 (
				Input.acceleration.x,
				0.0f,
				Input.acceleration.y
			) * g;
			rb.AddForce (gravity, ForceMode.Acceleration);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 11) {
			winText.text = "You Did It!!!";
		}
	}

	void ifMobile_scaleFontSize() {
		if (isOnMobile()) {
			winText.fontSize = 14 * 3;
		}
	}

	bool isOnMobile() {
		return SystemInfo.deviceType == DeviceType.Handheld;
	}
}