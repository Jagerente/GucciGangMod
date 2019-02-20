//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.IO;
using UnityEngine;

public class UVAnimation
{
    public int curFrame;
    public Vector2[] frames;
    public int loopCycles;
    public bool loopReverse;
    public string name;
    protected int numLoops;
    protected int stepDir = 1;
    public Vector2[] UVDimensions;

    public void BuildFromFile(string path, int index, float uvTime, Texture mainTex)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("wrong ean file path!");
        }
        else
        {
            var input = new FileStream(path, FileMode.Open);
            var br = new BinaryReader(input);
            var file = new EanFile();
            file.Load(br, input);
            input.Close();
            var animation = file.Anims[index];
            frames = new Vector2[animation.TotalCount];
            UVDimensions = new Vector2[animation.TotalCount];
            int tileCount = animation.TileCount;
            var num2 = (animation.TotalCount + tileCount - 1) / tileCount;
            var num3 = 0;
            var width = mainTex.width;
            var height = mainTex.height;
            for (var i = 0; i < num2; i++)
            {
                for (var j = 0; j < tileCount && num3 < animation.TotalCount; j++)
                {
                    var zero = Vector2.zero;
                    zero.x = animation.Frames[num3].Width / (float) width;
                    zero.y = animation.Frames[num3].Height / (float) height;
                    frames[num3].x = animation.Frames[num3].X / (float) width;
                    frames[num3].y = 1f - animation.Frames[num3].Y / (float) height;
                    UVDimensions[num3] = zero;
                    UVDimensions[num3].y = -UVDimensions[num3].y;
                    num3++;
                }
            }
        }
    }

    public Vector2[] BuildUVAnim(Vector2 start, Vector2 cellSize, int cols, int rows, int totalCells)
    {
        var index = 0;
        frames = new Vector2[totalCells];
        UVDimensions = new Vector2[totalCells];
        frames[0] = start;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols && index < totalCells; j++)
            {
                frames[index].x = start.x + cellSize.x * j;
                frames[index].y = start.y - cellSize.y * i;
                UVDimensions[index] = cellSize;
                UVDimensions[index].y = -UVDimensions[index].y;
                index++;
            }
        }
        return frames;
    }

    public bool GetNextFrame(ref Vector2 uv, ref Vector2 dm)
    {
        if (curFrame + stepDir >= frames.Length || curFrame + stepDir < 0)
        {
            if (stepDir > 0 && loopReverse)
            {
                stepDir = -1;
                curFrame += stepDir;
                uv = frames[curFrame];
                dm = UVDimensions[curFrame];
            }
            else
            {
                if (numLoops + 1 > loopCycles && loopCycles != -1)
                {
                    return false;
                }
                numLoops++;
                if (loopReverse)
                {
                    stepDir *= -1;
                    curFrame += stepDir;
                }
                else
                {
                    curFrame = 0;
                }
                uv = frames[curFrame];
                dm = UVDimensions[curFrame];
            }
        }
        else
        {
            curFrame += stepDir;
            uv = frames[curFrame];
            dm = UVDimensions[curFrame];
        }
        return true;
    }

    public void PlayInReverse()
    {
        stepDir = -1;
        curFrame = frames.Length - 1;
    }

    public void Reset()
    {
        curFrame = 0;
        stepDir = 1;
        numLoops = 0;
    }

    public void SetAnim(Vector2[] anim)
    {
        frames = anim;
    }
}

