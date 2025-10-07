using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;
    private SpriteRenderer sr;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.white;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = inactiveColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            activated = true;
            sr.color = activeColor;

            collision.GetComponent<Player>().SetCheckpoint(transform.position);
        }
    }
}