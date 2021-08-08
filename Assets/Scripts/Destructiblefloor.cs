using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructiblefloor : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject destroyedFloor;

    void OnMouseDown ()
    {
        Instantiate(destroyedFloor, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
