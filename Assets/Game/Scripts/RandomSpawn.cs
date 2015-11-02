using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private GameObject blokPrefab = null;
	[SerializeField]
	private Vector2 delayRange = Vector2.zero;
	[SerializeField]
	private GameObject[] spawnPoints;

	private float lastSpawn = 0.0f;

	#endregion


	//------------------------------------------------
	#region Update method

	private void Update() {
		if(Time.time > (lastSpawn + Random.Range(this.delayRange.x, this.delayRange.y))) {
			this.lastSpawn = Time.time;
			int sp = Random.Range(0, this.spawnPoints.Length);
			SimplePool.Spawn(this.blokPrefab, this.spawnPoints[sp].transform.position, Quaternion.Euler(0, 0, Random.Range(-45.0f, 45.0f)));
		}
	}

	#endregion
}
