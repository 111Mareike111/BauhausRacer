using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class ColorManager : MonoBehaviour
    {
        //inspector variables, assign Materials 
        [Header("Red")]
        public Material carMaterialRed;
        public Material colorShowerMaterialRed;
        [Header("Blue")]
        public Material carMaterialBlue;
        public Material colorShowerMaterialBlue;
        [Header("Yellow")]
        public Material carMaterialYellow;
        public Material colorShowerMaterialYellow;
        [Header("Green")]
        public Material carMaterialGreen;
        [Header("Violet")]
        public Material carMaterialViolet;
        [Header("Orange")]
        public Material carMaterialOrange;
        [Header("NoColor")]
        public Material carMaterialNoColor;
        public Material colorShowerMaterialNoColor;

        Dictionary<string, ColorData> colors = new Dictionary<string, ColorData>();    //contains data of all available colors
        
        public ColorData CurrentColor { get; set; }     //current color of the car

        void Awake()
        {
            //initializing colors and adding them to color-list
            colors.Add("Red", new ColorData("Red", colorShowerMaterialRed, carMaterialRed));
            colors.Add("Blue", new ColorData("Blue", colorShowerMaterialBlue, carMaterialBlue));
            colors.Add("Yellow", new ColorData("Yellow", colorShowerMaterialYellow, carMaterialYellow));
            colors.Add("Orange", new ColorData("Orange", carMaterialOrange));
            colors.Add("Green", new ColorData("Green", carMaterialGreen));
            colors.Add("Violet", new ColorData("Violet", carMaterialViolet));
            colors.Add("NoColor", new ColorData("NoColor", colorShowerMaterialNoColor, carMaterialNoColor));

            CurrentColor = GetColorByName("NoColor"); //no color in the beginning
        }

        //get color data by name
        public ColorData GetColorByName(string colorName)
        {
            ColorData colorData = null;
            if (!colors.TryGetValue(colorName, out colorData))
            {
                Debug.LogError("Colors - No ColorData with name: " + colorName);
            }
            return colorData;
        }

        //mix colors
        public ColorData MixColors(ColorData colorToMix)
        {
            if(colorToMix.colorName == "NoColor")
            {
                CurrentColor = GetColorByName("NoColor");
            }
            else
            {
                switch (CurrentColor.colorName)
                {
                    case "Red":
                        if (colorToMix.colorName == "Blue")
                        {
                            CurrentColor = GetColorByName("Violet");
                        }
                        else if(colorToMix.colorName == "Yellow")
                        {
                            CurrentColor = GetColorByName("Orange");
                        }
                        break;
                    case "Blue":
                        if (colorToMix.colorName == "Red")
                        {
                            CurrentColor = GetColorByName("Violet");
                        }
                        else if (colorToMix.colorName == "Yellow")
                        {
                            CurrentColor = GetColorByName("Green");
                        }
                        break;
                    case "Yellow":
                        if (colorToMix.colorName == "Blue")
                        {
                            CurrentColor = GetColorByName("Green");
                        }
                        else if (colorToMix.colorName == "Red")
                        {
                            CurrentColor = GetColorByName("Orange");
                        }
                        break;
                    case "NoColor":
                        CurrentColor = colorToMix;
                        break;
                    default:
                        CurrentColor = null;
                        break;
                }
            }
            
            Debug.Log(CurrentColor.colorName);
            return CurrentColor;
        }          
    }
}

