---
title: "ArgTrack: Track Conversion"
---

<h1 id="track-conversion-window">GP2 Track Conversion Window</h1>

<p>
    This functionality allows you to convert parts of a GP2 track into an F1GP track.
</p>

<img alt="Track conversion window screenshot" src="/argtrack/images/argtrack-gp2-track-conversion-window.png" class="img-fluid" />

<p>
    Data that is converted from the GP2 track is:<br/>
    <ul>
        <li>Track sections</li>
        <li>Pit lane sections</li>
    </ul>
</p>

<p>
    Data that is <em>not</em> converted from the GP2 track, but is instead copied from the "Template F1GP track" is:<br/>
    <ul>
        <li>Header info (kerb colors, etc)</li>
        <li>Best line segments</li>
        <li>Object settings</li>
        <li>Object shapes</li>
        <li>Horizon</li>
        <li>Computer car setup</li>
    </ul>
</p>

<p>
    Note that since objects are not copied, the 0x80 command that places objects along the track are removed from the converted track.
    This means that the converted track will be very empty, with no trackside objects at all.
</p>
<p>
    Another thing to note is that since the computer car line is not converted, driving on the converted track is very difficult
    with a keyboard or joystick, since the computer car line assists with the steering help. You need to engage Spin Gear to "enjoy"
    driving around the converted track, or use a controller that allows you to turn off all forms of steering help.
</p>

<br />

<p>
    To convert a GP2 track you must fill in the following information:
</p>

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Source GP2 Track</td>
            <td>The path to the GP2 track file that you wish to convert</td>
        </tr>
        <tr>
            <td>Template F1GP Track</td>
            <td>The F1GP track that will be used to "fill in the blanks", i.e. all data except track and pit lane sections.</td>
        </tr>
        <tr>
            <td>New F1GP Track</td>
            <td>The new F1GP track file that will be created by the conversion process.</td>
        </tr>
    </tbody>
</table>

<br/>

<p>
    Then you can click the OK button. ArgTrack will convert the track and open the newly created file.
</p>
