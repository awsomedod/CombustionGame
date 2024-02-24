using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetColliderScript : MonoBehaviour
{

	

	// Returns whether the obj is a floor, platform, or wall
	bool isFloor(GameObject obj) {
		return obj.layer == LayerMask.NameToLayer ("Floor");
	}

    // use coll.gameObject if you need a reference coll's GameObject
	void OnCollisionEnter2D(Collision2D coll) {
        if (isFloor(coll.gameObject)) {
			GetComponentInParent<PlayerMovement>().feetContact = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
        if (isFloor(coll.gameObject)) {
			GetComponentInParent<PlayerMovement>().feetContact = false;
		}
    }
}
