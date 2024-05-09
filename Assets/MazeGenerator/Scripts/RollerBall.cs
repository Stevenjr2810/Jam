using UnityEngine;
using System.Collections;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour {

	public GameObject ViewCamera = null;
	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;

	private AudioSource mAudioSource = null;
	private bool mFloorTouched = false;

	[Header("Movement")]
	public float moveSpeed;

	public Transform orientation;

	float horizontalInput;
	float verticalInput;

	Vector3 moveDirection;

	Rigidbody rb;




	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
		mAudioSource = GetComponent<AudioSource> ();
	}

    private void Update()
    {
        MyInput();
		SpeedControl();
    }

	private void FixedUpdate()
	{
		MovePlayer();
	}

    private void MyInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput =Input.GetAxisRaw("Vertical");
	}

	private void MovePlayer()
	{
		moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

		rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
	}

	private void SpeedControl()
	{
		Vector3 floatVel= new Vector3(rb.velocity.x, 0f, rb.velocity.z);

		if (floatVel.magnitude > moveSpeed)
		{
			Vector3 limitedVel = floatVel.normalized * moveSpeed;
			rb.velocity = new Vector3 (limitedVel.x, rb.velocity.y, limitedVel.z);
		}
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = true;
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		} else {
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		}
	}

	void OnCollisionExit(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Coin")) {
			if(mAudioSource != null && CoinSound != null){
				mAudioSource.PlayOneShot(CoinSound);
			}
			Destroy(other.gameObject);

            GameController.instance.CollectCoin();
        }
	}
}
