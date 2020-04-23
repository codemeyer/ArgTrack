---
title: "ArgTrack: Overview"
aliases:
    - "/introduction"
    - "/track-editing-screen"
---

# Overview

A track in F1GP consists of a number of [track sections](/argtrack/topics/track-section/). These can be thought of as the sections that
make up a [Scalextric](https://en.wikipedia.org/wiki/Scalextric) track.

Each track section has a set of properties that define it, and a number of related "commands" that
handle track-side object placement, etc.

This section will be expanded in future versions. More technical details about the various types
of track data can be found at [ArgDocs](/argdocs/).


## Window layout

The ArgTrack main window consists of three different parts.

There is the Top Menu, the Tree View Menu and the Track Layout.

When editing, there are also some [keyboard shortcuts](/argtrack/keyboard-shortcuts/) available.

To open the help documentation (at this site), you can press F1 on any screen to open the
related section in the documentation.


### Top Menu

<img src="/argtrack/images/argtrack-main-focus-menu.png" alt="ArgTrack main window" class="img-fluid" />

The [Top Menu](/argtrack/menus/top-menu/) of the window contains various
functionality, e.g. loading and saving the track.


### Tree View Menu

<img src="/argtrack/images/argtrack-main-focus-tree.png" alt="ArgTrack main window" class="img-fluid" />

After loading a track, the [Tree View Menu](/argtrack/menus/tree-view-menu/) to the left allows
you to manipulate the various data that describes the track.


### Track Layout

<img src="/argtrack/images/argtrack-main-focus-layout.png" alt="ArgTrack main window" class="img-fluid" />

The right part of the screen will display a track drawing. By default it draws the track and the pit lane.

Using the toolbar at the top, you can toggle what is drawn:

- Track
- Pit Lane
- Verges
- Track-side objects

Track-side objects do not resemble what they actually are, but are just indications of where the center point of
the track-side object is. Note that track-side objects placed in the pit lane are currently not displayed.

The Refresh button will force a redraw of the content.

You can zoom in and out by using the + and - buttons in the top right corner, or by using Ctrl+ and Ctrl-.

If the content does not fit the screen you can use the scrollbars to look around.
