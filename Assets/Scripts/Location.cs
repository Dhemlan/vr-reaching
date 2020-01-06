using UnityEngine;

public class Location{
    int name;
    Vector3 position;

    public Location(int name, float x, float y, float z){
        this.name = name;
        position = new Vector3(x, y, z);
    }

    public int GetName(){
        return name;
    }

    public Vector3 GetPosition(){
        return position;
    }
}