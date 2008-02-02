using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI
{
    public enum ResourceID
    {
        // Static Images for SokobanMap Simple
        StaticTileVoid = 1,
        StaticTileWall = 2,
        StaticTileFloor = 3,
        StaticTileFloorCrate = 4,
        StaticTileFloorGoal = 5,
        StaticTileFloorGoalCrate = 6,
        StaticTileFloorPlayer = 7,
        StaticTileFloorGoalPlayer = 8,

        // Game Tiles
        GameTileVoid = 100,
        GameTileWall = 101,
        GameTileFloor = 102,
        GameTileGoal = 103,
        GameTileCrate = 104,
        GameTilePlayer = 105,

        // Game Buttons
        GameButtonBackGround = 150,
        GameButtonUndo = 151,
        GameButtonRestart = 152,
        GameButtonExit = 153,
        GameButtonUp = 154,
        GameButtonDown = 155,
        GameButtonLeft = 156,
        GameButtonRight = 157,
        GameButtonMouseOverBrush = 158,
        
        GameButtonBookmark1 = 161,
        GameButtonBookmark2 = 162,
        GameButtonBookmark3 = 163,
        GameButtonBookmark4 = 164,
        GameButtonBookmark5 = 165,
        GameButtonHome = 166,
        GameButtonCancel = 167,
        GameButtonSave = 168,
        GameButtonHelp = 169,

        // Game SFX
        GameSoundPushGoal = 500,
        GameSoundUndo = 500,
        GameSoundRestart = 501,
        GameSoundWin = 502,
        GameSoundWelcome = 504,
        GameSoundInvalid= 503,
        

        // Game Misc
        GameMiscFontDefault = 200,
        GameMiscFontDefaultBrush = 201,
        GameMiscFontDefaultBrushShaddow = 202,

        GameMiscStringInvalidMove = 300,
        GameMiscStringCrateGoal = 301,

    }
}
