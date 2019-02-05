using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class ColorShower : MonoBehaviour
    {
        public enum ColorSelectionShower { Red, Blue, Yellow, NoColor } //possible Colors

        public ColorSelectionShower selectColor;  //Display enum as Dropdown in inspector

        private ColorData colorShower; //Color of the shower - selected in inspector     

        private ColorManager colorManager; //Reference to colorManager - Script

        void Awake()
        {
            colorManager = Game.Instance.ColorManager; 
            colorShower = colorManager.GetColorByName(selectColor.ToString()); //colorShower gets color selected in inspector
          
        }

        //wenn colliding with car, car changes its color
        public void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player")
            {
                string c = colorManager.CurrentColor.ColorName;
                Color[] colors = colorManager.MixColors(colorShower);
                
                if(!c.Equals(colorShower.ColorName)){
                    if(
                        ((c.Equals("Green") || c.Equals("Orange") || c.Equals("Violet")) && colorManager.CurrentColor.ColorName.Equals("NoColor"))
                    || (c.Equals("Blue") || c.Equals("Yellow") || c.Equals("Red") || c.Equals("NoColor")))
                    {
                        Debug.Log("C "+colorManager.CurrentColor.ColorName);
                        Debug.Log("m "+colorShower.ColorName);
                        //Color[] colors = MixColor(colorManager.GetColorByName(selectColor.ToString()).CarTexture;
                        

                        GameObject carController = col.gameObject;
                        carController.GetComponentsInParent<ChangeColorByShader>()[0].PrepareTransition(colors[0]);
                        carController.GetComponentsInParent<ChangeColorByShader>()[1].PrepareTransition(colors[1]);
                        carController.GetComponentsInParent<ChangeColorByShader>()[2].PrepareTransition(colors[2]);
                        GetComponent<AudioSource>().Play();
                    }
                    
                    
                }
               
            }
        }
    }
}

