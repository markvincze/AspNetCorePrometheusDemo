namespace PrometheusDemo.Web
{
    public interface IMetricsStore
    {
        void ObserveProductPurchased(string category);

        void ObserveResponseTimeDuration(string route, double durationSeconds);
    }
}