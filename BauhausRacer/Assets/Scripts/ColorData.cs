using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class ColorData
    {
        public string ColorName;
        public Material[] CarTexture; 
        public Material ColorShowerMaterial;
        public Material ColorBarrierMaterial;

        public ColorData[] MixingParents {get; set;}
        public ColorData(string name, Material colorShowerMaterial, Material[] carTexture, Material colorBarrierMaterial)
        {
            ColorName = name;
            ColorShowerMaterial = colorShowerMaterial;
            CarTexture = carTexture;
            ColorBarrierMaterial = colorBarrierMaterial;
        }

        public ColorData(string name, Material[] carTexture, Material colorBarrierMaterial, ColorData[] mixingParents)
        {
            ColorName = name;
            CarTexture = carTexture;
            ColorBarrierMaterial = colorBarrierMaterial;
            MixingParents = mixingParents;
        }
    }
}
