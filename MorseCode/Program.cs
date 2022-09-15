using MorseCode;
using System.Diagnostics;
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

Dictionary<char, string> _morseAlphabetDictionary = new Dictionary<char, string>() { { 'a', ".-" }, { 'b', "-..." }, { 'c', "-.-." }, { 'd', "-.." }, { 'e', "." }, { 'f', "..-." }, { 'g', "--." }, { 'h', "...." }, { 'i', ".." }, { 'j', ".---" }, { 'k', "-.-" }, { 'l', ".-.." }, { 'm', "--" }, { 'n', "-." }, { 'o', "---" }, { 'p', ".--." }, { 'q', "--.-" }, { 'r', ".-." }, { 's', "..." }, { 't', "-" }, { 'u', "..-" }, { 'v', "...-" }, { 'w', ".--" }, { 'x', "-..-" }, { 'y', "-.--" }, { 'z', "--.." }, { '0', "-----" }, { '1', ".----" }, { '2', "..---" }, { '3', "...--" }, { '4', "....-" }, { '5', "....." }, { '6', "-...." }, { '7', "--..." }, { '8', "---.." }, { '9', "----." } };
const int _timeUnitTimeSpan = 80;
int _timeUnitCount = 0;

// Get keyState for hold down counter
bool _isKeyDown = false;
bool _isKeyDownLastState = false;

Stopwatch stopWatch_buttonDuration = new Stopwatch();

// Main program
while (true) {
    _isKeyDown = IsPressed(VirtualKey.VK_DOWN);

    if (_isKeyDown != _isKeyDownLastState) {
        if(_isKeyDown) {
            stopWatch_buttonDuration.Start();
        } else {
            Console.Write(milliesecondToMorse(_timeUnitTimeSpan, (int)stopWatch_buttonDuration.Elapsed.TotalMilliseconds));
            stopWatch_buttonDuration.Reset();
        }
    }

    _isKeyDownLastState = _isKeyDown;
}

static char milliesecondToMorse(int timeUnit, int millieseconds) {
    if (millieseconds >= timeUnit * 3)
        return '-';
    else if (millieseconds >= 1 && millieseconds <= timeUnit * 3)
        return '.';

    return ' ';
}

static bool IsPressed(VirtualKey virtualKey) {
    return Convert.ToBoolean(GetKeyState(virtualKey) & 0x8000);
}