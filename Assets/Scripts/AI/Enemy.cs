using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Mummy mummy;
    private Anubis anubis;

    private void Awake()
    {
        mummy = gameObject.GetComponent<Mummy>();
        anubis = gameObject.GetComponent<Anubis>();
    }

    public void TakeDamage(int damage)
    {
        if (mummy == null)
        {
            anubis.TakeDamage(damage);
        }
        else
        {
            mummy.TakeDamage(damage);
        }
    }
}
