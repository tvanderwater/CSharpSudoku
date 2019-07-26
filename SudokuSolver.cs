using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuSolver : Form
    {
        private ArrayList storedPuzzle = new ArrayList();
        private ArrayList currentPuzzle = new ArrayList();

        private bool stepStart = false;

        public SudokuSolver()
        {
            InitializeComponent();
        }

        public SudokuSolver(string startGrid)
        {

        }

        private int PuzzleInteger(string value)
        {
            if (value == "")
                return 0;
            else
                return int.Parse(value);
        }

        private ArrayList StoreSolution()
        {
            ArrayList toStore = new ArrayList();

            toStore.Add(new Cell(1, 1, SafeValue(textBox1.Text)));
            toStore.Add(new Cell(1, 2, SafeValue(textBox2.Text)));
            toStore.Add(new Cell(1, 3, SafeValue(textBox3.Text)));
            toStore.Add(new Cell(1, 4, SafeValue(textBox4.Text)));
            toStore.Add(new Cell(1, 5, SafeValue(textBox5.Text)));
            toStore.Add(new Cell(1, 6, SafeValue(textBox6.Text)));
            toStore.Add(new Cell(1, 7, SafeValue(textBox7.Text)));
            toStore.Add(new Cell(1, 8, SafeValue(textBox8.Text)));
            toStore.Add(new Cell(1, 9, SafeValue(textBox9.Text)));

            toStore.Add(new Cell(2, 1, 1, SafeValue(textBox10.Text)));
            toStore.Add(new Cell(2, 2, 1, SafeValue(textBox11.Text)));
            toStore.Add(new Cell(2, 3, 1, SafeValue(textBox12.Text)));
            toStore.Add(new Cell(2, 4, 2, SafeValue(textBox13.Text)));
            toStore.Add(new Cell(2, 5, 2, SafeValue(textBox14.Text)));
            toStore.Add(new Cell(2, 6, 2, SafeValue(textBox15.Text)));
            toStore.Add(new Cell(2, 7, 3, SafeValue(textBox16.Text)));
            toStore.Add(new Cell(2, 8, 3, SafeValue(textBox17.Text)));
            toStore.Add(new Cell(2, 9, 3, SafeValue(textBox18.Text)));

            toStore.Add(new Cell(3, 1, 1, SafeValue(textBox19.Text)));
            toStore.Add(new Cell(3, 2, 1, SafeValue(textBox20.Text)));
            toStore.Add(new Cell(3, 3, 1, SafeValue(textBox21.Text)));
            toStore.Add(new Cell(3, 4, 2, SafeValue(textBox22.Text)));
            toStore.Add(new Cell(3, 5, 2, SafeValue(textBox23.Text)));
            toStore.Add(new Cell(3, 6, 2, SafeValue(textBox24.Text)));
            toStore.Add(new Cell(3, 7, 3, SafeValue(textBox25.Text)));
            toStore.Add(new Cell(3, 8, 3, SafeValue(textBox26.Text)));
            toStore.Add(new Cell(3, 9, 3, SafeValue(textBox27.Text)));

            toStore.Add(new Cell(4, 1, 4, SafeValue(textBox28.Text)));
            toStore.Add(new Cell(4, 2, 4, SafeValue(textBox29.Text)));
            toStore.Add(new Cell(4, 3, 4, SafeValue(textBox30.Text)));
            toStore.Add(new Cell(4, 4, 5, SafeValue(textBox31.Text)));
            toStore.Add(new Cell(4, 5, 5, SafeValue(textBox32.Text)));
            toStore.Add(new Cell(4, 6, 5, SafeValue(textBox33.Text)));
            toStore.Add(new Cell(4, 7, 6, SafeValue(textBox34.Text)));
            toStore.Add(new Cell(4, 8, 6, SafeValue(textBox35.Text)));
            toStore.Add(new Cell(4, 9, 6, SafeValue(textBox36.Text)));

            toStore.Add(new Cell(5, 1, 4, SafeValue(textBox37.Text)));
            toStore.Add(new Cell(5, 2, 4, SafeValue(textBox38.Text)));
            toStore.Add(new Cell(5, 3, 4, SafeValue(textBox39.Text)));
            toStore.Add(new Cell(5, 4, 5, SafeValue(textBox40.Text)));
            toStore.Add(new Cell(5, 5, 5, SafeValue(textBox41.Text)));
            toStore.Add(new Cell(5, 6, 5, SafeValue(textBox42.Text)));
            toStore.Add(new Cell(5, 7, 6, SafeValue(textBox43.Text)));
            toStore.Add(new Cell(5, 8, 6, SafeValue(textBox44.Text)));
            toStore.Add(new Cell(5, 9, 6, SafeValue(textBox45.Text)));

            toStore.Add(new Cell(6, 1, 4, SafeValue(textBox46.Text)));
            toStore.Add(new Cell(6, 2, 4, SafeValue(textBox47.Text)));
            toStore.Add(new Cell(6, 3, 4, SafeValue(textBox48.Text)));
            toStore.Add(new Cell(6, 4, 5, SafeValue(textBox49.Text)));
            toStore.Add(new Cell(6, 5, 5, SafeValue(textBox50.Text)));
            toStore.Add(new Cell(6, 6, 5, SafeValue(textBox51.Text)));
            toStore.Add(new Cell(6, 7, 6, SafeValue(textBox52.Text)));
            toStore.Add(new Cell(6, 8, 6, SafeValue(textBox53.Text)));
            toStore.Add(new Cell(6, 9, 6, SafeValue(textBox54.Text)));

            toStore.Add(new Cell(7, 1, SafeValue(textBox55.Text)));
            toStore.Add(new Cell(7, 2, SafeValue(textBox56.Text)));
            toStore.Add(new Cell(7, 3, SafeValue(textBox57.Text)));
            toStore.Add(new Cell(7, 4, SafeValue(textBox58.Text)));
            toStore.Add(new Cell(7, 5, SafeValue(textBox59.Text)));
            toStore.Add(new Cell(7, 6, SafeValue(textBox60.Text)));
            toStore.Add(new Cell(7, 7, SafeValue(textBox61.Text)));
            toStore.Add(new Cell(7, 8, SafeValue(textBox62.Text)));
            toStore.Add(new Cell(7, 9, SafeValue(textBox63.Text)));

            toStore.Add(new Cell(8, 1, 7, SafeValue(textBox64.Text)));
            toStore.Add(new Cell(8, 2, 7, SafeValue(textBox65.Text)));
            toStore.Add(new Cell(8, 3, 7, SafeValue(textBox66.Text)));
            toStore.Add(new Cell(8, 4, 8, SafeValue(textBox67.Text)));
            toStore.Add(new Cell(8, 5, 8, SafeValue(textBox68.Text)));
            toStore.Add(new Cell(8, 6, 8, SafeValue(textBox69.Text)));
            toStore.Add(new Cell(8, 7, 9, SafeValue(textBox70.Text)));
            toStore.Add(new Cell(8, 8, 9, SafeValue(textBox71.Text)));
            toStore.Add(new Cell(8, 9, 9, SafeValue(textBox72.Text)));

            toStore.Add(new Cell(9, 1, 7, SafeValue(textBox73.Text)));
            toStore.Add(new Cell(9, 2, 7, SafeValue(textBox74.Text)));
            toStore.Add(new Cell(9, 3, 7, SafeValue(textBox75.Text)));
            toStore.Add(new Cell(9, 4, 8, SafeValue(textBox76.Text)));
            toStore.Add(new Cell(9, 5, 8, SafeValue(textBox77.Text)));
            toStore.Add(new Cell(9, 6, 8, SafeValue(textBox78.Text)));
            toStore.Add(new Cell(9, 7, 9, SafeValue(textBox79.Text)));
            toStore.Add(new Cell(9, 8, 9, SafeValue(textBox80.Text)));
            toStore.Add(new Cell(9, 9, 9, SafeValue(textBox81.Text)));

            return toStore;
        }

        private void DisplaySolution(ArrayList toSolve)
        {
            DisplaySolution(toSolve, false);
        }

        private void DisplaySolution(ArrayList toSolve, bool showBlank)
        {
            if (showBlank)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox15.Text = "";
                textBox16.Text = "";
                textBox17.Text = "";
                textBox18.Text = "";
                textBox19.Text = "";
                textBox20.Text = "";
                textBox21.Text = "";
                textBox22.Text = "";
                textBox23.Text = "";
                textBox24.Text = "";
                textBox25.Text = "";
                textBox26.Text = "";
                textBox27.Text = "";
                textBox28.Text = "";
                textBox29.Text = "";
                textBox30.Text = "";
                textBox31.Text = "";
                textBox32.Text = "";
                textBox33.Text = "";
                textBox34.Text = "";
                textBox35.Text = "";
                textBox36.Text = "";
                textBox37.Text = "";
                textBox38.Text = "";
                textBox39.Text = "";
                textBox40.Text = "";
                textBox41.Text = "";
                textBox42.Text = "";
                textBox43.Text = "";
                textBox44.Text = "";
                textBox45.Text = "";
                textBox46.Text = "";
                textBox47.Text = "";
                textBox48.Text = "";
                textBox49.Text = "";
                textBox50.Text = "";
                textBox51.Text = "";
                textBox52.Text = "";
                textBox53.Text = "";
                textBox54.Text = "";
                textBox55.Text = "";
                textBox56.Text = "";
                textBox57.Text = "";
                textBox58.Text = "";
                textBox59.Text = "";
                textBox60.Text = "";
                textBox61.Text = "";
                textBox62.Text = "";
                textBox63.Text = "";
                textBox64.Text = "";
                textBox65.Text = "";
                textBox66.Text = "";
                textBox67.Text = "";
                textBox68.Text = "";
                textBox69.Text = "";
                textBox70.Text = "";
                textBox71.Text = "";
                textBox72.Text = "";
                textBox73.Text = "";
                textBox74.Text = "";
                textBox75.Text = "";
                textBox76.Text = "";
                textBox77.Text = "";
                textBox78.Text = "";
                textBox79.Text = "";
                textBox80.Text = "";
                textBox81.Text = "";
            }
            else
            {
                textBox1.Text = ((Cell)toSolve[0]).PossibleValueString;
                textBox2.Text = ((Cell)toSolve[1]).PossibleValueString;
                textBox3.Text = ((Cell)toSolve[2]).PossibleValueString;
                textBox4.Text = ((Cell)toSolve[3]).PossibleValueString;
                textBox5.Text = ((Cell)toSolve[4]).PossibleValueString;
                textBox6.Text = ((Cell)toSolve[5]).PossibleValueString;
                textBox7.Text = ((Cell)toSolve[6]).PossibleValueString;
                textBox8.Text = ((Cell)toSolve[7]).PossibleValueString;
                textBox9.Text = ((Cell)toSolve[8]).PossibleValueString;
                textBox10.Text = ((Cell)toSolve[9]).PossibleValueString;
                textBox11.Text = ((Cell)toSolve[10]).PossibleValueString;
                textBox12.Text = ((Cell)toSolve[11]).PossibleValueString;
                textBox13.Text = ((Cell)toSolve[12]).PossibleValueString;
                textBox14.Text = ((Cell)toSolve[13]).PossibleValueString;
                textBox15.Text = ((Cell)toSolve[14]).PossibleValueString;
                textBox16.Text = ((Cell)toSolve[15]).PossibleValueString;
                textBox17.Text = ((Cell)toSolve[16]).PossibleValueString;
                textBox18.Text = ((Cell)toSolve[17]).PossibleValueString;
                textBox19.Text = ((Cell)toSolve[18]).PossibleValueString;
                textBox20.Text = ((Cell)toSolve[19]).PossibleValueString;
                textBox21.Text = ((Cell)toSolve[20]).PossibleValueString;
                textBox22.Text = ((Cell)toSolve[21]).PossibleValueString;
                textBox23.Text = ((Cell)toSolve[22]).PossibleValueString;
                textBox24.Text = ((Cell)toSolve[23]).PossibleValueString;
                textBox25.Text = ((Cell)toSolve[24]).PossibleValueString;
                textBox26.Text = ((Cell)toSolve[25]).PossibleValueString;
                textBox27.Text = ((Cell)toSolve[26]).PossibleValueString;
                textBox28.Text = ((Cell)toSolve[27]).PossibleValueString;
                textBox29.Text = ((Cell)toSolve[28]).PossibleValueString;
                textBox30.Text = ((Cell)toSolve[29]).PossibleValueString;
                textBox31.Text = ((Cell)toSolve[30]).PossibleValueString;
                textBox32.Text = ((Cell)toSolve[31]).PossibleValueString;
                textBox33.Text = ((Cell)toSolve[32]).PossibleValueString;
                textBox34.Text = ((Cell)toSolve[33]).PossibleValueString;
                textBox35.Text = ((Cell)toSolve[34]).PossibleValueString;
                textBox36.Text = ((Cell)toSolve[35]).PossibleValueString;
                textBox37.Text = ((Cell)toSolve[36]).PossibleValueString;
                textBox38.Text = ((Cell)toSolve[37]).PossibleValueString;
                textBox39.Text = ((Cell)toSolve[38]).PossibleValueString;
                textBox40.Text = ((Cell)toSolve[39]).PossibleValueString;
                textBox41.Text = ((Cell)toSolve[40]).PossibleValueString;
                textBox42.Text = ((Cell)toSolve[41]).PossibleValueString;
                textBox43.Text = ((Cell)toSolve[42]).PossibleValueString;
                textBox44.Text = ((Cell)toSolve[43]).PossibleValueString;
                textBox45.Text = ((Cell)toSolve[44]).PossibleValueString;
                textBox46.Text = ((Cell)toSolve[45]).PossibleValueString;
                textBox47.Text = ((Cell)toSolve[46]).PossibleValueString;
                textBox48.Text = ((Cell)toSolve[47]).PossibleValueString;
                textBox49.Text = ((Cell)toSolve[48]).PossibleValueString;
                textBox50.Text = ((Cell)toSolve[49]).PossibleValueString;
                textBox51.Text = ((Cell)toSolve[50]).PossibleValueString;
                textBox52.Text = ((Cell)toSolve[51]).PossibleValueString;
                textBox53.Text = ((Cell)toSolve[52]).PossibleValueString;
                textBox54.Text = ((Cell)toSolve[53]).PossibleValueString;
                textBox55.Text = ((Cell)toSolve[54]).PossibleValueString;
                textBox56.Text = ((Cell)toSolve[55]).PossibleValueString;
                textBox57.Text = ((Cell)toSolve[56]).PossibleValueString;
                textBox58.Text = ((Cell)toSolve[57]).PossibleValueString;
                textBox59.Text = ((Cell)toSolve[58]).PossibleValueString;
                textBox60.Text = ((Cell)toSolve[59]).PossibleValueString;
                textBox61.Text = ((Cell)toSolve[60]).PossibleValueString;
                textBox62.Text = ((Cell)toSolve[61]).PossibleValueString;
                textBox63.Text = ((Cell)toSolve[62]).PossibleValueString;
                textBox64.Text = ((Cell)toSolve[63]).PossibleValueString;
                textBox65.Text = ((Cell)toSolve[64]).PossibleValueString;
                textBox66.Text = ((Cell)toSolve[65]).PossibleValueString;
                textBox67.Text = ((Cell)toSolve[66]).PossibleValueString;
                textBox68.Text = ((Cell)toSolve[67]).PossibleValueString;
                textBox69.Text = ((Cell)toSolve[68]).PossibleValueString;
                textBox70.Text = ((Cell)toSolve[69]).PossibleValueString;
                textBox71.Text = ((Cell)toSolve[70]).PossibleValueString;
                textBox72.Text = ((Cell)toSolve[71]).PossibleValueString;
                textBox73.Text = ((Cell)toSolve[72]).PossibleValueString;
                textBox74.Text = ((Cell)toSolve[73]).PossibleValueString;
                textBox75.Text = ((Cell)toSolve[74]).PossibleValueString;
                textBox76.Text = ((Cell)toSolve[75]).PossibleValueString;
                textBox77.Text = ((Cell)toSolve[76]).PossibleValueString;
                textBox78.Text = ((Cell)toSolve[77]).PossibleValueString;
                textBox79.Text = ((Cell)toSolve[78]).PossibleValueString;
                textBox80.Text = ((Cell)toSolve[79]).PossibleValueString;
                textBox81.Text = ((Cell)toSolve[80]).PossibleValueString;
            }
        }

        private ArrayList StringPuzzle()
        {
            string puzzleString = "530070000600195000098000060800060003400803001700020006060000280000419005000080079";

            ArrayList toSolve = new ArrayList();
            int stringLoc = 0;

            for (int i = 1; i < 10; i++)
            {
                for (int k = 1; k < 10; k++)
                {
                    toSolve.Add(new Cell(i, k, int.Parse(puzzleString.Substring(stringLoc, 1))));
                    stringLoc++;
                }
            }

            return toSolve;
        }

        private ArrayList UseFormPuzzle()
        {
            ArrayList toSolve = new ArrayList();

            toSolve.Add(new Cell(1, 1, 1, SafeValue(textBox1.Text)));
            toSolve.Add(new Cell(1, 2, 1, SafeValue(textBox2.Text)));
            toSolve.Add(new Cell(1, 3, 1, SafeValue(textBox3.Text)));
            toSolve.Add(new Cell(1, 4, 2, SafeValue(textBox4.Text)));
            toSolve.Add(new Cell(1, 5, 2, SafeValue(textBox5.Text)));
            toSolve.Add(new Cell(1, 6, 2, SafeValue(textBox6.Text)));
            toSolve.Add(new Cell(1, 7, 3, SafeValue(textBox7.Text)));
            toSolve.Add(new Cell(1, 8, 3, SafeValue(textBox8.Text)));
            toSolve.Add(new Cell(1, 9, 3, SafeValue(textBox9.Text)));

            toSolve.Add(new Cell(2, 1, 1, SafeValue(textBox10.Text)));
            toSolve.Add(new Cell(2, 2, 1, SafeValue(textBox11.Text)));
            toSolve.Add(new Cell(2, 3, 1, SafeValue(textBox12.Text)));
            toSolve.Add(new Cell(2, 4, 2, SafeValue(textBox13.Text)));
            toSolve.Add(new Cell(2, 5, 2, SafeValue(textBox14.Text)));
            toSolve.Add(new Cell(2, 6, 2, SafeValue(textBox15.Text)));
            toSolve.Add(new Cell(2, 7, 3, SafeValue(textBox16.Text)));
            toSolve.Add(new Cell(2, 8, 3, SafeValue(textBox17.Text)));
            toSolve.Add(new Cell(2, 9, 3, SafeValue(textBox18.Text)));

            toSolve.Add(new Cell(3, 1, 1, SafeValue(textBox19.Text)));
            toSolve.Add(new Cell(3, 2, 1, SafeValue(textBox20.Text)));
            toSolve.Add(new Cell(3, 3, 1, SafeValue(textBox21.Text)));
            toSolve.Add(new Cell(3, 4, 2, SafeValue(textBox22.Text)));
            toSolve.Add(new Cell(3, 5, 2, SafeValue(textBox23.Text)));
            toSolve.Add(new Cell(3, 6, 2, SafeValue(textBox24.Text)));
            toSolve.Add(new Cell(3, 7, 3, SafeValue(textBox25.Text)));
            toSolve.Add(new Cell(3, 8, 3, SafeValue(textBox26.Text)));
            toSolve.Add(new Cell(3, 9, 3, SafeValue(textBox27.Text)));

            toSolve.Add(new Cell(4, 1, 4, SafeValue(textBox28.Text)));
            toSolve.Add(new Cell(4, 2, 4, SafeValue(textBox29.Text)));
            toSolve.Add(new Cell(4, 3, 4, SafeValue(textBox30.Text)));
            toSolve.Add(new Cell(4, 4, 5, SafeValue(textBox31.Text)));
            toSolve.Add(new Cell(4, 5, 5, SafeValue(textBox32.Text)));
            toSolve.Add(new Cell(4, 6, 5, SafeValue(textBox33.Text)));
            toSolve.Add(new Cell(4, 7, 6, SafeValue(textBox34.Text)));
            toSolve.Add(new Cell(4, 8, 6, SafeValue(textBox35.Text)));
            toSolve.Add(new Cell(4, 9, 6, SafeValue(textBox36.Text)));

            toSolve.Add(new Cell(5, 1, 4, SafeValue(textBox37.Text)));
            toSolve.Add(new Cell(5, 2, 4, SafeValue(textBox38.Text)));
            toSolve.Add(new Cell(5, 3, 4, SafeValue(textBox39.Text)));
            toSolve.Add(new Cell(5, 4, 5, SafeValue(textBox40.Text)));
            toSolve.Add(new Cell(5, 5, 5, SafeValue(textBox41.Text)));
            toSolve.Add(new Cell(5, 6, 5, SafeValue(textBox42.Text)));
            toSolve.Add(new Cell(5, 7, 6, SafeValue(textBox43.Text)));
            toSolve.Add(new Cell(5, 8, 6, SafeValue(textBox44.Text)));
            toSolve.Add(new Cell(5, 9, 6, SafeValue(textBox45.Text)));

            toSolve.Add(new Cell(6, 1, 4, SafeValue(textBox46.Text)));
            toSolve.Add(new Cell(6, 2, 4, SafeValue(textBox47.Text)));
            toSolve.Add(new Cell(6, 3, 4, SafeValue(textBox48.Text)));
            toSolve.Add(new Cell(6, 4, 5, SafeValue(textBox49.Text)));
            toSolve.Add(new Cell(6, 5, 5, SafeValue(textBox50.Text)));
            toSolve.Add(new Cell(6, 6, 5, SafeValue(textBox51.Text)));
            toSolve.Add(new Cell(6, 7, 6, SafeValue(textBox52.Text)));
            toSolve.Add(new Cell(6, 8, 6, SafeValue(textBox53.Text)));
            toSolve.Add(new Cell(6, 9, 6, SafeValue(textBox54.Text)));

            toSolve.Add(new Cell(7, 1, 7, SafeValue(textBox55.Text)));
            toSolve.Add(new Cell(7, 2, 7, SafeValue(textBox56.Text)));
            toSolve.Add(new Cell(7, 3, 7, SafeValue(textBox57.Text)));
            toSolve.Add(new Cell(7, 4, 8, SafeValue(textBox58.Text)));
            toSolve.Add(new Cell(7, 5, 8, SafeValue(textBox59.Text)));
            toSolve.Add(new Cell(7, 6, 8, SafeValue(textBox60.Text)));
            toSolve.Add(new Cell(7, 7, 9, SafeValue(textBox61.Text)));
            toSolve.Add(new Cell(7, 8, 9, SafeValue(textBox62.Text)));
            toSolve.Add(new Cell(7, 9, 9, SafeValue(textBox63.Text)));

            toSolve.Add(new Cell(8, 1, 7, SafeValue(textBox64.Text)));
            toSolve.Add(new Cell(8, 2, 7, SafeValue(textBox65.Text)));
            toSolve.Add(new Cell(8, 3, 7, SafeValue(textBox66.Text)));
            toSolve.Add(new Cell(8, 4, 8, SafeValue(textBox67.Text)));
            toSolve.Add(new Cell(8, 5, 8, SafeValue(textBox68.Text)));
            toSolve.Add(new Cell(8, 6, 8, SafeValue(textBox69.Text)));
            toSolve.Add(new Cell(8, 7, 9, SafeValue(textBox70.Text)));
            toSolve.Add(new Cell(8, 8, 9, SafeValue(textBox71.Text)));
            toSolve.Add(new Cell(8, 9, 9, SafeValue(textBox72.Text)));

            toSolve.Add(new Cell(9, 1, 7, SafeValue(textBox73.Text)));
            toSolve.Add(new Cell(9, 2, 7, SafeValue(textBox74.Text)));
            toSolve.Add(new Cell(9, 3, 7, SafeValue(textBox75.Text)));
            toSolve.Add(new Cell(9, 4, 8, SafeValue(textBox76.Text)));
            toSolve.Add(new Cell(9, 5, 8, SafeValue(textBox77.Text)));
            toSolve.Add(new Cell(9, 6, 8, SafeValue(textBox78.Text)));
            toSolve.Add(new Cell(9, 7, 9, SafeValue(textBox79.Text)));
            toSolve.Add(new Cell(9, 8, 9, SafeValue(textBox80.Text)));
            toSolve.Add(new Cell(9, 9, 9, SafeValue(textBox81.Text)));

            return toSolve;
        }

        private ArrayList UseClearPuzzle()
        {
            ArrayList toSolve = new ArrayList();

            toSolve.Add(new Cell(1, 1, 1, 0));
            toSolve.Add(new Cell(1, 2, 1, 0));
            toSolve.Add(new Cell(1, 3, 1, 0));
            toSolve.Add(new Cell(1, 4, 2, 0));
            toSolve.Add(new Cell(1, 5, 2, 0));
            toSolve.Add(new Cell(1, 6, 2, 0));
            toSolve.Add(new Cell(1, 7, 3, 0));
            toSolve.Add(new Cell(1, 8, 3, 0));
            toSolve.Add(new Cell(1, 9, 3, 0));

            toSolve.Add(new Cell(2, 1, 1, 0));
            toSolve.Add(new Cell(2, 2, 1, 0));
            toSolve.Add(new Cell(2, 3, 1, 0));
            toSolve.Add(new Cell(2, 4, 2, 0));
            toSolve.Add(new Cell(2, 5, 2, 0));
            toSolve.Add(new Cell(2, 6, 2, 0));
            toSolve.Add(new Cell(2, 7, 3, 0));
            toSolve.Add(new Cell(2, 8, 3, 0));
            toSolve.Add(new Cell(2, 9, 3, 0));

            toSolve.Add(new Cell(3, 1, 1, 0));
            toSolve.Add(new Cell(3, 2, 1, 0));
            toSolve.Add(new Cell(3, 3, 1, 0));
            toSolve.Add(new Cell(3, 4, 2, 0));
            toSolve.Add(new Cell(3, 5, 2, 0));
            toSolve.Add(new Cell(3, 6, 2, 0));
            toSolve.Add(new Cell(3, 7, 3, 0));
            toSolve.Add(new Cell(3, 8, 3, 0));
            toSolve.Add(new Cell(3, 9, 3, 0));

            toSolve.Add(new Cell(4, 1, 4, 0));
            toSolve.Add(new Cell(4, 2, 4, 0));
            toSolve.Add(new Cell(4, 3, 4, 0));
            toSolve.Add(new Cell(4, 4, 5, 0));
            toSolve.Add(new Cell(4, 5, 5, 0));
            toSolve.Add(new Cell(4, 6, 5, 0));
            toSolve.Add(new Cell(4, 7, 6, 0));
            toSolve.Add(new Cell(4, 8, 6, 0));
            toSolve.Add(new Cell(4, 9, 6, 0));

            toSolve.Add(new Cell(5, 1, 4, 0));
            toSolve.Add(new Cell(5, 2, 4, 0));
            toSolve.Add(new Cell(5, 3, 4, 0));
            toSolve.Add(new Cell(5, 4, 5, 0));
            toSolve.Add(new Cell(5, 5, 5, 0));
            toSolve.Add(new Cell(5, 6, 5, 0));
            toSolve.Add(new Cell(5, 7, 6, 0));
            toSolve.Add(new Cell(5, 8, 6, 0));
            toSolve.Add(new Cell(5, 9, 6, 0));

            toSolve.Add(new Cell(6, 1, 4, 0));
            toSolve.Add(new Cell(6, 2, 4, 0));
            toSolve.Add(new Cell(6, 3, 4, 0));
            toSolve.Add(new Cell(6, 4, 5, 0));
            toSolve.Add(new Cell(6, 5, 5, 0));
            toSolve.Add(new Cell(6, 6, 5, 0));
            toSolve.Add(new Cell(6, 7, 6, 0));
            toSolve.Add(new Cell(6, 8, 6, 0));
            toSolve.Add(new Cell(6, 9, 6, 0));

            toSolve.Add(new Cell(7, 1, 7, 0));
            toSolve.Add(new Cell(7, 2, 7, 0));
            toSolve.Add(new Cell(7, 3, 7, 0));
            toSolve.Add(new Cell(7, 4, 8, 0));
            toSolve.Add(new Cell(7, 5, 8, 0));
            toSolve.Add(new Cell(7, 6, 8, 0));
            toSolve.Add(new Cell(7, 7, 9, 0));
            toSolve.Add(new Cell(7, 8, 9, 0));
            toSolve.Add(new Cell(7, 9, 9, 0));

            toSolve.Add(new Cell(8, 1, 7, 0));
            toSolve.Add(new Cell(8, 2, 7, 0));
            toSolve.Add(new Cell(8, 3, 7, 0));
            toSolve.Add(new Cell(8, 4, 8, 0));
            toSolve.Add(new Cell(8, 5, 8, 0));
            toSolve.Add(new Cell(8, 6, 8, 0));
            toSolve.Add(new Cell(8, 7, 9, 0));
            toSolve.Add(new Cell(8, 8, 9, 0));
            toSolve.Add(new Cell(8, 9, 9, 0));

            toSolve.Add(new Cell(9, 1, 7, 0));
            toSolve.Add(new Cell(9, 2, 7, 0));
            toSolve.Add(new Cell(9, 3, 7, 0));
            toSolve.Add(new Cell(9, 4, 8, 0));
            toSolve.Add(new Cell(9, 5, 8, 0));
            toSolve.Add(new Cell(9, 6, 8, 0));
            toSolve.Add(new Cell(9, 7, 9, 0));
            toSolve.Add(new Cell(9, 8, 9, 0));
            toSolve.Add(new Cell(9, 9, 9, 0));

            return toSolve;
        }

        private ArrayList HardestPuzzle()
        {
            ArrayList toSolve = new ArrayList();

            toSolve.Add(new Cell(1, 1, 1, 1));
            toSolve.Add(new Cell(1, 2, 1, 0));
            toSolve.Add(new Cell(1, 3, 1, 0));
            toSolve.Add(new Cell(1, 4, 2, 0));
            toSolve.Add(new Cell(1, 5, 2, 0));
            toSolve.Add(new Cell(1, 6, 2, 7));
            toSolve.Add(new Cell(1, 7, 3, 0));
            toSolve.Add(new Cell(1, 8, 3, 9));
            toSolve.Add(new Cell(1, 9, 3, 0));

            toSolve.Add(new Cell(2, 1, 1, 0));
            toSolve.Add(new Cell(2, 2, 1, 3));
            toSolve.Add(new Cell(2, 3, 1, 0));
            toSolve.Add(new Cell(2, 4, 2, 0));
            toSolve.Add(new Cell(2, 5, 2, 2));
            toSolve.Add(new Cell(2, 6, 2, 0));
            toSolve.Add(new Cell(2, 7, 3, 0));
            toSolve.Add(new Cell(2, 8, 3, 0));
            toSolve.Add(new Cell(2, 9, 3, 8));

            toSolve.Add(new Cell(3, 1, 1, 0));
            toSolve.Add(new Cell(3, 2, 1, 0));
            toSolve.Add(new Cell(3, 3, 1, 9));
            toSolve.Add(new Cell(3, 4, 2, 6));
            toSolve.Add(new Cell(3, 5, 2, 0));
            toSolve.Add(new Cell(3, 6, 2, 0));
            toSolve.Add(new Cell(3, 7, 3, 5));
            toSolve.Add(new Cell(3, 8, 3, 0));
            toSolve.Add(new Cell(3, 9, 3, 0));

            toSolve.Add(new Cell(4, 1, 4, 0));
            toSolve.Add(new Cell(4, 2, 4, 0));
            toSolve.Add(new Cell(4, 3, 4, 5));
            toSolve.Add(new Cell(4, 4, 5, 3));
            toSolve.Add(new Cell(4, 5, 5, 0));
            toSolve.Add(new Cell(4, 6, 5, 0));
            toSolve.Add(new Cell(4, 7, 6, 9));
            toSolve.Add(new Cell(4, 8, 6, 0));
            toSolve.Add(new Cell(4, 9, 6, 0));

            toSolve.Add(new Cell(5, 1, 4, 0));
            toSolve.Add(new Cell(5, 2, 4, 1));
            toSolve.Add(new Cell(5, 3, 4, 0));
            toSolve.Add(new Cell(5, 4, 5, 0));
            toSolve.Add(new Cell(5, 5, 5, 8));
            toSolve.Add(new Cell(5, 6, 5, 0));
            toSolve.Add(new Cell(5, 7, 6, 0));
            toSolve.Add(new Cell(5, 8, 6, 0));
            toSolve.Add(new Cell(5, 9, 6, 2));

            toSolve.Add(new Cell(6, 1, 4, 6));
            toSolve.Add(new Cell(6, 2, 4, 0));
            toSolve.Add(new Cell(6, 3, 4, 0));
            toSolve.Add(new Cell(6, 4, 5, 0));
            toSolve.Add(new Cell(6, 5, 5, 0));
            toSolve.Add(new Cell(6, 6, 5, 4));
            toSolve.Add(new Cell(6, 7, 6, 0));
            toSolve.Add(new Cell(6, 8, 6, 0));
            toSolve.Add(new Cell(6, 9, 6, 0));

            toSolve.Add(new Cell(7, 1, 7, 3));
            toSolve.Add(new Cell(7, 2, 7, 0));
            toSolve.Add(new Cell(7, 3, 7, 0));
            toSolve.Add(new Cell(7, 4, 8, 0));
            toSolve.Add(new Cell(7, 5, 8, 0));
            toSolve.Add(new Cell(7, 6, 8, 0));
            toSolve.Add(new Cell(7, 7, 9, 0));
            toSolve.Add(new Cell(7, 8, 9, 1));
            toSolve.Add(new Cell(7, 9, 9, 0));

            toSolve.Add(new Cell(8, 1, 7, 0));
            toSolve.Add(new Cell(8, 2, 7, 4));
            toSolve.Add(new Cell(8, 3, 7, 0));
            toSolve.Add(new Cell(8, 4, 8, 0));
            toSolve.Add(new Cell(8, 5, 8, 0));
            toSolve.Add(new Cell(8, 6, 8, 0));
            toSolve.Add(new Cell(8, 7, 9, 0));
            toSolve.Add(new Cell(8, 8, 9, 0));
            toSolve.Add(new Cell(8, 9, 9, 7));

            toSolve.Add(new Cell(9, 1, 7, 0));
            toSolve.Add(new Cell(9, 2, 7, 0));
            toSolve.Add(new Cell(9, 3, 7, 7));
            toSolve.Add(new Cell(9, 4, 8, 0));
            toSolve.Add(new Cell(9, 5, 8, 0));
            toSolve.Add(new Cell(9, 6, 8, 0));
            toSolve.Add(new Cell(9, 7, 9, 3));
            toSolve.Add(new Cell(9, 8, 9, 0));
            toSolve.Add(new Cell(9, 9, 9, 0));

            return toSolve;
        }

        private ArrayList UseDefinedPuzzle()
        {
            ArrayList toSolve = new ArrayList();

            toSolve.Add(new Cell(1, 1, 1, 0));
            toSolve.Add(new Cell(1, 2, 1, 0));
            toSolve.Add(new Cell(1, 3, 1, 0));
            toSolve.Add(new Cell(1, 4, 2, 0));
            toSolve.Add(new Cell(1, 5, 2, 0));
            toSolve.Add(new Cell(1, 6, 2, 1));
            toSolve.Add(new Cell(1, 7, 3, 6));
            toSolve.Add(new Cell(1, 8, 3, 0));
            toSolve.Add(new Cell(1, 9, 3, 0));

            toSolve.Add(new Cell(2, 1, 1, 8));
            toSolve.Add(new Cell(2, 2, 1, 0));
            toSolve.Add(new Cell(2, 3, 1, 0));
            toSolve.Add(new Cell(2, 4, 2, 0));
            toSolve.Add(new Cell(2, 5, 2, 0));
            toSolve.Add(new Cell(2, 6, 2, 9));
            toSolve.Add(new Cell(2, 7, 3, 0));
            toSolve.Add(new Cell(2, 8, 3, 5));
            toSolve.Add(new Cell(2, 9, 3, 2));

            toSolve.Add(new Cell(3, 1, 1, 5));
            toSolve.Add(new Cell(3, 2, 1, 0));
            toSolve.Add(new Cell(3, 3, 1, 0));
            toSolve.Add(new Cell(3, 4, 2, 0));
            toSolve.Add(new Cell(3, 5, 2, 0));
            toSolve.Add(new Cell(3, 6, 2, 0));
            toSolve.Add(new Cell(3, 7, 3, 0));
            toSolve.Add(new Cell(3, 8, 3, 0));
            toSolve.Add(new Cell(3, 9, 3, 0));

            toSolve.Add(new Cell(4, 1, 4, 7));
            toSolve.Add(new Cell(4, 2, 4, 0));
            toSolve.Add(new Cell(4, 3, 4, 1));
            toSolve.Add(new Cell(4, 4, 5, 0));
            toSolve.Add(new Cell(4, 5, 5, 0));
            toSolve.Add(new Cell(4, 6, 5, 0));
            toSolve.Add(new Cell(4, 7, 6, 0));
            toSolve.Add(new Cell(4, 8, 6, 0));
            toSolve.Add(new Cell(4, 9, 6, 0));

            toSolve.Add(new Cell(5, 1, 4, 0));
            toSolve.Add(new Cell(5, 2, 4, 0));
            toSolve.Add(new Cell(5, 3, 4, 4));
            toSolve.Add(new Cell(5, 4, 5, 0));
            toSolve.Add(new Cell(5, 5, 5, 3));
            toSolve.Add(new Cell(5, 6, 5, 0));
            toSolve.Add(new Cell(5, 7, 6, 8));
            toSolve.Add(new Cell(5, 8, 6, 0));
            toSolve.Add(new Cell(5, 9, 6, 0));

            toSolve.Add(new Cell(6, 1, 4, 0));
            toSolve.Add(new Cell(6, 2, 4, 0));
            toSolve.Add(new Cell(6, 3, 4, 0));
            toSolve.Add(new Cell(6, 4, 5, 4));
            toSolve.Add(new Cell(6, 5, 5, 2));
            toSolve.Add(new Cell(6, 6, 5, 0));
            toSolve.Add(new Cell(6, 7, 6, 0));
            toSolve.Add(new Cell(6, 8, 6, 0));
            toSolve.Add(new Cell(6, 9, 6, 0));

            toSolve.Add(new Cell(7, 1, 7, 9));
            toSolve.Add(new Cell(7, 2, 7, 5));
            toSolve.Add(new Cell(7, 3, 7, 0));
            toSolve.Add(new Cell(7, 4, 8, 0));
            toSolve.Add(new Cell(7, 5, 8, 0));
            toSolve.Add(new Cell(7, 6, 8, 0));
            toSolve.Add(new Cell(7, 7, 9, 0));
            toSolve.Add(new Cell(7, 8, 9, 0));
            toSolve.Add(new Cell(7, 9, 9, 0));

            toSolve.Add(new Cell(8, 1, 7, 2));
            toSolve.Add(new Cell(8, 2, 7, 0));
            toSolve.Add(new Cell(8, 3, 7, 0));
            toSolve.Add(new Cell(8, 4, 8, 0));
            toSolve.Add(new Cell(8, 5, 8, 9));
            toSolve.Add(new Cell(8, 6, 8, 0));
            toSolve.Add(new Cell(8, 7, 9, 3));
            toSolve.Add(new Cell(8, 8, 9, 8));
            toSolve.Add(new Cell(8, 9, 9, 0));

            toSolve.Add(new Cell(9, 1, 7, 0));
            toSolve.Add(new Cell(9, 2, 7, 0));
            toSolve.Add(new Cell(9, 3, 7, 7));
            toSolve.Add(new Cell(9, 4, 8, 0));
            toSolve.Add(new Cell(9, 5, 8, 0));
            toSolve.Add(new Cell(9, 6, 8, 0));
            toSolve.Add(new Cell(9, 7, 9, 0));
            toSolve.Add(new Cell(9, 8, 9, 0));
            toSolve.Add(new Cell(9, 9, 9, 0));

            return toSolve;
        }

        private ArrayList CalculateNextPuzzle(Puzzle p)
        {
            ArrayList NextPuzzles = new ArrayList();

            if (!p.CellColl.ContradictionExists)
            {
                Cell leastCell = p.FindLeastPossibleCell();

                for (int i = 0; i < leastCell.PossibleValueCount; i++)
                {
                    NextPuzzles.Add(CreateNewPuzzle(p, leastCell, i));
                }
            }

            return NextPuzzles;
        }

        private Puzzle CreateNewPuzzle(Puzzle p, Cell leastCell, int index)
        {
            ArrayList newCellColl = new ArrayList();
            Cell newCell = new Cell(1, 1, 1, 1);

            foreach (Cell c in p.Cells)
            {
                int newCellValue = int.Parse(leastCell.PossibleValueString.Substring(index + 1, 1));

                if (c.Row == leastCell.Row && c.Column == leastCell.Column)
                {
                    newCell = new Cell(c.Row, c.Column, c.Square, newCellValue);
                }
                else
                {
                    newCell = new Cell(c.Row, c.Column, c.Square, c.Value);
                }

                newCellColl.Add(newCell);
            }

            return new Puzzle(newCellColl);
        }

        private ArrayList SolvePuzzles(ArrayList puzzleColl)
        {
            ArrayList solveColl = new ArrayList();

            foreach (Puzzle p in puzzleColl)
            {
                if (p.Solve())
                {
                    solveColl.Add(p);
                    return solveColl;
                }
            }

            if (puzzleColl.Count < 500)
            {
                System.Diagnostics.Debug.Print("Puzzle Count: " + puzzleColl.Count.ToString());

                foreach (Puzzle p in puzzleColl)
                {

                    ArrayList tempColl = CalculateNextPuzzle(p);
                    foreach (Puzzle np in tempColl)
                    {
                        solveColl.Add(np);
                    }
                }

                return SolvePuzzles(solveColl);
            }
            else
                return puzzleColl;
        }

        private void cmdSolve_Click(object sender, EventArgs e)
        {
            currentPuzzle = UseFormPuzzle();

            Puzzle thePuzzle = new Puzzle(currentPuzzle);
            ArrayList puzzColl = new ArrayList();

            puzzColl.Add(thePuzzle);

            ArrayList solveColl = SolvePuzzles(puzzColl);

            if (solveColl.Count > 1 || !((Puzzle)solveColl[0]).CellColl.IsSolved)
            {
                MessageBox.Show("No solution found.  Trying more solutions");

            }

            DisplaySolution(((Puzzle)solveColl[0]).Cells);
        }

        private int SafeValue(string intString)
        {
            if (intString.Length == 0) { return 0; }
            else
            {
                try
                {
                    int returnVal = int.Parse(intString);
                    return returnVal;
                }
                catch (Exception e) { return 0; }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList toSolve = UseDefinedPuzzle();

            Puzzle thePuzzle = new Puzzle(toSolve);

            if (!thePuzzle.Solve())
                MessageBox.Show("No solution found");

            if (thePuzzle.CellColl.ContradictionExists)
                MessageBox.Show("There is a contradiction");

            DisplaySolution(thePuzzle.Cells);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList toSolve = UseClearPuzzle();
            DisplaySolution(toSolve, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            storedPuzzle = StoreSolution();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ArrayList toSolve = new ArrayList();

            foreach (Cell c in storedPuzzle)
            {
                toSolve.Add(new Cell(c.Row, c.Column, c.Square, c.Value));
            }
            DisplaySolution(toSolve);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ArrayList toSolve = UseDefinedPuzzle();
            DisplaySolution(toSolve);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ArrayList toSolve = UseFormPuzzle();

            Puzzle thePuzzle = new Puzzle(toSolve);

            thePuzzle.Solve(true);

            if (thePuzzle.CellColl.ContradictionExists)
                MessageBox.Show("There is a contradiction");

            DisplaySolution(thePuzzle.Cells);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Puzzle thePuzzle = new Puzzle(currentPuzzle);

            thePuzzle.Solve(true);

            if (thePuzzle.CellColl.ContradictionExists)
                MessageBox.Show("There is a contradiction");

            DisplaySolution(thePuzzle.Cells);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ArrayList toSolve = HardestPuzzle();
            DisplaySolution(toSolve);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ArrayList toSolve = StringPuzzle();
            DisplaySolution(toSolve);
        }
    }
}
