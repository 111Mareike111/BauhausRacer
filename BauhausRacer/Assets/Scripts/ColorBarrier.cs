using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer{
	public class ColorBarrier : MonoBehaviour {


		public SelectColor selectColor; //display dropdown in Inspektor
		public BoxCollider collider;

		private ColorData colorBarrier;
		private ColorManager colorManager;
	
		// Use this for initialization
		void Awake(){
			colorManager = Game.instance.ColorManager; 
            colorBarrier = colorManager.GetColorByName(selectColor.ToString()); //color gets color selected in inspector
            GetComponent<Renderer>().material = colorBarrier.ColorBarrierMaterial;
		}

		void OnTriggerEnter(Collider col){
			if(col.tag == "Player"){
				//same color as the car
				if(colorManager.CurrentColor.ColorName == colorBarrier.ColorName){
					collider.enabled = false;
					return; 
				}
				//color of the car was mixed with color of barrier
				if(colorManager.CurrentColor.MixingParents != null){
					foreach(ColorData c in colorManager.CurrentColor.MixingParents){
						Debug.Log("c "+ c.ColorName);
						if(c == colorBarrier){
							collider.enabled = false;
							return;
						}
					}
				}
				//wrong color - car can not pass
				collider.enabled=true;
				
			}
		}
	}
}
