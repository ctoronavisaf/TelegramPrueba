//Envio mensajes por medio de API Telegram
//Instalar paquete Nuget TLSharp(Libreria), Ademas se instalaran BigMath, DotNetZip, MarkerMetro.Unity.Ionic.Zlib

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeleSharp.TL;
using TLSharp;
using TLSharp.Core;

namespace Telegram1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TelegramClient client;

        private async void btnenviar_Click(object sender, EventArgs e)
        {

            client = new TelegramClient(api_id, "api_hash"); //Datos que provee Telegram


            await client.ConnectAsync();
            string hash;
            hash = await client.SendCodeRequestAsync("numeroenvia");

            //El code cambia en un periodo minimo de tiempo, cada code se utiliza en una sola ejecución.
            //The code changes in a minimum period of time, each code is used in a single execution.
            //Para pedir el codigo a telegram en la linea 58 debe ir el numero sin (+), y para enviar el mensaje debe tener el (+).
            var code = "codigo"; // Codigo lo provee Telegram
            var user = await client.MakeAuthAsync("numeroenvia", hash, code);

            //Obtiene los contactos disponibles.
            var result = await client.GetContactsAsync();

            //Encuentra el contacto, sincroniza el contacto y envia el mensaje.
            var us = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.Phone == "numerorecibe");
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = us.Id }, "Hola, Te estoy enviando un mensaje desde el codigo fuente.");
        }
    }
}
