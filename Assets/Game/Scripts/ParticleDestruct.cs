using UnityEngine;
using System.Collections;

public class ParticleDestruct : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	private ParticleSystem ps = null;

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start() {
		this.ps = this.GetComponent<ParticleSystem>();
	}

	#endregion


	//------------------------------------------------
	#region Update method

	private void Update() {
		if(this.ps != null) {
			if(this.ps.IsAlive() == false) {
				Destroy(this.gameObject);
			}
		}
	}

	#endregion
}
