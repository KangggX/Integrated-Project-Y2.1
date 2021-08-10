/******************************************************************************
Author: Cher Wei
Name of Class: DestructibleFloor
Description of Class: To be used to create a destructible floor kind of effect when player collides with it
Date Created: 08/08/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructiblefloor : MonoBehaviour
{
    /// <summary>
    /// The GameObject of the destroyed floor, to be dragged in from Inspector
    /// </summary>
    public GameObject destroyedFloor;

    void OnMouseDown ()
    {
        Instantiate(destroyedFloor, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    /// <summary>
    /// Once player collides with it, start the countdown
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Countdown());
        }
    }

    /// <summary>
    /// When countdown finishes, floor breaks
    /// </summary>
    /// <returns></returns>
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);

        Instantiate(destroyedFloor, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
