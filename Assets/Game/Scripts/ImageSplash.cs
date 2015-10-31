using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ImageSplash : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private float delay = 2.0f;

	private float startTime = 0.0f;

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start() {
		this.startTime = Time.time;
	}

	#endregion


	//------------------------------------------------
	#region Update method

	private void Update() {
		if(Time.time > (this.startTime + this.delay)) {
			Application.LoadLevel("AspectAdjust");
		}
	}

	#endregion
}
