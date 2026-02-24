# What is this?

It's a small program that breaks the MSFS cursor lock when switching between desktop and VR via a desktop switching tool such as [DesktopXR](https://github.com/glenimp617/DesktopXR)

# Details

When using the amazing [DesktopXR](https://github.com/glenimp617/DesktopXR) with MSFS2024, jumping to the desktop presents a problem because the mouse cursor remains locked to MSFS2024 and unresponsive to input. To break the lock you must press the Windows key (or another magic combination). This can be quite awkward in VR, especially if you're doing something that only requires mouse input (referencing a document, adjusting simhub settings etc). The same problem occurs when switching _back_ to MSFS: if a desktop window has focus, the mouse cursor will be inactive in the game until you click on the MSFS window so it can re-capture the mouse cursor.

# How to use

In combination with a key mapper, in my case SPAD.next, CursorLockBreaker breaks the lock so the mouse is immediately active in both directions.

I use it by mapping a button on my yoke for switching back and forth between the game and desktop. When the button is pressed it fires off the the DesktopXR hotkey (in my case F12) _and runs CursorLockBreaker_.

<img width="1587" height="191" alt="image" src="https://github.com/user-attachments/assets/50aac754-2bbb-4fc8-8811-3a2c698487f0" />

## Limitation

There is one limitation: When you enter the desktop you **must** click a Window to complete the focus switch, if you switch straight back to the game it will not have focus (I'm sure this could be automated - but the single-click didn't bother me enough to try).

# Installation

Put the contents of the zip into a folder of your choosing.
