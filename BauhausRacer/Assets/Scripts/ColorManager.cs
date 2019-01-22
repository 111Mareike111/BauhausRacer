using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class ColorManager : MonoBehaviour
    {
        //inspector variables, assign Materials 
        [Header("Red")]
        public Material[] carMaterialRed;
        public Material colorShowerMaterialRed;
        public Material colorBarrierMaterialRed;
        [Header("Blue")]
        public Material[] carMaterialBlue;
        public Material colorShowerMaterialBlue;
        public Material colorBarrierMaterialBlue;
        [Header("Yellow")]
        public Material[] carMaterialYellow;
        public Material colorShowerMaterialYellow;
        public Material colorBarrierMaterialYellow;
        [Header("Green")]
        public Material[] carMaterialGreen;
        public Material colorBarrierMaterialGreen;
        [Header("Violet")]
        public Material[] carMaterialViolet;
        public Material colorBarrierMaterialViolet;
        [Header("Orange")]
        public Material[] carMaterialOrange;
        public Material colorBarrierMaterialOrange;
        [Header("NoColor")]
        public Material[] carMaterialNoColor;
        public Material colorShowerMaterialNoColor;

        Dictionary<string, ColorData> colors = new Dictionary<string, ColorData>();    //contains data of all available colors
        
        public ColorData CurrentColor { get; set; }     //current color of the car

        void Awake()
        {
            //initializing colors and adding them to color-list
            var red = new ColorData("Red", colorShowerMaterialRed, carMaterialRed, colorBarrierMaterialRed);
            var blue = new ColorData("Blue", colorShowerMaterialBlue, carMaterialBlue, colorBarrierMaterialBlue);
            var yellow =  new ColorData("Yellow", colorShowerMaterialYellow, carMaterialYellow, colorBarrierMaterialYellow);
            colors.Add("Red", red);
            colors.Add("Blue", blue);
            colors.Add("Yellow", yellow);
            colors.Add("Orange", new ColorData("Orange", carMaterialOrange, colorBarrierMaterialOrange, new ColorData[]{red, yellow}));
            colors.Add("Green", new ColorData("Green", carMaterialGreen, colorBarrierMaterialGreen, new ColorData[]{yellow, blue}));
            colors.Add("Violet", new ColorData("Violet", carMaterialViolet, colorBarrierMaterialViolet, new ColorData[]{blue, red}));
            colors.Add("NoColor", new ColorData("NoColor", colorShowerMaterialNoColor, carMaterialNoColor, colorShowerMaterialNoColor));

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
            if(colorToMix.ColorName == "NoColor")
            {
                CurrentColor = GetColorByName("NoColor");
            }
            else
            {
                switch (CurrentColor.ColorName)
                {
                    case "Red":
                        if (colorToMix.ColorName == "Blue")
                        {
                            CurrentColor = GetColorByName("Violet");
                        }
                        else if(colorToMix.ColorName == "Yellow")
                        {
                            CurrentColor = GetColorByName("Orange");
                        }
                        break;
                    case "Blue":
                        if (colorToMix.ColorName == "Red")
                        {
                            CurrentColor = GetColorByName("Violet");
                        }
                        else if (colorToMix.ColorName == "Yellow")
                        {
                            CurrentColor = GetColorByName("Green");
                        }
                        break;
                    case "Yellow":
                        if (colorToMix.ColorName == "Blue")
                        {
                            CurrentColor = GetColorByName("Green");
                        }
                        else if (colorToMix.ColorName == "Red")
                        {
                            CurrentColor = GetColorByName("Orange");
                        }
                        break;
                    case "NoColor":
                        CurrentColor = colorToMix;
                        break;
                    
                }
            }
            
            return CurrentColor;
        }          
    }
}

