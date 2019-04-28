using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class BoardManager : MonoBehaviour {
    public GameObject exit;                                           // Prefab to spawn for exit.
    public GameObject trapdoor;                                           // Prefab to spawn for exit.
    public GameObject[] floorTiles;                                   // Array of floor prefabs.
    public GameObject[] wallTiles;                                    // Array of wall prefabs.
    public GameObject pileOfGold;                                     // Reward prefabs.
    public GameObject chest;                                          // Reward prefabs.
    public GameObject enemy;                                          // Enemy prefabs.

    private Transform boardHolder;                                    //A variable to store a reference to the transform of our Board object.
    private List <Vector3> gridPositions = new List <Vector3> ();    //A list of possible locations to place tiles.


    string[][] readLevelFile (int level) {
        string text = Tilesmaps.GetLevel(level);
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;
        
        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)  {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }

    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup (int levelNumber) {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject ("Board").transform;
        string[][] level = readLevelFile(levelNumber);

        for (int y = 0; y < level.Length; y++) {
            for (int x = 0; x < level[0].Length; x++) {
                if (level[y][x] == "X") {
                    InstantiateObject(x, y, wallTiles[Random.Range (0,wallTiles.Length)]);
                } else if (level[y][x] == "0" || level[y][x] == "E" || level[y][x] == "C" || level[y][x] == "G" || level[y][x] == "P" || level[y][x] == "T") {
                    InstantiateObject(x, y, floorTiles[Random.Range (0,floorTiles.Length)]);
                }

                switch (level[y][x]) {
                    case "E":
                        InstantiateObject(x, y, exit);
                        break;
                    case "T":
                        InstantiateObject(x, y, trapdoor);
                        break;
                    case "G":
                        InstantiateObject(x, y, pileOfGold);
                        break;
                    case "C":
                        InstantiateObject(x, y, chest);
                        break;
                    case "P":
                        InstantiateObject(x, y, enemy);
                        break;
                }
            }
        }
    }

    void InstantiateObject(int x, int y, GameObject toInstantiate) {
        GameObject instance =
            Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

        //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
        instance.transform.SetParent (boardHolder);
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene (int level) {
        //Creates the outer walls and floor.
        BoardSetup (level);

        // //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
        // LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
    }
}