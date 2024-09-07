using UnityEngine;

public class Flock : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) < 20)
        {
            ApplyFlock();
        }
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void ApplyFlock()
    {
        GameObject[] gameObjects;
        gameObjects = FlockManager.FM.allFish;
        Vector3 flockCentre = Vector3.zero;
        Vector3 avoidVector = Vector3.zero;
        float groupSpeed = 0.01f;
        float neighbourDist;
        int groupSize = 0;

        foreach (GameObject go in gameObjects)
        {
            if (go != this.gameObject)
            {
                neighbourDist = Vector3.Distance(go.transform.position, this.transform.position);
                if (neighbourDist <= FlockManager.FM.neighbourDistances)
                {
                    flockCentre += go.transform.position;
                    groupSize++;

                    if (neighbourDist < 1.0f)
                    {
                        avoidVector = avoidVector + (this.transform.position - go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    groupSpeed = groupSpeed + anotherFlock.speed;
                }
            }
        }
        if (groupSize > 0)
        {
            flockCentre = flockCentre / groupSize + (FlockManager.FM.goalPos - transform.position);
            speed = groupSpeed / groupSize;
            if (speed > FlockManager.FM.maxSpeed)
                speed = FlockManager.FM.maxSpeed;

            Vector3 direction = (flockCentre + avoidVector) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockManager.FM.rotationSpeed * Time.deltaTime);
        }

    }
}
