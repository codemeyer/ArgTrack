---
title: "ArgTrack: Object Settings Details Window"
aliases:
    - "/object-settings-details-window"
---

# Object Settings Details Window

An object setting refers to an instance of either a flat (billboard) track-side object,
or a 3D object such as a buildling.

This window allows you to edit various properties of instances of objects, e.g. location and rotation.

<img src="/argtrack/images/argtrack-object-settings-details.png" alt="Object shape details window screenshot" class="img-fluid" />


The values that can be edited are:

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Offset</td>
            <td>
                The object setting offset value is the value that is used when referencing
                the object in a <a href="/argdocs/track-data/track-commands/#command-80">0x80 track command</a>.
            </td>
        </tr>
        <tr>
            <td>Id</td>
            <td>
                The object shape that will be used. The first 17 items are of various internal types, as well as "flat"
                track-side objects such as bushes and trees.<br />
                <br />
                3D objects will have the text "3D Object" next to them.
            </td>
        </tr>
        <tr>
            <td>Id2</td>
            <td></td>
        </tr>
        <tr>
            <td>Unknown</td>
            <td>
                Possibly a subitem selection and/or color related
            </td>
        </tr>
        <tr>
            <td>Unknown2</td>
            <td>
                Possibly related to the drawing order of the objects
            </td>
        </tr>
        <tr>
            <td>Distance from Track</td>
            <td>
                The distance from the center of the track. Negative values indicate that the object should
                appear on the left side of the track.
            </td>
        </tr>
        <tr>
            <td>Angle X</td>
            <td>The X angle of the object</td>
        </tr>
        <tr>
            <td>Angle Y</td>
            <td>The Y angle of the object</td>
        </tr>
        <tr>
            <td>Height</td>
            <td>The height above the track that the object will be placed.</td>
        </tr>
        <tr>
            <td>Detail Level</td>
            <td>
                Determines at which level of graphical detail level the object will appear. Also affects drawing order.
            </td>
        </tr>
    </tbody>
</table>
