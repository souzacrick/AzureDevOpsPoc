using AzureDevOpsPOC.Helper;
using AzureDevOpsPOC.Model;
using AzureDevOpsPOC.Repository;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AzureDevOpsPOC.ConsoleService
{
    class Program
    {
        static void Main(string[] args)
        {
            APIHelper aPIHelper = null;
            AzureServiceConfigurationRepository _repository = null;
            AzureServiceConfiguration azureServiceConfiguration = null;

            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("################################################");
                Console.WriteLine("######### Configurador do Azure DevOps #########");
                Console.WriteLine("################################################");
                Console.WriteLine();
                Console.WriteLine();

                _repository = new AzureServiceConfigurationRepository(new AzureDevOpsDbContextFactory().CreateDbContext());
                azureServiceConfiguration = _repository.Get();

                if (azureServiceConfiguration == null)
                {
                    azureServiceConfiguration = new AzureServiceConfiguration();

                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Parâmetros de conexão com o Azure não encontrados, favor criá-los.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Digite a URL do Azure DevOps:");
                        azureServiceConfiguration.URL = Console.ReadLine();
                        Console.WriteLine("Digite a Organização:");
                        azureServiceConfiguration.Organization = Console.ReadLine();
                        Console.WriteLine("Digite o Token de Acesso:");
                        azureServiceConfiguration.AccessToken = Console.ReadLine();
                        Console.WriteLine("Digite o Nome de projeto:");
                        azureServiceConfiguration.Project = Console.ReadLine();

                    } while (!azureServiceConfiguration.IsValid());

                    _repository.Add(azureServiceConfiguration);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Parâmetros de conexão com o Azure foram encontrados.");
                    Console.WriteLine("Para visualizar digite V. ");
                    Console.WriteLine("Para visualizar e atualizar digite A.");
                    Console.WriteLine("Para seguir em frente com a integração tecle Enter.");
                    Console.ForegroundColor = ConsoleColor.White;

                    switch (Console.ReadLine())
                    {
                        case "V":
                            Console.WriteLine($"URL do Azure DevOps: {azureServiceConfiguration.URL}");
                            Console.WriteLine($"Organização: {azureServiceConfiguration.Organization}");
                            Console.WriteLine($"Token de Acesso: {azureServiceConfiguration.AccessToken}");
                            Console.WriteLine($"Nome do projeto: {azureServiceConfiguration.Project}");
                            break;
                        case "A":
                            do
                            {
                                Console.WriteLine($"URL do Azure DevOps: {azureServiceConfiguration.URL}");
                                Console.WriteLine("Digite a nova URL do Azure DevOps:");
                                azureServiceConfiguration.URL = Console.ReadLine();
                                Console.WriteLine($"Organização: {azureServiceConfiguration.Organization}");
                                Console.WriteLine("Digite a nova Organização:");
                                azureServiceConfiguration.Organization = Console.ReadLine();
                                Console.WriteLine($"Token de Acesso: {azureServiceConfiguration.AccessToken}");
                                Console.WriteLine("Digite o novo Token de Acesso:");
                                azureServiceConfiguration.AccessToken = Console.ReadLine();
                                Console.WriteLine($"Nome do projeto: {azureServiceConfiguration.Project}");
                                Console.WriteLine("Digite o novo Nome de projeto:");
                                azureServiceConfiguration.Project = Console.ReadLine();
                            } while (!azureServiceConfiguration.IsValid());
                            _repository.Update(azureServiceConfiguration);
                            break;
                        default:
                            break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("## Iniciando a Integração com o Azure DevOps ##");
                //api

                aPIHelper = new APIHelper(azureServiceConfiguration.URL, azureServiceConfiguration.AccessToken);
                var a = aPIHelper.Get<dynamic>($"{azureServiceConfiguration.Organization}/{azureServiceConfiguration.Project}/_apis/wit/workitems/1?fields=System.Id,System.Title,System.WorkItemType,System.CreatedDate&api-version=5.1").GetAwaiter().GetResult();

                //https://dev.azure.com/fabrikam/_apis/wit/workitems?ids=297,299,300&fields=System.Id,System.Title,System.WorkItemType,Microsoft.VSTS.Scheduling.RemainingWork&api-version=5.1

                //using (HttpClient client = new HttpClient())
                //{
                //    client.DefaultRequestHeaders.Accept.Add(
                //        new MediaTypeWithQualityHeaderValue("application/json"));

                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($":{azureServiceConfiguration.AccessToken}")));
                //    using (HttpResponseMessage response = client.GetAsync($"{azureServiceConfiguration.URL}/{azureServiceConfiguration.Project}/_apis/wit/workitems/1?fields=System.Id,System.Title,System.WorkItemType,System.CreatedDate&api-version=5.1").GetAwaiter().GetResult())
                //    {
                //        response.EnsureSuccessStatusCode();
                //        string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                //        Console.WriteLine(responseBody);
                //    }
                //}

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
