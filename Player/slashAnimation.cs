using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashAnimation : MonoBehaviour
{

    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ps && !ps.IsAlive()) {
            DesroySelf();
        }
    }

    public void DesroySelf()
    {
        Destroy(gameObject);
    }

}
