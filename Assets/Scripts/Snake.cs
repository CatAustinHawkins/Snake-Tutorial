using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public GameObject apple;

    private Vector2 direction = Vector2.right;

    public int deaths;
    public Text deathstext;

    private List<Transform> _segments = new List<Transform>();

    public Transform segmentPrefab;

    public int initialsize = 4;


    private void Start()
    { 
        ResetState();
    }

    void Update()
    {
        deathstext.text = deaths.ToString();

        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {

        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0.0f);
    }


    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);

        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }

        if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
            apple.GetComponent<Food>().OnDeath();

        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialsize; i++ )
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;

        deaths = deaths + 1;
        deathstext.text = deaths.ToString();
    }
}
