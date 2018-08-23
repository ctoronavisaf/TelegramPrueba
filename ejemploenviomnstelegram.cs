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
            /*
            int api_id = 365623;
            string api_hash = "e175ce3c6795aefa038ed1c2845f9b7c";
            string phone_envio = "+573167965374";
            string phone_recibido = "573233775322";
            */

            
            //client = new TelegramClient(319411, "ce78ea96660f05c450d9f6cd9c82f04a"); //Datos andre
            client = new TelegramClient(365623, "e175ce3c6795aefa038ed1c2845f9b7c"); //Datos cata
            

            await client.ConnectAsync();
            string hash;
            hash = await client.SendCodeRequestAsync("+573233775322");
            
            //El code cambia en un periodo minimo de tiempo, cada code se utiliza en una sola ejecución.
            //The code changes in a minimum period of time, each code is used in a single execution.
            //Para pedir el codigo a telegram en la linea 58 debe ir el numero sin (+), y para enviar el mensaje debe tener el (+).
            var code = "50800"; // you can change code in debugger
            var user = await client.MakeAuthAsync("+573233775322", hash, code);

            //Obtiene los contactos disponibles.
            var result = await client.GetContactsAsync();

            //Encuentra el contacto, sincroniza el contacto y envia el mensaje.
            var us = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.Phone == "+573167965322");
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = us.Id }, "Hola, Te estoy enviando un mensaje desde el codigo fuente.");
        }
    }
}