using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSwitcher : MonoBehaviour
{
    public enum GameMode
    {
        Simulation,
        Edition,
        Planner
    }

    GameMode current;
    public Camera[] cameras;
    // Use this for initialization
    private void Awake()
    {
        current = GameMode.Edition;
    }

    public void SwitchToSimulation()
    {
        SwitchTo(GameMode.Simulation);
    }
    public void SwitchToEdition()
    {
        SwitchTo(GameMode.Edition);
    }
    public void SwitchToPlanner()
    {
        SwitchTo(GameMode.Planner);
    }

    public void SwitchTo(GameMode input)
    {
        if (input != current && !MapSaveState.current.isDead)
        {
            GameObject tmp = GameObject.FindGameObjectWithTag("Player");
            cameras[0] = tmp.GetComponentInChildren<Camera>(true);
            tmp.GetComponent<PlayerControl>().transform.position = tmp.GetComponent<PlayerControl>().pos;
            tmp.GetComponent<PlayerControl>().GetComponent<Rigidbody>().velocity = Vector3.zero;
            tmp.GetComponent<PlayerControl>().enabled = false;
            current = input;
            int cam = -1;
            switch (input)
            {
                case GameMode.Simulation:
                    cam = 0;
                    tmp.GetComponent<PlayerControl>().enabled = true;
                    break;
                case GameMode.Edition:
                    cam = 1;
                    break;
                case GameMode.Planner:
                    cam = 2;
                    break;
                default:
                    break;
            }
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(i == cam);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
