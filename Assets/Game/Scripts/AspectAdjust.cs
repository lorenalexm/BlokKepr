using UnityEngine;
using System.Collections;

#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;
#endif

public class AspectAdjust : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private float inputDelay = 0.25f;

	private float safeArea = 1.0f;
	private float lastInput = 0.0f;

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start() {
		if(PlayerPrefs.HasKey("SafeArea") == true) {
			this.safeArea = PlayerPrefs.GetFloat("SafeArea");

			#if UNITY_ANDROID && !UNITY_EDITOR
			OuyaSDK.setSafeArea(this.safeArea);
			#endif

			Application.LoadLevel("Testbed");
		}
	}

	#endregion


	//------------------------------------------------
	#region Update method

	private void Update() {
		if(Time.time > (this.lastInput + this.inputDelay)) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			if(OuyaSDK.OuyaInput.GetButton(0, OuyaController.BUTTON_DPAD_LEFT) == true) {
				this.safeArea -= 0.05f;
				if(this.safeArea <= 0.0f) {
					this.safeArea = 0.0f;
				}
				OuyaSDK.setSafeArea(this.safeArea);
				this.lastInput = Time.Time;
			}

			if(OuyaSDK.OuyaInput.GetButton(0, OuyaController.BUTTON_DPAD_RIGHT) == true) {
				this.safeArea += 0.05f;
				if(this.safeArea >= 1.0f) {
					this.safeArea = 1.0f;
				}
				OuyaSDK.setSafeArea(this.safeArea);
				this.lastInput = Time.Time;
			}

			if(OuyaSDK.OuyaInput.GetAxis(0, OuyaController.AXIS_LS_X) < 0.0f) {
				this.safeArea -= 0.05f;
				if(this.safeArea <= 0.0f) {
					this.safeArea = 0.0f;
				}
				OuyaSDK.setSafeArea(this.safeArea);
				this.lastInput = Time.Time;
			}

			if(OuyaSDK.OuyaInput.GetAxis(0, OuyaController.AXIS_LS_X) > 0.0f) {
				this.safeArea += 0.05f;
				if(this.safeArea >= 1.0f) {
					this.safeArea = 1.0f;
				}
				OuyaSDK.setSafeArea(this.safeArea);
				this.lastInput = Time.Time;
			}
			#endif
		}

		#if UNITY_ANDROID && !UNITY_EDITOR
		if(OuyaSDK.OuyaInput.GetButton(0, OuyaController.BUTTON_O) == true) {
			//PlayerPrefs.SetFloat("SafeArea", this.safeArea);
			//PlayerPrefs.Save();
			Application.LoadLevel("Testbed");
		}
		#endif
	}

	#endregion
}

