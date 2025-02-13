using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockWall : MonoBehaviour
{
    float stopSec = 1.4f;
    float count = 8;
    ParticleSystem ps;
    void Awake()
    {
        ps=GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        count-=Time.deltaTime;
        stopSec-=Time.deltaTime;
        if(count<0)
        {
            ps.Play();
            count=float.MaxValue;
        }
        if(stopSec<0)
        {
            ps.Pause();
            stopSec=float.MaxValue;
        }
    }
}
