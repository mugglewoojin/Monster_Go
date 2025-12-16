using UnityEngine;

public class Game_Data : MonoBehaviour
{
    public Room[,] room;
    public int[,] monster;
    void Start(){
        room = new Room[5, 5];
        monster = new int[2, 2];
        for(int i = 0; i < 5;i++){
            for(int j = 0;j<5;j++){
                room[i, j] = new Room();
                for(int k = 0;k<4;k++){
                    room[i, j].door[k] = false;
                }
            }
        }
        for(int i = 0;i<2;i++){

        }
    }

    int Mob_Move(int x, int y){

    }
}

public class Room
{
    public bool[] door = new bool[4];
    public int light = 0;

    public bool canmove(int dir){
        
    }
}
