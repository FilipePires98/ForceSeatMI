# ForceSeatMI
A Study on MotionSystem's ForceSeatMI SDK for Programmatic Platform Interaction

[MotionSystems](https://motionsystems.eu/) | 
[ForceSeatMI SDK](https://motionsystems.eu/product/software/sdk-forceseatmi/) | 
[PS-3TM-200](https://motionsystems.eu/product/motion-platforms/ps-3tm-200/) | 
[Showcase](https://c4z3q2x8.rocketcdn.me/wp/wp-content/uploads/2020/09/movie-bg-ps-3tm-200.webm)

![](https://img.shields.io/badge/Software-Unity-lightgrey)
![](https://img.shields.io/badge/Language-C%20Sharp-lightgrey)
![](https://img.shields.io/badge/License-MIT-lightgrey)

![wallpaper](https://github.com/FilipePires98/ForceSeatMI/blob/main/img/MotionSystems.png)

## Description

The goal of this project is to study ForceSeatMI (Motion Interface), which is a programming interface that allows to add a motion platform support to basically any application or game.
Based on Motion System's [documentation](https://motionsystems.eu/category/forceseatmi), a Unity application was developed to:

1. Connect with ForceSeat Platform Manager 
2. Read positional data and visually present it in a simulated platform
3. Send commands programmatically and/or through user interaction

The current version only works with Unity bindings, but the adaptation to other game engines and programmming languages should be easy.
Also, the implementation only works on Windows.

## Instructions to Install and Run

You need to own a MotionSystem's platform and have working versions of the ForceSeatPM and the appropriate ForceSeatMI.
You also need an updated Unity, this project uses version 2019.4.10f1.

Assuming all dependencies are complied, a direct usage by opening the project in Unity and pressing the play button should work just fine.
Make sure you follow all of the security measures provided by MotionSystems.

<p float="left">
  <img src="https://github.com/FilipePires98/ForceSeatMI/blob/main/img/UnityCapture.gif" width="360px">
  <img src="https://github.com/FilipePires98/ForceSeatMI/blob/main/img/LiveCapture.gif" width="360px">
</p>

## Credits

The project was developed by me, based on the example projects provided in MotionSystem's [documentation](https://motionsystems.eu/category/forceseatmi).
