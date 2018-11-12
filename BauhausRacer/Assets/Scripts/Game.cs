using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public enum SelectColor {Red, Yellow, Blue, Orange, Green, Violet};
    public class Game : MonoBehaviour
    {
        public static Game instance = null;
        public ColorManager ColorManager { get; private set; }

        public GameObject carBody;


        void Awake()
        {
            if(instance == null)
            {
                instance = this;
            } else if(instance != this)
            {
                Destroy(gameObject);
            }

            ColorManager = GetComponent<ColorManager>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

   
}

