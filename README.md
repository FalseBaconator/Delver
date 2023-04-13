# TextRPG

  This project is for my Intro to Object Oriented Programming (or OOP) class. While we needed to make a game like the original rogue, wherein the player explored a map, attacking enemies they walked into. While we needed to pull a map from a text file I decided to make mine procedurally generate the map, pulling from different text files for each pattern of exits possible. The map tries to fill in the entire grid, before removing rooms that are inaccessible to the player. This should ensure that the map is large enough to explore and doesn't have the problem that some map generators have with single hallway maps.
  
  The game is still in development, but it's currently functional. The game has start and end screens, multiple floors, and a boss who is pretty easy to cheese.

![Screenshot (8)](https://user-images.githubusercontent.com/113189775/231907326-a6f5f08b-4c66-449b-be81-4b094b2969a4.png)

# Controls:

Move: WASD/Arrows

Attack: WASD/Arrows

Exit: Esc

# Objective:

Find the stairs on each floor until you defeat the boss monster.

# Items:

Red *: Attack Boost

Green +: Health Restore

Blue #: Shield Restore

# Monsters:

Cyan O: Slime, moves randomly

Dark Green X: Goblin, chases player after they get too close

Dark Red X: Kobold, runs from player after they get too close

Dark Red M: Boss Monster, chases player
