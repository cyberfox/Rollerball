using System;
using UnityEngine;

public class BadGuys : MonoBehaviour
{
    public int score = 10;

    private Boolean hasScored = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScored()
    {
        hasScored = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Terrain" && !hasScored)
        {
            GameObject hsObj = GameObject.Find("HighScore");
            HighScore hs = hsObj.GetComponent<HighScore>();
            hs.AddScore(score);
            hasScored = true;
        }
    }
}
