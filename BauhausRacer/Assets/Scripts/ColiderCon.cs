using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderCon : MonoBehaviour {

    private GameObject player;
    private Ray rayR, rayL;
    private RaycastHit rayHit;

    public float rayPoint = 0.5f;
    public float rayDistance = 1.0f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        rayR = new Ray(new Vector3(player.transform.position.x + rayPoint, player.transform.position.y + 1f, player.transform.position.z + 1.5f), Vector3.down);
        rayL = new Ray(new Vector3(player.transform.position.x - rayPoint, player.transform.position.y + 1f, player.transform.position.z + 1.5f), Vector3.down);
        Debug.DrawRay(rayR.origin, rayR.direction * rayDistance, Color.yellow);
        Debug.DrawRay(rayL.origin, rayR.direction * rayDistance, Color.yellow);

        if(!Physics.Raycast(rayR, out rayHit, rayDistance, LayerMask.GetMask("Ground")))
        {
        }
        if (!Physics.Raycast(rayL, out rayHit, rayDistance, LayerMask.GetMask("Ground")))
        {
        }
    }
}
