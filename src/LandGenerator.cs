using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Location {
    private Vector3 position;
    private int scale;
    private string name;
    private GameObject gameObject;

    public Location(int xpos, int zpos, int scale) {
        this.position = new Vector3(xpos * scale, 0, zpos * scale);
        this.scale = scale;
        this.name = "Location_" + xpos + "_" + zpos;
        initializeGameObject();
    }

    public Vector3 getPosition() {
        return position;
    }

    public int getScale() {
        return scale;
    }

    public GameObject getGameObject() {
        return gameObject;
    }

    private void initializeGameObject() {
        this.gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        this.gameObject.name = name;
        this.gameObject.transform.position = position;
        this.gameObject.transform.localScale = new Vector3(scale, 1, scale);
        setColor(new Color(Random.value, Random.value, Random.value, 1.0f));
    }

    private void setColor(Color color) {
        this.gameObject.GetComponent<Renderer>().material.color = color;
    }
}

public class Chunk {
    private int size;
    private Location[,] locations;
    private Vector3 position;
    private int xpos;
    private int zpos;
    private string name;
    private GameObject gameObject;

    public Chunk(int xpos, int zpos, int size, int locationScale) {
        this.size = size;
        this.locations = new Location[size, size];
        this.xpos = xpos;
        this.zpos = zpos;
        this.name = "Chunk_" + xpos + "_" + zpos;
        this.position = calculatePosition(locationScale);
        initializeGameObject();
        generateLocations(locationScale);
    }

    public int getSize() {
        return size;
    }

    public int getX() {
        return xpos;
    }

    public int getZ() {
        return zpos;
    }

    public Vector3 getPosition() {
        return position;
    }

    public Location[,] getLocations() {
        return locations;
    }

    public Location getLocation(int x, int z) {
        return locations[x, z];
    }

    private Vector3 calculatePosition(int locationScale) {
        return new Vector3(xpos * size * locationScale, 0, zpos * size * locationScale);
    }

    private void initializeGameObject() {
        this.gameObject = new GameObject(name);
        this.gameObject.transform.position = position;
    }

    private void generateLocations(int locationScale) {
        for (int x = 0; x < size; x++) {
            for (int z = 0; z < size; z++) {
                locations[x, z] = new Location(xpos * size + x, zpos * size + z, locationScale);
                locations[x, z].getGameObject().transform.parent = gameObject.transform;
            }
        }
    }
}

public class Environment {
    private List<Chunk> chunks = new List<Chunk>();

    public Environment(int chunkSize, int locationScale) {
        // create initial chunk
        Chunk chunk = new Chunk(0, 0, chunkSize, locationScale);
        chunks.Add(chunk);
    }

    public void addChunk(Chunk chunk) {
        chunks.Add(chunk);
    }

    public Chunk getChunk(int xpos, int zpos) {
        foreach (Chunk chunk in chunks) {
            if (chunk.getX() == xpos && chunk.getZ() == zpos) {
                return chunk;
            }
        }
        return null;
    }

    public int getSize() {
        return chunks.Count;
    }
}