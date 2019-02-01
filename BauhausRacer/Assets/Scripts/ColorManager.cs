using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class ColorManager : MonoBehaviour
    {
        //inspector variables, assign Materials 
        [Header("Red")]
        public string[] carMaterialRed;
        public Material colorShowerMaterialRed;
        public Material colorBarrierMaterialRed;
        [Header("Blue")]
        public string[] carMaterialBlue;
        public Material colorShowerMaterialBlue;
        public Material colorBarrierMaterialBlue;
        [Header("Yellow")]
        public string[] carMaterialYellow;
        public Material colorShowerMaterialYellow;
        public Material colorBarrierMaterialYellow;
        [Header("Green")]
        public string[] carMaterialGreen;
        public Material colorBarrierMaterialGreen;
        [Header("Violet")]
        public string[] carMaterialViolet;
        public Material colorBarrierMaterialViolet;
        [Header("Orange")]
        public string[] carMaterialOrange;
        public Material colorBarrierMaterialOrange;
        [Header("NoColor")]
        public string[] carMaterialNoColor;
        public Material colorShowerMaterialNoColor;

        Dictionary<string, ColorData> colors = new Dictionary<string, ColorData>();    //contains data of all available colors
        
        public ColorData CurrentColor { get; set; }     //current color of the car

        void Awake()
        {
            //initializing colors and adding them to color-list
            var red = new ColorData("Red", colorShowerMaterialRed, 
                    new Color[]{GetColorByHexcode(carMaterialRed[0]), GetColorByHexcode(carMaterialRed[1]), GetColorByHexcode(carMaterialRed[1])}, 
                      colorBarrierMaterialRed);
            var blue = new ColorData("Blue", colorShowerMaterialBlue,  new Color[]{GetColorByHexcode(carMaterialBlue[0]), GetColorByHexcode(carMaterialBlue[1]), GetColorByHexcode(carMaterialBlue[1])}, colorBarrierMaterialBlue);
            var yellow =  new ColorData("Yellow", colorShowerMaterialYellow,  new Color[]{GetColorByHexcode(carMaterialYellow[0]), GetColorByHexcode(carMaterialYellow[1]), GetColorByHexcode(carMaterialYellow[1])}, colorBarrierMaterialYellow);
            var mixingParentesOrange = new List<ColorData>();
            mixingParentesOrange.Add(red);
            mixingParentesOrange.Add(yellow);

            var mixingParentsGreen = new List<ColorData>();
            mixingParentsGreen.Add(yellow);
            mixingParentsGreen.Add(blue);

            var mixingParentsViolet = new List<ColorData>();
            mixingParentsViolet.Add(blue);
            mixingParentsViolet.Add(red);

            colors.Add("Red", red);
            colors.Add("Blue", blue);
            colors.Add("Yellow", yellow);
            colors.Add("Orange", new ColorData("Orange", 
                    new Color[]{GetColorByHexcode(carMaterialOrange[0]), GetColorByHexcode(carMaterialOrange[1]),GetColorByHexcode(carMaterialOrange[2])
                            }, colorBarrierMaterialOrange, mixingParentesOrange));
            colors.Add("Green", new ColorData("Green", new Color[]{GetColorByHexcode(carMaterialGreen[0]), GetColorByHexcode(carMaterialGreen[1]),GetColorByHexcode(carMaterialGreen[2])
                            }, colorBarrierMaterialGreen, mixingParentsGreen));
            colors.Add("Violet", new ColorData("Violet", new Color[]{GetColorByHexcode(carMaterialViolet[0]), GetColorByHexcode(carMaterialViolet[1]),GetColorByHexcode(carMaterialViolet[2])
                            }, colorBarrierMaterialViolet, mixingParentsViolet));
            colors.Add("NoColor", new ColorData("NoColor", colorShowerMaterialNoColor, new Color[]{GetColorByHexcode(carMaterialNoColor[0]), GetColorByHexcode(carMaterialNoColor[1]),GetColorByHexcode(carMaterialNoColor[2])
                            }, colorShowerMaterialNoColor));

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

        public Color GetColorByHexcode(string hexcode){
            Color color;
            ColorUtility.TryParseHtmlString(hexcode, out color);
            return color;
        }

        //mix colors
        public Color[] MixColors(ColorData colorToMix)
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
            Color[] newColor = GetColorByName(CurrentColor.ColorName).CarTexture;
            return newColor;
        }          
    }
}

