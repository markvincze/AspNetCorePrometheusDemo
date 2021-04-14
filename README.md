# AspNetCorePrometheusDemo

Demo project accompanying an introductory presentation I did about Prometheus and ASP.NET Core.

## Run Prometheus

(The path to the configuration file has to be adjusted according to the local setup.)

```
$ docker run -p 9090:9090 -v /c/Workspaces/Github/AspNetCorePrometheusDemo/prometheus/prometheus.yml:/etc/prometheus/prometheus.yml prom/prometheus
```

## Run Grafana

```
$ docker run -i -t -p 3000:3000 grafana/grafana
```

The data source http://host.docker.internal:9090 can be used to access Prometheus.

## Generate test traffic

```
$ hey -c 4 -z 100m http://localhost:5000/products
$ hey -c 2 -z 100m http://localhost:5000/products/2
$ hey -c 1 -z 100m -m POST http://localhost:5000/products/purchase/2
```

## Example queries:

```
histogram_quantile(0.9, rate(http_request_duration_seconds_bucket{action="Get"}[30s]))
```
