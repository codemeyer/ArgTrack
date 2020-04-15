---
title: "ArgTrack: Racing Line Details Window"
aliases:
    - "/best-line-details-window"
---

# Racing Line Details Window

There are two types of Best Line segments: Normal and Wide Radius.

### Normal

The Normal type of best line makes up the bulk of all best line segments in the default tracks.

<img src="/argtrack/images/argtrack-bestline-normal-details.png" alt="Best line normal details window screenshot" class="img-fluid" />


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
            <td>The length of the segment</td>
        </tr>
        <tr>
            <td>Radius</td>
            <td>
                Positive values for right-hand turns, negative values for left-hand turns.
                Smaller value for sharper turns.
            </td>
        </tr>
        <tr>
            <td>Correction</td>
            <td>
                Adjusts the starting point of the segment forward or backward along the
                best line. Is used to make fine-tuning adjustments when adjusting the
                length of a segment would not be exact enough.
            </td>
        </tr>
    </tbody>
</table>


### Wide Radius

The Wide Radius type is used for very long curved segments, such as the back straight at Montreal.

<img src="/argtrack/images/argtrack-bestline-wideradius-details.png" alt="Best line wide radius details window screenshot" class="img-fluid" />

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
            <td>The length of the segment</td>
        </tr>
        <tr>
            <td>Low Radius</td>
            <td></td>
        </tr>
        <tr>
            <td>High Radius</td>
            <td></td>
        </tr>
    </tbody>
</table>
