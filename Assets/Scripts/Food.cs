using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridarea;
    public GameObject grid;

    public Text scorecount;
    public int score;

    public int totalfoodeaten;
    public Text totalfoodcount;

    public int highscore;
    public Text highscorecount;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridarea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            score = score + 1;
            scorecount.text = score.ToString();

            totalfoodeaten = totalfoodeaten + 1;
            totalfoodcount.text = totalfoodeaten.ToString();

            if(score > highscore)
            {
                highscore = score;
                highscorecount.text = highscore.ToString();

            }
            RandomizePosition();
        }
    }
    
    public void OnDeath()
    {
        score = 0;
        scorecount.text = score.ToString();
    }

    private void Update()
    {
        scorecount.text = score.ToString();
        highscorecount.text = highscore.ToString();
    }
}
