using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Web;
using CookComputing.XmlRpc;
using SOA_Client.SoapAPI;

namespace SOA_Client
{
    public partial class frm_People : Form
    {
        IMyProxy xmlrpc_proxy;
        MyServicePortTypeClient soap_proxy;
        IRest2018 rest_proxy;

        DataTable table;
        DataRow myrow;
        DataColumn col;
        int id=-1;
        List<ItemComboBox> list;

        public frm_People()
        {
            InitializeComponent();
           
            //Создание прокси-объекта в зависимости от протокола,
            //выбранного в настройках
            if (MySettings.Default.ProtocolXmlRpc)            
                xmlrpc_proxy = XmlRpcProxyGen.Create<IMyProxy>();            
            else if (MySettings.Default.ProtocolSoap)            
                soap_proxy = new SoapAPI.MyServicePortTypeClient();            
            else if (MySettings.Default.ProtocolRest)
            {
                ChannelFactory<IRest2018> factory;
                factory = new ChannelFactory<IRest2018>("REST2018");
                rest_proxy = factory.CreateChannel();
            }
           
        }

        private void frm_People_Load(object sender, EventArgs e)
        {
            //Создание таблицы для отображения списка сотрудников
            table = new DataTable();
            col = new DataColumn("ID");
            table.Columns.Add(col);
            col = new DataColumn("Name");
            col.Caption = "Название";
            table.Columns.Add(col);
            col = new DataColumn("Age");
            col.Caption = "Возраст";
            table.Columns.Add(col);
            col = new DataColumn("Mail");
            col.Caption = "Почта";
            table.Columns.Add(col);
            col = new DataColumn("Language");
            col.Caption = "Язык программирования";
            table.Columns.Add(col);

            //Заполнение таблицы данными
            UpdateGrid();

            //Формирование раскрывающегося списка "Языки"           
            list = new List<ItemComboBox>();
            
            //В зависимости от выбранного протокола 
            //вызов метода ListLanguages у соответствующего
            //прокси-объекта
            if (MySettings.Default.ProtocolXmlRpc)
            {
                XMLRPC_Language[] languages = xmlrpc_proxy.ListLanguages();
                foreach (XMLRPC_Language language in languages)
                    list.Add(new ItemComboBox(Convert.ToInt32(language.ID), language.Name));
            }
            else if (MySettings.Default.ProtocolSoap)
            {
                SOAP_Language[] languages = soap_proxy.ListLanguages();
                foreach (SOAP_Language language in languages)
                    list.Add(new ItemComboBox(Convert.ToInt32(language.ID), language.Name));
            }
            else if (MySettings.Default.ProtocolRest)
            {
                REST_Language[] languages = rest_proxy.ListLanguages();
                foreach (REST_Language language in languages)
                    list.Add(new ItemComboBox(Convert.ToInt32(language.ID), language.Name));
            }
            
            //Вывод полученного списка языков в раскрывающийся список
            cb_Language.DataSource = list;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //Сохранение данных, введённых в форму
            if (MySettings.Default.ProtocolXmlRpc)
            {
                XMLRPC_Person person = new XMLRPC_Person();
                person.Name = tb_Name.Text;
                person.Age = Convert.ToInt32(tb_Age.Text);
                person.Mail = tb_Mail.Text;
                person.LanguageID = (cb_Language.SelectedItem as ItemComboBox).Id;

                if (id < 0) //Если идентификатор не задан или сброшен - 
                    xmlrpc_proxy.CreatePerson(person); //создание нового человека
                else //иначе (идентификатор задан) -
                    //обновление человека с заданным идентификатором:
                    xmlrpc_proxy.UpdatePerson(
                            id,
                            person
                    ); 
            }
            //...то же самое для остальных протоклов:
            else if (MySettings.Default.ProtocolSoap)
            {
                if (id < 0)
                    soap_proxy.CreatePerson(
                            tb_Name.Text,
                            Convert.ToInt32(tb_Age.Text),
                            tb_Mail.Text,
                            (cb_Language.SelectedItem as ItemComboBox).Id
                    );
                else
                    soap_proxy.UpdatePerson(
                            id, 
                            tb_Name.Text,
                            Convert.ToInt32(tb_Age.Text),
                            tb_Mail.Text,
                            (cb_Language.SelectedItem as ItemComboBox).Id
                    );
            }
            else if (MySettings.Default.ProtocolRest)
            {
                if (id < 0)                
                    rest_proxy.CreatePerson(new REST_Person(
                            tb_Name.Text,
                            Convert.ToInt32(tb_Age.Text),
                            tb_Mail.Text,
                            (cb_Language.SelectedItem as ItemComboBox).Id
                    ));                
                else                
                    rest_proxy.UpdatePerson(id, new REST_Person(
                            tb_Name.Text,
                            Convert.ToInt32(tb_Age.Text),
                            tb_Mail.Text,
                            (cb_Language.SelectedItem as ItemComboBox).Id
                    ));                
            }

            //По завершении сохранения:

            if(id>=0) id = -1; //Сброс идентификатора

            UpdateGrid(); //Обновление таблицы
            ClearForm(); //Очистка форма
        }

        private void UpdateGrid()
        {
            //Функция обновления таблицы со списком людей

            //Очистка таблицы
            table.Clear();

            //Заполнение таблицы новыми данными
            if (MySettings.Default.ProtocolXmlRpc)
            {
                XMLRPC_Person[] people = xmlrpc_proxy.ListPeople();

                foreach (XMLRPC_Person person in people)
                {
                    myrow = table.NewRow();
                    myrow["ID"] = person.ID;
                    myrow["Name"] = person.Name;
                    myrow["Age"] = person.Age;
                    myrow["Mail"] = person.Mail;
                    myrow["Language"] = person.Language;

                    table.Rows.Add(myrow);
                }
            }
            else if (MySettings.Default.ProtocolSoap)
            {
                SOAP_Person[] people = soap_proxy.ListPeople();
                
                foreach (SOAP_Person person in people)
                {
                    myrow = table.NewRow();
                    myrow["ID"] = person.ID;
                    myrow["Name"] = person.Name;
                    myrow["Age"] = person.Age;
                    myrow["Mail"] = person.Mail;
                    myrow["Language"] = person.Language;

                    table.Rows.Add(myrow);
                }
            }
            else if (MySettings.Default.ProtocolRest)
            {
                REST_Person[] people = rest_proxy.ListPeople();
                foreach (REST_Person person in people)
                {
                    myrow = table.NewRow();
                    myrow["ID"] = person.ID;
                    myrow["Name"] = person.Name;
                    myrow["Age"] = person.Age;
                    myrow["Mail"] = person.Mail;
                    myrow["Language"] = person.Language;

                    table.Rows.Add(myrow);
                }
            }

            dgv_People.DataSource = table;
        }

        private void ClearForm()
        {
            //Функция очистки формы
            tb_ID.Text = "";
            tb_Name.Text = "";
            tb_Age.Text = "";
            tb_Mail.Text = "";
        }

        private void dgv_People_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_People.CurrentRow.Index+1 == dgv_People.Rows.Count)
            {
                //Если щёлкнули в последней строке таблицы - переход в режим
                //добавления новой записи -
                id = -1; //Сброс идентификатора
                ClearForm(); //Очистка формы
                return; //Выход из обработчика
            }

            //Подгрузка в форму данных выбранного в таблице человека
            id = Convert.ToInt32(dgv_People.CurrentRow.Cells[0].Value);

            if (MySettings.Default.ProtocolXmlRpc)
            {
                XMLRPC_Person person = xmlrpc_proxy.ReadPerson(id);

                tb_ID.Text = person.ID.ToString();
                tb_Name.Text = person.Name;
                tb_Age.Text = person.Age.ToString();
                tb_Mail.Text = person.Mail;

                //Выделение в раскрывающемся списке
                //языка, который знает данный человек.
                foreach (ItemComboBox item in list)
                {
                    if (item.Id == person.LanguageID)
                        cb_Language.SelectedItem = item;
                }
            }
            //...то же самое для остальных протоколов:
            else if (MySettings.Default.ProtocolSoap) 
            {
                SOAP_Person person = soap_proxy.ReadPerson(id);

                tb_ID.Text = person.ID.ToString();
                tb_Name.Text = person.Name;
                tb_Age.Text = person.Age.ToString();
                tb_Mail.Text = person.Mail;

                foreach (ItemComboBox item in list)
                {
                    if (item.Id == person.LanguageID)
                        cb_Language.SelectedItem = item;
                }
            }
            else if (MySettings.Default.ProtocolRest)
            {
                REST_Person person = rest_proxy.ReadPerson(id);
            
                tb_ID.Text = person.ID;
                tb_Name.Text = person.Name;
                tb_Age.Text = person.Age.ToString();
                tb_Mail.Text = person.Mail;

                foreach(ItemComboBox item in list) {
                    if (item.Id == person.LanguageID)
                        cb_Language.SelectedItem = item;
                }
            }

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //Если идентификатор не задан или сброшен -
            if (id < 0) return; //удалить нельзя, выход.
            
            //Вывод предупреждения
            if (MessageBox.Show("Действительно удалить?", "", MessageBoxButtons.YesNo) == DialogResult.No) return;

            //Собственно удаление
            if(MySettings.Default.ProtocolXmlRpc)
                xmlrpc_proxy.DeletePerson(id);
            else if (MySettings.Default.ProtocolSoap)            
                soap_proxy.DeletePerson(id);            
            else if (MySettings.Default.ProtocolRest)            
                rest_proxy.DeletePerson(id);
            
            id = -1; //Сброс идентификатора
            UpdateGrid(); //Обновление таблицы
            ClearForm(); //Очистка формы

        }
    }
}
