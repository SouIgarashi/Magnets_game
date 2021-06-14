using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour {

    private ParticleSystem pl;
    void Start () {
        pl = GetComponent<ParticleSystem>();
		var collis =pl.collision;
        pl.startSize = 2f;
		
		
    }

    // Update is called once per frame
    void Update () {
        
        if(hani.ofa!=null)
        transform.LookAt (hani.ofa.transform);
        var collis = pl.collision;
        if (!player.player2Flag)
        {
            int layerMask = 1 << 9 | 1 << 14 | 1 << 16;
            collis.collidesWith = layerMask;
        }
        else
        {
            int layerMask = 1 << 8 | 1 << 14 | 1 << 16;
            collis.collidesWith = layerMask;
        }

    }
    

   

}
