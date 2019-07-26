using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Puzzle
    {
        private CellCollection _cellColl = new CellCollection();
        public ArrayList Cells = new ArrayList();
        public ArrayList NextPuzzles = new ArrayList();

        //private bool _solvable = true;

        public Puzzle(ArrayList startCells)
        {
            foreach (Cell c in startCells)
            {
                Cells.Add(c);
            }

            _cellColl = new CellCollection(Cells);
        }

        public CellCollection CellColl
        {
            get { return _cellColl; }
        }

        public ArrayList Solve2()
        {
            FixLonely();
            return Cells;
        }

        public bool Solve()
        {
            return Solve(false);
        }

        public bool Solve(bool showSteps)
        {
            bool bDone = false;

            while (!bDone)
            {
                //Reset Dirty Flag for all cells
                _cellColl.ClearDirty();

                // If a cell has a value, set appropriate Possible Values for Cell 
                // Additionally remove possible values from all Cells in Same Groups
                foreach (Cell c in Cells)
                {
                    c.ProcessValue();
                    ProcessGroups(c);
                }

                // If Dirty or Contradiction, start over or finish step
                if (!_cellColl.IsDirty && !_cellColl.ContradictionExists)
                {
                    foreach (Cell c in Cells)
                    {
                        if (c.CheckIfKnown())
                            break;
                    }
                }

                if (!_cellColl.IsDirty && !_cellColl.ContradictionExists)
                {
                    for (int j = 1; j < 10; j++)
                    {
                        LastInColumn(j);
                        LastInRow(j);
                        LastInSquare(j);
                    }
                }

                if (!_cellColl.IsDirty && !_cellColl.ContradictionExists)
                {
                    FixLonely();
                }

                bDone = showSteps || !_cellColl.IsDirty || _cellColl.ContradictionExists;
            }

            return _cellColl.IsSolved;
        }

        public Cell FindLeastPossibleCell()
        {
            Cell cellReturn = (Cell)Cells[0];
            bool unknownFound = false;

            foreach (Cell c in Cells)
            {
                if (!unknownFound)
                {
                    if (c.Value == 0)
                    {
                        unknownFound = true;
                        cellReturn = c;
                    }
                }
                else
                {
                    if (c.Value == 0 && c.PossibleValueCount < cellReturn.PossibleValueCount)
                    {
                        cellReturn = c;
                    }
                }
            }

            return cellReturn;
        }

        private void ProcessGroups(Cell c)
        {
            // If a cell has a value, each cell sharing the same row, column or square cannot have the same value
            if (c.Value != 0)
            {
                foreach (Cell sc in Cells)
                {
                    if (SameGroup(c, sc))
                    {
                        sc.PossibleValue(c.Value, -1);
                    }
                }
            }
        }

        private bool SameGroup(Cell c1, Cell c2)
        {
            // If ALL equal then Same Cell (defined to be NOT in Same Grouping)
            if ((c1.Row == c2.Row) && (c1.Column == c2.Column) && (c1.Square == c2.Square))
                return false;
            // If at least ONE equal, then Same Group = true
            else   
                return (c1.Row == c2.Row || c1.Column == c2.Column || c1.Square == c2.Square);
        }

        private void LastInGroup(ArrayList groupColl)
        {
            // If a cell
            for(int i = 1; i < 10; i++)
            {
                bool foundOnce = false;
                bool foundAgain = false;
                Cell tempCell = new Cell(0, 0, 0, 0);

                foreach(Cell c in groupColl)
                {
                    if (c.Value == i)
                        foundAgain = true;

                    if (c.Value == 0 && c.PossibleValue(i) == 0 && !foundAgain)
                    {
                        if (foundOnce)
                        {
                            foundAgain = true;
                        }
                        else
                        {
                            tempCell = c;
                            foundOnce = true;
                        }
                    }
                }
                if (foundOnce && !foundAgain)
                {
                    tempCell.Value = i;
                    i = 10;
                }
            }
        }

        private void LastInColumn(int column)
        {
            ArrayList colColl = new ArrayList();

            for (int i = 1; i < 10; i++)
            {
                colColl = new ArrayList();
                foreach (Cell c in Cells)
                {
                    if (c.Column == column)
                        colColl.Add(c);
                }
                LastInGroup(colColl);
            }
        }

        private void LastInRow(int row)
        {
            ArrayList rowColl = new ArrayList();

            for (int i = 1; i < 10; i++)
            {
                rowColl = new ArrayList();
                foreach (Cell c in Cells)
                {
                    if (c.Row == row)
                        rowColl.Add(c);
                }
                LastInGroup(rowColl);
            }
        }

        private void LastInSquare(int square)
        {
            ArrayList squareColl = new ArrayList();

            for (int i = 1; i < 10; i++)
            {
                squareColl = new ArrayList();
                foreach(Cell c in Cells)
                {
                    if (c.Square == square)
                        squareColl.Add(c);
                }
                LastInGroup(squareColl);
            }
        }

        private void FixLonely()
        {
            CellCollection squareColl = new CellCollection();
            CellCollection rowColl = new CellCollection();
            CellCollection colColl = new CellCollection();

            for (int nCntr = 1; nCntr < 10; nCntr++)
            {
                squareColl = new CellCollection();
                rowColl = new CellCollection();
                colColl = new CellCollection();

                foreach (Cell c in Cells)
                {
                    if (c.Square == nCntr && c.Value == 0)
                        squareColl.Cells.Add(c);

                    if (c.Row == nCntr && c.Value == 0)
                        rowColl.Cells.Add(c);

                    if (c.Column == nCntr && c.Value == 0)
                        colColl.Cells.Add(c);
                }
                
                bool bChanged = squareColl.FixLonely();
                
                if (!bChanged)
                    bChanged = rowColl.FixLonely();

                if (!bChanged)
                    bChanged = colColl.FixLonely();

                if (bChanged)
                    nCntr = 10;
            }
        }
    }

    class Cell
    {
        private int _Value = 0;
        private int _Row = 0;
        private int _Column = 0;
        private int _Square = 0;
        private bool _Contradiction = false;
        private bool _isDirty = false;

        private int[] _possibleValues = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public Cell(int row, int col, int val)
        {
            // Initialize Cell (Row, Column, Square and Value)
            _Row = row;
            _Column = col;
            _Square = CalcSquare(row, col);
            _Value = val;

            // Remove all other possibilities (if val > 0)
            ProcessValue();
        }

        public Cell (int row, int col, int square, int val)
        {
            // Initialize Cell (Row, Column, Square and Value)
            _Row = row;
            _Column = col;
            _Square = square;
            _Value = val;

            // Remove all other possibilities (if val > 0)
            ProcessValue();
        }

        private int CalcSquare(int row, int col)
        {
            int row3 = (row - 1) / 3;
            int col3 = (col - 1)/ 3;

            return (row3 * 3) + col3 + 1;
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public bool Contradiction
        {
            get 
            {
                return (_Contradiction);
            }
        }

        public int PossibleValueCount
        {
            get { return PossibleValueString.Length - 1;  }
        }

        public string PossibleValueString
        {
            get
            {
                if (_Value > 0)
                    return _Value.ToString();
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("*");
                    for (int i = 1; i < 10; i++)
                    {
                        if (PossibleValue(i) == 0)
                            sb.Append(i.ToString());
                    }

                    return sb.ToString();
                }
            }
        }
        public int Value
        {
            get { return _Value;}
            set
            {
                if (_possibleValues[value - 1] == -1)
                {
                    _Contradiction = true;
                    _isDirty = true;
                }
                else if (_Value != value)
                {
                    _Value = value;
                    _isDirty = true;
                }
            }
        }

        public int PossibleValue(int index)
        {
            // Returns the status for a specific value of a cell.  1 = Known value, -1 = Not the value, 0 = Don't know
            return _possibleValues[index - 1];
        }

        public int PossibleValue(int index, int status)
        {
            // Sets and Returns the status for a specific value of a cell.  1 = Known value, -1 = Not the value, 0 = Don't know
            if (_possibleValues[index - 1] != status)
            {
                _isDirty = true;
                _possibleValues[index - 1] = status;

                bool allNo = false;

                for (int i = 0; i < 9; i++)
                {
                    allNo = allNo || (_possibleValues[i] != -1);
                }

                if (!allNo)
                    _Contradiction = true;
            }

            return _possibleValues[index - 1];
        }

        public void ProcessValue()
        {
            // Sets assigned value to 1.  Sets all other possible values to -1
            if (_Value > 0)
            {
                if (_possibleValues[_Value - 1] != -1)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if (i == _Value)
                            PossibleValue(i, 1);
                        else
                            PossibleValue(i, -1);
                    }
                }
                else
                {
                    _Contradiction = true;
                }
            }
        }

        public int Column
        {
            get { return _Column; }
        }

        public int Row
        {
            get { return _Row; }
        }

        public int Square
        {
            get { return _Square; }
        }

        public bool CheckIfKnown()
        {
            //If 8 possibilities have been removed for cell, the remaining unknown becomes the Value
            int firstUnknown = -1;

            if (_Value == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    // First unknown is first "0"
                    if (firstUnknown < 0 && _possibleValues[i] == 0)
                    {
                        firstUnknown = i;
                    }
                    // 2nd unknown means we still have multiple unknowns.  No action get out of loop (i = 8)
                    else if(firstUnknown >= 0 && _possibleValues[i] == 0)
                    {
                        firstUnknown = -1;
                        i = 8;
                    }
                }

                if (firstUnknown >= 0)
                {
                    Value = firstUnknown + 1;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public int LonelyBlendCount
        {
            get
            {
                int nCount = 0;
    
                // Counts the number of unknown (Status = 0) values in a cell
                for (int nCntr = 0; nCntr <= 8; nCntr++)
                {
                    if (_possibleValues[nCntr] == 0)
                        nCount++;
                }
                return nCount;
            }
        }
    }

    class CellCollection
    {
        private ArrayList cellColl = new ArrayList();

        public CellCollection()
        {
            cellColl = new ArrayList();
        }

        public CellCollection(ArrayList startCells)
        {
            foreach (Cell c in startCells)
            {
                cellColl.Add(c);
            }
        }

        public ArrayList Cells
        {
            get { return cellColl; }
        }

        public void ClearDirty()
        {
            foreach (Cell c in cellColl) { c.IsDirty = false; }
        }

        public bool IsDirty
        {
            get
            {
                foreach (Cell c in cellColl)
                {
                    if (c.IsDirty)
                        return true;
                }

                return false;
            }
        }

        public bool IsSolved
        {
            get
            {
                foreach (Cell c in cellColl)
                {
                    if (c.Value == 0)
                        return false;
                }
                return true;

            }
        }

        public bool ContradictionExists
        {
            get 
            { 
                foreach (Cell c in cellColl)
                {
                    if (c.Contradiction)
                        return true;
                }

                return false;
            }
        }

        public bool FixLonely()
        {
            // The cell collection will be checked to see if, within a group of cells, there exists a subset of cells
            // that only have the same number of unknown possibility values.

            // Examples:
            // Two cells in the collection can only be 1 or 2, the remaining cells cannot be 1 or 2
            // Three cells in the collection can only be 1, 2 or 3, the remaining cells cannot be 1, 2 or 3

            //  Note:  This function WILL update the status values if the specific condition is identified

            bool bChanged = false;

            // 'Lonely' Pair
            if (cellColl.Count >= 3)
            {
                for (int nCntr = 0; nCntr < cellColl.Count; nCntr++)
                {
                    for (int nCntr2 = nCntr + 1; nCntr2 < cellColl.Count; nCntr2++)
                    {
                        ArrayList tempColl = new ArrayList();
                        tempColl.Add((Cell)cellColl[nCntr]);
                        tempColl.Add((Cell)cellColl[nCntr2]);

                        if (MatchLonely(tempColl, 2))
                        {
                            bChanged = RemoveLonely(tempColl);
                            if (bChanged)
                            {
                                nCntr = cellColl.Count;
                                nCntr2 = cellColl.Count;
                            }
                        }
                    }
                }
            }
    
            //// 'Lonely' Triple
            if (cellColl.Count >= 4 && !bChanged)
            {
                for (int nCntr = 0; nCntr < cellColl.Count; nCntr++)
                {
                    for (int nCntr2 = nCntr + 1; nCntr2 < cellColl.Count; nCntr2++)
                    {
                        for (int nCntr3 = nCntr2 + 1; nCntr3 < cellColl.Count; nCntr3++)
                        {
                            ArrayList tempColl = new ArrayList();
                            tempColl.Add((Cell)cellColl[nCntr]);
                            tempColl.Add((Cell)cellColl[nCntr2]);
                            tempColl.Add((Cell)cellColl[nCntr3]);

                            if (MatchLonely(tempColl, 3))
                            {
                                bChanged = RemoveLonely(tempColl);
                                if (bChanged)
                                {
                                    nCntr = cellColl.Count;
                                    nCntr2 = cellColl.Count;
                                    nCntr3 = cellColl.Count;
                                }
                            }
                        }
                    }
                }
            }
    
            //' Naked Quad
            if (cellColl.Count >= 5 && !bChanged)
            {
                for (int nCntr = 0; nCntr < cellColl.Count; nCntr++)
                {
                    for (int nCntr2 = nCntr + 1; nCntr2 < cellColl.Count; nCntr2++)
                    {
                        for (int nCntr3 = nCntr2 + 1; nCntr3 < cellColl.Count; nCntr3++)
                        {
                            for (int nCntr4 = nCntr3 + 1; nCntr4 < cellColl.Count; nCntr4++)
                            {
                                ArrayList tempColl = new ArrayList();
                                tempColl.Add((Cell)cellColl[nCntr]);
                                tempColl.Add((Cell)cellColl[nCntr2]);
                                tempColl.Add((Cell)cellColl[nCntr3]);
                                tempColl.Add((Cell)cellColl[nCntr4]);

                                if (MatchLonely(tempColl, 4))
                                {
                                    bChanged = RemoveLonely(tempColl);
                                    if (bChanged)
                                    {
                                        nCntr = cellColl.Count;
                                        nCntr2 = cellColl.Count;
                                        nCntr3 = cellColl.Count;
                                        nCntr4 = cellColl.Count;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bChanged;
        }

        private bool MatchLonely(ArrayList lonelyColl, int blendCount)
        {
            Cell testCell = LonelyBlend(lonelyColl);

            return testCell.LonelyBlendCount == blendCount;
        }

        private bool RemoveLonely(ArrayList lonelyColl)
        {
            bool bReturn = false;
            Cell tempCell = LonelyBlend(lonelyColl);

            foreach (Cell c in cellColl)
            {
                bool bFound = false;
                foreach(Cell c1 in lonelyColl)
                {
                    if (c1.Row == c.Row && c1.Column == c.Column)
                        bFound = true;
                }

                if (!bFound)
                {
                    for (int nCntr = 1; nCntr < 10; nCntr++)
                    {
                        if (tempCell.PossibleValue(nCntr) == 0 && c.PossibleValue(nCntr) == 0)
                        {
                            c.PossibleValue(nCntr, -1);
                            bReturn = true;
                        }
                    }
                }
            }

            return bReturn;
        }

        private Cell LonelyBlend(ArrayList lonelyColl)
        {
            ArrayList tempColl = new ArrayList();

            foreach (Cell c in lonelyColl)
            {
                tempColl.Add(c);
            }

            if (tempColl.Count == 2)
                return LonelyBlend((Cell)tempColl[0], (Cell)tempColl[1]);
            else
            {
                Cell c1 = (Cell)tempColl[0];
                Cell c2 = (Cell)tempColl[1];

                tempColl.Remove(c1);
                tempColl.Remove(c2);

                tempColl.Add(LonelyBlend(c1, c2));

                return LonelyBlend(tempColl);
            }
        }

        private Cell LonelyBlend(Cell cell1, Cell cell2)
        {
            Cell returnCell = new Cell(0, 0, 0, 0);
            int tempStatus = 1;

            for (int nCntr = 1; nCntr < 10; nCntr++)
            {
                tempStatus = 1;
                if (cell1.PossibleValue(nCntr) == 0 || cell2.PossibleValue(nCntr) == 0)
                {
                    tempStatus = 0;
                }
                returnCell.PossibleValue(nCntr, tempStatus);
            }

            return returnCell;
        }

    }
}
