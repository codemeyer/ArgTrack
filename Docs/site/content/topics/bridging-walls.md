---
title: "ArgTrack: Topic: Bridging Walls/Fences"
---

# Topic: Bridging Walls/Fences

By default, the walls along the track follow the shape and arc of the track section that they belong to.
When walls are bridged (or joined), they will follow a straight line from the start point of the first
bridged wall to the start point of the first non-bridged wall.

For example, consider this 90&deg; corner, which consists of three
different [track sections](/argtrack/topics/track-section/).

<img src="/argtrack/images/topics/bridging-0-walls.png" alt="Walls without bridging" class="img-fluid" />

These walls are not bridged, and as you can see they follow the arc of the corner.


### Bridging a part of the corner

If we use the [track section details](/argtrack/screens/track-section-details-window/) window to edit the first two sections
of the corner and check the "Bridged Right" property under "Walls/Fences", we will get the following track layout:

<img src="/argtrack/images/topics/bridging-2-walls.png" alt="Walls bridged 2 of 3" class="img-fluid" />

Note that the first two sections of the corner no longer have a wall that follows the track arc, but instead
the wall goes in a straight line from the starting point in the verge of the first bridged section, to the starting point
of the first non-bridged section, which in this case is the third section of the corner.



### Bridging the entire corner

If we use the  [track section details](/argtrack/screens/track-section-details-window/) window one more time to
check the "Bridged Right" property for the final section of the corner, we will end up with this:

<img src="/argtrack/images/topics/bridging-3-walls.png" alt="Walls bridged 3 of 3" class="img-fluid" />

The wall now goes all the way to the starting point of the following straight.


### Remarks

A final note regarding bridging: It is possible to bridge walls with verges that are so narrow that the
resulting straight wall runs across the track. Not only will this look strange, it will also
result in unexpected behavior when cars get close to the wall.
