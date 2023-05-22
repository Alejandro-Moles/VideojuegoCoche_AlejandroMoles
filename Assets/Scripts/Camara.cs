using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform Target;

    private void Update()
    {
        this.transform.position = Target.position;
        this.transform.rotation = Target.rotation;
    }
}
