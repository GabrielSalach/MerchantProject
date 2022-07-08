using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGraph
{
    int maxFloorCount;
    List<LinkedList<Room>> graph;

    public RoomGraph(int maxFloorCount) {
        this.maxFloorCount = maxFloorCount;

        graph = new List<LinkedList<Room>>(maxFloorCount);
        for(int i = 0; i < maxFloorCount; i++) {
            graph[i] = new LinkedList<Room>();
        }
    }
    

}
