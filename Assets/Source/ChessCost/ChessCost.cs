using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessCost
{
    #region TotCost
    public static int[,] TotCostTable = new int[10, 9]
    {
        { 0, 0, 0, 1, 1, 1, 0, 0, 0 },
        { 1, 1, 1, 2, 3, 2, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    public static int GetTotCost(int x, int y)
    {
        // Adjusting to handle out-of-bounds access
        if (x < 0 || x >= TotCostTable.GetLength(0) || y < 0 || y > TotCostTable.GetLength(1))
        {
            Debug.LogError("Invalid indices for TotCostTable");
            return 0;
        }

        return TotCostTable[x, y];
    }
    #endregion

    #region MaCost
    public static int[,] MaCostTable = new int[10, 9]
    {
        { 4, 4, 4, 6, 4, 6, 4, 4, 4 },
        { 4, 4, 5, 5, 4, 5, 5, 4, 4 },
        { 4, 4, 4, 6, 6, 6, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 }
    };

    public static int GetMaCost(int x, int y)
    {
       

        return MaCostTable[x, y];
    }
    #endregion

    #region PhaoCost
    public static int[,] PhaoCostTable = new int[10, 9]
    {
        {6, 5, 4, 4, 4, 4, 4, 5, 6 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 6, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 6, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 6, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 6, 4, 4, 4, 4 },
        { 4, 4, 4, 5, 6, 5, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        { 4, 4, 4, 4, 4, 4, 4, 4, 4 }
    };

    public static int GetPhaoCost(int x, int y)
    {
        // Adjusting to handle out-of-bounds access
        if (x < 0 || x >= PhaoCostTable.GetLength(0) || y < 0 || y > PhaoCostTable.GetLength(1))
        {
            Debug.LogError("Invalid indices for PhaoCostTable");
            return 0;
        }

        return PhaoCostTable[x, y ];
    }
    #endregion

    #region XeCost
    public static int[,] XeCostTable = new int[10, 9]
    {
        { 12, 12, 12, 10, 10, 10, 12, 12, 12 },
        { 12, 12, 12, 10, 10, 10, 12, 12, 12 },
        { 12, 12, 12, 10, 10, 10, 12, 12, 12 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 10, 10, 10, 10, 10, 10, 10, 10 },
        { 11, 11, 11, 12, 12, 12, 11, 11, 11 },
        { 10, 11, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 11, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 11, 10, 10, 10, 10, 10, 10, 10 },
        { 10, 11, 10, 10, 10, 10, 10, 10, 10 }
    };

    public static int GetXeCost(int x, int y)
    {
        // Adjusting to handle out-of-bounds access
        if (x < 0 || x >= XeCostTable.GetLength(0) || y < 0 || y > XeCostTable.GetLength(1))
        {
            Debug.LogError("Invalid indices for XeCostTable");
            return 0;
        }

        return XeCostTable[x, y];
    }
    #endregion

    #region TuongCost
    public static int[,] TuongCostTable = new int[10, 9]
    {
        { 0, 0, 2, 0, 0, 0, 2, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 2, 0, 0, 0, 2, 0, 0, 0, 2 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 2, 0, 0, 0, 2, 0, 0 },
        { 0, 0, 2, 0, 0, 0, 2, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 2, 0, 0, 0, 2, 0, 0, 0, 2 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 2, 0, 0, 0, 2, 0, 0 }
    };

    public static int GetTuongCost(int x, int y)
    {
        // Adjusting to handle out-of-bounds access
        if (x < 0 || x >= TuongCostTable.GetLength(0) || y < 0 || y > TuongCostTable.GetLength(1))
        {
            Debug.LogError("Invalid indices for TuongCostTable");
            return 0;
        }

        return TuongCostTable[x, y ];
    }
    #endregion

    #region SiCost
    public static int[,] SiCostTable = new int[10, 9]
    {
        { 0, 0, 0, 2, 0, 2, 0, 0, 0 },
        { 0, 0, 0, 0, 2, 0, 0, 0, 0 },
        { 0, 0, 0, 2, 0, 2, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 2, 0, 2, 0, 0, 0 },
        { 0, 0, 0, 0, 2, 0, 0, 0, 0 },
        { 0, 0, 0, 2, 0, 2, 0, 0, 0 }
    };

    public static int GetSiCost(int x, int y)
    {
        // Adjusting to handle out-of-bounds access
        if (x < 0 || x >= SiCostTable.GetLength(0) || y < 0 || y > SiCostTable.GetLength(1))
        {
            Debug.LogError("Invalid indices for SiCostTable");
            return 0;
        }

        return SiCostTable[x, y];
    }
    #endregion

    #region SoaiCost
    public static int[,] SoaiCostTable = new int[10, 9]
    {
        { 0, 0, 0, 1000, 1050, 1000, 0, 0, 0 },
        { 0, 0, 0, 900, 950, 900, 0, 0, 0 },
        { 0, 0, 0, 800, 850, 800, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 800, 850, 800, 0, 0 },
        { 0, 0, 0, 900, 950, 900, 0, 0, 0 },
        { 0, 0, 0, 1000, 1050, 1000, 0, 0, 0 }
    };

    public static int GetSoaiCost(int x, int y)
    {
        // Adjusting to handle out-of-bounds access
        if (x < 0 || x >= SoaiCostTable.GetLength(0) || y < 0 || y > SoaiCostTable.GetLength(1))
        {
            Debug.LogError("Invalid indices for SoaiCostTable");
            return 0;
        }

        return SoaiCostTable[x, y];
    }
    #endregion

    public static string RemoveLastCharacter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return input.Substring(0, input.Length - 1);
    }
}
