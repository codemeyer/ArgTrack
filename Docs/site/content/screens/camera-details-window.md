---
title: "ArgTrack: Track Camera Details window"
---

# Track Camera Details window

The track camera command details window allows you to manipulate a command.

<img src="/argtrack/images/camera-command-details-window.png" alt="Camera commands list screenshot" class="img-fluid" />

There are three types of track camera commands:

- Adjustment
- Move Range Right
- Delete


### Type: Adjustment

Adjusts a single camera.

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Item</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Camera Index</td>
            <td>Index of camera to adjust</td>
        </tr>
        <tr>
            <td>Adjustment</td>
            <td>Number of track length units to move the camera(?)</td>
        </tr>
        <tr>
            <td>Track Side</td>
            <td>Which side of the track the camera should be</td>
        </tr>
    </tbody>
</table>


### Type: Move Range Right

Moves a range of cameras from the default left-hand side to the right-hand side
of the track.

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Item</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Camera Index From</td>
            <td>Index of first camera to move to the right side</td>
        </tr>        
        <tr>
            <td>Camera Index To</td>
            <td>Index of last camera to move to the right side</td>
        </tr>
    </tbody>
</table>


### Type: Delete

This deletes a specific camera.


<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Item</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Camera Index</td>
            <td>Index of camera to remove</td>
        </tr>
    </tbody>
</table>


