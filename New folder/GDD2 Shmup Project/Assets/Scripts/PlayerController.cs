using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        float z = -(Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f) / 20;
        float x = -(Input.GetAxis("Vertical") * Time.deltaTime * 150.0f) / 20;

        transform.Translate(z, 0, x);
    }
}
