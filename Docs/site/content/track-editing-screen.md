---
title: "ArgTrack: Track Editing Screen"
---

# Track Editing Screen

The Track Editing screen allows you to load and edit an existing track file.
It can then be saved and enjoyed in the game.

Note that you need to load a track before you can make any changes.

There is currently no preview of the track available, which makes large editing projects difficult.
More work on this will be done in the future.

More technical details about the various types of track data can be found
at [ArgDocs](/argdocs).

For a tutorial that introduces some simple track editing, have a
look at the [track editing tutorial](/argtrack/tutorials/kerbs-and-walls).


## Menus and Buttons

At the top there are buttons for loading and saving tracks. Depending on which tab is selected,
additional buttons may appear. They are described in the sections below.

There is also a <a href="/argtrack/menu-info">menu at the top</a> of the window which contains various
functionality.


## Misc tab

This tab contains some miscellaneous track data.

<img src="/argtrack/images/argtrack-misc-02.png" alt="ArgTrack Misc tab screenshot" class="img-fluid" />

The data that can be edited now is:

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>First Section Angle</td>
            <td>The angle of the first track section in the 3D world</td>
        </tr>
        <tr>
            <td>First Section Height</td>
            <td>The height change of the first track section</td>
        </tr>
        <tr>
            <td>Track Center X</td>
            <td>X-axis location of track in 3D world</td>
        </tr>
        <tr>
            <td>Track Center Y</td>
            <td>Y-axis location of track in 3D world</td>
        </tr>
        <tr>
            <td>Track Center Z</td>
            <td>Z-axis location of track in 3D world</td>
        </tr>
        <tr>
            <td>Track Start Width</td>
            <td>Width of first track section</td>
        </tr>
        <tr>
            <td>Left Verge Start Width</td>
            <td>Width of the left verge of the first track section</td>
        </tr>
        <tr>
            <td>Right Verge Start Width</td>
            <td>Width of the right verge of the first track section</td>
        </tr>
        <tr>
            <td>Surrounding Area</td>
            <td>Whether the area surrounding the track is green (grass) or gray</td>
        </tr>
        <tr>
            <td>Pole Side</td>
            <td>Which side of the track that pole position is on, left or right</td>
        </tr>
        <tr>
            <td>Pits Side</td>
            <td>Which side of the track that the pits are located</td>
        </tr>
        <tr>
            <td>Kerb Type</td>
            <td>Whether kerbs have two or three colors (one of which is always white)</td>
        </tr>
        <tr>
            <td>Kerb Fixed Upper Color</td>
            <td>Upper color of kerbs that cannot be changed</td>
        </tr>
        <tr>
            <td>Kerb Fixed Lower Color</td>
            <td>Lower color of kerbs that cannot be changed</td>
        </tr>
    </tbody>
</table>
<p>
    Kerbs can have two or three colors.
</p>


<h2 id="sections-tab">Sections tab</h2>
<p>
    Each track consists of a number of track sections, which are either straight
    or curved.
</p>
<p>
    <img src="/argtrack/images/argtrack-sections-02.png" alt="ArgTrack Sections tab screenshot" class="img-fluid" />
</p>
<p>
    You can use the buttons at the top to add a new section or delete an existing section.
    To edit a section, double-click it in the list.
</p>
<p>
    For more details, see <a href="/argtrack/track-section-details-window">Track Section Details Window</a>.
</p>

<h2 id="pit-lane-tab">Pit Lane tab</h2>

<p>
    Like the basic track, the pit lane consists of a number of sections.
</p>
<p>
    <img src="/argtrack/images/argtrack-pitlane-02.png" alt="ArgTrack Pit Lane tab screenshot" class="img-fluid" />
</p>
<p>
    You can use the buttons at the top to add a new section or delete an existing section.
    To edit a section, double-click it in the list.
</p>
<p>
    For more details, see <a href="/argtrack/track-section-details-window">Track Section Details Window</a>.
</p>


<h2 id="best-line-tab">Best Line tab</h2>

<p>
    Describes the best line/computer car line.
</p>
<p>
    <img src="/argtrack/images/argtrack-bestline-02.png" alt="ArgTrack Best Line tab screenshot" class="img-fluid" />
</p>
<p>
    You can use the buttons at the top to add a new section or delete an existing section.
    To edit a section, double-click it in the list.
</p>
<p>
    For more details, see the <a href="/argtrack/best-line-details-window">Best Line Details Window</a>.
</p>


<h2 id="object-settings-tab">Object Settings tab</h2>
<p>
    An object setting is an instance of a either a 3D object, a "flat" object
    (such as trees and bushes) or other special types of objects.
</p>
<p>
    <img src="/argtrack/images/argtrack-object-settings-02.png" alt="ArgTrack Object Settings tab screenshot" class="img-fluid" />
</p>
<p>
    Instances of objects can then be placed around the track with the 0x80 track command.
</p>
<p>
    Object Settings can be edited by double-clicking an item in the list.
    This opens the <a href="/argtrack/object-settings-details-window">Object Settings Details Window</a> for the object setting
    that you double-clicked.
</p>


<h2 id="object-shapes-tab">Object Shapes tab</h2>
<p>
    This tab displays data about the various 3D objects that are placed around
    the track. Since the structure of this data is still not fully known,
    it only shows the data as "Data1", "Data2" etc.
</p>
<p>
    However, what we do know is that these lumps of data most likely represent
    points, lines, planes, etc.
</p>
<p>
    <img src="/argtrack/images/argtrack-object-shapes-02.png" alt="ArgTrack Object Shapes tab screenshot" class="img-fluid" />
</p>
<p>
    Double-click on a line to open the object shape for editing with the
    <a href="/argtrack/object-shape-details-window">Object Shape Details</a> window.
</p>


<h2 id="horizon-tab">Horizon tab</h2>
<p>
    Here you can see the horizon that the game renders for the track.
    You can load a horizon (e.g. from another track file) or save it to disk.
</p>
<p>
    <img src="/argtrack/images/argtrack-horizon-02.png" alt="ArgTrack Horizon tab screenshot" class="img-fluid" />
</p>


<h2 id="setup-tab">Setup tab</h2>
<p>
    This tab allows you to edit the setup that is used by
    the computer-controlled cars.
</p>
<p>
    <img src="/argtrack/images/argtrack-setup-02.png" alt="ArgTrack Setup tab screenshot" class="img-fluid" />
</p>
