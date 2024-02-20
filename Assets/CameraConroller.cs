using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{

	[SerializeField]
	private Transform playerTransform;
	private Vector3 playerPosition;

    void Start()
    {
        
    }
    void Update()
    {
		playerPosition = playerTransform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y , -10);
    }
}
