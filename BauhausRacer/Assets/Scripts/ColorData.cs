﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public class ColorData
    {
        public string ColorName;
        public Color[] CarTexture; 
        public Material ColorShowerMaterial;
        public Material ColorBarrierMaterial;

        public List<ColorData> MixingParents {get; set;}
        public ColorData(string name, Material colorShowerMaterial, Color[] carTexture, Material colorBarrierMaterial)
        {
            ColorName = name;
            ColorShowerMaterial = colorShowerMaterial; 
            CarTexture = carTexture;
            ColorBarrierMaterial = colorBarrierMaterial;
        }

        public ColorData(string name, Color[] carTexture, Material colorBarrierMaterial, List<ColorData> mixingParents)
        {
            ColorName = name;
            CarTexture = carTexture;
            ColorBarrierMaterial = colorBarrierMaterial;
            MixingParents = mixingParents;
        }
    }
}

























































































































































































































































































































































































































































































































































































































































































































































































































































































































































