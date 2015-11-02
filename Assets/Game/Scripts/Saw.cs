using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Saw : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private Vector2 endPosition = Vector2.zero;
	[SerializeField]
	private float speed = 2.0f;
	[SerializeField]
	private bool affectBloks = false;
	[SerializeField]
	private ParticleSystem deathEffect = null;

	#endregion


	//------------------------------------------------
	#region Start method

	private void Start() {
		this.transform.DOMove(this.endPosition, this.speed, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}

	#endregion


	//------------------------------------------------
	#region OnDrawGizmos method

	private void OnDrawGizmos() {
		Gizmos.DrawLine(this.transform.position, this.endPosition);
		Gizmos.DrawWireSphere(this.endPosition, 0.5f);
	}

	#endregion


	//------------------------------------------------
	#region OnTriggerEnter2D method
	
	private void OnTriggerEnter2D(Collider2D col) {	
		if(col.gameObject.CompareTag("Blok") == true && this.affectBloks == true) {
			if(this.deathEffect != null) {
				Instantiate(this.deathEffect, col.transform.position, Quaternion.identity);
			}
			SimplePool.Despawn(col.gameObject);
		}
		if(col.gameObject.CompareTag("Player") == true) {
			if(this.deathEffect != null) {
				Instantiate(this.deathEffect, col.transform.position, Quaternion.identity);
			}
			Messenger<GameObject>.Broadcast("OnPlayerDeath", col.gameObject);
		}
	}
	
	#endregion
}
