---
title: "ArgTrack: Track Section Details Window"
aliases:
    - "/track-section-details-window"
---

# Track Section Details Window

This window contains details about a specific track section.

<img src="/argtrack/images/argtrack-sections-detail.png" alt="ArgTrack Sections tab screenshot" class="img-fluid" />
<br />

<h3>Section Info</h3>
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
            <td></td>
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
<br />

<h3>Kerbs</h3>
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
<br />

<h3>Walls/Fences</h3>
<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Remove Left</td>
            <td>Hides the wall on the left side</td>
        </tr>
        <tr>
            <td>Remove Right</td>
            <td>Hides the wall on the right side</td>
        </tr>
        <tr>
            <td>Join Left with Next</td>
            <td>
                Joins the starting point of the left-side wall in this track section with the starting point of the
                next non-bridged section
            </td>
        </tr>
        <tr>
            <td>Join Right with Next</td>
            <td>
                Joins the starting point of the right-side wall in this track section with the starting point of the
                next non-bridged section
            </td>
        </tr>
    </tbody>
</table>
<br />

<h3>Road Signs</h3>
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
<p>
    Note that these can be combined in various ways.
</p>
<br />

<h3>Misc</h3>
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
<br />


### Commands

There are various track commands associated with each section.
These can have a lot of different functions.

<p>
    A full list of track commands is available at
    <a href="/argdocs/track-data/track-commands/">ArgDocs</a>.
</p>
<p>
    To add a command of a specific type, select the type in the "Add Command of Type" dropdown list and click
    the "Add" button.
</p>
<p>
    When adding or editing track commands, this is the generic window
    that will appear.
</p>

<p>
    <img alt="Generic track command details window screenshot" src="/argtrack/images/argtrack-command-generic-details-window.png" class="img-fluid" />
</p>

<p>
    However, some commands have specialized windows.
    In the future, ArgTrack will feature more and more specialized windows.
</p>

<p>
    For example, this is the window that appears when editing a 0x80 object placement command:
</p>

<p>
    <img alt="Track command 0x80 details window screenshot" src="/argtrack/images/argtrack-command-0x80-details-window.png" class="img-fluid" />
</p>

<p>
    To delete the selected track command, click the "Delete Selected" button.
</p>
