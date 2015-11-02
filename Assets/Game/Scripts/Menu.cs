using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;
#endif

public class Menu : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private float flashDelay = 0.5f;
	[SerializeField]
	private Text line1 = null;
	[SerializeField]
	private Text line2 = null;
	[SerializeField]
	private Text line3 = null;

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start() {
		this.StartCoroutine(this.Flash());
	}

	#endregion


	//------------------------------------------------
	#region Update method

	private void Update() {
		#if UNITY_ANDROID && !UNITY_EDITOR
		if(OuyaSDK.OuyaInput.GetButton(0, OuyaController.BUTTON_O) == true) {
			SoundManager.PlaySFX("Select");
			Random.seed = (int)Time.time;
			int levelToLoad = Random.Range(1, 3);
			Application.LoadLevel(levelToLoad.ToString());
		}
		#endif
	}

	#endregion


	//------------------------------------------------
	#region Flash method

	private IEnumerator Flash() {
		while(true) {
			if(this.line1 != null && this.line2 != null && this.line3 != null) {
				this.line1.enabled = !this.line1.enabled;
				this.line2.enabled = !this.line2.enabled;
				this.line3.enabled = !this.line3.enabled;
			}
			yield return new WaitForSeconds(this.flashDelay);
		}
	}

	#endregion
}
