Control Scheme:

W - Move Forward
S - Move Backwards
A - Move Left
D - Move Right
Left Shift - Sprint, Doubles Speed
Space - Jump
Tab - Pause
Backspace - Restart Level 1
Escape - Exit to Main Menu
Mouse Movement - Look Around in Game, Move Cursor in Menus
Left Mouse Click - Click Menu Buttons, Shoot Primary Weapon
Right Mouse Click - Shoot Rewind Weapon

Innovations:

I added a lot of extra features as I was enjoying the development:

Pause Menu on Tab press
Enemies spawn at random intervals from 8 different locations
Two types of enemies, have different health, damage, attack frequency and speeds
Enemies have health bars that follow them around
Screen flashes to indicate damage, loss, win, heal, and full charge for special attack
On right click, special ability that sends a raycast and draws a line with a LineRenderer
and sends an enemy back to their spawn
Game countdiwn timer to add objective of survival
Enemies have random chance to drop health items when killed or rewound with the right click
Health pickups that heal the player
Enemy flashes red when hit
Enemies look at player and move towards them and only shoot when in range

Troubles:

I had an issue with the mouse cursor that appears to only show up on Mac and not
in my Windows build, but every time the mouse is locked to the center of the screen,
the first mouse movement afterwards will jerk in the direction opposite of where
the mouse was relative to the center.

The enemies can push the player around and restrict their movement if they swarm
the player. They can also force the player through the ground if it happens in a
corner, so if this happens the game should be restarted with backspace or the menus.

Also, the mouse is not able to look around in the build over remote desktop, but
worked when I tested it in person, so I assume it is a deficiency of rdp and the
way it handles mouse input.
