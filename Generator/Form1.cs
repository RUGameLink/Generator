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

        public struct progressInfo
        {
            public int value;
            public string info;
        }

        SqlDataReader reader;
        DataTable table;

        readonly string[] cuptureOffernder; //Последствия (что придумал)

        public Form1()
        {
            InitializeComponent();
            ThreadPool.SetMinThreads(80, 80);
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

        private async void btnQuery_Click(object sender, EventArgs e) //Выдача запросов
        {
            if (txtConnectionString.Text == "" || txtQuery.Text == "")
            {
                MessageBox.Show("Не все поля заполнены", "Warning!");
            }
            else
            {

                SqlConnection connection = new();

                connection = GetConnection();
                string query = txtQuery.Text;
                btnMaster();

                await Task.Run(() =>
                {
                    ActionQuery(connection, query);
                });
                btnMaster();

                dgName.DataSource = table;


            }
        }

        private void ActionQuery(SqlConnection connection, string query)
        {
            connection.Open();

            SqlCommand command = new();
            command.Connection = connection;
            command.CommandText = query;

            reader = command.ExecuteReader();
            table = new();
            table.Load(reader);
            reader.Close();

            connection.Close();
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

            //var progress = new Progress<int>(value =>
            //{
            //    // меняем значение прогрессбара
            //    pbDrGenerator.Value = value;
            //});
            var progress = new Progress<progressInfo>(progress =>
            {
                pbDrGenerator.Value = progress.value; // прогресс пихаем в прогресс бар
                lblDrInfo.Text = progress.info; // а текст пихаем в лейбл
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
                                     IProgress<progressInfo> progress, 
                                     CancellationTokenSource cancellationToken)
        {
            using (connection)
            {
                connection.Open();
                SqlTransaction transaction = null;
                if (chkbDrTrans.Checked)
                {
                    transaction = connection.BeginTransaction();
                }
                SqlCommand command;

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
                        command = new(@$"insert Driver 
                        values ('{fName}', '{region}', {rnd.Next(100000, 999999999)})"
                        , connection);

                        if (chkbDrTrans.Checked)
                        {
                            command.Transaction = transaction;
                            
                        }
                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {

                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return; // то выходим их функции
                    }

                    //   progress?.Report(i + 1);

                    command = new("SELECT count(*) FROM Driver", connection);
                    if (chkbDrTrans.Checked)
                        command.Transaction = transaction;

                    var info = $"Водитей в транзакции {command.ExecuteScalar()}";
                    progress?.Report(new progressInfo
                    {
                        value = i + 1,
                        info = info,
                    });

                    Thread.Sleep(30);
                }
                if (chkbDrTrans.Checked)
                {
                    transaction.Commit();
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

            //var progress = new Progress<int>(value =>
            //{
            //    // меняем значение прогрессбара
            //    pbDrGenerator2.Value = value;
            //});
            var progress = new Progress<progressInfo>(progress =>
            {
                pbDrGenerator2.Value = progress.value; // прогресс пихаем в прогресс бар
                lblDrInfo2.Text = progress.info; // а текст пихаем в лейбл
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
                                  IProgress<progressInfo> progress,
                                  CancellationTokenSource cancellationToken)
        {
            using (connection)
            {

                connection.Open();
                SqlCommand command = null;
                SqlTransaction transaction = null;

                if (chkbCarTrans.Checked)
                {
                    transaction = connection.BeginTransaction();
                }

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
                        command = new(@$"insert Car 
                        values ({rnd.Next(100000, 9999999)}, {rnd.Next(100000, 9999999)}, '{fName}', '{model}', 
                        '{carNumber}', {rnd.Next(100000, 9999999)}, '{region}', 
                        {rnd.Next(100000, 999999999)})", connection);

                        if (chkbCarTrans.Checked)
                        {
                            command.Transaction = transaction;

                        }
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }


                    if (cancellationToken.IsCancellationRequested)
                    {
                        return; // то выходим их функции
                    }

                    //progress?.Report(i + 1);
                    command = new("SELECT count(*) FROM Car", connection);

                    if (chkbCarTrans.Checked)
                        command.Transaction = transaction;

                    var info = $"Автомобилей в транзакции {command.ExecuteScalar()}";
                    progress?.Report(new progressInfo
                    {
                        value = i + 1,
                        info = info,
                    });

                    Thread.Sleep(30);
                }
                if (chkbCarTrans.Checked)
                {
                    transaction.Commit();
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
            //var progress = new Progress<int>(value =>
            //{
            //    // меняем значение прогрессбара
            //    pbViol.Value = value;
            //});
            var progress = new Progress<progressInfo>(progress =>
            {
                pbViol.Value = progress.value; // прогресс пихаем в прогресс бар
                lblViolGen.Text = progress.info; // а текст пихаем в лейбл
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

        private void startGenerViol(int count, SqlConnection connection,
                                    int QuantityGr, 
                                    int QuantityGrValue, 
                                    int Quantity, 
                                    int QuantityValue, 
                                    int ViolCup, 
                                    int ViolCupValue, 
                                    bool aff, 
                                    bool kill,
                                    IProgress<progressInfo> progress, 
                                    CancellationTokenSource cancellationToken)
        {
            using (connection)
            {
                connection.Open();
                SqlTransaction transaction = null;

                if (chkbViolTrans.Checked)
                {
                    transaction = connection.BeginTransaction();
                }

                for (var i = 0; i < count; ++i)
                {
                    Random rnd = new();

                    Faker faker = new("ru");
                    string viol = violationGroup[rnd.Next(0, violationGroup.Length)];
                    var cuptureCount = rnd.Next(ViolCup, ViolCupValue + 1);

                    string violType = "";

                    var violCount = rnd.Next(QuantityGr, QuantityGrValue + 1);

                    var violTypeCount = rnd.Next(Quantity, QuantityValue + 1);

                    SqlCommand command = new($@"INSERT  Violation OUTPUT inserted.id VALUES(@viol, null)", connection);
                    command.Parameters.Add("@viol", SqlDbType.Text);
                    command.Parameters["@viol"].Value = viol;

                    if (chkbViolTrans.Checked)
                    {
                        command.Transaction = transaction;

                    }
                    int violId = (int)command.ExecuteScalar();

                    for (var a = 1; a <= violTypeCount; ++a)
                    {
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
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType;
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                        if (chkbViolTrans.Checked)
                        {
                            command.Transaction = transaction;

                        }
                        int violTypeId = (int)command.ExecuteScalar();

                        for (var b = 1; b <= cuptureCount; ++b)
                        {
                            string cupture = cuptureOffernder[rnd.Next(0, cuptureOffernder.Length)];

                            command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@cupture, @violTypeId)", connection);
                            command.Parameters.Add("@cupture", SqlDbType.Text);
                            command.Parameters["@cupture"].Value = cupture;
                            command.Parameters.Add("@violTypeId", SqlDbType.Int);
                            command.Parameters["@violTypeId"].Value = violTypeId;
                            if (chkbViolTrans.Checked)
                            {
                                command.Transaction = transaction;

                            }
                            int cuptId = (int)command.ExecuteScalar();

                            for (var j = 1; j <= violCount; j++)
                            {
                                command = new($@"INSERT INTO Violation VALUES(@attracted, @cuptId)", connection);
                                int attracted = faker.Random.Int(0, 3);
                                command.Parameters.Add("@attracted", SqlDbType.Text);
                                command.Parameters["@attracted"].Value = $"Количество нарушений до этого: {attracted}";
                                command.Parameters.Add("@cuptId", SqlDbType.Int);
                                command.Parameters["@cuptId"].Value = cuptId;
                                if (chkbViolTrans.Checked)
                                {
                                    command.Transaction = transaction;

                                }
                                command.ExecuteNonQuery();
                            }
                        }

                        if (aff == true)
                        {
                            var affected = faker.Random.Int(1, 10);

                            command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @violId)", connection);
                            command.Parameters.Add("@violType", SqlDbType.Text);
                            command.Parameters["@violType"].Value = $" Пострадавшие: {affected}";
                            command.Parameters.Add("@violId", SqlDbType.Int);
                            command.Parameters["@violId"].Value = violId;
                            if (chkbViolTrans.Checked)
                            {
                                command.Transaction = transaction;

                            }
                            command.ExecuteNonQuery();
                        }
                        if (kill == true)
                        {
                            var killed = faker.Random.Int(1, 10);

                            command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @violId)", connection);
                            command.Parameters.Add("@violType", SqlDbType.Text);
                            command.Parameters["@violType"].Value = $" Смертей: {killed}";
                            command.Parameters.Add("@violId", SqlDbType.Int);
                            command.Parameters["@violId"].Value = violId;
                            if (chkbViolTrans.Checked)
                            {
                                command.Transaction = transaction;

                            }
                            command.ExecuteNonQuery();
                        }
                    }
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return; // то выходим их функции
                    }

                    //progress?.Report(i + 1);
                    command = new("SELECT count(*) FROM Violation", connection);

                    if (chkbViolTrans.Checked)
                        command.Transaction = transaction;

                    var info = $"Нарушений в транзакции {command.ExecuteScalar()}";
                    progress?.Report(new progressInfo
                    {
                        value = i + 1,
                        info = info,
                    });
                    Thread.Sleep(30);
                }

                if (chkbViolTrans.Checked)
                {
                    transaction.Commit();
                }
                connection.Close();
                MessageBox.Show("Все готово, мой лорд");
            }

        }

        private async  void btnActionSql_Click(object sender, EventArgs e) //Обслуживание кнопок Выдачи и Удаления
        {
            var button = sender as Button;
            var connection = GetConnection();

            btnMaster();

            await Task.Run(() =>
            {
                ActionSql(connection, button);
            });
            dgName.DataSource = table;
            btnMaster();
        }

        private void btnMaster()
        {
            btnGetDriver.Enabled = !btnGetDriver.Enabled;
            btnGetCar.Enabled = !btnGetCar.Enabled;
            btnGetViol.Enabled = !btnGetViol.Enabled;
            btnDelDriver.Enabled = !btnDelDriver.Enabled;
            btnDelCar.Enabled = !btnDelCar.Enabled;
            btnDelViol.Enabled = !btnDelViol.Enabled;
            btnQuery.Enabled = !btnQuery.Enabled;
            cmbSel.Enabled = !cmbSel.Enabled;
            lblLoad.Visible = !lblLoad.Visible;
        }

        private void ActionSql(SqlConnection connection, Button button)
        {

            using (connection)
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
                reader = sqlCommand.ExecuteReader();
                table = new();
                table.Load(reader);

                reader.Close();
                connection.Close();
            }
        }

        private Statistics GetStatistics(SqlConnection connection)
        {
            var statistics = new Statistics();

            using (connection)
            {
                try
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
                        if (!reader.IsDBNull("violation_name")) // проверяем что не пустj
                        {
                            // добавляем в словарик запись, в качестве ключа нарушение, в качестве значение количество
                            statistics.violGroupCount[reader.GetString("violation_name")] = reader.GetInt32("count");
                        }
                    }
                }
                    command = new SqlCommand("SELECT violation_name, count(*) as count FROM Violation Where violation_name = 'Не оказал сопротивление' or violation_name = 'Оказал сопротивление' or violation_name = 'Удалось не задержать' or violation_name = 'Удалось задержать' GROUP BY violation_name ", connection);
                    statistics.violTypeCount = new Dictionary<string, int>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull("violation_name")) // проверяем что не пусто
                            {
                                statistics.violTypeCount[reader.GetString("violation_name")] = reader.GetInt32("count");
                            }
                        }
                    }
                }
                catch
                {

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
            lblStatistics.Text = $"Водителей в базе: { statistics.driverCount}\n"
        + $"Автомобилей в базе: {statistics.carCount} \n"
        + $"Нарушений зарегистрировано: {statistics.violCount}\n"
            + $"{violInfo}" +
            "===================" +
            $"\n{violTypeInfo}";
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
