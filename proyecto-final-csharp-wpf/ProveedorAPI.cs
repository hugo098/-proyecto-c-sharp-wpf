using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final_csharp_wpf
{
    class ProveedorAPI
    {
        public static async Task<string> ObtenerProveedores()
        {
            
            using (var client = new HttpClient())
            {
               
                client.BaseAddress = new Uri("https://proyectofinalasp-mvc2020.azurewebsites.net/api/");
                
                client.DefaultRequestHeaders.Accept.Clear();
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                
                HttpResponseMessage respuesta = await client.GetAsync("Proveedores");
                
                if (respuesta.IsSuccessStatusCode)
                {
                    
                    var proveedores = await respuesta.Content.ReadAsStringAsync();
                    return proveedores;
                }
            }
            return null;
        }

        public static async Task<string> AgregarProveedor(dynamic u)
        {
            
            using (var client = new HttpClient())
            {
               
                client.BaseAddress = new Uri("https://proyectofinalasp-mvc2020.azurewebsites.net/api/");

                
                client.DefaultRequestHeaders.Accept.Clear();

                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("Proveedores", content);
                if (response.IsSuccessStatusCode)
                {
                    return "Se agrego satisfactoriamente";
                }
                else
                {
                    return "Ocurrio un error: " + response.StatusCode;
                }
            }
        }

      public static async Task<string> ModificarProveedor(dynamic u)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://proyectofinalasp-mvc2020.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));               
                StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("Proveedores/" + u.idProveedor, content);
                if (response.IsSuccessStatusCode)
                {
                    return "Se modifico satisfactoriamente";
                }
                else
                {
                    return "Ocurrio un error: " + response.StatusCode;
                }
            }
        }
        public static async Task<string> EliminarProveedor(dynamic u)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://proyectofinalasp-mvc2020.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync("Proveedores/" + u.idProveedor);
                if (response.IsSuccessStatusCode)
                {
                    return "Se modifico satisfactoriamente";
                }
                else
                {
                    return "Ocurrio un error: " + response.StatusCode;
                }
            }
        }
    }
}
