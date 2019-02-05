using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer {
	
public class Boost : MonoBehaviour {
	public GameObject car;
	public float accleration= 1000f;

	public SelectColor selectColor;
	public ColorData colorBoost;
	private ColorManager colorManager;

	void Awake(){
			colorManager = Game.Instance.ColorManager; 
            colorBoost = colorManager.GetColorByName(selectColor.ToString()); //color gets color selected in inspector
			foreach(Renderer r in GetComponentsInChildren<Renderer>()){
				r.material = colorBoost.ColorBarrierMaterial;
			}
		}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		Debug.Log("hi");
		if(collider.tag == "Player"){
			//same color as the car
				if(colorManager.CurrentColor.ColorName == colorBoost.ColorName){
					Debug.Log("hi2");
					car.GetComponent<Rigidbody>().AddForce(car.transform.forward*Time.deltaTime*accleration, ForceMode.Acceleration);
					GetComponent<AudioSource>().Play();
					return;
				}
				//color of the car was mixed with color of barrier
				if(colorManager.CurrentColor.MixingParents != null){
					foreach(ColorData c in colorManager.CurrentColor.MixingParents){
						Debug.Log("c "+ c.ColorName);
						if(c == colorBoost){
							car.GetComponent<Rigidbody>().AddForce(car.transform.forward*accleration);
							GetComponent<AudioSource>().Play();
							StartCoroutine(DeleteForce());
						}
					}
				}
			
		}
	}

	IEnumerator DeleteForce(){
		yield return new WaitForSeconds(3f);
		car.GetComponent<Rigidbody>().AddForce(-car.transform.forward*accleration);
	}
}

}