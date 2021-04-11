using Prometheus;

namespace PrometheusDemo.Web
{
    public class MetricsStore : IMetricsStore
    {
        private static readonly Counter productsPurchased =
            Metrics.CreateCounter("demoapp_productspurchased_total", "The total number of products purchased.", new string[] { "product_category" });

        private static readonly Summary httpResponseTime =
            Metrics.CreateSummary("demoapp_http_request_duration_seconds", "HTTP request duration.",
            new SummaryConfiguration
            {
                LabelNames = new string[] { "route" },
                Objectives = new[]
                {
                    new QuantileEpsilonPair(0.5, 0.05),
                    new QuantileEpsilonPair(0.9, 0.05),
                    new QuantileEpsilonPair(0.95, 0.01),
                    new QuantileEpsilonPair(0.99, 0.005),
                }
            });

        public void ObserveProductPurchased(string category)
        {
            productsPurchased.WithLabels(category).Inc();
        }

        public void ObserveResponseTimeDuration(string route, double durationSeconds)
        {
            httpResponseTime.WithLabels(route).Observe(durationSeconds);
        }
    }
}