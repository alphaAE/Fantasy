using UnityEngine;

public struct NextObstacleInfo {
    public NextObstacleInfo(Vector3 pos, int count, bool isLeft) {
        this.pos = pos;
        this.count = count;
        this.isLeft = isLeft;
    }

    public NextObstacleInfo(NextObstacleInfo info) {
        this.pos = info.pos;
        this.count = info.count;
        this.isLeft = info.isLeft;
    }

    public Vector3 pos;
    public int count;
    public bool isLeft;
}