using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	private float playerOneScore = 0;
	private float playerTwoScore = 0;

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++) {
			players[i].GetComponent<PlayerController>().PlayerNumber = i;
		}
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
	#region PlayerGoal method

	private void PlayerGoal(int player) {

	}

	#endregion
}
