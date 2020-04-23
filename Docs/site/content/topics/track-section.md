---
title: "ArgTrack: Topic: Track Section"
---

# Topic: Track Section

A track section is the basic building block of an F1GP track.

It can be thought of as the sections that
make up a [Scalextric](https://en.wikipedia.org/wiki/Scalextric) track.

Each track section has a set of properties that define it, and a number of related "commands" that
handle track-side object placement, etc.

Track sections are used to describe both the track that the cars race on, as well as the pit lane.

In ArgTrack, track sections are edited using the [track section details](/argtrack/screens/track-section-details-window/) window.

The _curvature_ of a track section describes how much it turns. If it is 0, the section is a straight.

The allowed _curvature_ value for a corner goes from -32,768 to 32,768, where a positive value indicates a right-hand
corner, and a negative value indicates a left-hand turn.

More details about how corners are defined can be found in the [corner geometry](/argtrack/topics/corner-geometry/) description.
