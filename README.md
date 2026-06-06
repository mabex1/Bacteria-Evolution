# Bacteria Evolution

A 2D evolution simulator where bacteria with different genes compete for survival.

![Screenshot](screenshots/screenshot1.png)

## Features

- 8 gene types, each with a unique color and ability
- Natural selection: energy, hunger, reproduction, mutation
- Hunters attack other bacteria
- Vampires steal energy from other bacteria
- Armored bacteria take less damage
- Scientists have higher mutation chance
- Peaceful bacteria reproduce faster
- Food spawns automatically over time
- Custom shader background
- Division sound effect

## Controls

- **Add food** — Left mouse click
- **Restart simulation** — R button

## Genes

- **Default (Gray)** — Balanced stats
- **Speed (Blue)** — Moves faster, consumes more energy
- **Hunter (Red)** — Attacks other bacteria
- **Hunger (Green)** — Consumes less energy
- **Peaceful (White)** — Reproduces faster
- **Scientist (Light Blue)** — Higher mutation chance
- **Armor (Orange)** — Takes less damage, moves slower
- **Vampire (Purple)** — Steals energy from other bacteria

## How to Play

1. Watch the simulation run
2. Add food manually by clicking anywhere
3. Speed up time to see evolution in action
4. Restart to start a new simulation

## Requirements

- Godot 4.6 or later (.NET version)
- .NET 8.0 SDK

## Build from Source

git clone https://github.com/yourusername/bacteria-evolution.git
cd bacteria-evolution

Open the project in Godot and press F5 to run.
License

MIT License
Author

Created by https://github.com/mabex1
