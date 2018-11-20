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
            GetComponent<Renderer>().material = colorShower.ColorShowerMaterial;
        }

        //wenn colliding with car, car changes its color
        public void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player")
            {
                Game.Instance.carBody.GetComponent<Renderer>().material = colorManager.MixColors(colorShower).CarTexture;
            }
        }
    }
}

