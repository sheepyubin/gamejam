using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInstantDie : MonoBehaviour
{
    float time;
    float Angle;
    public float AngleSpeed;
    public float PlusAngleSpeed;
    void Start()
    {
        Angle = 0.0f;
    }
    public void skillinstantdie()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Angle));
        Angle += AngleSpeed;
        AngleSpeed += PlusAngleSpeed;
    }
    void Update()
    {   
        if(Angle < 360)
        {
            skillinstantdie();
        }
        else
        {
            time += Time.deltaTime;
            if(time >0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
