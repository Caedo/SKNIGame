using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBall : MonoBehaviour {

    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private Element element;

    private void Start()
    {
        //create particle system for game object
        if (particle != null)
        {
            Instantiate(particle, this.transform);
        }
    }

}
