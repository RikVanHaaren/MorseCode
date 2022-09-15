﻿using MorseCode;
using System.Runtime.InteropServices;

/*  Morse Rules
*  - duration of '.' is 1 time unit
*  - duration of '-' is 3 times '.'
*  - space between one letters of same letter is one unit
*  - space between letters is three units
*  - space between word is seven units
*  - T stands for unit of time. W stands for speed in wpm(words per minutes)
*/

// Import P/Invoke
[DllImport("USER32.dll")]
static extern short GetKeyState(VirtualKey nVirtKey);

// Get keyState for hold down counter
bool _isKeyDown = false;
bool _isKeyDownLastState = false;

// Main program
while (true) {
    _isKeyDown = IsPressed(VirtualKey.VK_DOWN);

    if (_isKeyDown != _isKeyDownLastState) {
        Console.WriteLine("press");
    }

    _isKeyDownLastState = _isKeyDown;
}

static bool IsPressed(VirtualKey virtualKey) {
    return Convert.ToBoolean(GetKeyState(virtualKey) & 0x8000);
}