using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
	#region Editor Variables
	[SerializeField]
	[Tooltip("All of the main information about this particluar ability.")]
	protected Ability m_Info;
	#endregion

	#region Private Variables
	private GameObject playerObj;
	private PlayerAttackInfo p_attack;
	#endregion

	#region Collision
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Enemy")) {
			Debug.Log("HIT Enemy!");
			other.gameObject.GetComponent<EnemyController>().DecreaseHealth(m_Info.Power);
		}
	}
	#endregion

	#region Initialization
	public void Use(PlayerAttackInfo attack)
	{
		playerObj = GameObject.FindGameObjectWithTag("Player");
		p_attack = attack;
	}
	#endregion

	#region Update
	private void Update() {
		Vector2 offset = playerObj.GetComponent<PlayerMovement>().getAttackOffset(p_attack);
		transform.position = (Vector2)playerObj.transform.position + offset;
	}
	#endregion
}
