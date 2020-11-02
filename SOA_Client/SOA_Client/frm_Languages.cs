using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CookComputing.XmlRpc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using SOA_Client.SoapAPI;

namespace SOA_Client
{
    
    public partial class frm_Languages : Form
    {
        IMyProxy xmlrpc_proxy;
        MyServicePortTypeClient soap_proxy;
        IRest2018 rest_proxy;

        DataTable table;
        DataColumn col;
        DataRow myrow;

        int id = -1;

        public frm_Languages()
        {
            InitializeComponent();

            //Создание прокси-объекта в зависимости от выбранного
            //в настройках протокола
            if (MySettings.Default.ProtocolXmlRpc)            
                xmlrpc_proxy = XmlRpcProxyGen.Create<IMyProxy>();            
            else if(MySettings.Default.ProtocolSoap) 
                soap_proxy = new SoapAPI.MyServicePortTypeClient();            
            else if (MySettings.Default.ProtocolRest)
            {
                ChannelFactory<IRest2018> factory;  
                factory = new ChannelFactory<IRest2018>("REST2018");
                rest_proxy = factory.CreateChannel();
            }
        }


        private void frm_Languages_Load(object sender, EventArgs e)
        {
            //Формирование таблицы со списком языков
            table = new DataTable();
            col = new DataColumn("ID");
            table.Columns.Add(col);
            col = new DataColumn("Name");
            col.Caption = "Название";
            table.Columns.Add(col);

            //Заполнение таблицы данными
            UpdateGrid();
        }

        private void dgv_languages_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_languages.CurrentRow.Index + 1 == dgv_languages.Rows.Count)
            {
                //Если щёлкнули в последней строке таблицы - переход в режим
                //добавления новой записи -
                id = -1; //Сброс идентификатора
                ClearForm(); //Очистка формы
                return; //Выход из обработчика
            }

            //Подгрузка выбранного в таблице языка в форму
            id = Convert.ToInt32(dgv_languages.CurrentRow.Cells[0].Value);

            if (MySettings.Default.ProtocolXmlRpc)
            {
                XMLRPC_Language language = xmlrpc_proxy.ReadLanguage(id);
                tb_ID.Text = language.ID.ToString();
                tb_Name.Text = language.Name;
            }
            else if (MySettings.Default.ProtocolSoap)
            {
                SOAP_Language language = soap_proxy.ReadLanguage(id);
                tb_ID.Text = language.ID.ToString();
                tb_Name.Text = language.Name;
            }
            else if (MySettings.Default.ProtocolRest)
            {
                REST_Language language = rest_proxy.ReadLanguage(id);
                tb_ID.Text = language.ID.ToString();
                tb_Name.Text = language.Name;
            }            
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //Удаление языка
            if (id < 0) return; //Если язык не выбран - выход
            if (MessageBox.Show("Действительно удалить?", "", MessageBoxButtons.YesNo) == DialogResult.No) return;
               
            if (MySettings.Default.ProtocolXmlRpc)            
                xmlrpc_proxy.DeleteLanguage(id);            
            else if (MySettings.Default.ProtocolSoap)            
                soap_proxy.DeleteLanguage(id);            
            else if (MySettings.Default.ProtocolRest)            
                rest_proxy.DeleteLanguage(id); 
            
            UpdateGrid(); //Обновление таблицы
            ClearForm(); //Очистка формы
            id = -1; //Сброс идентификатора
        }

        private void UpdateGrid()
        {
            //Обновление таблицы

            //Очистка таблицы
            table.Clear();

            //Подгрузка в таблицу новых данных
            if (MySettings.Default.ProtocolXmlRpc)
            {
                XMLRPC_Language[] languages = xmlrpc_proxy.ListLanguages();

                foreach (XMLRPC_Language language in languages)
                {
                    myrow = table.NewRow();
                    myrow["ID"] = language.ID;
                    myrow["Name"] = language.Name;

                    table.Rows.Add(myrow);
                }
            }
            else if (MySettings.Default.ProtocolSoap){
                SOAP_Language[] languages = soap_proxy.ListLanguages();

                foreach (SOAP_Language language in languages)
                {
                    myrow = table.NewRow();
                    myrow["ID"] = language.ID;
                    myrow["Name"] = language.Name;

                    table.Rows.Add(myrow);
                }
            }
            else if (MySettings.Default.ProtocolRest)
            {
                REST_Language[] languages = rest_proxy.ListLanguages();
                foreach (REST_Language language in languages)
                {
                    myrow = table.NewRow();
                    myrow["ID"] = language.ID;
                    myrow["Name"] = language.Name;

                    table.Rows.Add(myrow);
                }
            }

            dgv_languages.DataSource = table;
        }

        private void ClearForm()
        {
            //Очистка формы
            tb_ID.Text = "";
            tb_Name.Text = "";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //Сохранение языка
            if (MySettings.Default.ProtocolXmlRpc)
            {
                XMLRPC_Language language = new XMLRPC_Language();
                language.Name = tb_Name.Text;

                if (id < 0) //Если идентификатор не задан (сброшен) - 
                    xmlrpc_proxy.CreateLanguage(language); //Создание нового языка
                else //иначе (то есть идентификатор задан) -
                    xmlrpc_proxy.UpdateLanguage(id, language); //перезапись существующего языка с заданным идентификатором
            }
            else if (MySettings.Default.ProtocolSoap) //... то же самое для остальных протоколов
            {
                if (id < 0)
                    soap_proxy.CreateLanguage(tb_Name.Text);
                else               
                    soap_proxy.UpdateLanguage(id, tb_Name.Text);
                   
            }
            else if (MySettings.Default.ProtocolRest)
            {
                if (id < 0)
                    rest_proxy.CreateLanguage(new REST_Language(tb_Name.Text));
                else               
                    rest_proxy.UpdateLanguage(id, new REST_Language(tb_Name.Text));
                  
            }

            if (id >= 0) id = -1; //Сброс идентификатора

            UpdateGrid(); //Обновление таблицы
            ClearForm(); //Очистка формы 
        }
        
    }
}
