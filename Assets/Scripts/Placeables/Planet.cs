using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    private float myMass;

    private GameObject nBody;

    // Use this for initialization
    void Start()
    {
        myMass = this.GetComponent<Rigidbody2D>().mass;
        nBody = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float euclidDist = Mathf.Sqrt(Mathf.Pow((nBody.transform.position.x - transform.position.x), 2) + Mathf.Pow((nBody.transform.position.y - transform.position.y), 2) + Mathf.Pow((nBody.transform.position.z - transform.position.z), 2));

        Vector3 dir = this.transform.position - nBody.transform.position;
        //transform.LookAt(nBody.transform.position);
        float newtonForce = ((myMass * nBody.GetComponent<Rigidbody2D>().mass) / Mathf.Pow(euclidDist, 2));

        Vector3 appliedForce = Mathf.Round(newtonForce) * dir;
        appliedForce.z = 0;

        if (!float.IsNaN(appliedForce.x) && !float.IsNaN(appliedForce.y) && !float.IsNaN(appliedForce.z) &&
            !float.IsInfinity(appliedForce.x) && !float.IsInfinity(appliedForce.y) && !float.IsInfinity(appliedForce.z) &&
            !float.IsNegativeInfinity(appliedForce.x) && !float.IsNegativeInfinity(appliedForce.y) && !float.IsNegativeInfinity(appliedForce.z))
        {
            nBody.GetComponent<Rigidbody2D>().AddForce(appliedForce);
        }
    }
}
