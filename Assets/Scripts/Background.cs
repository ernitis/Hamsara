using Unity.VisualScripting;
using UnityEngine;


public class Background : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform pos, player;
    public float xPan, yPan, yOffset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos.position = new Vector3(player.position.x - (player.position.x/xPan),calcY(), 0);
    }

    float calcY(){
        return yOffset + (float)(player.position.y * yPan);
    }
}
