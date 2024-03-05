using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	#region Editor Variables
	[SerializeField]
	[Tooltip("How much health this enemy has")]
	private int m_MaxHealth;

	[SerializeField]
	[Tooltip("How fast this enemy can move")]
	private float m_Speed;

	[SerializeField]
	[Tooltip("Approximate amount of damage dealt per frame")]
	private float m_Damage;
	#endregion

	#region Private Variables
	[SerializeField]
	private float p_curHealth;
	#endregion

	#region Cached References
	private Transform cr_Player;
	#endregion

	#region Public Variables
	public GameObject explosionObj;
	#endregion

	#region Initialization
	private void Awake() {
		p_curHealth = m_MaxHealth;

	}

	private void Start() {
		cr_Player = FindObjectOfType<PlayerMovement>().transform;
	}
	#endregion

	#region Main Updates
	private void FixedUpdate() {
		Vector3 dir = cr_Player.position - transform.position;
		dir.Normalize();
		transform.position = transform.position + dir * m_Speed * Time.fixedDeltaTime;
	}
	#endregion

	#region Collision Methods
	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.CompareTag("Player")) {
			Debug.Log("Hit Player");
			other.gameObject.GetComponent<PlayerMovement>().DecreaseHealth(m_Damage);
		}
	}
	#endregion

	#region Health Methods
	public void DecreaseHealth(float amount) {
		p_curHealth -= amount;
		if (p_curHealth <= 0) {
			Instantiate(explosionObj, transform.position, transform.rotation);
			CoinManager.singleton.IncreaseCoins(10);
			Destroy(this.gameObject);
		}
	}
	#endregion
}

