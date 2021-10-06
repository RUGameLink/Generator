using Bogus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
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
            openFileDialog1.Filter = "mdf files(*mdf)|*mdf|All files(*.*)|*.*";
            BindTrackBars();

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



        string filename;

        struct Range
        {
            public double min;
            public double max;

            public bool CheckContains(double value)
            {
                return min <= value && value <= max;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
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

        SqlConnection GetConnection()
        {
            SqlConnection connection = new();
            switch (cmbSel.SelectedIndex)
            {
                case 0:
                    {
                        SqlConnectionStringBuilder sqlConnectionStringBuilder = new();
                        sqlConnectionStringBuilder.DataSource = @"(LocalDB)\MSSQLLocalDB";
                        sqlConnectionStringBuilder.AttachDBFilename = openFileDialog1.FileName;
                        sqlConnectionStringBuilder.IntegratedSecurity = true;
                        sqlConnectionStringBuilder.ConnectTimeout = 30;
                        connection.ConnectionString = sqlConnectionStringBuilder.ToString();
                    }
                    break;
                case 1:
                    {
                        connection.ConnectionString = txtConnectionString.Text;

                    }
                    break;
            }
            return connection;
        }

        private void cmbSel_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            filename = openFileDialog1.FileName;
            txtConnectionString.Text = filename;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            using (var connection = GetConnection())
            {
                pbDrGenerator.Value = 0; // сбрасываем значение
                pbDrGenerator.Maximum = (int)upCount.Value; // ставим максимальное
                connection.Open();

                // создаем генератор случайных чисел
                Random rnd = new();

                // и начинаем в цикле создавать записи в БД,
                // в цикле повторяем по количеству записей указанных в поле udCount
                for (var i = 0; i < upCount.Value; ++i)
                {
                   
                    var isMale = ((double)tbGender.Value / 100) < rnd.NextDouble();
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
                    catch(Exception ex)
                    {

                    }
                    
                    
                    pbDrGenerator.Value++;
                }
                connection.Close();
                MessageBox.Show("Все готово, мой лорд");
            }
        }

        private void btnGenerate2_Click(object sender, EventArgs e)
        {
            using (var connection = GetConnection())
            {
                pbDrGenerator2.Value = 0; // сбрасываем значение
                pbDrGenerator2.Maximum = (int)upCount2.Value; // ставим максимальное
                connection.Open();

                // создаем генератор случайных чисел
                Random rnd = new();

                // и начинаем в цикле создавать записи в БД,
                // в цикле повторяем по количеству записей указанных в поле udCount
                for (var i = 0; i < upCount2.Value; ++i)
                {

                    var isMale = ((double)tbGender2.Value / 100) < rnd.NextDouble();
                    var gender = isMale ? "муж" : "жен";

                    string fName = "";
                    string region = "";
                    string model = "";
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
                    model = faker.Vehicle.Model();
                    region = faker.Address.FullAddress();
                    try
                    {
                        SqlCommand command = new(@$"insert Car 
                        values ({rnd.Next(100000, 9999999)}, {rnd.Next(100000, 9999999)}, '{fName}', '{model}', 
                        {rnd.Next(100000, 9999999)}, {rnd.Next(100000, 9999999)}, '{region}', {rnd.Next(100000, 999999999)})"
                        , connection);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }


                    pbDrGenerator2.Value++;
                }
                connection.Close();
                MessageBox.Show("Все готово, мой лорд");
            }
        }

        private void UpdateLabelForTrackbar(TrackBar tb, Label lbl)
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

        private void BindTrackBars()
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

        private void btGenerateViol_Click(object sender, EventArgs e)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                pbViol.Value = 0;
                pbViol.Maximum = (int)upCountViol.Value;

                for (var i = 0; i < upCountViol.Value; ++i)
                {
                    GenerateViolation(connection);
                    pbViol.Value++;
                }

                connection.Close();
            }
            MessageBox.Show("Все готово, мой лорд");
        }

        private void GenerateViolation(SqlConnection connection)
        {
            Random rnd = new();
            // генерим имя
            Faker faker = new("ru");
            string viol = violationGroup[rnd.Next(0, violationGroup.Length)];

            string violType = "";

            var violCount = rnd.Next(tbQuantityGr.Minimum, tbQuantityGr.Value + 1);

            var violTypeCount = rnd.Next(tbQuantity.Minimum, tbQuantity.Value + 1);
            for (var a = 1; a <= violCount; ++a)
            {
                SqlCommand command = new($@"INSERT  Violation OUTPUT inserted.id VALUES(@viol, null, null, null)", connection);
                command.Parameters.Add("@viol", SqlDbType.Text);
                command.Parameters["@viol"].Value = viol;

                int violId = (int)command.ExecuteScalar();

                // ну и вызываем уже привычный нам insert
                for (var i = 1; i <= violTypeCount; ++i)
                {
                    switch (viol)
                    {
                        case "Пересечение":
                            {
                                violType = violationType1[rnd.Next(0, violationType1.Length)];
                            }
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
                //    command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, null, null, @violId)", connection);
                //    command.Parameters.Add("@violType", SqlDbType.Text);
                //    command.Parameters["@violType"].Value = violType;
                 //   command.Parameters.Add("@violId", SqlDbType.Int);
                 //   command.Parameters["@violId"].Value = violId;

                    if (chkHasAff.Checked && chkHasKill.Checked)
                    {
                        var affected = faker.Random.Int(1, 10);
                        var killed = faker.Random.Int(1, 10);
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @affected, @killed, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType;
                        command.Parameters.Add("@affected", SqlDbType.Int);
                        command.Parameters["@affected"].Value = affected;
                        command.Parameters.Add("@killed", SqlDbType.Int);
                        command.Parameters["@killed"].Value = killed;
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                    }
                    else if(chkHasAff.Checked && !chkHasKill.Checked)
                    {
                        var affected = faker.Random.Int(1, 10);
                        var killed = 0;
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @affected, @killed, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType;
                        command.Parameters.Add("@affected", SqlDbType.Int);
                        command.Parameters["@affected"].Value = affected;
                        command.Parameters.Add("@killed", SqlDbType.Int);
                        command.Parameters["@killed"].Value = killed;
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                    }
                    else if(!chkHasAff.Checked && chkHasKill.Checked)
                    {
                        var affected = 0;
                        var killed = faker.Random.Int(1, 10);
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @affected, @killed, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType;
                        command.Parameters.Add("@affected", SqlDbType.Int);
                        command.Parameters["@affected"].Value = affected;
                        command.Parameters.Add("@killed", SqlDbType.Int);
                        command.Parameters["@killed"].Value = killed;
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                    }
                    else
                    {
                        var affected = 0;
                        var killed = 0;
                        command = new($@"INSERT INTO Violation OUTPUT inserted.id VALUES(@violType, @affected, @killed, @violId)", connection);
                        command.Parameters.Add("@violType", SqlDbType.Text);
                        command.Parameters["@violType"].Value = violType;
                        command.Parameters.Add("@affected", SqlDbType.Int);
                        command.Parameters["@affected"].Value = affected;
                        command.Parameters.Add("@killed", SqlDbType.Int);
                        command.Parameters["@killed"].Value = killed;
                        command.Parameters.Add("@violId", SqlDbType.Int);
                        command.Parameters["@violId"].Value = violId;
                    }

                        

                    int violTypeId = (int)command.ExecuteScalar();

                    var cuptureCount = rnd.Next(tbViolCup.Minimum, tbViolCup.Value + 1);

                    for (var j = 1; j <= cuptureCount; ++j)
                    {
                        string cupture = cuptureOffernder[rnd.Next(0, cuptureOffernder.Length)];

                        command = new($@"INSERT INTO Violation VALUES(@cupture, null, null, @violTypeId)", connection);
                        command.Parameters.Add("@cupture", SqlDbType.Text);
                        command.Parameters["@cupture"].Value = cupture;
                        command.Parameters.Add("@violTypeId", SqlDbType.Int);
                        command.Parameters["@violTypeId"].Value = violTypeId;
                        command.ExecuteNonQuery();
                    }


                }
            }
        }

        private void btnActionSql_Click(object sender, EventArgs e)
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
    }
}
