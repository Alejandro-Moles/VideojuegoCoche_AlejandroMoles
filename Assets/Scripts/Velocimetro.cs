using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Velocimetro : MonoBehaviour
{
    public Image aguja;
    public Rigidbody coche;

    public int ajusteAguja = 152;
    public float speed;

    public TextMeshProUGUI speedtext;
    public TextMeshProUGUI marchas;
    void Start()
    {
        
    }

    void Update()
    {
        speed = coche.velocity.magnitude;
        aguja.transform.eulerAngles = new Vector3(0, 0, speed * -3 + ajusteAguja);

        speedtext.text = ((int)speed * 3.4).ToString();

        double velocidad = (speed * 3.4);

        if (velocidad >= 20 && velocidad <= 50)
        {
            marchas.text = "Marcha: 1";
        }

        if (velocidad >= 50 && velocidad <= 70)
        {
            marchas.text = "Marcha: 2";
        }

        if (velocidad >= 70 && velocidad <= 90)
        {
            marchas.text = "Marcha: 3";
        }

        if (velocidad > 90 && velocidad <= 110)
        {
            marchas.text = "Marcha: 4";
        }

        if (velocidad > 110)
        {
            marchas.text = "Marcha: 5";
        }
    }
}
