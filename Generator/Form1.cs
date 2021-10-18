using Bogus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator
{
    public partial class Form1 : Form
    {
        readonly string[] violationGroup; //Группа нарушений

        readonly string[] violationType1; //Первый тип нарушения
        readonly string[] violationType2;
        readonly string[] violationType3;
        readonly string[] violationType4;

        readonly string[] cuptureOffernder; //Последствия (что придумал)

        public Form1()
        {
            InitializeComponent();
            cmbSel.SelectedIndex = 1;
            cmbSel.DropDownStyle = ComboBoxStyle.DropDownList;
            openFileDialog1.Filter = "mdf files(*mdf)|*mdf|All files(*.*)|*.*"; //Тип файла
            BindTrackBars();

            //Заполнение групп
            violationGroup = new string[]
            {
                "Пересечение",
                "Превышение скорости",
                "Вождение",
                "Обгон"
            };

            violationType1 = new string[]
            {
                "Двойная сплошная",
                "Парковочная линия",
                "Линия остановки",
            };

            violationType2 = new string[]
            {
                "В черте города",
                "За чертой города",
                "от 20 до 40 км/ч",
                "от 40 до 60 км/ч",
                "60 до 80 км/ч"
            };

            violationType3 = new string[]
            {
                "Без прав",
                "В состоянии алкогольного опьянения",
                "Неисправное странспортное средство"
            };

            violationType4 = new string[]
            {
                "В неположенном месте",
                "По встречной полосе",
                "В зоне действия запрещающего знака"
            };

            cuptureOffernder = new string[]
            {
                "Удалось задержать",
                "Удалось не задержать",
                "Оказал сопротивление",
                "Не оказал сопротивление"

            };

        }


        CancellationTokenSource driverGeneratorCanceletionToken = null;
        CancellationTokenSource carGeneratorCanceletionToken = null;
        CancellationTokenSource violGeneratorCanceletionToken = null;
        string filename;

        //struct Range
        //{
        //    public double min;
        //    public double max;

        //    public bool CheckContains(double value)
        //    {
        //        return min <= value && value <= max;
        //    }
        //}

        private void btnQuery_Click(object sender, EventArgs e) //Выдача запросов
        {
            if (txtConnectionString.Text == "" || txtQuery.Text == "")
            {
                MessageBox.Show("Не все поля заполнены", "Warning!");
            }
            else
            {

                SqlConnection connection = new();

                connection = GetConnection();


                connection.Open();

                SqlCommand command = new();
                command.Connection = connection;
                command.CommandText = txtQuery.Text;

                var reader = command.ExecuteReader();

                DataTable table = new();
                table.Load(reader);
                dgName.DataSource = table;

                reader.Close();

                connection.Close();
            }
        }

        SqlConnection GetConnection() //Коннектор
        {
            SqlConnection connection = new();
            switch (cmbSel.SelectedIndex)
            {
                case 0: //К локалке
                    {
                        SqlConnectionStringBuilder sqlConnectionStringBuilder = new();
                        sqlConnectionStringBuilder.DataSource = @"(LocalDB)\MSSQLLocalDB";
                        sqlConnectionStringBuilder.AttachDBFilename = openFileDialog1.FileName;
                        sqlConnectionStringBuilder.IntegratedSecurity = true;
                        sqlConnectionStringBuilder.ConnectTimeout = 30;
                        connection.ConnectionString = sqlConnectionStringBuilder.ToString();
                    }
                    break;
                case 1: //К сетевой
                    {
                        connection.ConnectionString = txtConnectionString.Text;

                    }
                    break;
            }
            return connection;
        }

        private void cmbSel_SelectedIndexChanged(object sender, EventArgs e) //Работа кнопки выбора локальных БД
        {
            switch (cmbSel.SelectedIndex)
            {
                case 0:
                    {
                        btnSelect.Enabled = true;
                    }
                    break;
                case 1:
                    {
                        btnSelect.Enabled = false;
                    }
                    break;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e) //Выбор локальной БД
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            filename = openFileDialog1.FileName;
            txtConnectionString.Text = filename;
        }

        private async void btnGenerate_ClickAsync(object sender, EventArgs e) //Генератор Водителей
        {
            if (driverGeneratorCanceletionToken != null)
            {
                // отправляем запрос на отмену генерации
                driverGeneratorCanceletionToken.Cancel();
                // выходим из функции
                return;
            }
            driverGeneratorCanceletionToken = new CancellationTokenSource();
            var connection = GetConnection();
            btnGenerate.Text = "Отмена генерации";
            pbDrGenerator.Value = 0; // сбрасываем значение
            pbDrGenerator.Maximum = (int)upCount.Value; // ставим максимальное
            double genderValue = (double)tbGender.Value;
            int count = (int)upCount.Value;

            var progress = new Progress<int>(value =>
            {
                // меняем значение прогрессбара
                pbDrGenerator.Value = value;
            });

            await Task.Run(() => 
            {
                generateDrivers(connection, genderValue, count, progress, driverGeneratorCanceletionToken);
            });
            btnGenerate.Text = "Сгенерировать";
            driverGeneratorCanceletionToken.Dispose();
            driverGeneratorCanceletionToken = null;
        }

        private void generateDrivers(SqlConnection connection, 
                                     double genderValue, 
                                     int count, 
                                     IProgress<int> progress, 
                                     CancellationTokenSource cancellationToken)
        {

            using (connection)
            {
                connection.Open();

                // создаем генератор случайных чисел
                Random rnd = new();

                for (var i = 0; i < count; ++i)
                {

                    var isMale = (genderValue / 100) < rnd.NextDouble();
                    var gender = isMale ? "муж" : "жен";

                    string fName = "";
                    string region = "";
                    //тут генерим все рандомные значения
                    Faker faker = new("ru");
                    if (gender == "муж")
                    {
                        fName = faker.Name.FullName(Bogus.DataSets.Name.Gender.Male);
                    }
                    else
                    {
                        fName = faker.Name.FullName(Bogus.DataSets.Name.Gender.Female);
                    }
                    region = faker.Address.FullAddress();
                    try
                    {
                        SqlCommand command = new(@$"insert Driver 
                        values ('{fName}', '{region}', {rnd.Next(100000, 999999999)})"
                        , connection);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return; // то выходим их функции
                    }

                    progress?.Report(i + 1);
                    Thread.Sleep(300);
                }
                connection.Close();
                MessageBox.Show("Все готово, мой лорд");
            }
        }

        private async void btnGenerate2_Click(object sender, EventArgs e) //Генератор Автомобилей
        {
            if (carGeneratorCanceletionToken != null)
            {
                // отправляем запрос на отмену генерации
               carGeneratorCanceletionToken.Cancel();
                // выходим из функции
                return;
            }
            carGeneratorCanceletionToken = new CancellationTokenSource();
            var connection = GetConnection();
            btnGenerate2.Text = "Отмена генерации";
            pbDrGenerator2.Value = 0; // сбрасываем значение
            pbDrGenerator2.Maximum = (int)upCount2.Value; // ставим максимальное
            int count = (int)upCount2.Value;
            double genderOwner = (double)tbGender2.Value;

            var progress = new Progress<int>(value =>
            {
                // меняем значение прогрессбара
                pbDrGenerator2.Value = value;
            });

            await Task.Run(() => 
            {
                generateCars(connection, count, genderOwner, progress, carGeneratorCanceletionToken);
            });
            btnGenerate2.Text = "Сгенерировать";
            carGeneratorCanceletionToken.Dispose();
            carGeneratorCanceletionToken = null;
        }

        private void generateCars(SqlConnection connection, 
                                  int count, 
                                  double genderOwner, 
                                  IProgress<int> progress,
                                  CancellationTokenSource cancellationToken)
        {
            using (connection)
            {

                connection.Open();


                Random rnd = new();


                for (var i = 0; i < count; ++i)
                {

                    var isMale = (genderOwner / 100) < rnd.NextDouble();
                    var gender = isMale ? "муж" : "жен";

                    string fName = "";
                    string region = "";
                    string model = "";
                    string carNumber = "";
                    //тут генерим все рандомные значения
                    Faker faker = new("ru");
                    if (gender == "муж")
                    {
                        fName = faker.Name.FullName(Bogus.DataSets.Name.Gender.Male);
                    }
                    else
                    {
                        fName = faker.Name.FullName(Bogus.DataSets.Name.Gender.Female);
                    }
                    model = $"{faker.Vehicle.Manufacturer()} {faker.Vehicle.Model()}";
                    carNumber = faker.Vehicle.Vin();
                    region = faker.Address.FullAddress();
                    try
                    {
                        SqlCommand command = new(@$"insert Car 
                        values ({rnd.Next(100000, 9999999)}, {rnd.Next(100000, 9999999)}, '{fName}', '{model}', 
                        '{carNumber}', {rnd.Next(100000, 9999999)}, '{region}', 
                        {rnd.Next(100000, 999999999)})", connection);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }


                    if (cancellationToken.IsCancellationRequested)
                    {
                        return; // то выходим их функции
                    }

                    progress?.Report(i + 1);
                    Thread.Sleep(300);
                }
                connection.Close();
                MessageBox.Show("Все готово, мой лорд");
            }
        }

        private void UpdateLabelForTrackbar(TrackBar tb, Label lbl)//Пределы значений ползунков (визуализатор)
        {
            if (tb.Minimum != tb.Value)
            {
                lbl.Text = $"от {tb.Minimum} до {tb.Value}";
            }
            else
            {
                lbl.Text = tb.Value.ToString();
            }
        }

        private void BindTrackBars()//Пределы значений ползунков Генератора Нарушений
        {
            // тут я добавляю реакцию на изменение значений
            tbQuantityGr.ValueChanged += (e, o) => UpdateLabelForTrackbar(tbQuantityGr, lblQuantityGr);
            tbQuantity.ValueChanged += (e, o) => UpdateLabelForTrackbar(tbQuantity, lblQuantity);
            tbViolCup.ValueChanged += (e, o) => UpdateLabelForTrackbar(tbViolCup, lblViol);

            // а тут один раз вызываю методы чтобы сразу обновить тексты на лейблах
            // лейблы использую которые зелененьким выделил
            UpdateLabelForTrackbar(tbQuantityGr, lblQuantityGr);
            UpdateLabelForTrackbar(tbQuantity, lblQuantity);
            UpdateLabelForTrackbar(tbViolCup, lblViol);
        }

        private async void btGenerateViol_Click(object sender, EventArgs e) //Генератор Нарушений
        {
            var aff = false;
            if (chkHasAff.Checked)
            {
                aff = true;
            }
            else
            {
                aff = false;
            }
            var kill = false;
            if (chkHasKill.Checked)
            {
                kill = true;
            }
            else
            {
                kill = false;
            }
            if (violGeneratorCanceletionToken != null)
            {
                // отправляем запрос на отмену генерации
                violGeneratorCanceletionToken.Cancel();
                // выходим из функции
                return;
            }
            violGeneratorCanceletionToken = new CancellationTokenSource();
            pbViol.Value = 0;
            var connection = GetConnection();
            pbViol.Maximum = (int)upCountViol.Value;
            var QuantityGr = tbQuantityGr.Minimum;
            var QuantityGrValue = tbQuantityGr.Value;
            var Quantity = tbQuantity.Minimum;
            var QuantityValue = tbQuantity.Value;
            var ViolCup = tbViolCup.Minimum;
            var ViolCupValue = tbViolCup.Value;
            btGenerateViol.Text = "Отмена генерации";
            var progress = new Progress<int>(value =>
            {
                // меняем значение прогрессбара
                pbViol.Value = value;
            });
            var count = (int)upCountViol.Value;
            await Task.Run(() =>
            {
                startGenerViol(count, connection, QuantityGr, QuantityGrValue, Quantity, QuantityValue, ViolCup, ViolCupValue, aff, kill, progress, violGeneratorCanceletionToken);
            });
            btGenerateViol.Text = "Сгенерировать";
            violGeneratorCanceletionToken.Dispose();
            violGeneratorCanceletionToken = null;

            
        }

        private void startGenerViol(int count, SqlConnection connection, int QuantityGr, int QuantityGrValue, int Quantity, int QuantityValue, int ViolCup, int ViolCupValue, bool aff, bool kill, IProgress<int> progress, CancellationTokenSource cancellationToken)
        {
            using (connection)
            {
                connection.Open();


                for (var i = 0; i < count; ++i)
                {
                    GenerateViolation(connection, QuantityGr, QuantityGrValue, Quantity, QuantityValue, ViolCup, ViolCupValue, aff, kill);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return; // то выходим их функции
                    }

                    progress?.Report(i + 1);
                    Thread.Sleep(300);
                }

                connection.Close();
            }
            MessageBox.Show("Все готово, мой лорд");
        }

        private void GenerateViolation(SqlConnection connection, 
                                       int QuantityGr, 
                                       int QuantityGrValue, 
                                       int Quantity, 
                                       int QuantityValue, 
                                       int ViolCup,
                                       int ViolCupValue,
                                       bool aff,
                                       bool kill) //Генератор Нарушений
        {
            Random rnd = new();

            Faker faker = new("ru");
            string viol = violationGroup[rnd.Next(0, violationGroup.Length)];

            string violType = "";

            var violCount = rnd.Next(QuantityGr, QuantityGrValue + 1);

            var violTypeCount = rnd.Next(Quantity, QuantityValue + 1);
            for (var a = 1; a <= violCount; ++a)
            {//Первый уровень иерархии
                SqlCommand command = new($@"INSERT  Violation OUTPUT inserted.id VALUES(@viol, null)", connection); 
                command.Parameters.Add("@viol", SqlDbType.Text);
                command.Parameters["@viol"].Value = viol;

                int violId = (int)command.ExecuteScalar();


                for (var i = 1; i <= violTypeCount; ++i)
                {
                    //violType = viol switch
                    //{
                    //    "Пересечение" => violationType1[rnd.Next(0, violationType1.Length)],
                    //    _ => ""
                    //};
                    switch (viol)
                    {
                        case "Пересечение":
                                violType = violationType1[rnd.Next(0, violationType1.Length)];
                            break;
                        case "Превышение скорости":
                            {
                                violType = violationType2[rnd.Next(0, violationType2.Length)];
                            }
                            break;
                        case "Вождение":
                            {
                                violType = violationType3[rnd.Next(0, violationType3.Length)];
                            }
                            break;
                        case "Обгон":
                            {
                                violType = violationType4[rnd.Next(0, violationType4.Length)];
                            }
                            break;
                    }

                    if (aff == true && kill == true)
                    {//Второй уровень иерархии
                        var affected = faker.Random.Int(1, 10);
                        var killed = faker.Random.Int(1, 10);
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType + $" Пострадавшие: {affected}, Смертей: {killed}";
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                    }
                    else if(aff == true && kill == false)
                    {
                        var affected = faker.Random.Int(1, 10);
                        var killed = 0;
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType + $" Пострадавшие: {affected}, Смертей: {killed}";
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                    }
                    else if(aff == false && kill == true)
                    {
                        var affected = 0;
                        var killed = faker.Random.Int(1, 10);
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType + $" Пострадавшие: {affected}, Смертей: {killed}";
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                    }
                    else
                    {
                        var affected = 0;
                        var killed = 0;
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType + $" Пострадавшие: {affected}, Смертей: {killed}";
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                    }

                        

                    int violTypeId = (int)command.ExecuteScalar();

                    var cuptureCount = rnd.Next(ViolCup, ViolCupValue + 1);

                    for (var j = 1; j <= cuptureCount; ++j)
                    {//Третий уровень иерархии
                        string cupture = cuptureOffernder[rnd.Next(0, cuptureOffernder.Length)];

                        command = new($@"INSERT INTO Violation VALUES(@cupture, @violTypeId)", connection);
                        command.Parameters.Add("@cupture", SqlDbType.Text);
                        command.Parameters["@cupture"].Value = cupture;
                        command.Parameters.Add("@violTypeId", SqlDbType.Int);
                        command.Parameters["@violTypeId"].Value = violTypeId;
                        command.ExecuteNonQuery();
                    }


                }
            }
        }

        private void btnActionSql_Click(object sender, EventArgs e) //Обслуживание кнопок Выдачи и Удаления
        {
            var button = sender as Button;
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand sqlCommand;
                switch (button.Name)
                {
                    case "btnGetDriver":
                        {
                            sqlCommand = new("select * from Driver", connection);
                        }
                        break;
                    case "btnDelDriver":
                        {
                            sqlCommand = new("delete Driver", connection);
                        }
                        break;
                    case "btnGetCar":
                        {
                            sqlCommand = new("select * from Car", connection);
                        }
                        break;
                    case "btnDelCar":
                        {
                            sqlCommand = new("delete Car", connection);
                        }
                        break;
                    case "btnGetViol":
                        {
                            sqlCommand = new("select * from Violation", connection);
                        }
                        break;
                    default:
                        {
                            sqlCommand = new("delete Violation", connection);
                        }
                        break;
                }
                var reader = sqlCommand.ExecuteReader();
                DataTable table = new();
                table.Load(reader);
                dgName.DataSource = table;
                reader.Close();
                connection.Close();
            }
        }

        private Statistics GetStatistics(SqlConnection connection)
        {
            var statistics = new Statistics();

            using (connection)
            {
                connection.Open();

                var command = new SqlCommand("SELECT count(*) FROM Car", connection);
                statistics.carCount = (int)command.ExecuteScalar();
                command = new SqlCommand("SELECT count(*) FROM Violation", connection);
                statistics.violCount = (int)command.ExecuteScalar();
                command = new SqlCommand("SELECT count(*) FROM Driver", connection);
                statistics.driverCount = (int)command.ExecuteScalar();

                command = new SqlCommand("SELECT violation_name, count(*) as count FROM Violation where parent_id is null GROUP BY violation_name ", connection);
                statistics.violGroupCount = new Dictionary<string, int>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull("violation_name")) // проверяем что порода не пустая
                        {
                            // добавляем в словарик запись, в качестве ключа название породы, в качестве значение количество
                            statistics.violGroupCount[reader.GetString("violation_name")] = reader.GetInt32("count");
                        }
                    }
                }

                command = new SqlCommand("SELECT violation_name, count(*) as count FROM Violation Where violation_name like 'Не оказал сопротивление' or violation_name like 'Оказал сопротивление' or violation_name like 'Удалось не задержать' or violation_name like 'Удалось задержать' GROUP BY violation_name ", connection);
                statistics.violTypeCount = new Dictionary<string, int>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull("violation_name")) // проверяем что порода не пустая
                        {
                            // добавляем в словарик запись, в качестве ключа название породы, в качестве значение количество
                            statistics.violTypeCount[reader.GetString("violation_name")] = reader.GetInt32("count");
                        }
                    }
                }
            }

            return statistics;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            var connection = GetConnection();
            var statistics = await Task.Run(() =>
            {
                return GetStatistics(connection);
            });

            var violInfo = String.Join(
        "\n",
        statistics.violGroupCount
            .Select(x => x.Key)
            .OrderBy(x => x)
            .Select(x => $" - {x}: {statistics.violGroupCount[x]}")
    );

            var violTypeInfo = String.Join(
        "\n",
        statistics.violTypeCount
            .Select(x => x.Key)
            .OrderBy(x => x)
            .Select(x => $" - {x}: {statistics.violTypeCount[x]}")
    );

            connection.Close();
            lblStatistics.Text = $"Автомобилей в базе: {statistics.carCount} \n"
        + $"Водителей в базе: { statistics.driverCount}\n"
        + $"Нарушений зарегистрировано: {statistics.violCount}\n"
            + $"{violInfo}" +
            "===================" +
            $"\n{violTypeInfo}";
        }
    }
}
