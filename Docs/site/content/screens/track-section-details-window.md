---
title: "ArgTrack: Track Section Details Window"
aliases:
    - "/track-section-details-window"
---

# Track Section Details Window

This window contains details about a specific [track section](/argtrack/topics/track-section/).

<img src="/argtrack/images/argtrack-sections-detail.png" alt="ArgTrack Sections tab screenshot" class="img-fluid" />


### Section Info

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Length</td>
            <td>Each section has a length, specified in a unit where 1 = 16 feet.</td>
        </tr>
        <tr>
            <td>Height Change</td>
            <td></td>
        </tr>
        <tr>
            <td>Curvature</td>
            <td>
                Describes if a section is straight (0) or turns (-32,768 to 32,768).
                See <a href="/argtrack/topics/corner-geometry">Topic: Corner Geometry</a>
            </td>
        </tr>
        <tr>
            <td>Left Verge Width</td>
            <td></td>
        </tr>
        <tr>
            <td>Right Verge Width</td>
            <td></td>
        </tr>
    </tbody>
</table>


### Kerbs

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Left Kerb</td>
            <td>Whether the section has a kerb on the left side of the track</td>
        </tr>
        <tr>
            <td>Right Kerb</td>
            <td>Whether the section has a kerb on the right side of the track</td>
        </tr>
        <tr>
            <td>Type</td>
            <td>Describes the type of kerb, either High or Low</td>
        </tr>
    </tbody>
</table>


### Walls/Fences

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Bridge Left</td>
            <td>
                When several walls are bridged, the wall will go directly from the
                starting point of the first bridged wall to the starting point of the
                next non-bridged wall.
                See: <a href="/argtrack/topics/bridging-walls/">Topic: Bridging walls</a>
            </td>
        <tr>
            <td>Remove Left</td>
            <td>Hides the wall on the left side(?)</td>
        </tr>
        <tr>
            <td>Bridge Right</td>
            <td>
                When several walls are bridged, the wall will go directly from the
                starting point of the first bridged wall to the starting point of the
                next non-bridged wall.
                See: <a href="/argtrack/topics/bridging-walls/">Topic: Bridging walls</a>
            </td>
        </tr>
        <tr>
            <td>Remove Right</td>
            <td>Hides the wall on the right side(?)</td>
        </tr>
    </tbody>
</table>


### Road Signs

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>300/200/100</td>
            <td></td>
        </tr>
        <tr>
            <td>Arrow/100</td>
            <td></td>
        </tr>
        <tr>
            <td>Arrow</td>
            <td></td>
        </tr>
    </tbody>
</table>

Note that these can be combined in various ways.


### Misc

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Pit Lane "Entrance"</td>
            <td></td>
        </tr>
        <tr>
            <td>Pit Lane "Exit"</td>
            <td></td>
        </tr>
        <tr>
            <td>Unknown</td>
            <td></td>
        </tr>
    </tbody>
</table>


### Commands

There are various track commands associated with each section.
These can have a lot of different functions.

A full list of track commands is available at [ArgDocs](/argdocs/track-data/track-commands/).


To add a command of a specific type, select the type in the "Add Command of Type" dropdown list and click
the "Add" button.

When adding or editing track commands, this is the generic window
that will appear.

<img alt="Generic track command details window screenshot" src="/argtrack/images/argtrack-command-generic-details-window.png" class="img-fluid" />

However, some commands have specialized windows.
In the future, ArgTrack will feature more and more specialized windows.

For example, this is the window that appears when editing a 0x80 object placement command:

<img alt="Track command 0x80 details window screenshot" src="/argtrack/images/argtrack-command-0x80-details-window.png" class="img-fluid" />

To delete the selected track command, click the "Delete Selected" button.
