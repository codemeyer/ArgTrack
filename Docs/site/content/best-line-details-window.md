---
title: "ArgTrack: Best Line Details Window"
---

<h1 id="best-line-details-window">Best Line Details Window</h1>

<p>
    There are three types of Best Line segments. Displacement, which is always the first one;
    Normal, and Wide Radius.
</p>




<h3>Displacement</h3>

<p>
    The Displacement best line type always appears as the first one in the list of best line segments.
    It cannot be moved or deleted.
</p>

<p>
    <img alt="Best line displacement details window screenshot" src="/argtrack/images/argtrack-bestline-displacement-details.png" class="img-fluid" />
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
            <td>Length</td>
            <td>The length of the segment</td>
        </tr>
        <tr>
            <td>Displacement</td>
            <td></td>
        </tr>
        <tr>
            <td>Radius</td>
            <td></td>
        </tr>
        <tr>
            <td>Correction</td>
            <td></td>
        </tr>
    </tbody>
</table>
<br />


<h3>Normal</h3>

<p>
    The Normal type of best line makes up the bulk of all best line segments in the default tracks.
</p>

<p>
    <img alt="Best line normal details window screenshot" src="/argtrack/images/argtrack-bestline-normal-details.png" class="img-fluid" />
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
<br />


<h3>Wide Radius</h3>

<p>
    The Wide Radius type is used for very long curved segments, such as the back straight at Montreal.
</p>

<p>
    <img alt="Best line wide radius details window screenshot" src="/argtrack/images/argtrack-bestline-wideradius-details.png" class="img-fluid" />
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
<br />
