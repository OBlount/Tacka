# **Tacka: A CS:GO radar project**

![TackaLogo](https://github.com/OBlount/Tacka/blob/main/CSGO-Radar-Cheat/Assets/Icons/TackaLogo.png)

## Description

Tacka *('Point' in Bosnian)*, is a personal C# project that attaches to the Counter-Strike: Global Offensive process. It is permitted to only read parts of the process's memory *(localPlayer position, localPlayer team, entityCoords, etc.)* and stream values to a locally hosted HTTP server in real-time.

Player data on the game server will be viewable on the HTTP server in the look of a traditional CS:GO radar. The user will then be able to see where players are on the map (if rendered), and see other data such as health and team.

This radar 'cheat' is against [CS:GO's rules & TOS](https://blog.counter-strike.net/) and should only be used on private servers where permission is given.

`git clone https://github.com/OBlount/Tacka.git`

## Usage

When `Tacka - CSGO-Radar-Cheat` is executed, a local server defaulted at port 8080 will run. Make sure CS:GO is running so the program can attach correctly. Once it is successfully attached, head to `http://localhost:PORT/` to see the radar in action!

## TODO

**Front End**
* :pushpin: Showcase all player data to root `/`
* :pushpin: Implement Radar with correct map
* :pushpin: Display where all players are located on the radar

**Internal**
* :pushpin: Post player and map data to local server in JSON format
* :pushpin: Make entity data more readable in console
* :pushpin: Make HTTP server able to send client images (such as the radar and favicon)

## License
This project is under [MIT](https://choosealicense.com/licenses/mit/).