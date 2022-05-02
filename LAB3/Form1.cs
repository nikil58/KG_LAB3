using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SharpGL;
using SharpGL.Enumerations;

namespace LAB3
{
    public partial class Form1 : Form
    {
        private ArrayPoints arrayPoints = new ArrayPoints(2);

        OpenGL controller;

        Form PointsEditor = new PointsEditor();
        public Form1()
        {
            InitializeComponent();
            arrayPoints.SetPoint(200, 0, 0);
            arrayPoints.SetPoint(150, 100, 0);
            arrayPoints.SetPoint(20, 100, 0);
            arrayPoints.SetPoint(100, 200, 0);
            arrayPoints.SetPoint(50, 300, 0);
            arrayPoints.SetPoint(350, 300, 0);
            arrayPoints.SetPoint(300, 200, 0);
            arrayPoints.SetPoint(380, 100, 0);
            arrayPoints.SetPoint(250, 100, 0);

            /* arrayPoints.SetPoint(50, 50, 0);
             arrayPoints.SetPoint(150, 10, 0);
             arrayPoints.SetPoint(250, 50, 0);
             arrayPoints.SetPoint(50, 150, 0);
             arrayPoints.SetPoint(150, 150, 0);
             arrayPoints.SetPoint(450, 150, 0);
             arrayPoints.SetPoint(10, 300, 0);
             arrayPoints.SetPoint(150, 250, 0);
             arrayPoints.SetPoint(250, 350, 0);*/
        }

        /// <summary>
        /// Класс для хранения точек в списке
        /// </summary>
        public class ArrayPoints
        {
            private int index = 0;
            private List<Point3D> points;

            public ArrayPoints(int size)
            {
                if (size <= 0) { size = 2; }
                points = new List<Point3D>(2);
            }

            public void SetPoint(float x, float y, float z)
            {

                points.Add(new Point3D(x, y, z));
                index++;
            }

            public void SetPoint(double[] coordinates)
            {
                points.Add(new Point3D(coordinates));
                index++;
            }

            public void SetPoint(Point3D point)
            {
                points.Add(point);
                index++;
            }

            public void ResetPoints()
            {
                index = 0;
                points.Clear();
            }

            public int GetCountPoints()
            {
                return index;
            }

            public List<Point3D> GetPoints()
            {
                return points;
            }

            public void DeletePoint(int index)
            {
                points.RemoveAt(index);
                this.index--;
            }
        }

        private void openGlController_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            ArrayPoints firstBasieArray = new ArrayPoints(2); //Массив для первой линии поверхности безье
            ArrayPoints secondBasieArray = new ArrayPoints(2); // Массив для второй линии поверхности безье
            controller = this.openGlController.OpenGL; //получение контроллера openGl
            controller.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            controller.Color(1.0, 0.0, 0.0); // red x
            controller.Begin(BeginMode.Lines);
            controller.Vertex(0, 0, 0);
            controller.Vertex(790, 0, 0);
            controller.End();

            controller.Color(1.0, 0.0, 0.0); // red arrow one x
            controller.Begin(BeginMode.Lines);
            controller.Vertex(790, 0, 0);
            controller.Vertex(750, -20, 0);
            controller.End();

            controller.Color(1.0, 0.0, 0.0); // red arrow two x
            controller.Begin(BeginMode.Lines);
            controller.Vertex(790, 0, 0);
            controller.Vertex(750, 20, 0);
            controller.End();

            controller.Color(0.0, 1.0, 0.0); // green y
            controller.Begin(BeginMode.Lines);
            controller.Vertex(0, 0, 0);
            controller.Vertex(0, 420, 0);
            controller.End();

            controller.Color(0.0, 1.0, 0.0); // green arrow one y
            controller.Begin(BeginMode.Lines);
            controller.Vertex(0, 420, 0);
            controller.Vertex(20, 400, 0);
            controller.End();

            controller.Color(0.0, 1.0, 0.0); // green arrow two y
            controller.Begin(BeginMode.Lines);
            controller.Vertex(0, 420, 0);
            controller.Vertex(-20, 400, 0);
            controller.End();


            controller.Color(1.0f, 1.0f, 1.0f);
            controller.PointSize(5);

            //рисуем наши точки
            controller.Begin(BeginMode.Points);
            for (int i = 0; i < arrayPoints.GetCountPoints(); i++)
            {
                controller.Vertex(arrayPoints.GetPoints().ToArray()[i].GetX(), arrayPoints.GetPoints().ToArray()[i].GetY(), arrayPoints.GetPoints().ToArray()[i].GetZ());
            }
            controller.End();

            //получаем число квадрата (матрица должна быть 2 на 2, 3 на 3, 4 на 4 и т.п.)
            double count = Math.Round(Math.Sqrt(arrayPoints.GetCountPoints()));
            //формирование задающей плоскости и плоскости безье
            if (count != 0 && count != 1 && count * count == arrayPoints.GetCountPoints()) //проверяем, что наше число является полным квадратом
            {
                Point3D[,] matrixPoints = ConvertArray(arrayPoints, count); //конвертируем наш List в матрицу размерности count на count
                for (int i = 0; i < count; i++)
                {
                    controller.LineStipple(1, 0x000F);
                    controller.Enable(OpenGL.GL_LINE_STIPPLE);
                    controller.Begin(BeginMode.LineStrip);
                    //рисуем по строчно, соединяя каждые count элементов
                    for (int j = 0; j < count; j++)
                    {
                        Point3D tempPoint = matrixPoints[i, j];
                        controller.Vertex(tempPoint.GetX(), tempPoint.GetY(), tempPoint.GetZ());

                    }
                    controller.End();


                    controller.LineStipple(1, 0x00FF);
                    controller.Begin(BeginMode.Lines);
                    //Если строка не последняя, но нужно соеденить элементы этой строки с элементами следующей для создания каркаса
                    if (i != count - 1)
                    {
                        for (int j = 0; j < count; j++)
                        {
                            Point3D tempPoint = matrixPoints[i, j];
                            controller.Vertex(tempPoint.GetX(), tempPoint.GetY(), tempPoint.GetZ());

                            tempPoint = matrixPoints[i + 1, j];
                            controller.Vertex(tempPoint.GetX(), tempPoint.GetY(), tempPoint.GetZ());
                        }
                    }
                    controller.End();
                }
                controller.Color(0.0f, 1.0f, 0.0f);
                controller.Disable(OpenGL.GL_LINE_STIPPLE);
                
                int lineCount = 0;

                //начинаем формировать поверхность безье
                for (float u = 0.0f; u < 1.03f; u += 0.05f)
                {
                    controller.Begin(BeginMode.LineStrip);
                    for (float v = 0.0f; v < 1.03f; v += 0.05f)
                    {
                        if (lineCount == 0)
                        {
                            firstBasieArray.SetPoint(BeziePoint(u, v, count)); //отрисовываем первую линию
                            controller.Vertex(firstBasieArray.GetPoints().Last().X, firstBasieArray.GetPoints().Last().Y, firstBasieArray.GetPoints().Last().Z);
                        }
                        else
                        {
                            secondBasieArray.SetPoint(BeziePoint(u, v, count)); //отрисовываем вторую линию
                            controller.Vertex(secondBasieArray.GetPoints().Last().X, secondBasieArray.GetPoints().Last().Y, secondBasieArray.GetPoints().Last().Z);
                        }
                    }
                    controller.End();
                    lineCount++;
                    if (lineCount == 2) //если обе линии уже отрисовали, то будем соединять попарно точки, а затем сделаем вторую линию первой, а саму вторую очистим.
                    {
                        lineCount = 1;
                        controller.Begin(BeginMode.Lines);
                        for (int i = 0; i < secondBasieArray.GetCountPoints(); i++)
                        {
                            controller.Vertex(firstBasieArray.GetPoints().ToArray()[i].X, firstBasieArray.GetPoints().ToArray()[i].Y, firstBasieArray.GetPoints().ToArray()[i].Z);
                            controller.Vertex(secondBasieArray.GetPoints().ToArray()[i].X, secondBasieArray.GetPoints().ToArray()[i].Y, secondBasieArray.GetPoints().ToArray()[i].Z);
                        }
                        controller.End();
                        firstBasieArray = CopyArrayPoints(firstBasieArray, secondBasieArray);
                        secondBasieArray.ResetPoints();

                    }
                }
            }
            controller.Flush();
        }

        /// <summary>
        /// Копирует один ArrayPoints в другой ArrayPoints
        /// </summary>
        /// <param name="firstArray">Первый массив</param>
        /// <param name="secondArray">Второй массив</param>
        /// <returns></returns>
        private ArrayPoints CopyArrayPoints(ArrayPoints firstArray, ArrayPoints secondArray)
        {
            firstArray.ResetPoints();
            for (int i = 0; i < secondArray.GetCountPoints(); i++)
            {
                firstArray.SetPoint(secondArray.GetPoints().ToArray()[i]);
            }
            return firstArray;
        }

        /// <summary>
        /// Сбрасывает все нарисованное на экране
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Клик по кнопке</param>
        private void resetButton_Click(object sender, EventArgs e)
        {
            arrayPoints.ResetPoints();
            openGlController.DoRender();
        }

        /// <summary>
        /// Отрисовываем вторую форму для добавления координат
        /// </summary>
        private void DrawPointEditor()
        {
            if (arrayPoints.GetCountPoints() != 0) //Если есть хотя бы одна точка, то форма отобразится, иначе - отобразится только кнопка добавления
            {
                for (int i = 0; i < arrayPoints.GetCountPoints(); i++) //Отрисовываем столько элементов, сколько у нас точек
                {
                    //Лейбл для координаты X
                    Label labelX = new Label();
                    labelX.Text = $"X{i}";
                    labelX.Name = "labelX" + i.ToString();
                    labelX.Font = new Font(labelX.Font.Name, 14,
                    labelX.Font.Style, labelX.Font.Unit);
                    labelX.Width = 60;
                    labelX.Location = new Point(10, 10 + 40 * i);

                    //Текстбокс для координаты X
                    TextBox textBoxX = new TextBox();
                    textBoxX.Name = "textBoxX" + i.ToString();
                    textBoxX.Font = new Font(textBoxX.Font.Name, 14,
                   textBoxX.Font.Style, textBoxX.Font.Unit);
                    textBoxX.Location = new Point(70, 10 + 40 * i);
                    textBoxX.Text = arrayPoints.GetPoints().ToArray()[i].X.ToString();

                    //Лейбл для координаты Y
                    Label labelY = new Label();
                    labelY.Text = $"Y{i}";
                    labelY.Name = "labelY" + i.ToString();
                    labelY.Font = new Font(labelY.Font.Name, 14,
                    labelY.Font.Style, labelY.Font.Unit);
                    labelY.Width = 60;
                    labelY.Location = new Point(320, 10 + 40 * i);

                    //Текстбокс для координаты Y
                    TextBox textBoxY = new TextBox();
                    textBoxY.Name = "textBoxY" + i.ToString();
                    textBoxY.Font = new Font(textBoxY.Font.Name, 14,
                   textBoxY.Font.Style, textBoxY.Font.Unit);
                    textBoxY.Location = new Point(380, 10 + 40 * i);
                    textBoxY.Text = arrayPoints.GetPoints().ToArray()[i].Y.ToString();

                    //Лейбл для координаты Z
                    Label labelZ = new Label();
                    labelZ.Text = $"Z{i}";
                    labelZ.Name = "labelZ" + i.ToString();
                    labelZ.Font = new Font(labelZ.Font.Name, 14,
                    labelZ.Font.Style, labelZ.Font.Unit);
                    labelZ.Width = 60;
                    labelZ.Location = new Point(600, 10 + 40 * i);

                    //Текстбокс для координаты Z
                    TextBox textBoxZ = new TextBox();
                    textBoxZ.Name = "textBoxZ" + i.ToString();
                    textBoxZ.Font = new Font(textBoxZ.Font.Name, 14,
                    textBoxZ.Font.Style, textBoxZ.Font.Unit);
                    textBoxZ.Location = new Point(660, 10 + 40 * i);
                    textBoxZ.Text = arrayPoints.GetPoints().ToArray()[i].Z.ToString();

                    //Кнопка удаления координаты
                    Button deleteButton = new Button();
                    deleteButton.Name = "deleteButton" + i.ToString();
                    deleteButton.BackgroundImageLayout = ImageLayout.Center;
                    deleteButton.TabIndex = 1;
                    deleteButton.TabStop = true;
                    deleteButton.BackgroundImage = Properties.Resources.delete;
                    deleteButton.Size = deleteButton.BackgroundImage.Size + new Size(8, 8);
                    deleteButton.Location = new Point(760, 5 + 40 * i);
                    deleteButton.Click += deletePointButton_Click;

                    //Добавляем все эти элементы в форму
                    PointsEditor.Controls.Add(labelX);
                    PointsEditor.Controls.Add(textBoxX);
                    PointsEditor.Controls.Add(labelY);
                    PointsEditor.Controls.Add(textBoxY);
                    PointsEditor.Controls.Add(labelZ);
                    PointsEditor.Controls.Add(textBoxZ);
                    PointsEditor.Controls.Add(deleteButton);

                }
                //Отрисовываем кнопку "Сохранить"
                Button saveButton = new Button();
                saveButton.Name = "saveButton";
                saveButton.Text = "Сохранить";
                saveButton.Font = new Font(saveButton.Font.Name, 14,
                   saveButton.Font.Style, saveButton.Font.Unit);
                saveButton.Width = 130;
                saveButton.Height = 35;
                saveButton.Location = new Point(10, 70 + arrayPoints.GetCountPoints() * 40);
                saveButton.Click += SaveButton_Click;
                //Добавление кнопки к форме и действия по закрытию формы
                PointsEditor.FormClosed += CloseBottun_Click;
                PointsEditor.Controls.Add(saveButton);

            }
            Button addButton = new Button();
            addButton.Name = "addButton";
            addButton.BackgroundImageLayout = ImageLayout.Center;
            addButton.TabIndex = 1;
            addButton.TabStop = true;
            addButton.BackgroundImage = Properties.Resources.add;
            addButton.Size = addButton.BackgroundImage.Size + new Size(8, 8);
            addButton.Location = new Point(10, 30 + arrayPoints.GetCountPoints() * 40);
            addButton.Click += addPointButton_Click;
            PointsEditor.Controls.Add(addButton);
        }

        /// <summary>
        /// Удаляет выбранную координату
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Клик</param>
        private void deletePointButton_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button; //получаем кнопку
            string name = clicked.Name; //получаем ее имя
            int value;
            int.TryParse(string.Join("", name.Where(c => char.IsDigit(c))), out value); //находим число в названии (Он будет индексом)

            //удаляем
            arrayPoints.DeletePoint(value);
            PointsEditor.Controls.Remove(clicked);
            PointsEditor.Controls.Clear();

            //перерисовываем окна
            DrawPointEditor();
            openGlController.DoRender();

        }

        /// <summary>
        /// Добавляет 3 лейбла и текстбокса, чтобы создать новую координату
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Клик</param>
        private void addPointButton_Click(object sender, EventArgs e)
        {
            SaveButton_Click(sender, e);
            arrayPoints.SetPoint(0, 0, 0);
            PointsEditor.Controls.Clear();
            DrawPointEditor();

        }

        /// <summary>
        /// Закрытие окна редактирования
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Клик</param>
        private void CloseBottun_Click(object sender, EventArgs e)
        {
            PointsEditor.Controls.Clear(); //очищаем форму редактирования, чтобы потом при открытии не было дубликатов
        }

        /// <summary>
        /// Кнопка сохранения в панеле редактирования координат
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Клик</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            //Введенные координаты в текстовое поле
            float? X = null;
            float? Y = null;
            float? Z = null;
            //Очищаем наши массивы точек
            arrayPoints.ResetPoints();

            var allTextBoxesOnMyFormAsList = PointsEditor.Controls.OfType<TextBox>().ToList(); // находим все текстбоксы
            foreach (var textbox in allTextBoxesOnMyFormAsList)
            {
                if (X == null) X = float.Parse(textbox.Text); //Получаем координату X
                else if (Y == null) Y = float.Parse(textbox.Text); //Получаем координату Y
                else Z = float.Parse(textbox.Text);
                if (Z != null)
                {
                    arrayPoints.SetPoint((float)X, (float)Y, (float)Z); //Вставляем координаты в массив
                    X = Y = Z = null;
                }
            }
            PointsEditor.Controls.Clear(); //очищаем форму*/
            DrawPointEditor();

        }

        /// <summary>
        /// Кнопка для вызова второй формы
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Клик</param>
        private void pointsButton_Click(object sender, EventArgs e)
        {
            DrawPointEditor();
            PointsEditor.ShowDialog();
        }

        /// <summary>
        /// Настраивает камеру так, чтобы было удобно смотреть на задающий многоугольник
        /// </summary>
        /// <param name="sender">Форма</param>
        /// <param name="e">Событие</param>
        private void openGlController_Resized(object sender, EventArgs e)
        {
            var gl = this.openGlController.OpenGL;
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(-801 / 2, 801, 435, -435 / 2, -450.0f, 450.0f);
            gl.MatrixMode(MatrixMode.Modelview);
            gl.LookAt(3.0, 3.0, 3.0 - 4.5, 0.0, 0.0, -4.5, 0, 1, 0);
            gl.LoadIdentity();

        }

        /// <summary>
        /// Кнопка для поворота поверхности безье в пространстве относительно выбранной оси
        /// </summary>
        /// <param name="sender">Кнопка</param>
        /// <param name="e">Клик</param>
        private void rotateButton_Click(object sender, EventArgs e)
        {
            controller = this.openGlController.OpenGL;

            controller.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            controller.LoadIdentity();

            double rad = float.Parse(this.angleTextBox.Text) * Math.PI / 180;
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            float[][] transform = null;

            if (int.Parse(this.axisComboBox.SelectedIndex.ToString()) == 0)
            {
                transform = new float[][]
                    {
                    new float[]{ 1, 0, 0, 1 },
                    new float[]{ 0, cos, sin, 0 },
                    new float[]{ 0, -sin, cos, 0 },
                    new float[]{ 0, 0, 0, 1 }
                    };
            }
            if (int.Parse(this.axisComboBox.SelectedIndex.ToString()) == 1)
            {
                transform = new float[][]
                {
                    new float[]{ cos, 0, -sin, 0 },
                    new float[]{ 0, 1, 0, 0 },
                    new float[]{ sin, 0, cos, 0 },
                    new float[]{ 0, 0, 0, 1 }
                };
            }
            if (int.Parse(this.axisComboBox.SelectedIndex.ToString()) == 2)
            {
                transform = new float[][]
                {
                    new float[]{ cos, sin, 0, 0 },
                    new float[]{ -sin, cos, 0, 0 },
                    new float[]{ 0, 0, 1, 0 },
                    new float[]{ 0, 0, 0, 1 } };
            }

            arrayPoints = MatrixMultiply(arrayPoints.GetPoints().ToArray(), transform);
        }

        /// <summary>
        /// Конвертирует матрицу в ArrayPoints
        /// </summary>
        /// <param name="matrix">Конвертируемая матрица</param>
        /// <returns>Возвращается ArrayPoints</returns>
        private ArrayPoints ConvertMatrix(float[] matrix)
        {
            float currentX;
            float currentY;
            float currentZ;
            ArrayPoints result = new ArrayPoints(matrix.GetLength(0) * matrix.GetLength(0));
            for (int i = 0; i < matrix.Length; i += 3)
            {
                currentX = matrix[i];
                currentY = matrix[i + 1];
                currentZ = matrix[i + 2];
                result.SetPoint(currentX, currentY, currentZ);
            }
            return result;
        }

        /// <summary>
        /// Получает либо X, либо Y, либо Z в зависимости от параметра
        /// </summary>
        /// <param name="count"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private float GetXYZ(int count, Point3D point)
        {
            if (count == 0) return point.GetX();
            if (count == 1) return point.GetY();
            if (count == 2) return point.GetZ();

            else return 0;
        }

        /// <summary>
        /// Метод перемножения матриц
        /// </summary>
        /// <param name="coordLine"></param>
        /// <param name="transformMatrix"></param>
        /// <returns></returns>
        public ArrayPoints MatrixMultiply(Point3D[] coordLine, float[][] transformMatrix)
        {
            ArrayPoints result = new ArrayPoints(2);
            float[] input1, input2;
            float[] temp = new float[coordLine.GetLength(0) * 3];
            float tempCalc = 0;


            //convert input to line array
            input1 = new float[coordLine.GetLength(0) * 3];
            for (int i = 0; i < coordLine.GetLength(0); ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    input1[i * 3 + j] = GetXYZ(j, coordLine[i]);
                }
            }


            input2 = new float[9];
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    input2[i * 3 + j] = transformMatrix[i][j];
                }
            }


            //matrix multiplication
            for (int i = 0; i < coordLine.GetLength(0); ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        float temp1 = input1[i * 3 + k];
                        float temp2 = input2[k * 3 + j];
                        float temp3 = input1[i * 3 + k] * input2[k * 3 + j];
                        tempCalc += input1[i * 3 + k] * input2[k * 3 + j];
                    }
                    temp[i * 3 + j] = tempCalc;
                    tempCalc = 0.0f;

                }
            }
            result = ConvertMatrix(temp);

            return result;
        }

        /// <summary>
        /// Конвертирует ArrayPoints в матрицу
        /// </summary>
        /// <param name="arrayPoints">Конвертирующийся ArrayPoints</param>
        /// <param name="count">Порядок матрицы</param>
        /// <returns></returns>
        private Point3D[,] ConvertArray(ArrayPoints arrayPoints, double count)
        {
            int counter = 0;
            Point3D[,] result = new Point3D[Convert.ToInt32(count), Convert.ToInt32(count)];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    result[i, j] = arrayPoints.GetPoints().ToArray()[counter++];
                }
            }
            return result;
        }

        /// <summary>
        /// Высчитывает очередную точку поверхности безье
        /// </summary>
        /// <param name="u">Вектор u</param>
        /// <param name="v">Вектор v</param>
        /// <param name="count">Порядок матрицы</param>
        /// <returns></returns>
        private Point3D BeziePoint(float u, float v, double count)
        {
            Point3D patch = new Point3D(0, 0, 0);
            float currentX = 0;
            float currentY = 0;
            float currentZ = 0;
            Point3D[,] points = ConvertArray(arrayPoints, count);
            for (int i = 0; i < Convert.ToInt32(count); i++)
            {
                for (int j = 0; j < Convert.ToInt32(count); j++)
                {
                    currentX += points[i, j].X * (float)(bk(Convert.ToInt32(count) - 1, i) * Math.Pow(u, i) * Math.Pow((1 - u), Convert.ToInt32(count) - i - 1)) * (float)(bk(Convert.ToInt32(count) - 1, j) * Math.Pow(v, j) * Math.Pow((1 - v), Convert.ToInt32(count) - j - 1)); //Считаем координату X поверхности безье
                    currentY += points[i, j].Y * (float)(bk(Convert.ToInt32(count) - 1, i) * Math.Pow(u, i) * Math.Pow((1 - u), Convert.ToInt32(count) - i - 1)) * (float)(bk(Convert.ToInt32(count) - 1, j) * Math.Pow(v, j) * Math.Pow((1 - v), Convert.ToInt32(count) - j - 1)); //Считаем координату Y поверхности безье
                    currentZ += points[i, j].Z * (float)(bk(Convert.ToInt32(count) - 1, i) * Math.Pow(u, i) * Math.Pow((1 - u), Convert.ToInt32(count) - i - 1)) * (float)(bk(Convert.ToInt32(count) - 1, j) * Math.Pow(v, j) * Math.Pow((1 - v), Convert.ToInt32(count) - j - 1)); //Считаем координату Z поверхности безье
                }
                patch = new Point3D(currentX, currentY, currentZ);
            }
            return patch;
        }

        /// <summary>
        /// Высчитывает факториал числа
        /// </summary>
        /// <param name="n">Число, факториал которого будет считать</param>
        /// <returns>возвращает факториал</returns>
        static float factorial(float n)
        {
            float factorial = 1;
            for (int i = 1; i <= n; i++)
                factorial *= i;

            return factorial;
        }

        /// <summary>
        /// Высчитывает число по формуле перестановок
        /// </summary>
        /// <param name="n">C из N</param>
        /// <param name="k">С по K</param>
        /// <returns>Возвращает полученное число перестановок</returns>
        static double bk(int n, int k)
        {
            double result = factorial(n) / (factorial(k) * factorial(n - k));
            return result;
        }
    }
}
