using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Location;
using static Chunk;
using static Environment;

/**
 * A land generator is a component that generates land.
 * It is a part of the world.
 */
public class LandGenerator : MonoBehaviour {
    public int chunkSize = 16;
    public int locationScale = 10;

    private Environment environment;
    private int currentChunkX = 0;
    private int currentChunkZ = 0;

    // Start is called before the first frame update
    void Start() {
        environment = new Environment(chunkSize, locationScale);
    }

    // Update is called once per frame
    void Update() {
        // change current chunk Z & X upon arrow key press
        handleInput();

        // check if chunk exists
        Chunk chunk = environment.getChunk(currentChunkX, currentChunkZ);
        if (chunk == null) {
            createNewChunk();
        }
    }

    void handleInput() {
        // change current chunk Z & X upon arrow key press
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentChunkZ++;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            currentChunkZ--;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentChunkX--;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            currentChunkX++;
        }
    }

    void createNewChunk() {
        // create new chunk
        Debug.Log("Creating new chunk at " + currentChunkX + ", " + currentChunkZ);
        Chunk chunk = new Chunk(currentChunkX, currentChunkZ, chunkSize, locationScale);
        environment.addChunk(chunk);
    }
}