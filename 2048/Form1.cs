using System;
using System.Collections.Generic;

namespace _2048
{
    public partial class Form1 : Form
    {
        private bool[,] _slots = { { false, false, false, false }, { false, false, false, false }, { false, false, false, false }, { false, false, false, false } };
        private int _fieldSize = 4;
        private int _size = 150;
        private List<int> _coords = new List<int> { 0, 150, 300, 450 };
        private Random _random = new Random();
        private Dictionary<string, Color> Colors = new Dictionary<string, Color>
            {
                {"2", Color.FromArgb(255, 238, 228, 218)},
                {"4", Color.FromArgb(255, 237, 224, 200)},
                {"8", Color.FromArgb(255, 242, 177, 121)},
                {"16", Color.FromArgb(255, 245, 149, 99)},
                {"32", Color.FromArgb(255, 246, 124, 95)},
                {"64", Color.FromArgb(255, 246, 94, 59)},
                {"128", Color.FromArgb(255, 237, 207, 114)},
                {"256", Color.FromArgb(255, 237, 204, 97)},
                {"512", Color.FromArgb(255, 237, 200, 80)},
                {"1024", Color.FromArgb(255, 237, 197, 63)},
                {"2048", Color.FromArgb(255, 237, 194, 46)},
            };
        public Form1()
        {
            InitializeComponent();
        }
        private void Game(object sender, EventArgs e)
        {
            CreateButton();
            CreateButton();
        }

        private Button CreateButton()
        {
            int x = _random.Next(_fieldSize);
            int y = _random.Next(_fieldSize);
            Button button = new Button();
            button.Size = new Size(_size, _size);
            button.Text = "2";
            if (_slots[x, y])
            {
                while (_slots[x, y] && this.Controls.Count <= _fieldSize * _fieldSize)
                {
                    x = _random.Next(_fieldSize);
                    y = _random.Next(_fieldSize);
                }
            }
            button.Location = new Point(_coords[x], _coords[y]);
            button.BackColor = Colors[button.Text];
            _slots[x, y] = true;
            this.Controls.Add(button);
            return button;
        }

        private void MoveUp(List<Button> buttons)
        {
            for (int i = 0; i < _fieldSize; i++)
            {
                int tempX = i;
                int tempY = 0;
                bool f = false;
                for (int j = 0; j < _fieldSize; j++)
                {
                    int index = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j]));
                    if ((index == -1 && !f) || (index != -1 && !f))
                    {
                        tempX = i;
                        tempY = j;
                        f = true;
                    }
                    if (index != -1)
                    {
                        _slots[i, j] = false;
                        buttons[index].Location = new Point(_coords[tempX], _coords[tempY]);
                        _slots[tempX, tempY] = true;
                        j = tempY;
                        f = false;
                    }
                }
            }
        }
        private void MoveLeft(List<Button> buttons)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                int tempX = 0;
                int tempY = j;
                bool f = false;
                for (int i = 0; i < _fieldSize; i++)
                {
                    int index = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j]));
                    if ((index == -1 && !f) || (index != -1 && !f))
                    {
                        tempX = i;
                        tempY = j;
                        f = true;
                    }
                    if (index != -1)
                    {
                        _slots[i, j] = false;
                        buttons[index].Location = new Point(_coords[tempX], _coords[tempY]);
                        _slots[tempX, tempY] = true;
                        j = tempY;
                        f = false;
                    }
                }
            }
        }

        private void MoveDown(List<Button> buttons)
        {
            for (int i = 0; i < _fieldSize; i++)
            {
                int tempX = i;
                int tempY = 3;
                bool f = false;
                for (int j = _fieldSize-1; j >= 0 ; j--)
                {
                    int index = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j]));
                    if ((index == -1 && !f) || (index != -1 && !f))
                    {
                        tempX = i;
                        tempY = j;
                        f = true;
                    }
                    if (index != -1)
                    {
                        _slots[i, j] = false;
                        buttons[index].Location = new Point(_coords[tempX], _coords[tempY]);
                        _slots[tempX, tempY] = true;
                        j = tempY;
                        f = false;
                    }
                }
            }
        }

        private void MoveRight(List<Button> buttons)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                int tempX = 3;
                int tempY = j;
                bool f = false;
                for (int i = _fieldSize - 1; i >= 0; i--)
                {
                    int index = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j]));
                    if ((index == -1 && !f) || (index != -1 && !f))
                    {
                        tempX = i;
                        tempY = j;
                        f = true;
                    }
                    if (index != -1)
                    {
                        _slots[i, j] = false;
                        buttons[index].Location = new Point(_coords[tempX], _coords[tempY]);
                        _slots[tempX, tempY] = true;
                        j = tempY;
                        f = false;
                    }
                }
            }
        }

        private void MoveOnKey(object sender, KeyEventArgs e)
        {
            List<Button> buttons = Controls.Cast<Button>().ToList();
            if (e.KeyCode.Equals(Keys.W))
            {
                MoveUp(buttons);
                for (int i = 0; i < _fieldSize; i++)
                {
                    for (int j = 0; j < _fieldSize - 1; j++)
                    {
                        int index = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j]));
                        int indexTwo = -1;
                        if (index != -1)
                            indexTwo = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j + 1]) && x.Text.Equals(buttons[index].Text));
                        if (indexTwo != -1)
                        {
                            buttons[index].Text = (int.Parse(buttons[index].Text) * 2).ToString();
                            buttons[index].BackColor = Colors[buttons[index].Text];
                            _slots[i, j + 1] = false;
                            buttons.Remove(buttons[indexTwo]);
                            this.Controls.RemoveAt(indexTwo);
                        }
                    }
                }
                MoveUp(buttons);
            }


            if (e.KeyCode.Equals(Keys.S))
            {
                MoveDown(buttons);
                for (int i = 0; i < _fieldSize; i++)
                {
                    for (int j = _fieldSize - 1; j > 0; j--)
                    {
                        int index = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j]));
                        int indexTwo = -1;
                        if (index != -1)
                            indexTwo = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j - 1]) && x.Text.Equals(buttons[index].Text));
                        if (indexTwo != -1)
                        {
                            buttons[index].Text = (int.Parse(buttons[index].Text) * 2).ToString();
                            buttons[index].BackColor = Colors[buttons[index].Text];
                            _slots[i, j - 1] = false;
                            buttons.Remove(buttons[indexTwo]);
                            this.Controls.RemoveAt(indexTwo);
                        }
                    }
                }
                MoveDown(buttons);
            }

            if (e.KeyCode.Equals(Keys.A))
            {
                MoveLeft(buttons);
                for (int j = 0; j < _fieldSize; j++)
                {
                    for (int i = 0; i < _fieldSize - 1; i++)
                    {
                        int index = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j]));
                        int indexTwo = -1;
                        if (index != -1)
                            indexTwo = buttons.FindIndex(x => x.Location.X.Equals(_coords[i + 1]) && x.Location.Y.Equals(_coords[j]) && x.Text.Equals(buttons[index].Text));
                        if (indexTwo != -1)
                        {
                            buttons[index].Text = (int.Parse(buttons[index].Text) * 2).ToString();
                            buttons[index].BackColor = Colors[buttons[index].Text];
                            _slots[i + 1, j] = false;
                            buttons.Remove(buttons[indexTwo]);
                            this.Controls.RemoveAt(indexTwo);
                        }
                    }
                }
                MoveLeft(buttons);
            }

            if (e.KeyCode.Equals(Keys.D))
            {
                MoveRight(buttons);
                for (int j = 0; j < _fieldSize; j++)
                {
                    for (int i = _fieldSize - 1; i > 0; i--)
                    {
                        int index = buttons.FindIndex(x => x.Location.X.Equals(_coords[i]) && x.Location.Y.Equals(_coords[j]));
                        int indexTwo = -1;
                        if (index != -1)
                            indexTwo = buttons.FindIndex(x => x.Location.X.Equals(_coords[i - 1]) && x.Location.Y.Equals(_coords[j]) && x.Text.Equals(buttons[index].Text));
                        if (indexTwo != -1)
                        {
                            buttons[index].Text = (int.Parse(buttons[index].Text) * 2).ToString();
                            buttons[index].BackColor = Colors[buttons[index].Text];
                            _slots[i - 1, j] = false;
                            buttons.Remove(buttons[indexTwo]);
                            this.Controls.RemoveAt(indexTwo);
                        }
                    }
                }
                MoveRight(buttons);
            }
            CreateButton();
        }
    }
}