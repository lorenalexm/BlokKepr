using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;
#endif

public class GameManager : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private Text playerOneLabel = null;
	[SerializeField]
	private Text playerTwoLabel = null;
	[SerializeField]
	private Text timeLabel = null;
	[SerializeField]
	private GameObject winContainer = null;
	[SerializeField]
	private Text winText = null;
	[SerializeField]
	private int totalTime = 99;
	[SerializeField]
	private float flashDelay = 0.5f;
	[SerializeField]
	private Text line1 = null;
	[SerializeField]
	private Text line2 = null;
	[SerializeField]
	private Text line3 = null;

	private string playerOneString = "";
	private string playerTwoString = "";
	private float playerOneScore = 0;
	private float playerTwoScore = 0;
	private bool finished = false;

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++) {
			players[i].GetComponent<PlayerController>().PlayerNumber = i;
		}

		if(this.playerOneLabel != null && this.playerTwoLabel != null) {
			this.playerOneString = this.playerOneLabel.text;
			this.playerTwoString = this.playerTwoLabel.text;
			this.playerOneLabel.text = this.playerOneString + this.playerOneScore.ToString();
			this.playerTwoLabel.text = this.playerTwoScore.ToString() + this.playerTwoString;
		}

		this.StartCoroutine(this.CountDown());
	}

	#endregion


	//------------------------------------------------
	#region OnEnable method

	private void OnEnable() {
		Messenger<int>.AddListener("OnPlayerGoal", this.PlayerGoal);
	}

	#endregion


	//------------------------------------------------
	#region OnDisable method

	private void OnDisable() {
		Messenger<int>.RemoveListener("OnPlayerGoal", this.PlayerGoal);
	}

	#endregion


	//------------------------------------------------
	#region Update method

	private void Update() {
		if(this.finished == true) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			if(OuyaSDK.OuyaInput.GetButtonDown(0, OuyaController.BUTTON_O) == true) {
				SoundManager.PlaySFX("Select");
				Random.seed = (int)Time.time;
				int levelToLoad = Random.Range(1, 3);
				Application.LoadLevel(levelToLoad.ToString());
			}
			#else
			if(Input.GetButtonDown("Jump") == true) {
				SoundManager.PlaySFX("Select");
				Random.seed = (int)Time.time;
				int levelToLoad = Random.Range(1, 4);
				Application.LoadLevel(levelToLoad.ToString());
			}
			#endif
		}
	}

	#endregion


	//------------------------------------------------
	#region PlayerGoal method

	private void PlayerGoal(int player) {
		switch(player) {
			case 0:
				this.playerOneScore++;
				break;
			case 1:
				this.playerTwoScore++;
				break;
			default:
				break;
		}

		if(this.playerOneLabel != null && this.playerTwoLabel != null) {
			this.playerOneLabel.text = this.playerOneString + this.playerOneScore.ToString();
			this.playerTwoLabel.text = this.playerTwoScore.ToString() + this.playerTwoString;
		}
	}

	#endregion


	//------------------------------------------------
	#region CountDown method
	
	private IEnumerator CountDown() {
		while(this.totalTime > 0) {
			this.totalTime--;
			this.timeLabel.text = this.totalTime.ToString();
			yield return new WaitForSeconds(1.0f);
		}
		
		this.finished = true;
		Messenger.Broadcast("OnPause");
		
		if(this.winContainer != null && this.winText != null) {
			this.winContainer.SetActive(true);
			this.StartCoroutine(this.Flash());
			
			if(this.playerOneScore > this.playerTwoScore) {
				this.winText.text = "<color=#00FF00FF>PLAYER ONE</color> wins the match!";
			} else if (this.playerTwoScore > this.playerOneScore) {
				this.winText.text = "<color=#587BFBFF>PLAYER TWO</color> wins the match!";
			} else {
				this.winText.text = "The match resulted in a tie..";
			}
		}
		
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
