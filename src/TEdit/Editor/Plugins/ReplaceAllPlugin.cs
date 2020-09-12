using System.Windows;
using System;
using TEdit.Terraria;
using TEdit.ViewModel;
using System.Runtime.InteropServices;

namespace TEdit.Editor.Plugins
{
    public class ReplaceAllPlugin : BasePlugin
    {
        public ReplaceAllPlugin(WorldViewModel worldViewModel)
            : base(worldViewModel)
        {
            Name = "Randomize All Tiles";
        }


        private void PerformReplace()
        {
            if (_wvm.CurrentWorld == null)
                return;

            bool randomizeTiles = false;
            bool randomizeWalls = false;


            if (_wvm.TilePicker.PaintMode == PaintMode.TileAndWall)
            {
                if (_wvm.TilePicker.TileStyleActive)
                    randomizeTiles = true;
                if (_wvm.TilePicker.WallStyleActive)
                    randomizeWalls = true;
            }


            ushort[] shuffledTiles = (ushort[])ShuffleTiles();
            ushort[] tileReference = {0,1,2,6,7,8,9,22,23,25,30,32,37,38,39,40,41,43,44,45,46,47,48,51,
            2,53,54,55,56,57,58,59,60,62,63,64,65,66,67,68,69,70,75,76,80,107,108,109,111,112,115,
            116,117,118,119,120,121,122,123,124,127,130,131,140,145,146,147,148,150,151,152,153,154,
            155,156,157,158,159,160,161,162,163,164,166,167,168,169,170,175,176,177,179,180,181,182,
            183,188,189,190,191,192,193,194,195,196,197,198,199,200,202,203,204,205,206,208,211,213,
            214,221,222,223,224,225,226,229,230,232,234,248,249,250,251,252,253,255,256,257,258,259,
            260,261,262,263,264,265,266,267,268,272,273,274,284,311,312,313,315,321,322,325,326,327,
            328,329,330,331,332,333,336,340,341,342,343,344,345,346,347,348,350,351,352,353,357,367,
            366,367,368,369,370,371,379,381,382,383,384,385,396,397,398,399,400,401,402,403,404,407,
            408,409,415,416,417,418,421,422,426,430,431,432,433,434,446,447,448,449,450,451,458,459,
            460,472,473,474,477,478,479,481,482,483,492,495,496,498,500,501,502,503,504,507,508,512,
            513,514,515,516,517,528,534,535,536,537,539,540,541,546,557,561,562,563,566,574,575,576,
            577,578,618};
            //ushort[] shuffledWalls = (ushort[])ShuffleWalls();

            for (int x = 0; x < _wvm.CurrentWorld.TilesWide; x++)
            {
                for (int y = 0; y < _wvm.CurrentWorld.TilesHigh; y++)
                {
                    bool doReplaceTile = false;
                    bool doReplaceWall = false;

                    Tile curTile = _wvm.CurrentWorld.Tiles[x, y];


                    if (randomizeTiles && Array.IndexOf(tileReference, curTile.Type) != -1)
                    {
                        if (_wvm.Selection.IsValid(x, y))
                        {
                            doReplaceTile = true;
                        }
                    }

                    if (randomizeWalls)
                    {
                        if ((_wvm.Selection.IsValid(x, y)))
                        {
                            doReplaceWall = true;
                        }
                    }

                    if (doReplaceTile || doReplaceWall)
                    {
                        _wvm.UndoManager.SaveTile(x, y);

                        if (doReplaceTile)
                            curTile.Type = (ushort)shuffledTiles.GetValue((int)Array.IndexOf(tileReference, curTile.Type));

                        if (doReplaceWall)
                        {

                        }
                        // curTile.Wall = (ushort)shuffledWalls.GetValue((int)curTile.Wall);

                        _wvm.UpdateRenderPixel(x, y);
                    }
                }
            }

            _wvm.UndoManager.SaveUndo();
        }
        private ushort[] ShuffleTiles()
        {
            // this one gets changed.
            ushort[] tileList = {0,1,2,6,7,8,9,22,23,25,30,32,37,38,39,40,41,43,44,45,46,47,48,51,
            2,53,54,55,56,57,58,59,60,62,63,64,65,66,67,68,69,70,75,76,80,107,108,109,111,112,115,
            116,117,118,119,120,121,122,123,124,127,130,131,140,145,146,147,148,150,151,152,153,154,
            155,156,157,158,159,160,161,162,163,164,166,167,168,169,170,175,176,177,179,180,181,182,
            183,188,189,190,191,192,193,194,195,196,197,198,199,200,202,203,204,205,206,208,211,213,
            214,221,222,223,224,225,226,229,230,232,234,248,249,250,251,252,253,255,256,257,258,259,
            260,261,262,263,264,265,266,267,268,272,273,274,284,311,312,313,315,321,322,325,326,327,
            328,329,330,331,332,333,336,340,341,342,343,344,345,346,347,348,350,351,352,353,357,367,
            366,367,368,369,370,371,379,381,382,383,384,385,396,397,398,399,400,401,402,403,404,407,
            408,409,415,416,417,418,421,422,426,430,431,432,433,434,446,447,448,449,450,451,458,459,
            460,472,473,474,477,478,479,481,482,483,492,495,496,498,500,501,502,503,504,507,508,512,
            513,514,515,516,517,528,534,535,536,537,539,540,541,546,557,561,562,563,566,574,575,576,
            577,578,618};
            Random blockPicker = new Random();
            ushort[] shuffledList = new ushort[tileList.Length];
            for (int i = 0; i < shuffledList.Length; i++)
            {
                int index = blockPicker.Next(tileList.Length);
                int coinFlip = blockPicker.Next(2) * 2 - 1;
                bool verified = false;
                if ((int)Array.IndexOf(shuffledList, (ushort)tileList.GetValue(index)) == -1)
                    verified = true;
                int iteration_count = 0;
                while (!verified && iteration_count < 100)
                {
                    index += coinFlip;
                    if (index >= tileList.Length || index < 0)
                    {
                        index -= coinFlip;
                        coinFlip = -coinFlip;
                    }
                    if ((int)Array.IndexOf(shuffledList, (ushort)tileList.GetValue(index)) == -1 && (ushort)tileList.GetValue(index) <= (ushort)650)
                        verified = true;
                    else
                        iteration_count++;
                }
                if (verified)
                {
                    shuffledList.SetValue((ushort)tileList.GetValue(index), i);
                    // if it equals 800, that means the tile was already used.
                    tileList.SetValue((ushort)800, index);
                }
            }
            return shuffledList;

        }
        private ushort[] ShuffleWalls()
        {
            // fill with 0-316; in order.
            ushort[] wallList = new ushort[316];
            for (int i = 0; i < wallList.Length; i++)
            {
                wallList.SetValue((ushort)i, i);
            }
            Random blockPicker = new Random();
            ushort[] shuffledList = new ushort[316];
            for (int i = 0; i < shuffledList.Length; i++)
            {
                int index = blockPicker.Next(619);
                int coinFlip = blockPicker.Next(2) * 2 - 1;
                bool unverified = true;
                if ((int)Array.IndexOf(shuffledList, (ushort)wallList.GetValue(index)) != -1)
                    unverified = false;
                while (unverified)
                {
                    index += coinFlip;
                    if (index >= wallList.Length || index < 0)
                        index = blockPicker.Next(619);
                    if ((int)Array.IndexOf(shuffledList, (ushort)wallList.GetValue(index)) != -1)
                        unverified = false;
                }
                shuffledList.SetValue((ushort)index, i);
                // if it equals 800, that means the tile was already used.
                wallList.SetValue((ushort)800, index);
            }
            return shuffledList;
        }

        public override void Execute()
        {
            PerformReplace();
        }
    }
}
