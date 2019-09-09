using AzureDevOpsPOC.Helper;
using AzureDevOpsPOC.Model;
using AzureDevOpsPOC.Repository;
using System;
using System.Threading;

namespace AzureDevOpsPOC.ConsoleService
{
    class Program
    {
        private static AzureServiceConfiguration azureServiceConfiguration = null;
        private static AzureServiceConfigurationRepository azureServiceConfigurationRepository = null;

        static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("################################################");
                Console.WriteLine("######### Configurador do Azure DevOps #########");
                Console.WriteLine("################################################");
                Console.WriteLine();
                Console.WriteLine();

                azureServiceConfigurationRepository = new AzureServiceConfigurationRepository(new AzureDevOpsDbContextFactory().CreateDbContext());
                azureServiceConfiguration = azureServiceConfigurationRepository.Get();

                if (azureServiceConfiguration == null)
                {
                    azureServiceConfiguration = new AzureServiceConfiguration();

                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Parâmetros de conexão com o Azure não encontrados, favor criá-los.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Digite a URL da API do Azure DevOps:");
                        azureServiceConfiguration.URL = Console.ReadLine();
                        Console.WriteLine("Digite a Organização:");
                        azureServiceConfiguration.Organization = Console.ReadLine();
                        Console.WriteLine("Digite o Token de Acesso:");
                        azureServiceConfiguration.AccessToken = Console.ReadLine();
                        Console.WriteLine("Digite o Nome de projeto:");
                        azureServiceConfiguration.Project = Console.ReadLine();

                    } while (!azureServiceConfiguration.IsValid());

                    azureServiceConfigurationRepository.Add(azureServiceConfiguration);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Parâmetros de conexão com o Azure foram encontrados.");
                    Console.WriteLine("Para visualizar digite V. ");
                    Console.WriteLine("Para visualizar e atualizar digite A.");
                    Console.WriteLine("Para seguir em frente com a integração tecle Enter ou aguarde 2 segundos.");
                    Timer timer = new Timer(TimerCallback, null, 2000, -1);
                    Console.ForegroundColor = ConsoleColor.White;

                    switch (Console.ReadLine().ToUpper())
                    {
                        case "V":
                            timer.Change(-1, -1);
                            timer.Dispose();
                            Console.WriteLine($"URL da API do Azure DevOps: {azureServiceConfiguration.URL}");
                            Console.WriteLine($"Organização: {azureServiceConfiguration.Organization}");
                            Console.WriteLine($"Token de Acesso: {azureServiceConfiguration.AccessToken}");
                            Console.WriteLine($"Nome do projeto: {azureServiceConfiguration.Project}");
                            break;
                        case "A":
                            do
                            {
                                timer.Change(-1, -1);
                                timer.Dispose();
                                Console.WriteLine($"URL da API do Azure DevOps: {azureServiceConfiguration.URL}");
                                Console.WriteLine("Digite a nova URL da API do Azure DevOps:");
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
                            azureServiceConfigurationRepository.Update(azureServiceConfiguration);
                            break;
                        default:
                            timer.Change(-1, -1);
                            timer.Dispose();
                            break;
                    }
                }

                StartIntegration();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }

        private static void TimerCallback(Object o)
        {
            StartIntegration();
        }

        private static void StartIntegration()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("## Iniciando a Integração com o Azure DevOps ##");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;

            azureServiceConfiguration.LastWorkItemID = azureServiceConfiguration?.LastWorkItemID + 1 ?? 60000;
            APIHelper aPIHelper = new APIHelper(azureServiceConfiguration.URL, azureServiceConfiguration.AccessToken);
            GetWorkItem(aPIHelper, azureServiceConfiguration);
            GC.Collect();
            Environment.Exit(0);
        }

        private static void GetWorkItem(APIHelper aPIHelper, AzureServiceConfiguration azureServiceConfiguration)
        {
            Console.WriteLine($"Obtendo os dados do Work Item de ID {azureServiceConfiguration.LastWorkItemID}");
            WorkItemDTO workItemDTO = aPIHelper.Get<WorkItemDTO>($"{azureServiceConfiguration.Organization}/{azureServiceConfiguration.Project}/_apis/wit/workitems/{azureServiceConfiguration.LastWorkItemID}?fields=System.Id,System.Title,System.WorkItemType,System.CreatedDate&api-version=5.1").GetAwaiter().GetResult();

            if(workItemDTO != null)
            {
                Console.WriteLine($"Dados do Work Item de ID {azureServiceConfiguration.LastWorkItemID} foram encontrados e serão adicionados ao banco de dados");

                WorkItemRepository workItemRepository = new WorkItemRepository(new AzureDevOpsDbContextFactory().CreateDbContext());
                workItemRepository.Add(new WorkItem {
                    ID = workItemDTO.fields.SystemId,
                    Title = workItemDTO.fields.SystemTitle,
                    Type = workItemDTO.fields.SystemWorkItemType,
                    CreatedOn = workItemDTO.fields.SystemCreatedDate
                });

                azureServiceConfiguration.LastWorkItemID++;
                GetWorkItem(aPIHelper, azureServiceConfiguration);
            }
            else
            {
                Console.WriteLine($"Dados do Work Item de ID {azureServiceConfiguration.LastWorkItemID} não foram encontrados a rotina será encerrada");
                azureServiceConfiguration.LastWorkItemID--;
                azureServiceConfigurationRepository.Update(azureServiceConfiguration);
            }
        }

    }
}