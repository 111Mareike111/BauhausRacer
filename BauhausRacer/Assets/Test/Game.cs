using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game instance = null;

    public LineRenderer prefabTrail;

    GameObject car;
    LineRenderer trail;

    int pos = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        car = GameObject.FindWithTag("Player");
        trail = Instantiate(prefabTrail, car.transform.position, Quaternion.Euler(90f, 0f, 0f));
        trail.positionCount = 1;
        //trail.SetPosition(pos, transform.position);
        trail.widthMultiplier = 3f;
    }

    void Update()
    {
        trail.positionCount++;
        pos++;
       /* for(int i = 0; i< trail.positionCount; i++)
        {
            if(trail.GetPosition(i) == transform.position)
            {
                trail.material = overlap;
                break;
            }
        }*/
        trail.SetPosition(pos, car.transform.position);

        if (Input.GetKeyDown("space"))
        {
            Debug.Log(trail.GetPosition(100));
        }
    }

    public void newTrail(Material m)
    {
        trail = Instantiate(prefabTrail, car.transform.position, Quaternion.Euler(90f, 0f, 0f));
        trail.positionCount = 1;
        pos = 0;
        trail.SetPosition(pos, car.transform.position);
        trail.material = m;
        trail.widthMultiplier = 3f;
    }
}
