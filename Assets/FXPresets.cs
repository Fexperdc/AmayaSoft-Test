using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPresets : MonoBehaviour {

    [SerializeField]
    private ParticleSystem _starExplodeSystem;

    public void StarExplode(Vector3 position) {
        _starExplodeSystem.transform.position = position;
        _starExplodeSystem.Play();
    }

}
