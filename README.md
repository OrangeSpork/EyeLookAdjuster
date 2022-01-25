# EyeLookAdjuster

Plugin that allows you to tweak how the eye tracking behaviour in the game works.

In main game, maker and for new characters first added to scene in Studio, the plugin has adjustable (in F1 Plugin Settings) defaults, one for each eye mode.

The defaults that ship with the plugin are similar to the Illusion ones with the following adjustments:

- Minimum Near Focus and Front Focus difference are scale adjusted (Illusion used the HS numbers forgetting that AI/HS2 have a 10x scale multiplier). So the default 2 minimum focus distance from HS has been increased to 20 in Follow for example.
- Eye roll constraints have been loosened a little to allow the eyeball to track further up/down/left/right
- Bend Multiplier and Max Angle slightly increased to move the focus a little more aggressively on target in Follow/Adjust modes.

Eye Modes - For Reference (Plugin doesn't change anything here, just listing for documentation purposes) the Eye Modes (all available on the Studio Gaze Panel, Maker/Main game use fewer):

- Front: Character focuses straight ahead with a focus distance of the Front Focus Distance.
- Follow: Character attempts to lock onto target if target (camera) within H(orizontal) and V(ertical) Angle Limits. If target is out of bounds, character looks Front.
- Avert: Character looks in an opposing direction to the target (camera).
- Fixed: Character look position is locked into whatever it currently is.
- Adjust: Similar to follow but the character looks at an adjustable gimmick that can be positioned in Studio.

In studio there is a new UI to allow the settings to be adjusted on a per character basis. Once adjusted they are saved to the scene.

![Studio GUI!](https://raw.githubusercontent.com/OrangeSpork/EyeLookAdjuster/master/EyeLookAdjuster/StudioGUIExample.png)

Settings:

Target Related Settings

- H Angle Limit: Horizontal limit in degrees from head position the character is willing to track a target. Targets outside this arc cause the character to look front instead.
- V Angle Limit: As Horizontal limit but Vertical, also in degrees.
- Min Focus Distance (or Near Distance): Minimum focus depth the character will correct to. Increase to make the character less cross eyed, lower to increase the amount of cross eye (target needs to be within this distance to need it).
- Front Focus Distance: Focus depth the character uses when looking forward (forward mode). 
- Track Speed: Speed eyes traverse, higher is faster.

Eye Adjust Related Settings (There is a rather complex algorithm here, the descriptions here don't do a good job of describing the math, sorry...).

- Threshold Angle: Flat adder (floored at 0) added to the look at target angle.
- Max Angle Diff: Max angle variation allowed on target angle.
- Bending Multiplier: Multiplies both the Threshold and Max Angle. 
- Up Bend Angle: Eyeball rotation up limit angle.
- Down Bend Angle: Eyeball rotation down limit angle.
- Min Bend Angle: Eyeball rotation in limit angle.
- Max Bend Angle: Eyeball rotation out limit angle.

In more plain terms the Threshold angle, Max Angle and Bending Multiplier determine the degree of aggressiveness it tries to lock onto the target (over-high settings can result in over-correction).

These are then clamped within the Up/Down/Min/Max limits to determine how far the eyes are willing to rotate.

Example:

If the eye is reverting to Front too soon, increase H/V Angle Limits.  
If the eye is tracking the target but not following far enough in a direction, increase the up/down/min/max limits as appropriate to get the eye to roll further.  
To allow the eyes to cross more (focus close) decrease Min (Near) focus distance.  
To prevent cross eye increase Min (Near) focus distance.  

To adjust how aggressive (for lack of a better term) the eye track is on target try increasing Bending Multiplier a bit or add to Max Angle Diff.  
If the eye is overtracking (looking at a higher angle - too far up/down/left/right) lower Bending Multiplier or Max Angle Diff.  
