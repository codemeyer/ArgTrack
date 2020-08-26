---
title: "ArgTrack: Object Shape Details Window"
aliases:
    - "/object-shape-details-window"
---

# Object Shape Details Window

This window allows you to perform some very rough editing
of a 3D object.

<img alt="Object shape details window screenshot" src="/argtrack/images/object-shape-details-main.png" class="img-fluid" />

At the top, you get some information about the shape.

<table class="table table-bordered table-striped table--medium">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Id</td>
            <td>The Id that is used to reference the object in an Object Setting</td>
        </tr>
        <tr>
            <td>HeaderIndex</td>
            <td>The order that the object appears in the internal header list</td>
        </tr>
        <tr>
            <td>DataIndex</td>
            <td>The order that the object data appears in the actual file</td>
        </tr>
        <tr>
            <td>Description</td>
            <td>For some objects this will show a description of the object. This description is based on its points and vectors, so if these are edited the description may disappear.</td>
        </tr>
        <tr>
            <td>Shape Signature</td>
            <td>The unique hash of the object based on its points and vectors.</td>
        </tr>
    </tbody>
</table>


Below this data, there are four tabs.


## Scales, Points and Vectors

This tab allows you to edit the basic 3D shape of the object.

_NB: Since the entire data for a 3D object has not yet been fully decomposed, it is possible
that adding or removing the values here will either break the objects, or break the
entire track_


### Scales

A scale value is used as values in the point definitions.

Use the "Add" button to add a new scale value, which can then be used
in a scale point.

Use the "Remove" button to delete the selected scale value. This is only available if the scale value
is not used in any point. Otherwise, the button is disabled.

<img alt="Object shape scale value editing" src="/argtrack/images/object-shape-details-edit-scalevalue.png" class="img-fluid" />


### Points

A point can be of two types, either a scale point or a reference point.

A scale point uses one of the values if the scale values list for its X coordinate, and one scale value for its Y coordinate. The Z coordinate can be set to anything.

Double-clicking a scale point allows you to edit its X and Y scale value references, as well as its Z value.

Use the "Add Scale" button to add a new scale point, which uses scale values to set the
X and Y coordinates.

<img alt="Object shape scale point editing" src="/argtrack/images/object-shape-details-edit-scalepoint.png" class="img-fluid" />

A reference point uses a previously defined point to get its X and Y coordinates.

Double-clicking on a reference point allows you to select which other point it gets its X and Y coordinates from, as well as its Z value.

Use the "Add Ref." button to add a new reference point, which uses the same X and Y coordinates as
a previously defined scale point.

Use the "Remove" button to delete the selected point. This is only available if the point
is not used in any vector. Otherwise, the button is disabled.

<img alt="Object shape reference point editing" src="/argtrack/images/object-shape-details-edit-refpoint.png" class="img-fluid" />


### Vectors

Vectors define how the various points are connected to each other. Each vector has a point it goes from, and a point it goes to.

Double-clicking a vector allows you to edit which points it goes from and to.

Use the "Add" button to add a new vector.

Use the "Remove" button to delete the selected vector.

<img alt="Object shape vector editing" src="/argtrack/images/object-shape-details-edit-vector.png" class="img-fluid" />



## GraphicElements

This tab allows you to edit two types of information, GraphicElements and GraphicElementLists.

Both are currently just edited as a set of raw bytes.

For 3D shapes, the GraphicElements contain references to vectors and color information for the polygon that they defined. For some shapes that also contain flat objects, some data here refers to which "flat" object that will be displayed.


## Misc/Other

These are various headers and other values that are either irrelevant or not fully understood yet.


## Offsets

These are the offsets of the various data in the file. You cannot edit these.
