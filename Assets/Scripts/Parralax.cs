using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    float length, Startpos;
    public GameObject Cam;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (Cam.transform.position.x * (1 - speed));
        float distance = (Cam.transform.position.x * speed);

        transform.position = new Vector3(Startpos + distance, transform.position.y, transform.position.z);
        if (temp > Startpos + length) Startpos += length;
        else if (temp < Startpos - length) Startpos -= length;
    }
}
