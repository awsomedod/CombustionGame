using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	#region Editor Variables
	[SerializeField]
	[Tooltip("How fast the player can move, by default.")]
	public float movespeed;

	[SerializeField]
	[Tooltip("A list of all attacks and information about them")]
	private PlayerAttackInfo[] m_Attacks;

	[SerializeField]
	[Tooltip("Amount of health the player starts with, by default.")]
	private int m_MaxHealth;
	#endregion

	#region Cached References
	private Animator cr_Anim;
	private Rigidbody2D cr_Rb;
	private SpriteRenderer cr_Sr;
	#endregion

	#region Public Variables
	public bool feetContact;
	public Slider HPSlider;
	#endregion

	#region Private Variables
	[SerializeField]
	private float p_CurHealth;
	//In order to do anything we cannot be frozen (the timer must be 0)
	private float p_FrozenTimer;
	[SerializeField]
	private float p_Speed;
	private Vector2 p_FacingDirection;
	#endregion

	#region Initialization
	private void Awake() {
		p_FrozenTimer = 0;
		if (!PlayerPrefs.HasKey("Health"))
		{
			PlayerPrefs.SetInt("Health", m_MaxHealth);
			p_CurHealth = m_MaxHealth;
		}
		else
		{
			p_CurHealth = PlayerPrefs.GetInt("Health");
		}
		Debug.Log(PlayerPrefs.GetInt("Health"));

		for (int i = 0; i < m_Attacks.Length; i++) {
			PlayerAttackInfo attack = m_Attacks[i];
			attack.Cooldown = 0;

			if (attack.WindUpTime > attack.FrozenTime) {
				Debug.LogError(attack.AttackName + " has a windup time that is arger than the amount of time that the player is frozen for");
			}
		}
		if (!PlayerPrefs.HasKey("Speed"))
		{
			PlayerPrefs.SetFloat("Speed", movespeed);
			p_Speed = movespeed;
		}
		else
		{
			p_Speed = PlayerPrefs.GetFloat("Speed");
		}
		Debug.Log(PlayerPrefs.GetFloat("Speed"));
		HPSlider.value = p_CurHealth / m_MaxHealth;
	}

	void Start()
	{
		cr_Anim = GetComponentInChildren<Animator>();
		cr_Rb = GetComponent<Rigidbody2D>();
		cr_Sr = GetComponentInChildren<SpriteRenderer>();
		p_FacingDirection = transform.forward;		
	}
	#endregion

	#region Main Update
	void Update()
	{
		if (p_FrozenTimer > 0) {
			cr_Rb.velocity = new Vector2(0, 0);
			p_FrozenTimer -= Time.deltaTime;
			return;
		} else {
			p_FrozenTimer = 0;
		}

		//Ability use
		for (int i = 0; i < m_Attacks.Length; i++) {
			PlayerAttackInfo attack = m_Attacks[i];

			if (attack.IsReady()) {
				if (Input.GetButtonDown(attack.Button)) {
					cr_Anim.SetTrigger("Attack");
					p_FrozenTimer = attack.FrozenTime;
					StartCoroutine(UseAttack(attack));
					break;
				}
			} else if (attack.Cooldown > 0) {
				attack.Cooldown -= Time.deltaTime;
			}
		}

		// if (Input.GetKeyDown(KeyCode.UpArrow) && canJump())
		// {
		// 	cr_Anim.SetTrigger("Jump");
		// 	cr_Rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
		// }
		// if (Input.GetButtonDown("Fire1"))
		// {
		// 	cr_Anim.SetTrigger("Attack");
		// }
		// if (Input.GetKey(KeyCode.RightArrow))
		// {
		// 	cr_Anim.SetBool("Running", true);
		// 	transform.position = (Vector2)transform.position + new Vector2(p_Speed, 0) * Time.deltaTime;
		// 	transform.rotation = Quaternion.Euler(0, 0, 0);
		// }
		// else if (Input.GetKey(KeyCode.LeftArrow))
		// {
		// 	cr_Anim.SetBool("Running", true);
		// 	transform.position = (Vector2)transform.position + new Vector2(-p_Speed, 0) * Time.deltaTime;
		// 	transform.rotation = Quaternion.Euler(0, 180, 0);
		// } else
		// {
		// 	cr_Anim.SetBool("Running", false);
		// }
		if (Input.GetKey(KeyCode.UpArrow))
		{
			cr_Anim.SetBool("Running", true);
			p_FacingDirection = new Vector2(0, 1);
			p_Speed = PlayerPrefs.GetFloat("Speed");
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			cr_Anim.SetBool("Running", true);
			p_FacingDirection = new Vector2(1, 0);
			p_Speed = PlayerPrefs.GetFloat("Speed");
			cr_Sr.flipX = false;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			cr_Anim.SetBool("Running", true);
			p_FacingDirection = new Vector2(-1, 0);
			p_Speed = PlayerPrefs.GetFloat("Speed");
			cr_Sr.flipX = true;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			cr_Anim.SetBool("Running", true);
			p_FacingDirection = new Vector2(0, -1);
			p_Speed = PlayerPrefs.GetFloat("Speed");
		} else
		{
			p_Speed = 0;
			cr_Anim.SetBool("Running", false);
		}
		cr_Rb.velocity = p_FacingDirection * p_Speed;
	}
	#endregion

	#region Health/Dying Methods
	public void DecreaseHealth(float amount) {
		p_CurHealth -= amount;
		HPSlider.value = p_CurHealth / m_MaxHealth;
		if (p_CurHealth <= 0) {
			SceneManager.LoadScene("LoseScene");
			Debug.Log("DEAD!!!!");
		}
	}
	#endregion
	
	#region Attack Methods
	private IEnumerator UseAttack(PlayerAttackInfo attack) {

		cr_Anim.SetBool("AttackR", true);
		cr_Anim.SetTrigger(attack.TriggerName);
		yield return new WaitForSeconds(attack.WindUpTime);

		Vector2 offset = getAttackOffset(attack);
		GameObject go = Instantiate(attack.AbilityGO, (Vector2)transform.position + offset, transform.rotation);
		go.GetComponent<BasicAttack>().Use(attack);

		yield return new WaitForSeconds(attack.Cooldown);

		attack.ResetCooldown();
		cr_Anim.SetBool("AttackR", false);
	}

	public Vector2 getAttackOffset(PlayerAttackInfo attack) {
		Vector2 offset = p_FacingDirection * attack.Offset;
		return offset;
	}
	#endregion
}
