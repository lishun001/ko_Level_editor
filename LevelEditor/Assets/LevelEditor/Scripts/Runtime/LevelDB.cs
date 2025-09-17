using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    [Serializable]
    public class LevelDB
    {
        public List<List<CellData>> CellDatas;
        public List<List<WoolData>> WoolDatas;

        public void GenerateGrid(int row, int col)
        {
            CellDatas = new List<List<CellData>>(row);
            for (int i = 0; i < row; i++)
            {
                CellDatas.Add(new List<CellData>(col));
                for (int j = 0; j < col; j++)
                {
                    CellDatas[i].Add(new CellData { type = CellType.None, color = EColor.Red });
                }
            }
        }
    }

    [Serializable]
    public class CellData
    {
        public CellType type;
        public EColor color;
    }

    [Serializable]
    public class WoolData
    {
        public EColor color;
    }

    [Serializable]
    public enum CellType
    {
        None,
        Wall,
        Coil,
        PipelineMec,
    }

    [Serializable]
    public class PipelineMec
    {
        public Vector2 pos;
        public Vector2 dir;

        public List<CellData> cells;
    }

    [Serializable]
    public class CoilQusMark
    {
        public Vector2 pos;
    }
}