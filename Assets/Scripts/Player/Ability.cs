using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
	#region Editor Variables
	[SerializeField]
	[Tooltip("How much power this ability has")]
	private float m_Power;
	public float Power {
		get {
			return m_Power;
		}
	}

	[SerializeField]
	[Tooltip("How much heat this ability generates")]
	private float m_HeatGenerated;
	public float HeatGenerated {
		get {
			return HeatGenerated;
		}
	}
	#endregion
}