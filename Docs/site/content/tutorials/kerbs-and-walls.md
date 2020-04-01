---
title: "ArgTrack: Tutorial: Kerbs and Walls"
---

# Tutorial: Kerbs and Walls

This tutorial will introduce you to some of the different things you can change in
an existing track file.

By walking through this tutorial, you will update the Phoenix track in the following ways:

- Change the kerb colors
- Move walls further back
- Add kerbs to a corner

This tutorial will be expanded eventually, or more separate tutorials will be added.

Note: Please backup your F1GP folder before following this tutorial, otherwise you may lose data, and you will definitely
alter the Phoenix track.

Start off by loading the track in ArgTrack: Go to the Track tab and then
click Load Track. Then select F1CT01.DAT in your F1GP folder.


## Changing the kerb colors

On the first "Misc" tab, have a look at the various Kerb properties. You can change the type of kerb between
having two or three colors, and you can change which colors are used.

Let's flip the Kerb Type to Triple Color. Now you will see another pair of Kerb Color dropdowns appear.

You can change any of the colors by clicking the dropdown boxes and selecting another color. Remember though,
that for the best look you should choose a brighter color for the "Upper" kerb part, and a darker color for the "Lower" part.

Set the colors like this:

<img alt="Moved wall" src="/argtrack/images/tutorial-kerb-settings.png" class="img-fluid" />

We will now save the track and have a look in-game. If we drive to the far-end of the track, we find the large hairpin.
Its kerbs now has three colors.

<img alt="Kerb colors" src="/argtrack/images/tutorial-kerb-colors.png" class="img-fluid" />


## Moving walls further back

We will now learn how to move walls further back from the track. For this example, we will "open up" the right side
at the first corner by moving the walls a bit back.

Go to the first corner's first section in ArgTrack. This is the first section from the top that has the value "Right"
in the field to the right of the Curvature value. Double-click the section in the list, and then change the Right Verge Width value to 20.

<img alt="Moved wall values" src="/argtrack/images/tutorial-corner-right-verge.png" class="img-fluid" />

Repeat this step for the next two sections. They are both part of the corner.

Now let's go in-game and have a look.

<img alt="Moved wall" src="/argtrack/images/tutorial-wall-moved.png" class="img-fluid" />

As you can see, the wall has moved back a bit from the track.


## Adding kerbs to a corner

Let's add some kerbs to the first corner.

For each of the three sections you just edited, edit them again and check the "Right Kerb" checkbox in
the Kerbs groupbox. Let the dropdown say "High".

<img alt="Added kerbs" src="/argtrack/images/tutorial-check-right-kerb.png" class="img-fluid" />

Now when we go in-game and drive down to the first corner, it will look like this:

<img alt="Added kerbs" src="/argtrack/images/tutorial-kerbs-added.png" class="img-fluid" />

We have kerbs!

Congratulations! You have just performed your first track editing!
