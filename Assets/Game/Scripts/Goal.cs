using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private int goalForPlayer = 0;

	#endregion


	//------------------------------------------------
	#region OnTriggerEnter2D method

	private void OnTriggerEnter2D(Collider2D col) {
		if(col.CompareTag("Blok") == true) {
			SimplePool.Despawn(col.gameObject);
			Messenger<int>.Broadcast("OnPlayerGoal", this.goalForPlayer);
		}
	}

	#endregion
}
