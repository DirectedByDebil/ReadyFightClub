using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class PlayerControl: MonoBehaviour
{
    [SerializeField, Header("How far player can rewind")] private float rewindPower;

    [SerializeField, Header("Button to pause")] private KeyCode pauseButton;
    [SerializeField, Header("Button to switch to first camera")] private KeyCode jokeButton;
    [Space, SerializeField, Header("Playable Director")] private PlayableDirector director;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform jokes;


    private bool isPaused = false;
    private int amountOfjokes;
    [SerializeField] private List<PlayableDirector> listOfJokes = new ();
    

    private void CheckPause()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            director.Pause();
            canvas.gameObject.SetActive(true);
        }
        else
        {
            director.Resume();
            canvas.gameObject.SetActive(false);
        }

    }

    private void Rewind()
    {
        if (Input.GetButtonDown("Horizontal") && !isPaused)
        {
            float dir = Input.GetAxis("Horizontal");

            if (dir > 0) dir = 1;
            else dir = -1;
            director.time += dir * rewindPower;
        }
    }

    private void CheckBorders()
    {
        if (director.time < 0)
            director.time = 0;

        if (director.time >= director.duration - 2)
        {
            isPaused = true;
        }
    }

    private void MakeJoke()
    {

        var allKeys = System.Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>();

        foreach (var key in allKeys)
        {
            if (key >= jokeButton && key < (jokeButton + amountOfjokes))
                if (Input.GetKeyDown(key))
                {
                    int index = (int)key - (int)jokeButton;

                    listOfJokes[index].Play();

                }
        }

    }




    private void Start()
    {
        amountOfjokes = jokes.childCount;

        for (int i = 0; i < amountOfjokes; i++)
        {
            listOfJokes.Add(jokes.GetChild(i).GetComponent<PlayableDirector>());
        }

    }



    private void Update()
    {
        CheckBorders();

        CheckPause();

        Rewind();

        MakeJoke();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Should be closed!");
            Application.Quit();
        }

    }

}
