using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private Vector3 rotationAngle = Vector3.zero;
	[SerializeField]
	private float speed = 5.0f;

	#endregion


	//------------------------------------------------
	#region Update method

	private void Update() {
		this.transform.Rotate(this.rotationAngle * this.speed * Time.deltaTime);
	}

	#endregion
}
