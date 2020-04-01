---
title: "ArgTrack: Track Validation"
---

<h1 id="track-validation-window">Track Validation Window</h1>

<p>
    The track validation functionality inspect the track for any missing - but required - track commands, and more.
</p>

<img alt="Track validation window screenshot" src="/argtrack/images/argtrack-track-validation-window.png" class="img-fluid" />

<p>
    The window shows a list of all adjustments that need to be made.
</p>
<p>
    Note however that it is fully possible that the track can still be fully playable despite having
    items appearing in this list. For example, there is a track command that appears at every single track
    that came with the game except the Phoenix circuit.
</p>


<h2>Items being validated</h2>

<h3>Track Commands</h3>

<table class="table table-bordered table-striped table--large">
    <thead>
        <tr>
            <th>Command</th>
            <th>Validation</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-86">Command 0x86</a> (Connect start of pit lane to track)</td>
            <td>This command must appear in exactly one (1) track section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-87">Command 0x87</a> (Connect end of pit lane to track)</td>
            <td>This command must appear in exactly one (1) track section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-96">Command 0x96</a> (Pit lane start)</td>
            <td>This command must appear in exactly one (1) pit lane section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-97">Command 0x97</a> (Pit lane end)</td>
            <td>This command must appear in exactly one (1) pit lane section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-9B">Command 0x9B</a> (Unknown pitlane marker 1)</td>
            <td>This command must appear in exactly one (1) pit lane section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-9C">Command 0x9C</a> (Unknown pitlane marker 2)</td>
            <td>This command must appear in exactly one (1) pit lane section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-9D">Command 0x9D</a> (Unknown pitlane marker 3)</td>
            <td>This command must appear in exactly one (1) pit lane section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-9E">Command 0x9E</a> (Unknown pitlane marker 4)</td>
            <td>This command must appear in exactly one (1) pit lane section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-9F">Command 0x9F</a> (Pit lane fences start)</td>
            <td>This command must appear in exactly one (1) pit lane section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-A0">Command 0xA0</a> (Pit lane fences end)</td>
            <td>This command must appear in exactly one (1) pit lane section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-A1">Command 0xA1</a> (Pit lane entry, join right pit lane fence)</td>
            <td>This command must appear in exactly one (1) track section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-A2">Command 0xA2</a> (Pit lane entry, join left pit lane fence)</td>
            <td>This command must appear in exactly one (1) track section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-A3">Command 0xA3</a> (Pit lane exit, join right pit lane fence)</td>
            <td>This command must appear in exactly one (1) track section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-A4">Command 0xA4</a> (Pit lane exit, join left pit lane fence)</td>
            <td>This command must appear in exactly one (1) track section</td>
        </tr>
        <tr>
            <td><a href="/argdocs/track-data/track-commands#command-A8">Command 0xA8</a> (Section that triggers the chequered flag)</td>
            <td>This command must appear in exactly one (1) track section</td>
        </tr>
    </tbody>
</table>
