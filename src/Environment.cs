using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Location;
using static Chunk;

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