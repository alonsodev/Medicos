using System;
using System.Collections.Generic;
using System.Text;

namespace Medicos.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Medicos.ViewModels;
    using Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {

        /*private static async Task<bool> IsRemoteReachable(string host, int port = 80, int msTimeout = 5000)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException("host");

            if (!CrossConnectivity.Current.IsConnected)
                return false;

            host = host.Replace("http://www.", string.Empty).
              Replace("http://", string.Empty).
              Replace("https://www.", string.Empty).
              Replace("https://", string.Empty);

            return await Task.Run(async () =>
            {
                try
                {
                    InetSocketAddress sockaddr = await RunBlockingFuncOnOtherThread(() => new InetSocketAddress(host, port), msTimeout);
                    if (sockaddr == null)
                        return false;

                    using (var sock = new Socket())
                    {
                        await sock.ConnectAsync(sockaddr, msTimeout);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            });
        }

        public static async Task<T> RunBlockingFuncOnOtherThread<T>(Func<T> function, int msTimeout)
        {
            var tcs = new TaskCompletionSource<T>();
            new System.Threading.Thread(async () =>
            {
                T result = function.Invoke();
                if (!tcs.Task.IsCompleted)
                    tcs.TrySetResult(result);
            }).Start();

            Task.Run(async () =>
            {
                await Task.Delay(msTimeout);
                if (!tcs.Task.IsCompleted)
                    tcs.TrySetResult(default(T));
            });

            return await tcs.Task;
        }*/

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Por favor, active su configuración de internet.",
                };
            }

            /*   var isReachable = IsRemoteReachable("https://ast1.solutions-app.com");
               if (!isReachable)
               {
                   return new Response
                   {
                       IsSuccess = false,
                       Message = "Verificar la conexión a internet.",
                   };
               }*/

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }

        public async Task<TokenResponse> GetToken(
            string username,
            string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                var response = await client.PostAsync(GlobalSetting.Instance.TokenEndpoint,
                    new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<GeneralPostResponse> ReportarPaciente(
            Paciente pPaciente)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                string strPost = string.Format(
                    "nombre={0}&apellido={1}&telefono={2}&id_medicamento={3}&tipo_documento={4}&documento={5}&telefono_celular={6}" +
                    "&correo={7}&id_ciudad={8}&id_eps={9}&formula_img={10}&cuidador={11}&tipo_documento_cuidador={12}&documento_cuidador={13}" +
                    "&parentesco={14}&telefono_cuidador={15}&centro_atencion={16}&consentimiento_pdf={17}&usuario={18}&id_canal={19}&fecha_nacimiento={20}&id_patologia={21}",
                    pPaciente.Nombre, pPaciente.Apellido, pPaciente.Telefono, pPaciente.Id_medicamento, pPaciente.Tipo_documento,
                    pPaciente.Documento, pPaciente.Telefono_celular, pPaciente.Correo, pPaciente.Id_ciudad, pPaciente.Id_eps, pPaciente.Formula_img,
                    pPaciente.Cuidador, pPaciente.Tipo_documento_cuidador, pPaciente.Documento_cuidador, pPaciente.Parentesco, pPaciente.Telefono_cuidador,
                    pPaciente.Centro_atencion, pPaciente.Consentimiento_pdf, pPaciente.Usuario, pPaciente.Id_canal, pPaciente.Fecha_nacimiento, pPaciente.Id_patologia);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.PostAsync(GlobalSetting.Instance.ReportarPacienteEndpoint,
                    new StringContent(strPost,
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    /*var result = JsonConvert.DeserializeObject<GeneralPostResponse>(
                    resultJSON);
                    return result;*/
                    var oGeneralPostResponse = new GeneralPostResponse();
                    oGeneralPostResponse.Id = resultJSON;

                    return oGeneralPostResponse;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GeneralPostResponse> Request(
            Request pRequest)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.PostAsync(GlobalSetting.Instance.RequestEndpoint,
                    new StringContent(JsonConvert.SerializeObject(pRequest), Encoding.UTF8, "application/json"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    /*var result = JsonConvert.DeserializeObject<GeneralPostResponse>(
                    resultJSON);
                    return result;*/
                    var oGeneralPostResponse = new GeneralPostResponse();
                    oGeneralPostResponse.Id = resultJSON;

                    return oGeneralPostResponse;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PacienteView>> GetPaciente(
            String pUsuario,
            String pCriterio)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(string.Format(GlobalSetting.Instance.GetPacienteEndpoint +
                   "/{0}",
                    pCriterio));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<PacienteView>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<PacienteView>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Medicamento>> Medicamentos()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(string.Format(GlobalSetting.Instance.MedicamentosEndpoint));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<Medicamento>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<Medicamento>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<Patologia>> Patologias(string id_medicamento)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(string.Format(GlobalSetting.Instance.PatologiasEndpoint + "/" + id_medicamento));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<Patologia>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<Patologia>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<Eps>> Eps()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(string.Format(GlobalSetting.Instance.EpsEndpoint));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<Eps>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<Eps>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Ciudad>> Ciudades(String pDepartamentoId)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(string.Format(GlobalSetting.Instance.CiudadesEndpoint + "/{0}", pDepartamentoId));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<Ciudad>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<Ciudad>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Especialidad>> Especialidades()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(string.Format(GlobalSetting.Instance.EspecialidadesEndpoint));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<Especialidad>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<Especialidad>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Notificacion>> GetNotificaciones()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(GlobalSetting.Instance.NotificacionesEndpoint);
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<Notificacion>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<Notificacion>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PacienteView>> GetUltimosPacientes()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(GlobalSetting.Instance.GetUltimosPacientesEndpoint);
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<PacienteView>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<PacienteView>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PacienteMes>> GetCantidadesPacientes()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(string.Format(GlobalSetting.Instance.CantidadesEndpoint + "/{0}/{1}", DateTime.Now.Year, "null"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<PacienteMes>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<PacienteMes>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PacienteCantidad>> GetCantidadesPacientesConsolidado()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(string.Format(GlobalSetting.Instance.CantidadesEndpoint + "/{0}/{1}", "null", "null"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<PacienteCantidad>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<PacienteCantidad>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Beneficio>> Beneficio()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainViewModel.GetInstance().Token.AccessToken);
                var response = await client.GetAsync(GlobalSetting.Instance.BeneficiosEndpoint);
                var resultJSON = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonConvert.DeserializeObject<List<Beneficio>>(
                    resultJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<Beneficio>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
