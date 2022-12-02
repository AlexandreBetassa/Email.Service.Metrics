using Prometheus;

namespace Metricas
{
    public static class PrometheusConfig
    {
        public static Counter emailSuccess;
        public static Counter emailFail;
        public static Counter emailSend;

        public static IApplicationBuilder UsePrometheusConfiguration(this IApplicationBuilder app)
        {
            /*INICIO DA CONFIGURAÇÃO - PROMETHEUS*/

            // Custom Metrics to count requests for each endpoint and the method
            emailSuccess = Metrics.CreateCounter("emailSuccess", "Contador de emails enviados com sucesso");
            emailFail = Metrics.CreateCounter("emailFail", "Contador de falha de envio de emails");
            emailSend = Metrics.CreateCounter("emailSend", "Contador de envio total de emails");

            // Use the prometheus middleware
            app.UseMetricServer();
            app.UseHttpMetrics();

            /*FIM DA CONFIGURAÇÃO - PROMETHEUS*/

            return app;

        }
    }
}
