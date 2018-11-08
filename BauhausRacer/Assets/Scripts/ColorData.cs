using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class ColorData
    {
        public string colorName;
        public Material CarTexture; 
        public Material ColorShowerMaterial;

        public ColorData(string name, Material colorShowerMaterial, Material carTexture)
        {
            colorName = name;
            ColorShowerMaterial = colorShowerMaterial;
            CarTexture = carTexture;
        }

        public ColorData(string name, Material carTexture)
        {
            colorName = name;
            CarTexture = carTexture;
        }
    }
}
