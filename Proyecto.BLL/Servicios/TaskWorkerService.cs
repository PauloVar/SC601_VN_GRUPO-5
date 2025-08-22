using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Proyecto.BLL.Interfaces;
using Proyecto.ML.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoPA_G5.Services
{
    public class TaskWorkerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskWorkerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[WORKER] Servicio iniciado...");
            Console.ResetColor();

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var tareaService = scope.ServiceProvider.GetRequiredService<ITareaService>();

               
                    var tarea = await tareaService.ObtenerSiguienteTareaPendienteAsync();

                    if (tarea != null)
                    {
                        
                        tarea.IdEstadoTarea = 2;
                        await tareaService.ActualizarEstadoAsync(tarea);

                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"[WORKER] Iniciando tarea: {tarea.Descripcion} (ID: {tarea.IdTarea})");
                            Console.ResetColor();

                         
                            await Task.Delay(5000, stoppingToken);

                         
                            tarea.IdEstadoTarea = 3;
                            await tareaService.ActualizarEstadoAsync(tarea);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"[WORKER] Tarea finalizada: {tarea.Descripcion} (ID: {tarea.IdTarea})");
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"[WORKER] Error en tarea {tarea.IdTarea}: {ex.Message}");
                            Console.ResetColor();

                            tarea.IdEstadoTarea = 4; 
                            await tareaService.ActualizarEstadoAsync(tarea);
                        }

                
                        await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                    }
                    else
                    {
               
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("[WORKER] No hay tareas pendientes, esperando...");
                        Console.ResetColor();

                        await Task.Delay(30000, stoppingToken);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[WORKER] Servicio detenido.");
            Console.ResetColor();
        }
    }
}
