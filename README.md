# Cthulhu
Cthulhu is a RPG game, and implement with unity.
---
---
## Descrpition
Game type: RPG, 3D
Platform: Unity3D
---
---
## Background
The Background story of this game is based on the [The Call of Cthulhu](https://en.wikipedia.org/wiki/The_Call_of_Cthulhu), which is a short story by American writer [H. P. Lovecraft](https://en.wikipedia.org/wiki/H._P._Lovecraft).
### ![H. P. Lovecraft](store/H._P._Lovecraft,_June_1934.jpg)

Main character prototype based on Francis Wayland Thurston who is the a character in [The Call of Cthulhu](https://en.wikipedia.org/wiki/The_Call_of_Cthulhu).
---
---
## Character Setting
### ![Main Character 1](store/Pj01.jpg)
### ![Main Character 2](store/Pj02.jpg)

---
---
## Current stage
 - Player movement Complete
 - User interface Complete
 - Environment
  -  making WindMill rotating Complete
  - ![WindMill](store/WindMill.png)
 - Enemy [Path Finding](https://en.wikipedia.org/wiki/Pathfinding)
 - ![Pathfinding](store/pathfinding.png)
---
---
## Coding Information
#### 1. About WindMill rotating
## ![WindMill](store/WindMill.png)
Based on the code below in file [SpinMe.cs](https://github.com/DaBaiHao/Cthulhu/blob/master/Cthulhu/Assets/Utility/SpinMe.cs) :

``` C
float xDegreesPerFrame = Time.deltaTime / 60 * 360 * xRotationsPerMinute;
transform.RotateAround(transform.position, transform.right, xDegreesPerFrame);
float yDegreesPerFrame = Time.deltaTime / 60 * 360 * yRotationsPerMinute;
transform.RotateAround(transform.position, transform.up, yDegreesPerFrame);
float zDegreesPerFrame = Time.deltaTime / 60 * 360 * zRotationsPerMinute;
transform.RotateAround(transform.position, transform.forward, zDegreesPerFrame);
```

---
#### 2. About Enemy [Path finding](https://en.wikipedia.org/wiki/Pathfinding)
## ![Pathfinding](store/pathfinding.png)
## ![Enemy](store/enemy.png)
