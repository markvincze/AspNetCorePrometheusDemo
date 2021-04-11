# AspNetCorePrometheusDemo

Demo project accompanying an introductory presentation I did about Prometheus and ASP.NET Core.

## Run Prometheus

(The path to the local folder should be adjusted according to the local setup, any folder can be used.)

```
$ docker run --name prometheus -p 9090:9090 -v /c/Workspaces/Github/AspNetCorePrometheusDemo/prometheus/prometheus.yml:/etc/prometheus/prometheus.yml -v /c/Workspaces/Github/AspNetCorePrometheusDemo/data/prometheus:/data/prometheus prom/prometheus
```

## Run Grafana

(The path to the local folder should be adjusted according to the local setup, any folder can be used.)

```
$ docker run --name grafana -p 3000:3000 grafana/grafana -v /c/Workspaces/Github/AspNetCorePrometheusDemo/data/grafana:/var/lib/grafana
```

The data source http://host.docker.internal:9090 can be used to access Prometheus.

## Generate test traffic

```
$ hey -c 4 -z 100m http://localhost:5000/products
$ hey -c 2 -z 100m http://localhost:5000/products/2
$ hey -c 1 -z 100m -m POST http://localhost:5000/products/purchase/2
```