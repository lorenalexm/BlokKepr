#pragma warning disable 0108

using UnityEngine;
using System.Collections;

#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;
#endif

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	//------------------------------------------------
	#region Fields

	[SerializeField]
	private float maxSpeed = 6.0f;
	[SerializeField]
	private float movementForce = 100.0f;
	[SerializeField]
	private float jumpForce = 500.0f;
	[SerializeField]
	private LayerMask worldMask;
	[SerializeField]
	private float respawnDelay = 2.0f;

	private Rigidbody2D body = null;
	private BoxCollider2D collider = null;
	private Vector2 spawnPoint = Vector2.zero;
	private float inputAxis = 0.0f;
	private bool grounded = false;
	private bool disallowHorizontalForce = true;
	private bool jumpRequested = false;
	private bool dead = false;
	private bool paused = false;
	private float deathTime = 0.0f;
	private int playerNumber = 0;

	#endregion


	//------------------------------------------------
	#region Properties

	public int PlayerNumber {
		get {
			return this.playerNumber;
		}

		set {
			this.playerNumber = value;
		}
	}

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start() {
		this.collider = this.GetComponent<BoxCollider2D>();
		this.body = this.GetComponent<Rigidbody2D>();
		if(this.body != null) {
			this.body.freezeRotation = true;
		}
		this.spawnPoint = this.transform.position;
	}

	#endregion


	//------------------------------------------------
	#region OnEnable method

	private void OnEnable() {
		Messenger<GameObject>.AddListener("OnPlayerDeath", this.PlayerDeath);
		Messenger.AddListener("OnPause", this.Pause);
	}

	#endregion


	//------------------------------------------------
	#region OnDisable method

	private void OnDisable() {
		Messenger<GameObject>.RemoveListener("OnPlayerDeath", this.PlayerDeath);
		Messenger.AddListener("OnPause", this.Pause);
	}

	#endregion


	//------------------------------------------------
	#region Update method

	private void Update() {
		if(this.paused == true) {
			return;
		}

#if UNITY_ANDROID && !UNITY_EDITOR
		this.inputAxis = OuyaSDK.OuyaInput.GetAxis(this.playerNumber, OuyaController.AXIS_LS_X);
#else
		this.inputAxis = Input.GetAxis("Horizontal");
#endif
		Vector2 orgin = Vector2.zero;
		bool jumpButton = false;
		bool groundedLastFrame = this.grounded;
		float distance = 0.0f;
		float direction = Mathf.Sign(this.inputAxis);

		// Grounded collision
		distance = (this.transform.localScale.y / 2) + 0.1f;
		this.grounded = Physics2D.Raycast(this.transform.position, -this.transform.up, distance, this.worldMask);
		Debug.DrawRay(this.transform.position, -this.transform.up, Color.red);

		if(this.grounded == true && groundedLastFrame == false) {
			SoundManager.PlaySFX("Land");
		}

		// Horizontal collision
		distance = (this.transform.localScale.x / 2);
		for(int i = 0; i < 4; i++) {
			if(direction >= 0.0f) {
				orgin = new Vector2(this.collider.bounds.max.x, this.collider.bounds.min.y);
			} else if(direction <= 0.0f) {
				orgin = new Vector2(this.collider.bounds.min.x, this.collider.bounds.min.y);
			}
			orgin += Vector2.up * ((this.collider.size.x / 3) * i);
			this.disallowHorizontalForce = Physics2D.Raycast(orgin, Vector2.right * direction, distance, this.worldMask);
			if(this.disallowHorizontalForce == true) {
				break;
			}
			Debug.DrawRay(orgin, Vector2.right * direction, Color.red);
		}

#if UNITY_ANDROID && !UNITY_EDITOR
		jumpButton = OuyaSDK.OuyaInput.GetButtonDown(this.playerNumber, OuyaController.BUTTON_O);
#else
		jumpButton = Input.GetButtonDown("Jump");
#endif
		if(jumpButton == true && this.grounded == true) {
			this.jumpRequested = true;
		}

		if(dead == true && Time.time > (this.deathTime + respawnDelay)) {
			this.dead = false;
			this.body.velocity = Vector2.zero;
			this.transform.position = this.spawnPoint;
			MeshRenderer renderer = this.GetComponent<MeshRenderer>();
			if(renderer != null) {
				renderer.enabled = true;
			}

		}
	}

	#endregion


	//------------------------------------------------
	#region FixedUpdate method

	private void FixedUpdate() {
		if(this.inputAxis * this.body.velocity.x < this.maxSpeed && this.disallowHorizontalForce != true) {
			this.body.AddForce(Vector2.right * this.inputAxis * this.movementForce);
		}

		if(Mathf.Abs(this.body.velocity.x) > this.maxSpeed) {
			this.body.velocity = new Vector2(Mathf.Sign(this.body.velocity.x) * maxSpeed, this.body.velocity.y);
		}

		if(this.jumpRequested == true) {
			SoundManager.PlaySFX("Jump");
			this.body.AddForce(new Vector2(0.0f, this.jumpForce));
			this.jumpRequested = false;
		}
	}

	#endregion


	//------------------------------------------------
	#region Pause method

	private void Pause() {
		this.paused = !this.paused;

		if(this.body != null) {
			switch(this.paused) {
				case true:
					this.body.isKinematic = true;
					break;
				case false:
					this.body.isKinematic = false;
					break;
			}
		}
	}

	#endregion


	//------------------------------------------------
	#region PlayerDeath method

	private void PlayerDeath(GameObject obj) {
		if(this.gameObject.Equals(obj) == true) {
			this.dead = true;
			this.deathTime = Time.time;
			this.transform.position = new Vector3(0.0f, -100.0f, 0.0f);
			MeshRenderer renderer = this.GetComponent<MeshRenderer>();
			if(renderer != null) {
				renderer.enabled = false;
			}
		}
	}

	#endregion
}

#pragma warning restore 0108
