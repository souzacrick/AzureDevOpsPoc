using AzureDevOpsPOC.Helper;
using AzureDevOpsPOC.Model;
using AzureDevOpsPOC.Repository;
using System;

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

                    switch (Console.ReadLine().ToUpper())
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

                aPIHelper = new APIHelper(azureServiceConfiguration.URL, azureServiceConfiguration.AccessToken);
                WorkItemDTO workItemDTO = 
                    aPIHelper.Get<WorkItemDTO>($"{azureServiceConfiguration.Organization}/{azureServiceConfiguration.Project}/_apis/wit/workitems/1?fields=System.Id,System.Title,System.WorkItemType,System.CreatedDate&api-version=5.1").GetAwaiter().GetResult();

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
