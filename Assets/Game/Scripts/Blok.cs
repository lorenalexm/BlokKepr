using UnityEngine;
using System.Collections;

public class Blok : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private float gravityForce = 35.0f;
	[SerializeField]
	private float areaOfEffect = 3.5f;
	[SerializeField]
	private bool ignoreGravity = false;

	private Rigidbody2D body = null;
	private GameObject[] scoreZones;
	private bool paused = false;

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start () {
		this.body = this.GetComponent<Rigidbody2D>();
		this.scoreZones = GameObject.FindGameObjectsWithTag("Score");
	}

	#endregion


	//------------------------------------------------
	#region OnEnable method

	private void OnEnable() {
		Messenger.AddListener("OnPause", this.Pause);
	}

	#endregion


	//------------------------------------------------
	#region OnDisable method

	private void OnDisable() {
		Messenger.RemoveListener("OnPause", this.Pause);
	}

	#endregion


	//------------------------------------------------
	#region FixedUpdate method

	private void FixedUpdate() {
		if(this.ignoreGravity == true) {
			return;
		}

		foreach(GameObject zone in this.scoreZones) {
			float distance = Vector2.Distance(zone.transform.position, this.transform.position);
			if(distance <= this.areaOfEffect) {
				Vector2 vec = zone.transform.position - this.transform.position;
				if(this.body != null) {
					this.body.AddForce(vec.normalized * (1.0f - distance / this.areaOfEffect) * this.gravityForce);
				}
			}
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
}
