# ForceSeatMI-ScheduledInteraction
A Study on MotionSystem's ForceSeatMI SDK for Programmatic Platform Interaction


# TDAzureMerger
 A Point-Cloud Merger for Azure Kinects

[MotionSystems](https://motionsystems.eu/) | 
[ForceSeatMI SDK](https://motionsystems.eu/product/software/sdk-forceseatmi/) | 
[PS-3TM-200](https://motionsystems.eu/product/motion-platforms/ps-3tm-200/) | 
[Showcase](https://c4z3q2x8.rocketcdn.me/wp/wp-content/uploads/2020/09/movie-bg-ps-3tm-200.webm) | 
[Tutorial](https://vimeo.com/501525725)

![](https://img.shields.io/badge/Software-Unity-blue)
![](https://img.shields.io/badge/Language-C%20Sharp-blue)
![](https://img.shields.io/badge/Hardware-Azure%20Kinect-blue)
![](https://img.shields.io/badge/License-MIT-lightgrey)

![wallpaper](https://github.com/FilipePires98/ForceSeatMI-ScheduledInteraction/img/MotionSystems.png)

## Description

The goal of this project is to 



provide a tool for visualization and calibration of multiple Azure Kinect sensors with TouchDesigner.
This method is very fast and easy to use.
Its implementation based on [Open3D](http://www.open3d.org/) is fully automatic, so there is no need for calibration boards (no chess patterns or charuco required).
Some screen captures are provided below.

The current version only works really well for situations where you have enough overlap between Kinects, since it depends on overlapping data from similar POV's (it won't work with cameras facing each other for instance). 
This issue can be mitigated using intermediate Kinects.
Also, the implementation only works on Windows.

![001](https://github.com/FilipePires98/TDAzureMerger/blob/main/img/001.png)
![002](https://github.com/FilipePires98/TDAzureMerger/blob/main/img/002.png)

## Instructions to Install and Run

You need to have a working version of the Open3D library in TouchDesigner.
This repository contains version 0.11.0, so you can use that directly by copying it to your TouchDesigner site packages folder, usually in a path like this:

```
C:\Program Files\Derivative\TouchDesigner.2020.28110\bin\Lib\site-packages
```

You can also try pip or conda install:

```
# Install Open3D stable release with pip
$ pip install open3d

# Install Open3D stable release with Conda
$ conda install -c open3d-admin -c conda-forge open3d

# Test the installation
$ python -c "import open3d as o3d; print(o3d)"
```

Note, however, that this tool was built with version 0.11.0, so there may be incompatibilities with later versions of the library online which I have not tested.
Safest is to simply use version 0.11.0 (the one provided with this package).

## Credits

The original creator of this tool is [Vincent](https://gitlab.com/v_brault) in 2020.
It was then reworked by [Darien](https://github.com/DarienBrito), which is currently accepting support requests.
I merely tested and tweaked TDAzureMerger.
