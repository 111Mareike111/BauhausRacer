using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer{
	public class ColorBarrier : MonoBehaviour {

		public SelectColor selectColor; //display dropdown in Inspektor
		public BoxCollider collider;
        private Renderer renderer;

		private ColorData colorBarrier;
		private ColorManager colorManager;
	
		// Use this for initialization
		void Awake(){
			colorManager = Game.Instance.ColorManager; 
            colorBarrier = colorManager.GetColorByName(selectColor.ToString()); //color gets color selected in inspector
            renderer = GetComponent<Renderer>();
		}

		void Update(){
            if (colorManager.CurrentColor.ColorName == colorBarrier.ColorName)
            {
                //ChangeOpacity(0.7f);
                ChangeMaterial(true);
            }
            else if (colorManager.CurrentColor.MixingParents != null)
            {
                foreach (ColorData c in colorManager.CurrentColor.MixingParents)
                {
                    if (c == colorBarrier)
                    {
                        //ChangeOpacity(0.7f);
                        ChangeMaterial(true);
                    }
                }
            }
            else
            {
                //ChangeOpacity(1f);
                ChangeMaterial(false);
            }
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
						if(c == colorBarrier){
							collider.enabled = false;
							return;
						}
					}
				}
				//wrong color - car can not pass
				collider.enabled=true;
				GetComponent<AudioSource>().Play();
				Game.Instance.wrongColorText.SetActive(true);
				Game.Instance.wrongColorText.GetComponent<Animator>().Play("wrongColorTextAppear", -1, 0);
			}
		}

		private void ChangeOpacity(float opacity){
			for(int i = 0; i<GetComponent<MeshRenderer>().materials.Length; i++){
				UnityEngine.Color oldColor = GetComponent<MeshRenderer>().materials[i].color;
				GetComponent<MeshRenderer>().materials[i].SetColor("_BaseColor", new UnityEngine.Color(oldColor.r,oldColor.g,oldColor.b,opacity));
				
			} 
		}

        private void ChangeMaterial(bool passable)
        {
            Material[] materials = colorManager.GetBarrierMaterial(selectColor, passable);
            renderer.materials = materials;

            //for(int i = 0; i < materials.Length; i++)
            //{
                
            //    renderer.materials[i] = materials[i];
            //}
            
        }
	}
}
