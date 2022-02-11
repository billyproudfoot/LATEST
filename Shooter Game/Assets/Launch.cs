using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Launch : MonoBehaviour
{       
    
        public GameObject player;
        public GameObject enemy;
        public int enemies = -1;
        public int waveReached = 0;

    void Start()
    {
        string readText = File.ReadAllText("gameSettings.txt");
        switch (readText)
        {
            case "1":
                level1();
                break;
            case "2":
                level2();
                break;
            case "3":
                level3();
                break;
            case "4":
                level4();
                break;
            case "h":
                player.transform.position = new Vector3(28, 3, -250);
                newWave();
                break;
        }
    }

    void level1()
    {
        player.transform.position = new Vector3(28, 3, 21);
        enemies = 5;
    }

    void level2()
    {
        player.transform.position = new Vector3(-10, 3, -83);
        enemies = 7;
    }

    void level3()
    {
        player.transform.position = new Vector3(-20, 3, -110);
        enemies = 10;
    }

    void level4()
    {
        player.transform.position = new Vector3(27, 3, -195);
        enemies = 15;
    }

    void horde()
    {
        for (int i = 0; i < waveReached; i++)
        {
            enemies++;
            GameObject go = GameObject.Instantiate(enemy);
            go.transform.position = new Vector3(25,3,-200 + i*10);
        }
    }

    public int getEnemies()
    {
        return enemies;
    }

    void newWave()
    {
        waveReached++;
        horde();
    }

}
