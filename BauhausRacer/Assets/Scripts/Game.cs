using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BauhausRacer
{
    public enum SelectColor {Red, Yellow, Blue, Orange, Green, Violet};
    public class Game : MonoBehaviour
    {
        public static Game Instance = null;
        public ColorManager ColorManager { get; private set; }

        public GameObject carBody;

        public int rounds =3;


        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            } else if(Instance != this)
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

        public void EndGame(){
            Debug.Log("Ende");
        }
    }

   
}

