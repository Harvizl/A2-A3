using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingCamera : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;

    public bool followPlayer;
    public bool stayOnPlayer;






    void Awake()
    {
       
    }

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        {
            if (Physics.Raycast(transform.position, new Vector3 (-1, -1, 2), out hit))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);
                if (hit.collider.tag == "Player")
                {
                    print("Going Left");
                    followPlayer = true;
                }
            }
        }

        

        if (followPlayer == true)
        {

            transform.Translate(20 * Time.deltaTime * Vector3.left);

            RaycastHit hit2;
            {
                if (Physics.Raycast(transform.position, new Vector3(-1, -1, 2), out hit))
                    if (Physics.Raycast(transform.position, new Vector3(-1, -3, 6), out hit))
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.green);

                        if (hit.collider.tag == "Player")
                        {
                            print("On the Player");
                            followPlayer = false;
                            stayOnPlayer = true;
                        }
                    }
            }
        }
        if (stayOnPlayer == true)
        {
            transform.position = player.transform.position + offset;
        }

    }

    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        //StartCoroutine(DelayedFollow());
    }

    IEnumerator DelayedFollow()
    {
        yield return new WaitForSeconds(0);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 1f * Time.time);
    }

}
