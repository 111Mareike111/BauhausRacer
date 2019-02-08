using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer{
	public class ColorBridge : MonoBehaviour {


		public SelectColor selectColor; //display dropdown in Inspektor
		public BoxCollider collider;

		private ColorData colorBarrier;
		private ColorManager colorManager;
	
		// Use this for initialization
		void Awake(){
			colorManager = Game.Instance.ColorManager; 
            colorBarrier = colorManager.GetColorByName(selectColor.ToString()); //color gets color selected in inspector
            
		}

		void Update(){
			if(colorManager.CurrentColor.ColorName == colorBarrier.ColorName){
				ChangeOpacity(1f);
			} else if (colorManager.CurrentColor.MixingParents != null){
				
				foreach(ColorData c in colorManager.CurrentColor.MixingParents){
					if(c == colorBarrier){
						ChangeOpacity(1f);
					}
				}
			} else {
				ChangeOpacity(0.7f);
			}
				
			
		}

		void OnTriggerEnter(Collider col){
			if(col.tag == "Player"){
				//same color as the car
				if(colorManager.CurrentColor.ColorName == colorBarrier.ColorName){
					collider.enabled = true;
					return; 
				}
				//color of the car was mixed with color of barrier
				if(colorManager.CurrentColor.MixingParents != null){
					foreach(ColorData c in colorManager.CurrentColor.MixingParents){
						Debug.Log("c "+ c.ColorName);
						if(c == colorBarrier){
							collider.enabled = true;
							return;
						}
					}
				}
				collider.enabled=false;
			}
		}

		private void ChangeOpacity(float opacity){
			for(int i = 0; i<GetComponent<MeshRenderer>().materials.Length; i++){
				UnityEngine.Color oldColor = GetComponent<MeshRenderer>().materials[i].color;
				GetComponent<MeshRenderer>().materials[i].color = new UnityEngine.Color(oldColor.r,oldColor.g,oldColor.b,opacity);
				
			} 
		}
	}
}
