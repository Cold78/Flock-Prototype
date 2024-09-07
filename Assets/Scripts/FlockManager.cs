using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject FishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 moveLimits = new Vector3(5, 3, 5);
    public static FlockManager FM;
    public Vector3 goalPos = Vector3.zero;
    private float speed = 1.0f;
    private float y;

    [Header("Minion Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistances;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFish();
        FM = this;
        goalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GoalMovement();
    }

    private void SpawnFish()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-moveLimits.x, moveLimits.x), Random.Range(-moveLimits.y, moveLimits.y), Random.Range(-moveLimits.z, moveLimits.z));
           allFish[i] = Instantiate(FishPrefab, pos, Quaternion.identity);
        }
    }

    private void GoalMovement()
    {
        goalPos = this.transform.position;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.Space))
        {
             y = 1;
        }
        else
        {
             y = 0;
        }

        Vector3 movement = new Vector3(moveHorizontal, y, moveVertical);
        transform.Translate(speed * Time.deltaTime * movement, Space.World);
    }
}
