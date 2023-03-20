using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBGController : MonoBehaviour
{

    public int depth = 0;
    private float velocity = 1;

    private void FixedUpdate()
    {
        float realVelocity = 0;
        if (depth != 0)
        {
            realVelocity = velocity / depth;
        }

        Vector2 pos = transform.position;
        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= -17)
        {
            pos.x = 27.5f;
        }

        transform.position = pos;
    }

}
