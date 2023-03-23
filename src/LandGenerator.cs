using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // create empty land object
        GameObject land = new GameObject("Land");
        int scale = 17;
        spawnChunk(0, 0, 0, scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnCube(int x, float y, int z, int scale, Color color) {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = GameObject.Find("Land").transform;
        cube.transform.position = new Vector3(x * scale, y, z * scale);
        cube.transform.localScale = new Vector3(scale, 1, scale);
        cube.transform.rotation = Quaternion.identity;
        cube.GetComponent<Renderer>().material.color = color;
        cube.name = "Cube (" + x + ", " + y + ", " + z + ")";
    }

    void spawnChunk(int xpos, int ypos, int zpos, int scale) {
        // spawn 17x17 cubes
        for (int x = xpos; x < xpos + 17; x++) {
            for (int z = zpos; z < zpos + 17; z++) {
                float height = Random.value;
                spawnCube(x, ypos + height, z, scale, new Color(0, Random.value, 0));
            }
        }
    }
}
